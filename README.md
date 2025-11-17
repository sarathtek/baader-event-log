# BAADER Event Log Assignment

Event-Log is a small full-stack application where the backend simulates machine events (stored in memory) and the frontend displays recent events.

---

## 1. Instructions to Build, Start, and Use the Application

### **Backend (.NET 8 – ASP.NET Core Web API)**

cd EventLogBackend

dotnet restore

dotnet run

API available at:

https://localhost:<port>/api/events?startTime=<timestamp>

Swagger Documentation - https://localhost:7295/swagger

Note: If you encounter SSL certificate warnings, run: dotnet dev-certs https --trust

---

### **Frontend (Angular)**

cd EventLogFrontend/event-log-frontend
npm install
ng serve --port 4200

Open the UI at:
http://localhost:4200
