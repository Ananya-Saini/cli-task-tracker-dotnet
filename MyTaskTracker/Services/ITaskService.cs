namespace MyTaskTracker.Services;
using MyTaskTracker.Models;

public interface ITaskService
{
    bool AddTask(string description);
    bool MarkAsComplete(int taskId);
    bool DeleteTask(int taskId);
    List<TaskItem> GetFilteredTasks(TaskFilter filter);
}