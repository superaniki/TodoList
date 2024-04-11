

namespace TodoList
{
    public class EditMenu : Menu
    {
        public override void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Edit tasks (q to quit) \n-------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("(1) Update");
            Console.WriteLine("(2) Mark as Done");
            Console.WriteLine("(3) Remove");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-------------");
        }

        public override Command InputCommand()
        {
            var input = PromptInput().ToLower();
            switch (input)
            {
                case "1":
                    EditTask("Choose task to Update", Command.Update);
                    break;
                case "2":
                    EditTask("Choose task to toggle as Done", Command.ToggleDone);
                    break;
                case "3":
                    EditTask("Choose task to Remove", Command.Remove);
                    break;
                case "q":
                    return Command.MainMenu;
            }

            return Command.None;
        }

        private void RenameTask(int index)
        {
            if (TaskManager == null)
            {
                throw new NullReferenceException("Taskmanager is not initialised");
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Update task \"{TaskManager.GetTaskList().ElementAt(index - 1).Label}\" (enter to skip)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------");

            Console.Write($"Name > ");
            var newName = Console.ReadLine();
            Console.Write($"Project > ");
            var newProject = Console.ReadLine();
            DateTime? newDueDate = null;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Due Date >");
                var newDueDateString = Console.ReadLine();
                if (DateTime.TryParse(newDueDateString, out var parsedDueDate))
                {
                    newDueDate = parsedDueDate;
                    break;
                }
                else if (newDueDateString == "")
                    break;
                else
                {
                    PrintErrorMessage("Error : Wrong format", false);
                }
            }

            if (newName != "" || newProject != "" || newDueDate != null)
            {
                bool updateSuccess = TaskManager.UpdateTask(index - 1, newName, newProject, newDueDate);
                if (!updateSuccess)
                {
                    PrintErrorMessage("Failed to update task");
                }
                else
                {

                    PrintSuccessMessage("Task updated");
                }
            }
            else
            {
                PrintMessage("Skipping task update");
            }

        }

        private void EditTask(string taskDescription, Command command)
        {
            if (TaskManager == null)
            {
                throw new NullReferenceException("Taskmanager is not initialised");
            }
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                PrintIndexedTaskList();
                Console.WriteLine(taskDescription + " - (q to quit)");

                var input = PromptInput().Trim().ToLower();
                if (input == "q")
                    return;
                if (int.TryParse(input, out int index))
                {
                    if (TaskManager.GetTask(index - 1, out Task? task))
                    {
                        switch (command)
                        {
                            case Command.Update:
                                RenameTask(index);
                                break;
                            case Command.Remove:
                                var taskLabel = task?.Label;
                                TaskManager.RemoveTask(index - 1);
                                PrintSuccessMessage($"Task '{taskLabel}' removed!");
                                break;
                            case Command.ToggleDone:
                                TaskManager.ToggleTaskIsDone(index - 1);
                                break;
                        }
                    }
                    else
                    {
                        PrintErrorMessage("Task does not exist");
                    }

                }
                else
                {
                    PrintErrorMessage("Not a number");
                }
            }
        }
        protected void PrintIndexedTaskList()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            PrintTaskList(true);
        }


    }


}