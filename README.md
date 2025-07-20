# BookStoreAPI

BookStoreAPI is a RESTful Web API built with ASP.NET Core and Entity Framework Core. It manages books, authors, genres, and reviews, and supports scheduled data import with authentication and role-based authorization.

---

## Features

* CRUD operations for Books
* Relational data for Authors, Genres, and Reviews
* GET books with:

  * Title
  * Author Names
  * Genre Names
  * Average Review Rating
* GET top 10 books by average rating (raw SQL)
* Role-based authentication (Read, ReadWrite)
* Scheduled import of mocked book data using Quartz.NET
* Swagger documentation
* Structured logging with built-in .NET logging
* Dependency injection throughout

---

##  Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* PostgreSQL 

### Clone the Repository

```bash
git clone https://github.com/Markog19/BookStoreAPI.git
cd BookStoreAPI
```

### Setup Configuration

Modify `appsettings.json` with your PostgreSQL connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=BookStoreDb;Username=youruser;Password=yourpassword"
}
```

---

## Running the Application

### Apply Migrations and Seed (if needed)

```bash
dotnet ef database update
```

### Run the API

```bash
dotnet run
```
---

## Authentication & Authorization

* **Read** role: Can access GET endpoints
* **ReadWrite** role: Can access all endpoints (GET, POST, PUT, DELETE)
* JWT authentication is used

---
## Centralized Logging & Exception Handling

* All activity is logged using .NET's structured logging with ILogger

* Centralized exception handling via custom middleware

* Middleware catches unhandled exceptions and returns consistent error responses

* Log output includes HTTP method, path, status, and error details (where applicable)

* Sensitive data is excluded from logs for security

---
## Scheduled Import Task

* Uses **Quartz.NET** to run every hour
* Simulates import of 100,000+ books
* Avoids duplicates (case-insensitive, trimmed title match)
* Mocked API with hardcoded data



---

## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core (PostgreSQL & InMemory)
* Quartz.NET
* Swagger (Swashbuckle)
* Moq (unit testing)
* xUnit (testing framework)
* Serilog

---

##  Testing

### Run All Tests

```bash
dotnet test
```

### Tests Included

* Unit Test: Service logic 

---

##  Known Limitations

* No real external API connection for book imports (mocked only)
* Does not use DTOs for returning data, might make a problem for returning many-to-many tables because of looped references
* 
---

