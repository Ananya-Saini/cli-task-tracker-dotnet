namespace MyTaskTracker.UI;

using MyTaskTracker.Services;
using MyTaskTracker.Models;

public class ConsoleUI
{
    private readonly ITaskService taskService;

    public ConsoleUI(ITaskService taskService)
    {
        this.taskService = taskService;
    }
    public void Run()
    {

        bool running = true;
        while (running)
        {
            ReaderHeader();
            ReaderMenu();
            
            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            switch (choice.Trim())
            {
                case "1":
                    HandleAddTask();
                    break;
                case "2":
                    HandleMarkAsComplete();
                    break;
                case "3":
                    HandleDeleteTask();
                    break;
                case "4":
                    HandleViewTasks();
                    break;
                case "5":
                    running = false;
                    RenderSuccess("Exiting the application. Goodbye!");
                    break;
                default:
                    RenderError("Invalid choice. Please try again.");
                    break;
            }

            if(running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public void ReaderHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n===================================");
        Console.WriteLine("       My Task Tracker");
        Console.WriteLine("===================================\n");
        Console.ResetColor();
    }

    public void ReaderMenu()
    {
        Console.WriteLine("1. Add Task");
        Console.WriteLine("2. Mark Task as Complete");
        Console.WriteLine("3. Delete Task");
        Console.WriteLine("4. View Tasks");
        Console.WriteLine("5. Exit");
        Console.WriteLine("\n-----------------------------------\n");
    }

    public void HandleAddTask()
    {
        Console.WriteLine("------------Add Task-----------------\n");
        Console.Write("Enter task description: ");
        string description = Console.ReadLine() ?? string.Empty;

        if(string.IsNullOrWhiteSpace(description))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            RenderError("Task description cannot be empty.\n");
            Console.ResetColor();
            return;
        }
        if (taskService.AddTask(description))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            RenderSuccess("Task added successfully.\n");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            RenderError("Failed to add task.\n");
            Console.ResetColor();
        }
    }

    public void HandleMarkAsComplete()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("------------Mark Task as Complete-----------------\n");
        Console.Write("Enter task ID to mark as complete: ");
        if(int.TryParse(Console.ReadLine(), out int taskId))
        {
            if (taskService.MarkAsComplete(taskId))
            {
                RenderSuccess($"Task with ID {taskId} marked as complete.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                RenderError($"Failed to mark task with ID {taskId} as complete.\n");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            RenderError("Invalid task ID. Please enter a valid number.\n");
            Console.ResetColor();
        }
    }

    public void HandleDeleteTask()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("------------Delete Task-----------------\n");
        Console.Write("Enter task ID to delete: ");
        if(int.TryParse(Console.ReadLine(), out int taskId))
        {
            if (taskService.DeleteTask(taskId))
            {
                RenderSuccess($"Task with ID {taskId} deleted successfully.\n");
            }
            else
            {
                RenderError($"Failed to delete task with ID {taskId}. It may not exist.\n");
            }
        }
        else
        {
            RenderError("Invalid task ID. Please enter a valid number.\n");
        }
    }

    public void HandleViewTasks()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("------------View Tasks-----------------\n");
        Console.WriteLine("1. View All Tasks");
        Console.WriteLine("2. View Completed Tasks");
        Console.WriteLine("3. View To-Do Tasks");
        Console.ResetColor();
        Console.Write("\nEnter your choice: ");
        string choice = Console.ReadLine() ?? string.Empty;

        TaskFilter filter = choice switch
        {
            "1" => TaskFilter.All,
            "2" => TaskFilter.Completed,
            "3" => TaskFilter.ToDo,
            _ => TaskFilter.All
        };

        var tasks = taskService.GetFilteredTasks(filter);
        if (tasks.Count == 0)
        {
            RenderError("\nNo tasks found for the selected filter.\n");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nTasks ({filter}):\n");
        foreach (var task in tasks)
        {
            string status = task.isComplete ? "Completed" : "To-Do";
            Console.ForegroundColor = task.isComplete ? ConsoleColor.Green : ConsoleColor.Yellow;
            RenderSuccess($"ID: {task.Id}, Description: {task.description}, Status: {status}, Created At: {task.createdAt}");
        }
        Console.ResetColor();
    }

    private static void RenderSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nSuccess: {message}");
        Console.ResetColor();
    }

    private static void RenderError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nError: {message}");
        Console.ResetColor();
    }
}