using System.Text.Json;
using MyTaskTracker.Models;

namespace MyTaskTracker.Data.Implementation;

public class TaskRepository: ITaskRepository
{
    private readonly string filePath;
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public TaskRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public List<TaskItem> LoadTask()
    {
        if (!File.Exists(filePath))
        {
            return new List<TaskItem>();
        }

        try
        {
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<TaskItem>();
            }
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
            return tasks ?? new List<TaskItem>();
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Could not load: {filePath} : {ex.Message}");
            Console.ResetColor();
            return new List<TaskItem>();
        }
    }

    public void SaveTask(List<TaskItem> tasks)
    {
        try
        {
            string json = JsonSerializer.Serialize(tasks, JsonOptions);
            File.WriteAllText(filePath, json);
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Could not save: {filePath} : {ex.Message}");
            Console.ResetColor();
        }
    }
}