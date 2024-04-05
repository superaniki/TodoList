

namespace TodoList
{
  public class App
  {
    readonly TaskManager taskManager = new();
    public string? FileName { set; get; }
    readonly MainMenu mainMenu = new();
    readonly EditMenu editMenu = new();

    public App()
    {
      mainMenu.TaskManager = taskManager;
      editMenu.TaskManager = taskManager;
    }
    public void Start()
    {
      if (FileName != null)
        taskManager.LoadTasks(FileName);

      Menu currentMenu = mainMenu;
      Command command;

      do
      {
        currentMenu.PrintMenu();
        command = currentMenu.InputCommand();

        switch (command)
        {
          case Command.EditTasksMenu:
            currentMenu = editMenu;
            continue;
          case Command.MainMenu:
            currentMenu = mainMenu;
            continue;
        }
      } while (command != Command.SaveAndQuit);

      if (FileName != null)
        if (taskManager.SaveTasks(FileName))
        {
          Menu.PrintSuccessMessage($"Tasks saved successfully to file \"{FileName}\".", false);
        }
        else
        {
          Menu.PrintErrorMessage($"\nFailed to save file \"{FileName}\".\n", false);
        }
    }
  }
}
