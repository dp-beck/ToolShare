# Tool Share

## Description
Tools are expensive. Sometimes we need a tool for a job and won't need it 
again for a long time. Who wants to buy a tool to use it once or twice!?
Most of us are happy to share those tools we own, but rarely use. However,
it's hard to know who has what tool and is willing to share it. Let's face it...
Who wants to pick up a phone and call a bunch of people looking for a scroll saw
to borrow?

Many of us are of course already sharing our tools, but may struggle to 
track which ones we've shared with others and where they are. When we finally 
need them for a job or project, we can't find them and don't remember who has them.

Tool Share is designed to solve these problems. It is a social app designed to facilitate the sharing of 
tools among groups of individuals (called pods). You can sign up, join a pod
(or start your own!) and begin sharing. You can list all the tools you
own that you would like to make available for sharing within your pod, and
you can see all the tools your fellow pod members have made available.

Through the app you can request a tool, as well as accept (or reject!) others'
requests to borrow your tools. The app displays due dates so that you can track
how long someone has had your tool. 

## Instructions: running the App locally

** IN PROGRESS **

## Features
This app was created as the capstone project for the Code Kentucky Software
Development Course (2024). I have integrate the following features in the
app from the Features List:

1. **Implement a regular expression (regex) to validate or ensure a field is always stored and 
displayed in the correct format:**
   1. I use MudForms from Mudblazor to accept and validate user input to create a new user. 
   The validation function for the password uses Regexes to enforce password strength rules. 
   While most of these were provided by MudBlazor, I wrote a custom regex to enforce rule that 
   password must have at least one non-alphanumeric character.
   2. *Location in Code* - ToolShare.UI / Pages / Identity / Register.razor - Line 113

2. **Create a dictionary or list, populate it with several values, retrieve at least one value, 
and use it in your program**
   1. One Example: Both the AppUser and Pod Models use ICollections to store lists of objects associated
   with that user or pod (e.g., tools owned, tools borrowed, pod members). Those lists are accessed in the 
   UI projects and the contents are displayed in tables or lists for the user.
   2. *Location in Code* - ToolShare.Data / Models / Pod.cs & AppUser.cs; Usage: ToolShare.UI / Pages / Home / HomePodUser.cs & HomeNoPodUser.cs

3. **Make a generic class and use it**
   1. I created a Generic Repository class to handle some of the more generic operations on
   the database for the different tables (e.g., GetAll, FindById, Delete)
   2. *Location in Code* - ToolShare.Data / Repositories / GenericRepository.cs; Usage: ToolShare.API / Controllers

4. **Make your application an API.**
   1. I separated my Frontend / Client from my API in my solution, so interaction with the database is mediated by
   an API, thus enabling the API to interact with additional frontends. The client interacts with the
   database through a number of defined endpoints.
   2. *Location in Code* - ToolShare.API

5. **Make your application a CRUD API**
   1. The API for this project supports creating, reading, updating, and deleting databse entries.
   2. *Location in Code* - ToolShare.API

6. **Make your application asynchronous**
   1. Numerous methods in the solution as defined asynchronously, primarily any that mediate
   interaction with the database.
