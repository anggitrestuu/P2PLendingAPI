Certainly! I'll create a comprehensive README.md file that will guide others through the process of setting up and running your P2P Lending API for the first time. You can place this file in the root directory of your project.

```markdown
# P2P Lending API

This is a RESTful API for a Peer-to-Peer Lending platform built with ASP.NET Core and PostgreSQL.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/downloads) (optional, for cloning the repository)

## Getting Started

1. Clone the repository (if you haven't already):
```

git clone https://github.com/anggitrestuu/P2PLendingAPI.git
cd P2PLendingAPI

```

2. Install the required .NET tools:
```

dotnet tool install --global dotnet-ef

```

3. Install the project dependencies:
```

dotnet restore

````

4. Set up the database:
- Create a new PostgreSQL database for the project
- Update the connection string in `appsettings.json`:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=P2PLendingDB;Username=your_username;Password=your_password;"
  }
  ```

5. Apply the database migrations:
````

dotnet ef database update

```

6. Run the application:
```

dotnet run

```

The API should now be running on `http://localhost:5045` (or a similar port).

## API Documentation

Once the application is running, you can access the Swagger UI documentation at:

```

http://localhost:5045/swagger

```

This provides an interactive interface to explore and test the API endpoints.

## Authentication

The API uses JWT (JSON Web Tokens) for authentication. To access protected endpoints:

1. Register a new user using the `/api/auth/register` endpoint
2. Login using the `/api/auth/login` endpoint to receive a JWT token
3. For subsequent requests, include the token in the Authorization header:
```

Authorization: Bearer <your_token_here>

```

## Main Endpoints

- POST `/api/auth/register`: Register a new user
- POST `/api/auth/login`: Login and receive a JWT token
- GET `/api/lender/loan-requests`: Get all loan requests (for lenders)
- POST `/api/loan`: Create a new loan request (for borrowers)
- POST `/api/funding`: Fund a loan (for lenders)
- POST `/api/repayment`: Make a loan repayment (for borrowers)

Refer to the Swagger documentation for a complete list of endpoints and their usage.

## Development

To run the project in development mode with hot reload:

```

dotnet watch run

```

## Testing

(Add instructions for running tests once you have implemented them)

## Deployment

(Add deployment instructions specific to your hosting environment)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
```

This README provides a comprehensive guide for setting up and running the P2P Lending API. It covers:

1. Prerequisites
2. Getting started steps
3. Database setup
4. Running the application
5. Accessing API documentation
6. Authentication process
7. Main endpoints
8. Development and testing instructions
9. Placeholders for deployment and contributing guidelines

Feel free to adjust any parts of this README to better fit your specific project setup or to add any additional information you think would be helpful for users of your API.

```markdown
# Adding New Features to P2P Lending API

If you want to extend the functionality of this P2P Lending API, follow these steps to add new features:

## 1. Plan Your Feature

- Define the purpose and scope of your new feature.
- Identify which part of the API it will affect (e.g., Loans, Users, Transactions).
- Determine if it requires new database tables or modifications to existing ones.

## 2. Update the Database

If your feature requires database changes:

1. Create a new model in the `Models` folder if needed.
2. Update `ApplicationDbContext.cs` to include any new DbSet.
3. Create a new migration:
```

dotnet ef migrations add YourNewFeatureName

```
4. Apply the migration:
```

dotnet ef database update

````

## 3. Create or Update DTOs

1. Add or modify DTOs in the `DTOs` folder to represent the data structures for your new feature.

## 4. Implement the Repository

1. If needed, create a new repository interface in `Repositories/Interfaces`.
2. Implement the repository in the `Repositories` folder.
3. Register the repository in `Startup.cs`:
```csharp
services.AddScoped<IYourNewRepository, YourNewRepository>();
````

## 5. Implement the Service

1. Create a new service interface in `Services/Interfaces`.
2. Implement the service in the `Services` folder.
3. Register the service in `Startup.cs`:
   ```csharp
   services.AddScoped<IYourNewService, YourNewService>();
   ```

## 6. Create or Update the Controller

1. Create a new controller or update an existing one in the `Controllers` folder.
2. Implement the necessary action methods for your feature.
3. Use the appropriate HTTP methods (GET, POST, PUT, DELETE) and route attributes.

## 7. Update Authorization

1. Ensure proper authorization attributes are applied to new endpoints.
2. Update `JwtHelper.cs` if new claims are needed.

## 8. Add Validation

1. Implement input validation in DTOs using Data Annotations.
2. Add business logic validation in the service layer.

## 9. Update Error Handling

1. Add any new custom exceptions if needed.
2. Update `ErrorHandlingMiddleware.cs` to handle new exception types.

## 10. Update AutoMapper Profiles

1. Modify `AutoMapperProfile.cs` to include mappings for new DTOs and models.

## 11. Write Unit Tests

1. Add unit tests for new repositories, services, and controllers in the test project.

## 12. Update API Documentation

1. Ensure Swagger annotations are added to new endpoints.
2. Update any external API documentation.

## 13. Test Your Feature

1. Test the new feature thoroughly using Swagger UI or Postman.
2. Ensure it works correctly with existing features.

## 14. Update README

1. Document your new feature in the main README.md file.

## 15. Create a Pull Request

1. If working in a team, create a pull request with your changes for review.

Remember to follow the existing code style and conventions while implementing your new feature. If you're adding a significant feature, consider incrementing the API version number.

For any questions or clarifications about adding new features, please open an issue in the repository.

```

This README section provides a step-by-step guide for adding new features to the P2P Lending API. It covers all aspects of feature development, from planning and database changes to testing and documentation.

You can add this section to your existing README.md file, perhaps under a "Development" or "Contributing" section. This will help other developers (or yourself in the future) understand the process of extending the API's functionality.
```
