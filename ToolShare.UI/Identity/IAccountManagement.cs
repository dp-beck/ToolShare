using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.UI.Identity.Models;

namespace ToolShare.UI.Identity
{
    /// <summary>
    /// Account management services.
    /// </summary>
    public interface IAccountManagement
    {
        /// <summary>
        /// Login service.
        /// </summary>
        /// <param name="username">User's username.</param>
        /// <param name="password">User's password.</param>
        /// <returns>The result of the request serialized to <see cref="FormResult"/>.</returns>
        public Task<FormResult> LoginAsync(string username, string password);

        /// <summary>
        /// Log out the logged in user.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        public Task LogoutAsync();

        /// <summary>
        /// Registration service.
        /// </summary>
        /// <param name="registrationInfo">Information Needed to Register</param>
        /// <returns>The result of the request serialized to <see cref="FormResult"/>.</returns>
        public Task<FormResult> RegisterAsync(RegistrationInfo registrationInfo);

        public Task<bool> CheckAuthenticatedAsync();   
    }
}