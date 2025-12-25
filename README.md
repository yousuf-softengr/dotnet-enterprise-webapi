# EnterpriseWebApi

Enterprise-grade ASP.NET Core Web API built using Clean Architecture principles.

This project demonstrates how to design scalable, maintainable, and testable backend systems suitable for remote-first teams and enterprise environments.

---

## 🚀 Features

- Clean Architecture with strict separation of concerns
- CQRS pattern using MediatR
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server support
- Dependency Injection
- Swagger / OpenAPI
- Docker-ready (planned)
- CI/CD ready (planned)

---

## 🏗 Architecture Overview
```text
EnterpriseWebApi.API
├── Controllers
│ └── API endpoints
│
EnterpriseWebApi.Application
├── Business logic
├── CQRS handlers
│
EnterpriseWebApi.Domain
├── Core domain entities
├── Interfaces
│
EnterpriseWebApi.Infrastructure
├── Database access (EF Core)
├── External services


**Why Clean Architecture?**
- Business logic is independent of frameworks
- Easy to test and maintain
- Scales well for distributed teams

---

## 🛠 Tech Stack

- **Framework**: ASP.NET Core (.NET 8)
- **Architecture**: Clean Architecture, CQRS
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Messaging**: (Planned) RabbitMQ
- **Caching**: (Planned) Redis
- **Auth**: (Planned) JWT Authentication

---

## API Design

The API follows RESTful conventions where resources are accessed via consistent URLs and actions are determined by HTTP verbs.

---

## 🔐 Authentication & Authorization

The API uses JWT Bearer authentication with role-based authorization.
Protected endpoints require valid access tokens, and roles are enforced declaratively.

---

## 🔐 Token Management

The API implements secure JWT authentication using short-lived access tokens and rotating refresh tokens to ensure maximum security.

---

## ❗ Error Handling

This API follows RFC 7807 (ProblemDetails) for consistent error responses.

Handled status codes:
- 400 Bad Request
- 404 Not Found
- 409 Conflict
- 500 Internal Server Error

All errors include a traceId for diagnostics.
Unhandled exceptions are safely mapped to HTTP 500 responses to avoid leaking internal details.

## ▶️ How to Run Locally

### Prerequisites
- .NET 8 SDK
- SQL Server
- Visual Studio 2022

### Steps
```bash
git clone https://github.com/your-username/dotnet-enterprise-webapi.git
cd dotnet-enterprise-webapi
dotnet restore
dotnet run --project EnterpriseWebApi.API



