using TodoList;

public class TaskManager
{
  private List<Task> tasks = [];

  private bool LoadFile(string filename, List<string> outData)
  {
    try
    {
      using (StreamReader reader = new(filename))
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
      Menu.PrintErrorMessage("Error reading from file : IO error");
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

  public void AddTask(string label, string projectName, DateTime dueDate)
  {
    Task newTask = new(label, projectName, dueDate);
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
        writer.WriteLine(task.Project);
        writer.WriteLine(task.DueDate.ToShortDateString());
      }

      writer.Close();
    }
    catch
    {
      return false;
    }
    return true;
  }

  public bool AddTasks(List<string> taskLines)
  {
    for (int index = 0; index + 1 <= taskLines.Count; index += 4)
    {
      try
      {
        string label = taskLines.ElementAt(index);
        bool isDone = taskLines.ElementAt(index + 1) == "1";
        string project = taskLines.ElementAt(index + 2);
        string dateString = taskLines.ElementAt(index + 3);
        if (!DateTime.TryParse(dateString, out DateTime dueDate))
        {
          throw new Exception();
        }
        tasks.Add(new(label, project, dueDate, isDone));
      }
      catch (Exception)
      {
        Menu.PrintErrorMessage("Error reading from file : wrong format");
        return false;
      }
    }
    return true;
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