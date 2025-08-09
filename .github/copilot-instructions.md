# RswVocaMasterApi - GitHub Copilot Instructions

## Project Overview
This is an Azure Functions v4 API project built with .NET 8 and C# 12, following clean architecture principles and Microsoft's best practices for cloud-native API development.

## Core Programming Guidelines

### 1. Microsoft Ecosystem First
- **Primary Framework**: Use Microsoft libraries and packages exclusively when possible
- **Logging**: Use `Microsoft.Extensions.Logging.ILogger<T>` for all logging operations
- **Dependency Injection**: Leverage `Microsoft.Extensions.DependencyInjection` container
- **HTTP Operations**: Use `Microsoft.AspNetCore.Http` and `Microsoft.AspNetCore.Mvc` for HTTP handling
- **Azure Integration**: Prefer Azure-specific packages (`Microsoft.Azure.*`) for cloud services
- **Monitoring**: Use Application Insights (`Microsoft.ApplicationInsights.*`) for telemetry
- **Data Access**: Use Entity Framework Core (`Microsoft.EntityFrameworkCore`) for database operations and ORM functionality
- **API Documentation**: Use Swagger/OpenAPI (`Swashbuckle.AspNetCore`) for API documentation and testing interface

### 2. Layered Architecture Structure
Organize the solution into distinct layers with clear separation of concerns:

#### API Layer (`RswVocaMasterApi`)
- **Purpose**: HTTP endpoints, request/response handling, authentication
- **Dependencies**: Can reference Domain and Infrastructure layers
- **Responsibilities**: 
  - Azure Function endpoints
  - Request validation and mapping
  - Response formatting
  - Authentication/authorization
  - API documentation

#### Domain Layer (`RswVocaMaster.Domain`)
- **Purpose**: Business logic, domain models, interfaces
- **Dependencies**: No dependencies on other layers (pure business logic)
- **Responsibilities**:
  - Domain entities and value objects
  - Business rules and validation
  - Domain services
  - Repository interfaces
  - Use case/service interfaces

#### Infrastructure Layer (`RswVocaMaster.Infrastructure`)
- **Purpose**: External concerns (databases, file systems, external APIs)
- **Dependencies**: Can reference Domain layer
- **Responsibilities**:
  - Repository implementations
  - Database context and configurations
  - External service integrations
  - Caching implementations
  - File storage operations

### 3. Code Quality Standards

#### Naming Conventions
- **Classes**: PascalCase with descriptive names (`VocabularyService`, `WordRepository`)
- **Methods**: PascalCase with verb-noun pattern (`GetWordById`, `CreateVocabulary`)
- **Variables**: camelCase (`wordId`, `vocabularyList`)
- **Constants**: PascalCase (`MaxWordLength`, `DefaultPageSize`)
- **Private fields**: camelCase with underscore prefix (`_logger`, `_repository`)

#### Method and Class Structure
- **Single Responsibility**: Each class/method should have one clear purpose
- **Dependency Injection**: Constructor injection for all dependencies
- **Async/Await**: Use async methods for I/O operations with proper ConfigureAwait(false)
- **Nullable Reference Types**: Enable and properly use nullable reference types
- **Record Types**: Use records for DTOs and immutable data structures

#### Error Handling
- **Exceptions**: Use specific exception types, not generic Exception
- **HTTP Status Codes**: Return appropriate HTTP status codes (200, 201, 400, 404, 500)
- **Logging**: Log errors with appropriate log levels and structured data
- **Validation**: Validate inputs at API boundary and business logic layers

### 4. Azure Functions Best Practices

#### Function Structure
- **Program.cs**: Configure services, logging, and dependency injection using `FunctionsApplication.CreateBuilder()`
- **Function App**: Use `builder.Services` for app-level service registrations
- **Bindings**: Define function triggers and bindings in function method signature using `[Function]` attribute

```csharp
public class MyFunction
{
    private readonly IMyService _myService;
    private readonly ILogger<MyFunction> _logger;

    public MyFunction(IMyService myService, ILogger<MyFunction> logger)
    {
        _myService = myService;
        _logger = logger;
    }

    [Function("GetItem")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "items/{id}")] HttpRequest req,
        string id)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        // ...
    }
}
```

#### Dependency Injection Pattern
- Register all services and repositories in `Program.cs` using `builder.Services`
- Use constructor injection to receive dependencies in function classes
- Avoid using service locator pattern

```csharp
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();
```

#### Authorization Levels
- Use `AuthorizationLevel.Function` for production endpoints
- Use `AuthorizationLevel.Anonymous` only for health checks or public endpoints
- Implement custom authentication/authorization when needed

### 5. Data Transfer Objects (DTOs)
- **Request/Response Models**: Create specific DTOs for API contracts
- **Validation**: Use Data Annotations for input validation
- **Mapping**: Use explicit mapping between DTOs and domain models
- **Immutability**: Prefer immutable DTOs using records

Example:

```csharp
public record CreateVocabularyDto
{
    [Required]
    public string Name { get; init; }

    [MaxLength(100)]
    public string Description { get; init; }
}
```

### 6. Testing Guidelines
- **Unit Tests**: Test business logic in Domain layer
- **Integration Tests**: Test API endpoints with in-memory dependencies
- **Test Naming**: Use descriptive test method names following Given_When_Then pattern
- **Test Framework**: Use xUnit with Microsoft.Extensions.Testing for dependency injection

### 7. Configuration and Environment
- **Configuration**: Use `IConfiguration` for app settings
- **Environment Variables**: Store sensitive data in Azure Key Vault or environment variables
- **Connection Strings**: Use configuration providers, avoid hardcoding
- **Feature Flags**: Use Azure App Configuration for feature toggles

### 8. Performance Considerations
- **Caching**: Implement caching for frequently accessed data using `IMemoryCache`
- **Connection Pooling**: Use connection pooling for database connections
- **Async Operations**: Use async/await for all I/O operations
- **Resource Disposal**: Properly dispose of resources using `using` statements

### 9. Security Best Practices
- **Input Validation**: Validate all inputs at API boundary
- **SQL Injection**: Use parameterized queries or Entity Framework
- **CORS**: Configure CORS appropriately for your client applications
- **Authentication**: Implement Azure AD or JWT token authentication
- **Secrets**: Never commit secrets to source control

### 10. Monitoring and Observability
- **Structured Logging**: Use structured logging with meaningful context
- **Telemetry**: Leverage Application Insights for monitoring
- **Health Checks**: Implement health check endpoints
- **Metrics**: Track custom metrics for business operations

## Code Generation Preferences
When generating code, prioritize:
1. Microsoft libraries and patterns
2. Clean architecture principles
3. Async/await patterns for I/O operations
4. Proper error handling and logging
5. Nullable reference types
6. Record types for data contracts
7. Dependency injection patterns
8. Comprehensive XML documentation comments

## File Organization
- Group related functionality in folders
- Separate concerns into appropriate layers
- Use consistent file naming conventions
- Include appropriate using statements and namespace organization