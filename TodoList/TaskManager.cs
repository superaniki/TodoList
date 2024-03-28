public class TaskManager
{
  private List<Task> tasks = [];

  private bool LoadFile(string filename, List<string> outData)
  {
    try
    {
      using (StreamReader reader = new StreamReader(filename))
      {
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
          outData.Add(line);
        }
        return true;
      }
    }
    catch
    {
      return false;
    }
  }

  public void LoadTasks(string fileName)
  {
    List<string> loadedData = [];
    if (!LoadFile(fileName, loadedData))
      return;
    AddTasks(loadedData);
  }

  public void AddTask(string label)
  {
    Task newTask = new(label);
    tasks.Add(newTask);
  }

  public void SaveTasks(string fileName)
  {

    StreamWriter writer = new(fileName);
    foreach (Task task in tasks)
    {
      writer.WriteLine(task.Label);
      writer.WriteLine(task.IsDone ? "1" : "0");
    }

    writer.Close();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Tasks saved successfully to file \"{fileName}\".");
  }

  public void AddTasks(List<string> taskLines)
  {
    for (int index = 0; index + 1 <= taskLines.Count; index += 2)
    {
      string label = taskLines.ElementAt(index);
      bool isDone = taskLines.ElementAt(index + 1) == "1";
      Task newTask = new(label, isDone);
      tasks.Add(newTask);
    }
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