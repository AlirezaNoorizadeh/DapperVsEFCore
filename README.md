# Comparing Dapper and Entity Framework Core in CRUD Operations

This project demonstrates the differences between Dapper and Entity Framework Core (EF Core) in a .NET Web API, using CRUD operations as a comparison benchmark.

---

## 🚀 How to Run the Project

### 1. Database Setup
1. Create a new database in SQL Server Management Studio (SSMS)
2. Update the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_db;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2. Apply Migrations
Execute these commands in order in your terminal (from project directory):
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```



### 3. Build and Run
```bash
dotnet build
dotnet run
```

### 4. Test the API
1. The API will be available at `https://localhost:port`
2. Use Swagger UI for testing endpoints:
   - `/EfCore/students` for Entity Framework Core endpoints
   - `/dapper/students` for Dapper endpoints

---

## 🎯 Project Goal
### This project aims to:
- Compare performance and implementation between Dapper and EF Core
- Demonstrate CRUD operations with both technologies
- Show proper repository pattern implementation
- Highlight differences in query syntax and performance

---

## 📂 Project Structure

| Folder/File               | Description                                  |
|---------------------------|----------------------------------------------|
| **📁 Entities**           | Database entities                           |
| - `Student.cs`            | Student entity model                        |
|                           |                                              |
| **📁 Persistence**        | Database configuration                      |
| - `AppDbContext.cs`       | DbContext for EF Core                       |
| - `Configurations/`       | Entity configurations                       |
|                           |                                              |
| **📁 Repositories**       | Data access layer                           |
| - `StudentRepository.cs`  | Implements both Dapper and EF Core operations |
|                           |                                              |
| **📄 Program.cs**         | API endpoints configuration                 |
| **📄 appsettings.json**   | Configuration file                          |

---

## 🌐 API Endpoints

### EF Core Endpoints (`/EfCore/students`)
| Method | Endpoint         | Description                |
|--------|------------------|----------------------------|
| GET    | /GetAll          | Get all students           |
| GET    | /ReadById/{id}   | Get student by ID          |
| POST   | /Create          | Create new student         |
| PUT    | /Update          | Update existing student    |
| DELETE | /Delete/{id}     | Delete student             |

### Dapper Endpoints (`/dapper/students`)
| Method | Endpoint         | Description                |
|--------|------------------|----------------------------|
| GET    | /GetAll          | Get all students           |
| GET    | /ReadById/{id}   | Get student by ID          |
| POST   | /Create          | Create new student         |
| PUT    | /Update          | Update existing student    |
| DELETE | /Delete/{id}     | Delete student             |

---

## 🔍 Key Differences Highlighted

### Entity Framework Core Implementation:
- Uses LINQ for queries
- Automatic change tracking
- DbContext for database operations
- More abstraction from SQL

### Dapper Implementation:
- Raw SQL queries
- Manual connection management
- More control over exact queries
- Generally faster for simple queries

---

## 🛠️ Technologies Used
- .NET 8
- Entity Framework Core
- Dapper
- SQL Server
- Swagger for API documentation

---

## 📜 License [ [![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE) ]
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

Special thanks to:

- The .NET and Dapper communities for their excellent documentation, resources, and support.
- **[CodeCell](https://www.youtube.com/@codecell)** for their comprehensive tutorial video ["Dapper Tutorial in ASP.NET Core"](https://youtu.be/ZOX0eWJrzz4), which served as the foundation for this implementation. This project follows the architecture and patterns demonstrated in that outstanding tutorial.

---

The development of this project closely adheres to the concepts and structure taught in CodeCell's tutorial video. The integration of Dapper for efficient data access in ASP.NET Core, along with best practices in query optimization and application design, has been directly inspired by the guidelines presented in the video.
