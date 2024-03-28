
public class TodoList
{
  TaskManager taskManager = new();

  private void PrintMenu()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Welcome to Todoly");
    Console.WriteLine($"You have {taskManager.NumberOfTasks()} tasks and {taskManager.NumberOfDoneTasks()} tasks are done");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("--------------------------------------");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("(1) Show task list (by date and project)");
    Console.WriteLine("(2) Add new task");
    Console.WriteLine("(3) Edit Task (update, mark as done, remove)");
    Console.WriteLine("(4) Save and quit");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("-------------------------------------");
  }

  private string PromptInput()
  {
    string? input = null;
    while (String.IsNullOrEmpty(input))
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("> ");
      input = Console.ReadLine();
    }

    return input;
  }

  private void PrintTaskList(bool useIndex = false)
  {
    var tasks = taskManager.GetTaskList();
    Console.WriteLine("Name".PadRight(20) + "Is done");
    Console.WriteLine("------------------------------------");

    int index = 1;
    foreach (Task task in tasks)
    {
      if (useIndex)
        Console.Write(index + ") ");
      Console.WriteLine(task.Label.PadRight(20) + (task.IsDone ? "[X]" : "[ ]"));
      index++;
    }
    Console.WriteLine("------------------------------------");

  }

  private void PrintTaskListForDisplay()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Magenta;
    PrintTaskList();
    Console.ReadLine();
  }

  private void PrintIndexedTaskList()
  {
    Console.ForegroundColor = ConsoleColor.Magenta;
    PrintTaskList(true);
  }

  private void AddTasks()
  {
    var addAnotherTask = true;
    while (addAnotherTask)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("Add new task (quit with Q)\n-------------");

      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("name > ");
      string? taskName = Console.ReadLine();

      if (taskName != null)
      {
        if (taskName.Trim() == "q")
        {
          addAnotherTask = false;
        }
        else if (taskName.Trim() != "")
        {
          taskManager.AddTask(taskName);
        }
      }
    }
  }

  private void UpdateTasks()
  {
    Console.Clear();
    Console.WriteLine("Choose task to Update\n-------------");
    PrintIndexedTaskList();
    Console.WriteLine("-------------");
    Console.Write("\n> ");
    string? taskIndexString = Console.ReadLine();
    int index;
    if (taskIndexString != null)
    {
      bool success = int.TryParse(taskIndexString, out index);
      if (success && index <= taskManager.NumberOfTasks() && index != 0)
      {
        Console.Clear();
        Console.WriteLine($"Enter a new name for \"{taskManager.GetTaskList().ElementAt(index - 1).Label}\"");
        Console.WriteLine("-------------");
        Console.Write("\n> ");
        string? newName = Console.ReadLine();
        if (newName != null)
        {
          bool updateSuccess = taskManager.UpdateTask(index - 1, newName);
          if (!updateSuccess)
          {
            Console.WriteLine($"Falied to update task..");
            Console.ReadLine();
          }
          else
          {
            Console.WriteLine("Task updated with new name");
            Console.ReadLine();
          }
        }
      }
      else
      {
        Console.WriteLine($"Not a number or number out of bounds");
      }
    }
  }

  private void MarkTasksAsDone()
  {
    while (true)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Magenta;
      PrintIndexedTaskList();
      Console.WriteLine("Choose task to toggle Done/Not Done.(q to quit)\n");

      var input = PromptInput().Trim().ToLower();
      if (input == "q")
        return;
      var success = int.TryParse(input, out int number);
      if (success)
      {
        taskManager.ToggleTaskIsDone(number - 1);
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("*** Task does not exist ***");
        Console.ResetColor();
        Console.ReadKey();
      }
    }
  }

  private void RemoveTasks()
  {
    while (true)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Magenta;
      PrintIndexedTaskList();
      Console.WriteLine("Choose task to Remove.(q to quit)\n");

      var input = PromptInput().Trim().ToLower();
      if (input == "q")
        return;
      var success = int.TryParse(input, out int number);
      if (success)
      {
        taskManager.RemoveTask(number - 1);
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("*** Task does not exist ***");
        Console.ResetColor();
        Console.ReadKey();
      }
    }
  }


  private void EditTasks()
  {
    while (true)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("Edit task \n-------------");
      Console.WriteLine("(1) Update");
      Console.WriteLine("(2) Mark as Done");
      Console.WriteLine("(3) Remove");
      Console.WriteLine("(4) Quit");
      Console.WriteLine("-------------");
      Console.Write("\n> ");
      string? taskCode = Console.ReadLine();

      if (taskCode == "1")
      {
        UpdateTasks();
      }
      if (taskCode == "2")
      {
        MarkTasksAsDone();
      }
      if (taskCode == "3")
      {
        RemoveTasks();
      }
      if (taskCode == "4")
      {
        break;
      }
    }
  }

  private void ExecuteCommand(Command command)
  {
    switch (command)
    {
      case Command.PrintTasks:
        PrintTaskListForDisplay();
        break;
      case Command.AddTasks:
        AddTasks();
        break;
      case Command.EditTasks:
        EditTasks();
        break;
    }
  }

  private enum Command
  {
    PrintTasks, AddTasks, EditTasks, SaveAndQuit, None
  }

  private Command InputCommand()
  {
    var input = PromptInput();
    switch (input)
    {
      case "1":
        return Command.PrintTasks;
      case "2":
        return Command.AddTasks;
      case "3":
        return Command.EditTasks;
      case "4":
        return Command.SaveAndQuit;
    }

    return Command.None;
  }

  public void Start()
  {
    Command command = Command.None;
    do
    {
      PrintMenu();
      command = InputCommand();
      ExecuteCommand(command);
    } while (command != Command.SaveAndQuit);
  }
}