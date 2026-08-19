# Kontaktverwalter

A comprehensive contact management solution built with .NET 8, featuring a WPF desktop application and a REST API backend.

## Prerequisites

- **.NET 8 SDK**
- **SQL Server** (Express or higher)
- **Visual Studio 2022** (recommended)

## Setup Instructions

### 1. Database Setup

Run the SQL script to create and initialize the database:

1. Open SQL Server Management Studio (SSMS) or your preferred SQL Server client
2. Execute the script: `ContactManagerDB_MSSQL.sql`

### 2. Build the Solution

Open the solution in Visual Studio 2022 and build the project using the `Run WPF+API` configuration.

## Project Structure

- **Kontaktverwalter** - WPF desktop application for contact management
- **Kontaktverwalter.API** - REST API backend
- **Kontaktverwalter.Shared** - Shared DTOs and models
- **ContactManagerDB_MSSQL.sql** - Database initialization script

## Technologies

- **Frontend**: WPF (Windows Presentation Foundation)
- **Backend**: ASP.NET Core Web API + Entity Framework Core