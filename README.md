# 📝 Task Tracker API (.NET Clean Architecture)

A simple and scalable Task Tracker Web API built with .NET, following Clean Architecture principles.

It uses Entity Framework Core + SQLite and includes both unit and integration tests.

---

## 🚀 Features

* ✅ CRUD operations for TaskItem
* ✅ Clean Architecture (Domain / Application / Infrastructure / API)
* ✅ Entity Framework Core with SQLite
* ✅ Input validation & business rules
* ✅ Integration testing with full HTTP pipeline

---

## ⚠️ Business Rules

* Title is required
* Title maximum length is 100 characters
* ❌ A task cannot be marked as Done if the title is empty or whitespace

---

## 🧱 Architecture

```
TaskTracker
├── Domain          # Core business logic
├── Application     # Use cases / services
├── Infrastructure  # EF Core / database
├── API             # Controllers / endpoints
└── Tests           # Unit + integration tests
```

---

## 🌐 API Endpoints

| Method | Endpoint    | Description    |
| ------ | ----------- | -------------- |
| POST   | /tasks      | Create task    |
| GET    | /tasks      | Get all tasks  |
| GET    | /tasks/{id} | Get task by ID |
| PUT    | /tasks/{id} | Update task    |
| DELETE | /tasks/{id} | Delete task    |

---

## ▶️ Running the Application

### 1. Restore packages

```
dotnet restore
```

### 2. Apply migrations

```
dotnet ef database update --project TaskTracker.Infrastructure --startup-project TaskTracker.API
```

### 3. Run the API

```
dotnet run --project TaskTracker.API
```

---

## 🧪 Running Tests

```
dotnet test
```

---

## 🧪 Testing Strategy

### ✅ Unit Tests

* Domain validation
* Business rules
* Service logic (using Moq)

### ✅ Integration Tests

* Full HTTP pipeline testing
* SQLite in-memory database
* NUnit + FluentAssertions

---

## 📌 Summary

This project demonstrates:

* Clean Architecture in .NET
* Strong domain-driven validation
* Realistic API testing strategy
* Production-ready project structure
