using MyTaskTracker.UI;
using MyTaskTracker.Data;
using MyTaskTracker.Data.Implementation;
using MyTaskTracker.Services;
using MyTaskTracker.Services.Implementation;

ITaskRepository taskRepository = new TaskRepository("tasks.json");
ITaskService taskService = new TaskService(taskRepository);
ConsoleUI consoleUI = new ConsoleUI(taskService);
consoleUI.Run();