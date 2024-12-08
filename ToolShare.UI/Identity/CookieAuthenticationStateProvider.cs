using System.ComponentModel;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using ToolShare.UI.Identity.Dtos;

namespace ToolShare.UI.Identity
{
    /// <summary>
    /// Handles state for cookie-based auth.
    /// </summary>
    /// <remarks>
    /// Create a new instance of the auth provider.
    /// </remarks>
    /// <param name="httpClientFactory">Factory to retrieve auth client.</param>
    public class CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory) : AuthenticationStateProvider, IAccountManagement
    {
        /// <summary>
        /// Map the JavaScript-formatted properties to C#-formatted classes.
        /// </summary>
        private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        /// <summary>
        /// Special auth client.
        /// </summary>
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("Auth");

        /// <summary>
        /// Authentication state.
        /// </summary>
        private bool authenticated = false;

        /// <summary>
        /// Default principal for anonymous (not authenticated) users.
        /// </summary>
        private readonly ClaimsPrincipal unauthenticated = new(new ClaimsIdentity());

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registrationInfoDto">Dto with registration information.</param>
        /// <returns>The result serialized to a <see cref="ServiceResult"/>.
        /// </returns>
        public async Task<ServiceResult> RegisterAsync(RegistrationInfoDto registrationInfoDto)
        {
            string[] defaultDetail = [ "An unknown error prevented registration from succeeding." ];

            try
            {
                // make the request
                var result = await httpClient.PostAsJsonAsync(
                    "api/users/register", registrationInfoDto);

                // successful?
                if (result.IsSuccessStatusCode)
                {
                    return new ServiceResult { Succeeded = true };
                }

                // body should contain details about why it failed
                var details = await result.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);
                var errors = new List<string>();
                var errorList = problemDetails.RootElement.GetProperty("errors");
                var errorValues = errorList.GetProperty("$values");

                foreach (var errorValue in errorValues.EnumerateArray())
                {
                        errors.Add(errorValue.GetProperty("description").GetString() ?? string.Empty);
                }

                // return the error list
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = problemDetails == null ? defaultDetail : [.. errors]
                };
            }
            catch { }

            // unknown error
            return new ServiceResult
            {
                Succeeded = false,
                ErrorList = defaultDetail
            };
        }

        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="username">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The result of the login request serialized to a <see cref="ServiceResult"/>.</returns>
        public async Task<ServiceResult> LoginAsync(string username, string password)
        {
            try
            {
                // login with cookies
                var result = await httpClient.PostAsJsonAsync(
                    "api/users/login?useCookies=true", new
                    {
                        username,
                        password
                    });

                // success?
                if (result.IsSuccessStatusCode)
                {
                    // need to refresh auth state
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                    // success!
                    return new ServiceResult { Succeeded = true };
                }
            }
            catch { }

            // unknown error
            return new ServiceResult
            {
                Succeeded = false,
                ErrorList = [ "Invalid username and/or password." ]
            };
        }

        /// <summary>
        /// Get authentication state.
        /// </summary>
        /// <remarks>
        /// Called by Blazor anytime an authentication-based decision needs to be made, then cached
        /// until the changed state notification is raised.
        /// </remarks>
        /// <returns>The authentication state asynchronous request.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            authenticated = false;

            // default to not authenticated
            var user = unauthenticated;

            try
            {
                // the user info endpoint is secured, so if the user isn't logged in this will fail
                var userResponse = await httpClient.GetAsync("api/users/current-user");

                // throw if user info wasn't retrieved
                userResponse.EnsureSuccessStatusCode();

                // user is authenticated,so let's build their authenticated identity
                var userJson = await userResponse.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfoDto>(userJson, jsonSerializerOptions);

                if (userInfo != null)
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userInfo.userName),
                        new(ClaimTypes.Email, userInfo.Email),
                    };

                    // add any additional claims
                    claims.AddRange(
                        userInfo.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email)
                            .Select(c => new Claim(c.Key, c.Value)));

                    // request the roles endpoint for the user's roles
                    var rolesResponse = await httpClient.GetAsync("api/users/current-user/roles");

                    // throw if request fails
                    rolesResponse.EnsureSuccessStatusCode();

                    // read the response into a string
                    var rolesJson = await rolesResponse.Content.ReadAsStringAsync();

                    // deserialize the roles string into an array
                    var roles = JsonSerializer.Deserialize<RoleClaim[]>(rolesJson, jsonSerializerOptions);

                    // add any roles to the claims collection
                    if (roles?.Length > 0)
                    {
                        foreach (var role in roles)
                        {
                            if (!string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value))
                            {
                                claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
                            }
                        }
                    }

                    // set the principal
                    var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
                    user = new ClaimsPrincipal(id);
                    authenticated = true;
                }
            }
            catch { }

            // return the state
            return new AuthenticationState(user);
        }

        

        public async Task LogoutAsync()
        {
            const string Empty = "{}";
            var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("api/users/logout", emptyContent);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<bool> CheckAuthenticatedAsync()
        {
            await GetAuthenticationStateAsync();
            return authenticated;
        }

        public class RoleClaim
        {
            public string? Issuer { get; set; }
            public string? OriginalIssuer { get; set; }
            public string? Type { get; set; }
            public string? Value { get; set; }
            public string? ValueType { get; set; }
        }

    }
}