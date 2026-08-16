using MyTaskTracker.Models;
using MyTaskTracker.Data;

namespace MyTaskTracker.Services.Implementation;
public class TaskService: ITaskService
{
    private readonly ITaskRepository repository;
    private List<TaskItem> tasks;
    
    public TaskService(ITaskRepository repository)
    {
        this.repository = repository;
        tasks = repository.LoadTask();
    }
    public bool AddTask(string task)
    {
        if(string.IsNullOrWhiteSpace(task))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Task description cannot be empty.");
            Console.ResetColor();
            return false;
        }
        
        int newId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

        var newTask = new TaskItem
        {
            Id = newId,
            description = task,
            isComplete = false,
            createdAt = DateTime.Now
        };
        
        tasks.Add(newTask);
        repository.SaveTask(tasks);
        return true;
    }

    public bool MarkAsComplete(int taskId)
    {
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if(task == null) return false;

        if(task.isComplete)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Task with ID {taskId} is already marked as complete.");
            Console.ResetColor();
            return true;
        }

        task.isComplete = true;
        repository.SaveTask(tasks);
        return true;
    }

    public bool DeleteTask(int taskId)
    {
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if(task == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Task with ID {taskId} not found.");
            Console.ResetColor();
            return false;
        }

        tasks.Remove(task);
        repository.SaveTask(tasks);
        return true;
    }

    public List<TaskItem> GetFilteredTasks(TaskFilter filter)
    {
        return filter switch
        {
            TaskFilter.All => tasks.OrderBy(t => t.Id).ToList(),
            TaskFilter.Completed => tasks.Where(t => t.isComplete).OrderBy(t => t.Id).ToList(),
            TaskFilter.ToDo => tasks.Where(t => !t.isComplete).OrderBy(t => t.Id).ToList(),
            _ => tasks.OrderBy(t => t.Id).ToList(),
        };
    }
}