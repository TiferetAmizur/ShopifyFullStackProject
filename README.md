# ShopifyFullStackProject

## Overview

This is a full-stack web application for managing products, featuring a .NET backend and an Angular frontend. The system allows users to:

### Manage Products via API:
- Retrieve product details from the database
- Update existing product information
- Add new products
- Delete products

### User Interface Features:
- Display product details
- Edit product information
- Add new products through a simple UI
- Remove products

The application includes authentication and role-based access control using JWT, ensuring that only authorized users can perform specific actions. It follows a clean architecture with separation of concerns, secure configuration handling, and structured logging for tracking operations and errors.

## Technologies Used
- **Backend:** .NET (C#) with Entity Framework
- **Frontend:** Angular, TypeScript, HTML, CSS
- **Database:** SQL Server
- **Security:** JWT for authentication and authorization
- **Logging:** Serilog (configured via appsettings.json) for structured logging and easy configuration adjustments

## Database Initialization

Entity Framework Core is used for ORM (Object-Relational Mapping). The database is automatically seeded with initial data, including:
- **Users:** An admin user (admin) and a viewer user (viewer).
- **Products:** Sample products with descriptions.

The `OnModelCreating` method in `ApplicationDbContext` ensures initial data is populated when migrations are applied.

## Prerequisites
- **.NET SDK** (compatible with the project, e.g., .NET 6 or later)
- **Node.js** (latest LTS recommended)
- **Angular CLI** (install via `npm install -g @angular/cli`)
- **SQL Server** 

### Install `dotnet-ef` (if not already installed)
Before running database migrations, ensure `dotnet-ef` is installed:

```bash
dotnet tool install --global dotnet-ef --version 6.0.10
```

## Installation and Setup

### Backend
1. Clone the repository:
    ```bash
    git clone https://github.com/TiferetAmizur/ShopifyFullStackProject.git
    cd ShopifyFullStackProject/backend
    ```
2. Configure the database connection and logging:
    - Update `appsettings.json` with your database credentials.
    - Ensure Serilog settings are properly configured for logging.
    - Make sure your database server is running.
 

3. Apply database migrations and seed data:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. Install dependencies and run the backend:
    ```bash
    dotnet restore
    dotnet run
    ```

### Frontend
1. Navigate to the frontend directory:
    ```bash
    cd ../client
    ```

2. Install dependencies:
    ```bash
    npm install
    ```

3. Start the Angular application:
    ```bash
    ng serve
    ```

4. Open a browser and go to [http://localhost:4200](http://localhost:4200).
