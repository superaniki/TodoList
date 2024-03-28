

public class Task
{
  public string Label { get; set; }
  public bool IsDone { get; set; }

  public Task(string label)
  {
    Label = label;
    IsDone = false;
  }

  public Task(string label, bool isDone)
  {
    Label = label;
    IsDone = isDone;
  }
}