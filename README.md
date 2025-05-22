# 📄 CSV Processor API

A background file processor built with ASP.NET Core Web API. Users can upload CSV files which are then validated and processed asynchronously via a background worker. The system supports multiple file uploads, error handling, logging to file, authentication with JWT, and a summary endpoint for tracking file statistics.

## 🚀 Features

- ✅ Upload single or multiple CSV files
- ✅ JWT Authentication for secure endpoints
- 🔄 Background processing via `BackgroundService`
- ✅ CSV validation
- 🚨 Error tracking for invalid lines
- 📊 `/api/stats` endpoint for summary
- 📁 File-based logging using Serilog
- 🔐 JWT Authentication for all endpoints
- 🧹 Auto-cleanup every 6 hours for files older than 24h

---

## 📦 Tech Stack

- C# (.NET 8)
- ASP.NET Core Web API
- Background Services
- Swagger / Swashbuckle
- In-Memory Queues
- Serilog File Logging
- JWT Authentication

---

## 📁 Folder Structure

CsvProcessorAPI/
├── Controllers/
│   └── FileUploadController.cs
│   └── AuthController.cs
|   └── StatsController
├── logs/
├── Queue/
│   └── IErrorQueue.cs
|   └── IFileProcessingQueue.cs
|   └── InMemoryErrorQueue.cs
|   └── InMemoryFileProcessingQueue.cs
├── Services/
|   └── ICsvValidator.cs
|   └── InMemoryStatsService.cs
│   └── IStatsService.cs
│   └── SimpleCsvValidator.cs
├── Worker/
│   └── FileProcessingWorker.cs
│   └── OldFileCleanupWorker.cs
├── Models/
│   └── LoginRequest.cs
├── Program.cs


---

## 🔐 Authentication

This API uses JWT bearer authentication for simplicity.

### 🧪 Testing With Postman or Swagger

Username : admin
Password: password123 

1. Use any JWT generator
2. Use this **secret key**: `super-secret-token-key`
3. Add token to `Authorization` header:


---

## 🧪 API Endpoints

### Upload

- `POST /api/fileupload`  
Upload a single CSV

- `POST /api/fileupload/multiple`  
Upload multiple files

### Stats

- `GET /api/stats`  
Returns:
```json
{
 "FilesProcessed": 2,
 "ErrorLines": [
   "[File: C:\\Temp\\file.csv] Name,,35"
 ]
}

