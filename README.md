## Language About App
Spanish

## Clean Architecture
Hexagonal Architecture

Layers:
1) Api
2) Application
3) Domain
3) Infraestructure
4) Integration Test
5) Test

## Fron-End Modules

1) Athentication ( Login, Registration )
2) Dashboard Student
3) Dashboard Advisor

## Frameworks
1) .Net 8 (.Net Core) 
2) React (v 19.2.7) (Typescript) 

## Relational DataBase
SqlServer

## Azure Services

## Design patterns
The following are the design patterns implemented in the app

1) Repository: Repositories such as (EstudianteRepository, SolicitudApoyoRepository, UsuarioRepository) were created to encapsulate data access and separate persistence from business logic.
2) ServiceLayer: Interfaces such as IEstudianteService, ISolicitudApoyoService, and their implementations.They centralize business rules and coordinate repositories
3) DTO Data Transfer Object : Classes such as EstudianteRequestDto and EstudianteResponseDto. were created to reduce the coupling between domain entities and contracts exposed in the API
4) Dependency Injection: ASP.NET Core injects services (IStudentService, IUserRepository) into controllers. This makes unit testing easier and reduces coupling.

### Migrations
Migrations were created using Entity Framework (Code-First)

## Exceptions
Custom exception handling was implemented to control responses to requests, and DataAnnotations were used to validate properties in the request DTOs and data models.

## Authentication and Authorization
JWT Bearer Tokens (ASP.NET Core Identity) were used for user authentication and authorization, and two default roles were defined when the API was created: Asesor and Estudiante

# How do I log in for the first time? :

1) Once you open the app, click the “Register” button and enter your basic information, including your username and password. These are the two fields you'll need to remember for logging in.
2) Click “Register.” 
3) You can now log in.

## Test Integration
The tests were run using xUnit + Moq; handlers were created for the validations, and coverage was slightly over 70% for the entire application. This was verified using `dotnet test --collect:“XPlat Code Coverage”`

## Libraries

1) Microsoft.AspNetCore.Authentication.JwtBearer
2) Microsoft.AspNetCore.Identity.EntityFrameworkCore
3) Microsoft.AspNetCore.SpaProxy
4) Microsoft.Data.SqlClient
5) Microsoft.EntityFrameworkCore.Tools
6) Microsoft.AspNetCore.Http.Abstractions
7) Microsoft.AspNetCore.Identity.EntityFrameworkCore
8) Microsoft.EntityFrameworkCore
9) Microsoft.EntityFrameworkCore.SqlServer
10) Microsoft.Extensions.Configuration
11) Microsoft.Extensions.Configuration.FileExtensions
12) Microsoft.Extensions.Configuration.Json
13) AutoMapper
14) MediatR
15) System.IdentityModel.Tokens.Jwt
16) coverlet.collector
17) Microsoft.AspNetCore.Http
18) Microsoft.NET.Test.Sdk
19) xunit
20) xunit.runner.visualstudio
21) Moq
22) FluentValidation
23) Microsoft.AspNetCore.Identity.EntityFrameworkCore

## Relational Model
<img width="835" height="511" alt="image" src="https://github.com/user-attachments/assets/81334525-d3a4-4ebb-be4d-4b7c45db454c" />

## Requirements

a)    Authentication and JWT retrieval
b)    New user registration
c)    Paginated list of students
d)    Create student
e)    List requests (filters: status, type)
f)	  Create a support request
g)    Request details with status history
h)    Change request status
i)    A student’s requests (personal portal)

## Swagger implemented
You can use the endpoints to retrieve the request, and the Swagger configuration has been set up so that the user can be authenticated.

## Steps for Run project
1) Open Visual Studio Code or your development enviroment and clone this repository
2) Importa o crea la base de datos utilizando las migraciones correspondientes
3) Configure the database connection string
4) Now, you can run to project on Properties, IMPORTANT: I had trouble running the project with Chrome, I recommend running it on another one

## Azure Services
I would use the following Azure services

# Azure App Service
It is a platform-as-a-service (PaaS) solution that would allow us to host the app easily and securely. It features load balancing, and Azure handles its security updates. We could configure horizontal scaling to improve performance and optimize resources. It offers high availability and is compatible with .NET.

# Azure SQL Database
It uses a database to store information about students, applications, and users; it offers high availability and features robust security integrated with Azure AD.

# Azure Blob Storage 
It allows you to store documents containing student reports; it is a scalable service that enables easy integration with the API for uploading and downloading files.

# Azure Key Vault
I would use this module because it would allow me to store database connection strings and JWT configuration keys; I could even consider an enhancement to store passHashes.

