# Book Web App Project
[trReadmeHere](https://github.com/fatihemregit/BookWebApp/blob/master/README_TR.md)
<br>
A project where I implemented CRUD operations on a Book object using simple EF Core to improve my skills.  
<br>  
<br>  
I set up the authentication mechanism for the project using the Identity library.

## Project Purpose

To apply the topics I’ve learned so far in this [course](https://www.btkakademi.gov.tr/portal/course/aspnet-core-web-api-23993) (Logging, Layered Architecture, AutoMapper).  
<br>  
<br>  
And to implement an authentication mechanism with Identity.

## Tasks Completed in This Commit

- Readme Update

## Project Log

### Day 1 (14.11.2024)

- Removed the default `ErrorViewModel`.
- Created the `Book` object for the database (`Dto/BookDto`).
- Installed the necessary packages for the database (`Entity Framework Core`, `Entity Framework Core Design`, `Entity Framework Core SQL Server`).
- Added data annotations for the decimal property (`Price`) in the `BookDto` object.
- Created the `ApplicationDbContext` class for the database (`Data/Context/ApplicationDbContext`).
- Added a `BookDtoConfig` class to seed data into the database tables when migrating (`Data/Config/BookDtoConfig`).
- Linked the `BookDtoConfig` class to the `ApplicationDbContext` class (`OnModelCreating` method).
- Added the connection string to the `appsettings.json` file.
- Created the `ServiceExtensions` class with the `ConfigureSqlContext` method. This method sets up the database connection for `ApplicationDbContext` based on the connection string in `appsettings.json`.
- Called the `ConfigureSqlContext` method in `Program.cs`.
- Ran the `add-migration` command using the Package Manager Console to create the migration.
- Applied the migration to the database with the `Update-Database` command.
- Created `README.md` and `README_TR.md` files for GitHub.
- Wrote the content for the `README` files.
- Moved the `README` files to the root directory.
- Created a `.gitignore` file in the root directory and added the necessary content.
- Set up Git VCS connections.
- Added a link to `README_TR.md` in `README.md`.
- Installed the AutoMapper package and set it up (`MappingProfile.cs`, `Services.AddAutoMapper(typeof(Program))`).
- Created and wrote the `BookViewModel` class.
- Added mapping from `BookDto` to `BookViewModel` in `MappingProfile.cs`.
- Wrote the `Index` method for the Book Controller.
- Wrote the GET part of the `Edit` method for the Book Controller.
- Created and wrote the `BookViewModelForUpdate` class.
- Added mapping from `BookDto` to `BookViewModelForUpdate` in `MappingProfile.cs`.
- Tried to fix the decimal issue but couldn’t resolve it.

### Day 2 (15.11.2024)

- Fixed the decimal issue, though validation in the `ViewModel` still doesn’t work. For now, it’s acceptable (resolved by adding jQuery code to `_ValidationScriptsPartial.cshtml`).
- Temporarily created `denemedto.cs`, `denemedtoConfig.cs`, and a controller to test if the issue also occurred with `double`. Deleted all related files after resolving the problem.
- Deleted the migration containing `denemedto`.
- Deleted the database using SQL Server Management Studio.
- Created a new migration from scratch and applied it to the database.
- Wrote the POST part of the `Edit` method for the Book Controller.
- Created and wrote the `BookViewModelForCreate` class.
- Added mapping from `BookDto` to `BookViewModelForCreate` in `MappingProfile.cs`.
- Wrote the GET and POST parts of the `Create` method for the Book Controller.
- Created and wrote the `BookViewModelForDetails` class.
- Added mapping from `BookDto` to `BookViewModelForDetails` in `MappingProfile.cs`.
- Wrote the GET part of the `Details` method for the Book Controller.
- Started setting up the Identity mechanism.

### Day 3 (16.11.2024)

- Set up the Identity mechanism in the project.
- Installed `Microsoft.AspNetCore.Identity.EntityFrameworkCore`.
- Added Identity tables to `ApplicationDbContext`.
- Created the `ServiceExtensions` class and added a method to set up Identity.
- Ran a migration and applied it to the database.
- Added a `Register` view and controller.
- Created and configured the `RegisterViewModel`.
- Set up password rules in `Program.cs`.
- Wrote the GET and POST methods for the `Register` action.
- Tested the registration functionality and fixed any issues that arose.

### Day 4 (17.11.2024)

- Added login functionality to the project.
- Created the `LoginViewModel` class.
- Added a `Login` view and controller.
- Wrote the GET and POST methods for the `Login` action.
- Enabled cookie-based authentication.
- Tested login functionality and fixed any bugs.
- Added logout functionality and tested it.

### Day 5 (18.11.2024)

- Improved the overall design of the app using Bootstrap.
- Added a navigation bar with links for login, register, and logout.
- Configured authorization rules for specific pages.
- Tested the app with different user roles.
- Wrote documentation for the authentication system.
### Day 6 (30.11.2024)
- If a user who is logged in navigates to the login or sign-in page again, they are redirected to the authorization error page.

### Day 7 (5.12.2024)
- The necessary layers for transitioning to a layered architecture (Data, Entity) were created.
- The Data layer was written, but implementation into the main project is yet to be done.
- The Entity layer was started, and entities (models) are being created as needed for the project.

### Day 8 (7.12.2024)
- The AutoMapper library was added to the Data layer.
- In the Data layer, through AutoMapper, each method in the IBookRepository interface was mapped to a separate entity.
- The entities used in the IBookRepository interface were created in the Entity layer.
- The Business layer was written, but implementation into the main project is yet to be done.
- AutoMapper was added to the Business layer.
- In the Business layer, through AutoMapper, each method in the IBookService interface was mapped to a separate entity.
- Implementations for the main project started.
- In the main project, the program.cs file was updated with implementations for the other layers (builder.services).
- The data folder was deleted in the main project and replaced with the data layer.
- The BookController was rewritten according to the new architecture.

### Day 9 (9.12.2024)
- AuthUserRepository and IAuthUserRepository classes were written.
- AuthUserService and IAuthUserService were written.
- The UserController was updated to work with the new architecture (IAuthUserService was implemented into the main project).
- The role system was temporarily disabled.

### Day 10 (10.12.2024)
- IAuthRoleRepository and AuthRoleRepository classes were written.
- IAuthRoleService and AuthRoleService classes were written.
- It was noticed that the AddToRoleAsync method was missing in AuthUserService. Changes were made accordingly.
- It was noticed that the RemoveFromRoleAsync method was missing in AuthUserService. Changes were made accordingly.
- The Role Controller was updated to the new architecture (SetRoleForUser post method and delete role excluded).

### Day 11 (11.12.2024)
- It was noticed that the GetUsersInRoleAsync method was missing in AuthUserService. Changes were made accordingly.
- The Role Controller was updated to the new architecture.
- The Migration folder was moved from the main project to the Data project (Data/EfCore/Migrations).
- The Automapper, Auth, and Extensions folders were moved to the Utils subfolder in the main project.

### Day 12 (12.12.2024)
- A login issue was fixed (securitystamp and passwordhash properties were missing in service and repository classes, causing the issue. Necessary corrections were made).
- A bug in the role assignment system was identified but not solved.

### Day 13 (13.12.2024)
- The bug in the role assignment system was fixed (AuthUserRepository/AddToRoleAsync method).
  (The issue was that we were trying to map the parameter object to AppUser. This was not allowed, and as per my findings, EF Core doesn’t support this. Instead, the object’s ID is used to fetch the AppUser using the FindByIdAsync method).
- A small change was made in the logging system (moved Microsoft.EntityFrameworkCore.Database.Command logs to warnings in appsettings.json. This was done to make error tracking easier, as too many log messages on the console can make debugging difficult).
- All business logic in the UserController was moved to the UserService in the Business layer.

### Day 14 (14.12.2024)
- The GetUserAsync method in the UserController's Index method was modified.

### Day 15 (15.12.2024)
- The business logic of the CreateRolePost, DeleteRoleGet, and DeleteRolePost methods in the RoleController was moved to the RoleService in the Business layer.

### Day 16 (16.12.2024)
- The business logic for the SetRoleForUserGet and SetRoleForUserPost methods in the RoleController was moved to the RoleService in the Business layer.
- Minor code fixes were made in the DeleteRolePost method in the RoleController.
- Minor code fixes were made in the SetRoleForUserGet method in the AuthRoleService class.
- Minor code fixes were made in the DeleteUser method in the UserController.
- The Entity/Exceptions folder was reorganized (IAuthRoleService and IAuthUserService).
- After reorganizing the Entity/Exceptions folder, errors arising in the AuthUserService and AuthRoleService classes in the Business layer were resolved (fixed "using" errors).
- Null checks were added to the parameters in the methods of the AuthRoleService and AuthUserService classes.
- Custom routes were implemented for various actions (/Login, /SignIn, /DeleteUser, /CreateRole, /DeleteRole, /SetRoleForUser).
- A custom exception system was implemented in the BookService class (Entity/Exceptions/IBookService).
- The Index method in the BookController was rewritten according to the custom exception (IBookServiceGetAllBookSucceeded).

### Day 17 (17.12.2024)
- The Create, Edit, Details, and Delete methods in the BookController were rewritten according to the custom exception (Entity/Exceptions/IBookService).
- The logic to display buttons based on user permissions in the Index pages (/User/Index and /Book/Index) was moved to the UserService in the Business layer.
- Unnecessary functions in the AuthRoleService (GetAllRolesAsync, FindByIdAsync, DeleteAsync) and AuthUserService (FindByEmailAsync, FindByNameAsync, IsInRoleAsync, GetRolesAsync, CreateAsync, DeleteAsync, AddToRoleAsync, RemoveFromRoleAsync, GetUsersInRoleAsync) were removed.
- Unnecessary code in the Automapper classes (MappingProfile.cs, MappingProfileForBusinessLayer.cs) was removed.
- Unnecessary classes in the Entity layer (IAuthRoleService (IAuthRoleServiceDeleteAsync, IAuthRoleServiceFindByIdAsync), IAuthUserService (IAuthUserServiceAddToRoleAsync, IAuthUserServiceCreateAsync, IAuthUserServiceDeleteAsync, IAuthUserServiceFindByEmailAsync, IAuthUserServiceFindByNameAsync, IAuthUserServiceGetRolesAsync, IAuthUserServiceGetUsersInRoleAsync, IAuthUserServiceIsInRoleAsync, IAuthUserServiceRemoveFromRoleAsync)) were removed.

### Day 18 (18.12.2024)
- The price validation problem in the Book Create and Edit pages was resolved.


