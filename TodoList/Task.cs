

public class Task
{
  public string Label { get; set; }
  public bool IsDone { get; set; }

  public string Project { get; set; }

  public DateTime DueDate { get; set; }

  public Task(string label, string project, DateTime dueDate)
  {
    Label = label;
    Project = project;
    DueDate = dueDate;
    IsDone = false;
  }
  public Task(string label, string project, DateTime dueDate, bool isDone) : this(label, project, dueDate)
  {
    IsDone = isDone;
  }


}