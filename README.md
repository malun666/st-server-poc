# ST Server POC

A .NET Core Web API project with JWT authentication and user management.

## Tech Stack

- .NET 8.0
- ASP.NET Core Identity
- Entity Framework Core (In-Memory) We can use a real database in the future
- JWT Bearer Authentication

## Prerequisites

- .NET 8.0 SDK
- Visual Studio Code or Visual Studio 2022

## Quick Start

1. Clone and navigate to the project

```bash
git clone <repository-url>
cd st-server-poc
```

2. Run the application

```bash
dotnet run
```

The server will start at `http://localhost:3008`

## API Documentation

### Auth Endpoints

#### Register

```http
POST /api/auth/register
Content-Type: application/json

{
    "username": "testuser",
    "email": "test@example.com",
    "password": "Test123!"
}
```

#### Login

```http
POST /api/auth/login
Content-Type: application/json

{
    "username": "testuser",
    "password": "Test123!"
}

Response:
{
    "token": "jwt_token_string"
}
```

#### Get User Info

```http
GET /api/auth/user
Authorization: Bearer {token}

Response:
{
    "username": "testuser",
    "email": "test@example.com"
}
```

#### Logout

```http
POST /api/auth/logout
Authorization: Bearer {token}

Response:
{
    "message": "Logged out successfully"
}
```

## Testing

Use the provided `Login.http` file to test the API endpoints:

```http
### 1. Register
POST http://localhost:3008/api/auth/register

### 2. Login
POST http://localhost:3008/api/auth/login

### 3. Get User Info
GET http://localhost:3008/api/auth/user

### 4. Logout
POST http://localhost:3008/api/auth/logout
```

## Configuration

JWT settings in `appsettings.json`:

```json
{
  "Jwt": {
    "Issuer": "TestIssuer",
    "Audience": "TestAudience",
    "Key": "ThisIsASecretKeyForJwtTokenGeneration"
  }
}
```

## Security Features

- JWT token-based authentication
- Token expiration: 1 hour
- Password hashing with Identity
- Protected endpoints with [Authorize] attribute
- In-memory database (resets on restart)
