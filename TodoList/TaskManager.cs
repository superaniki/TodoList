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

  public bool SaveTasks(string fileName)
  {
    try
    {
      StreamWriter writer = new(fileName);
      foreach (Task task in tasks)
      {
        writer.WriteLine(task.Label);
        writer.WriteLine(task.IsDone ? "1" : "0");
      }

      writer.Close();
    }
    catch
    {
      return false;
    }
    return true;
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

  public bool GetTask(int index, out Task? task)
  {
    try
    {
      task = tasks.ElementAt(index);
      return true;
    }
    catch
    {
      task = null;
      return false;
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

  public string? GetTaskNameAtIndex(int index)
  {
    try
    {
      return tasks[index].Label;
    }
    catch
    {
      return "";
    }
  }

  public bool UpdateTask(int index, string newName)
  {
    try
    {
      tasks[index].Label = newName;
    }
    catch
    {
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
      return false;
    }

    return true;
  }
}