# CLI Task Tracker (.NET)

A modular, cross-platform Command Line Interface (CLI) application built with C# and .NET to manage daily tasks. The project demonstrates clean software architecture principles (Separation of Concerns, Dependency Injection), robust input validation, and automatic local JSON persistence.

---

## 🚀 Features

* **Task Management (CRUD):** Create new tasks, mark tasks complete, and delete tasks by ID.
* **Smart Filtering:** View tasks filtered by status (**All**, **Completed**, or **To-Do**) using LINQ expressions.
* **Auto-Persistence:** Automatically syncs tasks to a local `tasks.json` storage file on every state change using `System.Text.Json`.
* **Resilient Input & Error Handling:** Guard clauses, safe `int.TryParse` validations, and self-healing storage (automatically creates `tasks.json` if missing).
* **Terminal UI:** Color-coded status badges, formatted console output, and an interactive menu loop.

---

## 🏗️ Architecture & Project Structure

The project follows a decoupled 3-tier architecture:

```text
MyTaskTracker/
├── Models/
│   ├── TaskItem.cs           # Core domain entity (Id, description, isComplete, createdAt)
│   └── TaskFilter.cs         # Enum defining filter states (All, Completed, ToDo)
├── Data/
│   ├── ITaskRepository.cs    # Storage abstraction contract
│   └── JsonTaskRepository.cs # File I/O and JSON serialization logic
├── Services/
│   ├── ITaskService.cs       # Business logic contract
│   └── TaskService.cs        # In-memory management, LINQ queries, and auto-ID calculation
├── UI/
│   └── ConsoleUI.cs          # Interactive CLI menus, formatting, and user input validation
└── Program.cs                # Entry point & dependency wiring
