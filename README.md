# 📂 CSV Processor API

A production-style .NET Web API for processing uploaded CSV files in the background — with validation, logging, error tracking, authentication, and scheduled cleanup.

## 🚀 Features

- ✅ Upload single or multiple CSV files
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
- In-Memory Queues
- Serilog File Logging
- JWT Authentication

---

## 📁 Folder Structure

├── Controllers/
├── Queue/
├── Services/
├── Worker/
├── logs/
├── Program.cs

---

## 🔐 Authentication

This API uses JWT bearer authentication for simplicity.

### 🧪 Testing With Postman or Swagger

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

