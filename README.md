# TaskTracker
📝 Task Tracker API (.NET Clean Architecture)
A simple Task Tracker Web API built with .NET, following Clean Architecture principles, using Entity Framework Core + SQLite, and fully tested with NUnit, FluentAssertions, and integration tests.
________________________________________
🚀 Features
•	✅ CRUD operations for TaskItem
•	✅ Clean Architecture (Domain / Application / Infrastructure / API)
•	✅ EF Core with SQLite
•	✅ Validation and business rules
•	✅ Integration tests (NUnit + WebApplicationFactory)
________________________________________
⚠️ Business Rules
•	Title is required
•	Title max length = 100 characters
•	❌ A task cannot be marked as Done if the title is empty or whitespace
________________________________________
🧱 Architecture
TaskTracker
├── Domain          → Core business logic
├── Application     → Use cases / services
├── Infrastructure  → EF Core / database
├── API             → Controllers / endpoints
└── Tests           → Unit + integration tests
________________________________________
🌐 API Endpoints
Method	Endpoint	Description
POST	/tasks	Create task
GET	/tasks	Get all tasks
GET	/tasks/{id}	Get task by ID
PUT	/tasks/{id}	Update task
DELETE	/tasks/{id}	Delete task
________________________________________
▶️ Running the Application
1. Restore packages
dotnet restore
2. Apply migrations
dotnet ef database update --project TaskTracker.Infrastructure --startup-project TaskTracker.API
3. Run API
dotnet run --project TaskTracker.API
________________________________________
🧪 Running Tests
dotnet test
________________________________________
🧪 Testing Strategy
✅ Unit Tests
•	Domain validation
•	Business rules
•	Service logic (with Moq)
✅ Integration Tests (API)
•	Full HTTP pipeline testing
•	SQLite in-memory database
•	NUnit + FluentAssertions
________________________________________

📌 Summary
This project demonstrates:
•	Clean Architecture in .NET
•	Proper domain-driven validation
•	Realistic API testing strategy
•	Production-ready structure
