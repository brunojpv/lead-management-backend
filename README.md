# 🧠 Lead Management API (.NET)

API para gerenciamento de leads, construída com ASP.NET Core 6, arquitetura DDD + CQRS + MediatR, testes com xUnit, autenticação JWT simulada e suporte a Event Sourcing (opcional).

---

## 🧰 Tecnologias

- ✅ ASP.NET Core 6
- ✅ Entity Framework Core
- ✅ SQL Server (com suporte a Docker)
- ✅ MediatR + CQRS
- ✅ DDD (Domain-Driven Design)
- ✅ AutoMapper
- ✅ xUnit + Moq (testes)
- ✅ Autenticação JWT (simulada nos testes)
- ✅ Swagger (OpenAPI)
- ✅ CORS habilitado para localhost:5173 (front-end)

---

## 📁 Estrutura de pastas

LeadManagement
├── LeadManagement.Api # Camada de apresentação (Web API)
├── LeadManagement.Application # Casos de uso, comandos e handlers
├── LeadManagement.Domain # Entidades, enums, interfaces (DDD)
├── LeadManagement.Infrastructure # Repositórios, DbContext e EF
├── LeadManagement.Tests # Testes unitários e de integração


---

## 🚀 Como executar o projeto

### ▶️ 1. Clonar e acessar o projeto

```
git clone https://github.com/seu-usuario/lead-management.git
cd LeadManagement
```

## ▶️ 2. Restaurar pacotes

```
dotnet restore
```

## ▶️ 3. Rodar o banco com Docker

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Bjpv@1982" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

## ▶️ 4. Rodar as migrations

```
dotnet ef database update --project LeadManagement.Infrastructure --startup-project LeadManagement.Api
```

## ▶️ 5. Executar a API

```
dotnet run --project LeadManagement.Api
```

Acesse:
📎 http://localhost:5000/swagger







