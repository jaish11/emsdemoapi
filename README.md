# EMS Demo Application

This repository contains a full-stack Enterprise Management System (EMS) built using **ASP.NET Core Web API**, **Blazor Server**, and **SQL Server**.

---

## Solution Structure

emsdemoapi.sln
│
├── emsdemoapi # Backend Web API
├── emsdemoapi.Database # Database project (Tables & Stored Procedures)
├── EmsDemoBlazer # Blazor Server UI
---

## emsdemoapi (ASP.NET Core Web API)

- Provides REST APIs for the application
- Handles business logic and data access
- Uses Stored Procedures for database operations
- Implements JWT Authentication
- Supports caching (Memory / Redis)
- Logging configured using log4net

**Main folders**
- Controllers
- Data
  - Entities
  - Interfaces
  - Services
  - DTO
  - AppDbContext
- Migrations
- wwwroot
- Program.cs

---

## emsdemoapi.Database (Database Project)

- Contains SQL Server database schema
- Stores all tables and stored procedures
- Used to version control database changes

**Includes**
- Tables (Books, Students, Employees, Customers, etc.)
- Stored Procedures for CRUD operations

---

## EmsDemoBlazer (Blazor Server)

- User interface built using Blazor Server
- Communicates with Web API using HttpClient
- Implements CRUD screens for:
  - Books
  - Students
  - Employees
- Uses modal dialogs for Add/Edit
- Client-side validation using DataAnnotations
- SweetAlert used for success messages and delete confirmation
- Responsive UI using Bootstrap

**Main folders**
- Components (Pages, Layout)
- Models
- Services
- wwwroot
- Program.cs

---

## Application Flow



Blazor UI → Web API → Stored Procedures → SQL Server

---

## Technologies Used

- ASP.NET Core Web API
- Blazor Server
- SQL Server
- Entity Framework Core
- ADO.NET / Dapper
- Bootstrap
- SweetAlert
- Redis Cache
- JWT Authentication
- log4net

---

## How to Run

1. Clone the repository
2. Configure database connection string
3. Execute database scripts
4. Run the API project
5. Run the Blazor project
6. Open the Blazor application in browser

---

## Author

Jaish Ansari
