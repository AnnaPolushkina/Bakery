# Bakery Backend API

A backend application for a bakery, built with **ASP.NET Core Web API**,  
with a focus on clean architecture and clear separation of responsibilities.

This project was created as a **portfolio project for a junior backend developer**,  
emphasizing architectural thinking and conscious design decisions rather than tutorial-style CRUD.

---

## Project Goals

- Understand how backend systems are built **in real-world practice**
- Clearly separate:
  - business logic
  - infrastructure
  - HTTP/API concerns
- Be able to explain **why** certain architectural decisions were made
- Build a backend that can be connected to a real UI without changing the core architecture

---

## Architecture

The project follows **Clean Architecture principles**.
Project structure:

Bakery.sln
Bakery.Core
- Entities        // Domain entities and business rules
- Services        // Service interfaces (use cases)

Bakery.Infrastructure
- Persistence    // DbContext and EF Core setup
- Services       // Service implementations
- Seed           // Initial database seeding

Bakery.WebApi
- Controllers    // HTTP endpoints
- Dtos           // API contracts used by the UI

---

### Key Principles

- **Core** does not depend on Web API, EF Core, or databases
- **Infrastructure** is a replaceable implementation detail
- **WebApi** handles HTTP orchestration only
- Business rules live **only in the domain**

---

## Domain Model

### Product
- Represents a bakery product
- Contains business rules (active / inactive)
- Independent from persistence and UI

### Client
- Represents a bakery customer
- Can be deactivated instead of deleted
- Serves as the owner of orders

### Order (Aggregate Root)
- Belongs to a single client
- Contains order items
- Stores product price at the time of purchase
- Protects invariants:
  - an order cannot be confirmed if it has no items
  - a confirmed order cannot be modified

---

## API (Main Use Cases)

### Products
- `GET /products`
- `POST /products/{id}/deactivate`

### Clients
- `GET /clients`
- `POST /clients`
- `POST /clients/{id}/deactivate`

### Orders
- `POST /orders`
- `GET /orders/{id}`
- `POST /orders/{id}/items`
- `POST /orders/{id}/confirm`
- `GET /clients/{clientId}/orders`

The API is designed around **use cases**, not generic CRUD endpoints.

---

## Data Validation & Consistency

To keep the system in a consistent state:

- an order cannot be created for a non-existing or inactive client
- an order item cannot reference a non-existing or inactive product
- these checks are performed at the **Web API (application orchestration) level**,
  without polluting the domain or infrastructure layers

---

## Tech Stack

- C#
- .NET (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI
- Git & GitHub

---

## Why This Project Matters

This project intentionally avoids:

- template-generated CRUD controllers
- unnecessary abstractions
- adding features “just in case”

Instead, the focus of this project is on:

- making architectural decisions explicit
- keeping responsibilities of each layer clear
- adding new domain concepts without changing existing ones
- being able to explain design decisions during an interview

The project can be extended with:
- integration with a real frontend
- future extensions (payments, delivery, inventory)
- discussion during technical interviews

---

## Running the Project

1. Open `Bakery.sln`
2. Ensure EF Core migrations are applied
3. Run `Bakery.WebApi`
4. Open Swagger UI to explore the API
