# MeFit

MeFit is an application built for managing weekly workout goals.
Users of the application can get an exercise scheme based on programs which 
include exercises and sets. These schemes are catered to the users goals.

This repository contains the backend of the application, 
which is an ASP.NET Web API created in C# .NET 5 framework using Keycloak for user authentication.
For database creating we are using Enitity Framework Core for database creating and for API documentation we are using the extension Swashbuckle which allows us to use Swagger (a collection of html, css and js used to autogenerate a documentation UI).

TODO: Write more about what the database contains (tables) and the relationships between them.

# Deployed using Azure with continous deployment

[MeFit Azure API](https://mefit.azurewebsites.net/)

# Install

 - Clone to a local directory
```bash
    git clone https://github.com/StianOkland/MovieCharacterAPI.git
```
 - Open solution in Visual Studio or another IDE
 - Update to your SQL Server connection info in appsettings.json

# Usage

In the package manager console in Visual Studio:
```bash
update-database
```

Run the project in Visual Studio, and test the APIs in Swagger/Postman.

# Maintainers

Stian Økland [@StianOkland](https://github.com/StianOkland)<br />
Isak Hauknes [@larrycaw](https://github.com/larrycaw)<br />
Andrea Hårseth Nakstad [@andreahn](https://github.com/andreahn)
