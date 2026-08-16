namespace MyTaskTracker.Data;
using MyTaskTracker.Models;

public interface ITaskRepository
{
    List<TaskItem> LoadTask();
    void SaveTask(List<TaskItem> tasks);
}