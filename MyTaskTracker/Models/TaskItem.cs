namespace MyTaskTracker.Models;

public class TaskItem
{
    public int Id {get; set;}
    public string description{get; set;} = string.Empty;
    public bool isComplete{get; set;}
    public DateTime createdAt{get; set;} = DateTime.Now;
}