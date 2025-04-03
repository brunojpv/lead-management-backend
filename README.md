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

```
LeadManagement
├── LeadManagement.Api # Camada de apresentação (Web API)
├── LeadManagement.Application # Casos de uso, comandos e handlers
├── LeadManagement.Domain # Entidades, enums, interfaces (DDD)
├── LeadManagement.Infrastructure # Repositórios, DbContext e EF
├── LeadManagement.Tests # Testes unitários e de integração
```

---

## 🚀 Como executar o projeto

### ▶️ 1. Clonar e acessar o projeto

```
git clone https://github.com/seu-usuario/lead-management.git
cd LeadManagement
```

### ▶️ 2. Restaurar pacotes

```
dotnet restore
```

### ▶️ 3. Rodar o banco com Docker

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Bjpv@1982" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

### ▶️ 4. Rodar as migrations

```
dotnet ef database update --project LeadManagement.Infrastructure --startup-project LeadManagement.Api
```

### ▶️ 5. Executar a API

```
dotnet run --project LeadManagement.Api
```

### Acesse:

📎 http://localhost:5000/swagger

---

## ✅ Testes

### Execute os testes com:

```
dotnet test
```

- ✅ Testes de comandos/handlers (Create, Accept, Decline)
- ✅ Testes de integração com banco InMemory
- ✅ Testes simulando o fluxo completo

---

## 📦 Endpoints principais

| Método | Rota	                   | Descrição                |
|--------|-------------------------|--------------------------|
| GET    | /api/leads              | Listar leads com filtros |
| GET	   | /api/leads/{id}         | Buscar por ID            |
| POST   | /api/leads              | Criar novo lead          |
| POST   | /api/leads/accept/{id}  | Aceitar lead             |
| POST   | /api/leads/decline/{id} | Recusar lead             |

---

## 🧪 Exemplo de body (POST /api/leads)

```
{
  "firstName": "Bruno",
  "lastName": "Vieira",
  "suburb": "Centro",
  "category": "Obra",
  "description": "Reforma completa",
  "price": 1500,
  "phone": "11999999999",
  "email": "bruno@mail.com"
}
```

---

## 🙌 Autor

**Bruno Vieira**  
💻 Full Stack .NET + React  
🔗 [linkedin.com/in/brunojpv](https://www.linkedin.com/in/brunojpv)
