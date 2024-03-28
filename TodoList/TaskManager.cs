public class TaskManager
{
  private List<Task> tasks = [];

  public void AddTask(string label)
  {
    Task newTask = new(label);
    tasks.Add(newTask);
  }

  public bool RemoveTask(int index)
  {
    try
    {
      tasks.RemoveAt(index);
    }
    catch
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("*** Task does not exist ***");
      Console.ResetColor();
      Console.ReadKey();
      return false;
    }
    return true;
  }

  public int NumberOfTasks()
  {
    return tasks.Count;
  }

  public int NumberOfDoneTasks()
  {
    return tasks.Where(task => task.IsDone == true).Count();
  }

  public IEnumerable<Task> GetTaskList()
  {
    return tasks.AsReadOnly();
  }

  public bool UpdateTask(int index, string newName)
  {
    try
    {
      tasks[index].Label = newName;
    }
    catch
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("*** Task does not exist ***");
      Console.ReadKey();
      return false;
    }

    return true;
  }

  public bool ToggleTaskIsDone(int index)
  {
    try
    {
      tasks[index].IsDone = !tasks[index].IsDone;
    }
    catch
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("*** Task does not exist ***");
      Console.ResetColor();
      Console.ReadKey();
      return false;
    }

    return true;
  }
}