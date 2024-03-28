public class TaskManager
{
  private List<Task> tasks = [];

  public void AddTask(string label)
  {
    Task newTask = new(label);
    tasks.Add(newTask);
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
      return false;
    }

    return true;
  }
}