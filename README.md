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



