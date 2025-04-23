# JobPortal API

A simple Job Portal API built using ASP.NET Core with Clean Architecture principles.

  ## Features
    - Clean Architecture (Application, Domain, Infrastructure, API layers)
    - API Endpoints:
        POST /api/companies – Create a company.
        POST /api/companies/{id}/jobs – Create a job post for a company.
        GET /api/jobs – List all jobs with optional search by keyword, company, or date range.
        POST /api/candidates – Register a candidate.
        POST /api/applications – Submit an application to a job.
        GET /api/jobs/{id}/applications – List all applications for a jobs
    - Data validation using Data Annotations
    - Exception handling middleware
    - In-Memory database (EF Core)
    - Unit Testing with xUnit & Moq
    - Swagger UI for API testing

  ## Project Structure
  JobPortal.Api --> Entry point (controllers, middlewares, Swagger) JobPortal.Application --> Business logic, DTOs, Interfaces, Services JobPortal.Domain --> Core domain models & entities JobPortal.Infrastructure --> EF Core, Repository implementations JobPortal.Application.Tests --> Unit Tests for Services

  ## Getting Started

    **1. Clone the Repository**
      git clone https://github.com/<your-username>/JobPortal.git
      cd JobPortal
    **2. Run the Application**
      Open the solution in Visual Studio 2022
      Set JobPortal.Api as the startup project
      Press F5 or run without debugging
    **3. API Documentation**
      Navigate to: https://localhost:<port>/swagger/index.html
      Use Swagger UI to test the endpoint
  
  ## Running Unit Tests
    dotnet test JobPortal.Tests

  ## Sample Seed Data (In-Memory)
    Seed data is configured in JobPortalDbContextSeed.cs and loaded on startup
    **Path:   \JobPortal\JobPortal.Infrastructure\Seed\JobPortalDbContextSeed.cs**
    2 Companies
    
    2 Job Posts
    
    2 Candidates
    
    2 Job Applications

  ##  Technologies
    ASP.NET Core 8
    Entity Framework Core (In-Memory)
    AutoMapper
    xUnit & Moq
    Swagger / Swashbuckle

  ## Exception Handling
    Centralized error handling using custom middleware. Includes handling for:
    NotFoundException
    BadRequestException
    ServiceException
    Fallback for InternalServerError
