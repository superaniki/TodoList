

public class Task
{
  public string Label { get; set; }
  public bool IsDone { get; set; }

  public Task(string label)
  {
    Label = label;
    IsDone = false;
  }
}