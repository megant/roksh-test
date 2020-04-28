# Hello, Roksh!

Welcome to my demo work

To make demo work, please follow these steps below:

1.  Try to log in (you cannot).
2.  On login page click on "apply migration" button. This step will create database structure automatically.
3.  Go to "Register" and create a new user.
4.  Click on confirmation link to activate newly created user.
5.  Try to log in with the given credentials.
6.  Go to "Get items" page and click on "Get items" (products) button which will trigger item crawling action from an external website.
7.  Go to "Check packages" page and click on "Generate new package" button as many as you want.
8.  You can see your packages. You can filter your packages and see content of a certain package by clicking on its package number.

Please note that this demo was written on a Mac using Jetbrains Rider IDE, therefore it uses Sqlite by default. If you want to make the demo work with Sql Server, please follow the steps below (not tested before):

*   Install SQL Server nuget package: "PM > Install-Package Microsoft.EntityFrameworkCore.SqlServer"
*   Update ConnectionStrings in appsettings.json (line 3)
*   Update Startup.cs AddDbContext directive (line 28) and change UseSqlite to UseSqlServer

Known issues:

*   Website parser package cannot select some photos from HTML

The `ClientApp` subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any `ng` command (e.g., `ng test`), or use `npm` to install extra packages into it.

Have fun and drop me some message if you liked it!