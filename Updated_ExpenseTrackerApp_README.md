# 💰 Expense Tracker App (ASP.NET Core MVC + MySQL)

This project is a **beginner-friendly full-stack Expense Tracker** built with **ASP.NET Core MVC (.NET 8)** and **MySQL** using **Entity Framework Core**.

It allows you to:
✅ Track income and expenses  
✅ Categorize transactions  
✅ View dashboards with charts  
✅ Filter transactions by date  
✅ Export data (coming soon)

---

## 🧱 Tech Stack
- **Frontend:** Razor Views (Bootstrap + Chart.js)
- **Backend:** ASP.NET Core MVC (.NET 8)
- **Database:** MySQL (via Pomelo.EntityFrameworkCore.MySql)
- **ORM:** Entity Framework Core (Code-First)
- **IDE:** Visual Studio Code

---

## ⚙️ Setup Instructions

### 1️⃣ Prerequisites
- .NET 8 SDK  
- MySQL 5.6+ or 8+  
- Visual Studio Code or Visual Studio  
- EF Core tools

```bash
dotnet tool install --global dotnet-ef
```

---

### 2️⃣ Clone the Project
```bash
https://github.com/sivasai-yarlagadda/ExpensesTrackerApp.git
cd ExpensesTrackerApp

```

---

### 3️⃣ Configure MySQL Connection
Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=ExpenseTrackerDB;user=root;password=yourpassword;"
}
```

---

### 4️⃣ Run EF Migrations
```bash
dotnet ef database update
```

This creates the database and required tables.

---

### 5️⃣ Run the Application
```bash
dotnet run
```

Your app will open at:
```
https://localhost:9999
```

---

### 6️⃣ Features Implemented
- CRUD for Transactions  
- Category seeding  
- Income vs Expense chart  
- Category-wise expense bar chart  
- Date range filter  
- Currency formatting (₹ Indian Rupees)

---

### 7️⃣ Folder Structure
```
ExpenseTrackerApp/
│
├── Properties/
│   └── launchSettings.json        → Defines ports (HTTP/HTTPS) and environment
│
├── wwwroot/                       → Static files (CSS, JS, images)
│
├── Controllers/
│   ├── HomeController.cs          → Dashboard and chart logic
│   └── TransactionsController.cs  → CRUD operations for transactions
│
├── Migrations/
│   ├── 20251112064126_InitialCreate_MySQL56.cs → Initial EF migration file
│   └── ExpenseTrackerContextModelSnapshot.cs   → EF Core model snapshot
│
├── Models/
│   ├── Category.cs                → Defines Category entity
│   ├── Transaction.cs             → Defines Transaction entity
│   ├── ExpenseTrackerContext.cs   → DbContext (database context)
│   ├── DbInitializer.cs           → Seeds initial data
│   └── ErrorViewModel.cs          → Used for error pages
│
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           → Dashboard view (Income, Expense, Charts)
│   │   └── Privacy.cshtml         → Static privacy policy
│   │
│   ├── Transactions/
│   │   ├── Create.cshtml          → Form to create new transaction
│   │   ├── Edit.cshtml            → Form to edit existing transaction
│   │   ├── Delete.cshtml          → Confirm delete action
│   │   └── Index.cshtml           → List of all transactions
│   │
│   ├── Shared/
│   │   ├── _Layout.cshtml         → Common layout (header, footer)
│   │   └── _ValidationScriptsPartial.cshtml → Client-side validation
│   │
│   ├── _ViewImports.cshtml        → Global Razor imports
│   └── _ViewStart.cshtml          → Defines default layout
│
├── appsettings.json               → Configuration (DB connection string)
│
└── Program.cs                     → Entry point; configures services, middleware, routing
```
```
ExpenseTrackerApp/
 ┣ Controllers/
 ┃ ┣ HomeController.cs
 ┃ ┣ TransactionsController.cs
 ┣ Models/
 ┃ ┣ Category.cs
 ┃ ┣ Transaction.cs
 ┃ ┣ ExpenseTrackerContext.cs
 ┣ Views/
 ┃ ┣ Home/
 ┃ ┣ Transactions/
 ┣ wwwroot/
 ┣ appsettings.json
 ┣ Program.cs
 ┣ README.md
```

---

### 8️⃣ Run Ports Configuration
Inside `Properties/launchSettings.json`:
```json
"applicationUrl": "https://localhost:9999;http://localhost:9966"
```

---

## ⚙️ Important Configuration & Setup Notes

### 🧱 appsettings.json — Database Connection
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=ExpenseTrackerDB;user=root;password=yourpassword;"
}
```
Modify credentials as per your local MySQL setup.

---

### 🧱 Program.cs — Database Context Setup
```csharp
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 6, 0))
    ));
```

---

### 🧱 launchSettings.json — Fixed Local Port
```json
"applicationUrl": "https://localhost:9999;http://localhost:9966"
```
Use this for consistent development ports.

---

### 🧱 _ViewImports.cshtml — Razor Imports
```html
@using ExpenseTrackerApp
@using ExpenseTrackerApp.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

### 🧱 Program.cs — Auto Database Creation
```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExpenseTrackerContext>();
    db.Database.Migrate();
    DbInitializer.Initialize(db);
}
```

---

### 🧱 EF Core Tools
Install once:
```bash
dotnet tool install --global dotnet-ef
```

---

### 🧱 Currency Formatting (Indian Rupees ₹)
In controller:
```csharp
var culture = new CultureInfo("en-IN");
ViewBag.TotalIncome = totalIncome.ToString("N2", culture);
```

---

### 🧱 HTTPS Certificate
If HTTPS fails:
```bash
dotnet dev-certs https --trust
```

---

## 📦 Required Imports (Using Statements)

### Program.cs
```csharp
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApp.Models;
```

### ExpenseTrackerContext.cs
```csharp
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApp.Models;
```

### Category.cs
```csharp
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
```

### Transaction.cs
```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
```

### Controllers
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApp.Models;
using System.Linq;
```

### _ViewImports.cshtml
```html
@using ExpenseTrackerApp
@using ExpenseTrackerApp.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

## 👨‍💻 Author
Developed by **Siva Sai Yarlagadda**  
Built with ❤️ using ASP.NET Core MVC and MySQL.
