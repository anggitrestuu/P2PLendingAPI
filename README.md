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
