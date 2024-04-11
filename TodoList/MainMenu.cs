
using System.Text.Json.Nodes;

namespace TodoList
{
    public class MainMenu : Menu
    {
        public override void PrintMenu()
        {
            if (TaskManager == null)
            {
                throw new NullReferenceException("Taskmanager is not initialised");
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Todoly");
            Console.WriteLine($"You have {TaskManager.NumberOfTasks()} tasks and {TaskManager.NumberOfDoneTasks()} tasks are done");
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

        public override Command InputCommand()
        {
            var input = PromptInput();
            switch (input)
            {
                case "1":
                    PrintTaskListForDisplay();
                    //WaitForAnyKey("Press any key to close");
                    break;
                case "2":
                    AddTasks();
                    break;
                case "3":
                    return Command.EditTasksMenu;
                case "4":
                    return Command.SaveAndQuit;
            }

            return Command.None;
        }


        private void PrintTaskListForDisplay()
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                PrintTaskList();
            } while (SortOrderInput());
        }


        string UnderlinedString(string inputString)
        {
            return $"{AnsiColors.UnderLined}{inputString}{AnsiColors.ResetDecorations}";
        }

        private bool SortOrderInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            var projectString = TaskSortOrder == SortOrder.Project ? UnderlinedString("Project") : "Project";
            var dueDateString = TaskSortOrder == SortOrder.DueDate ? UnderlinedString("Due Date") : "Due Date";
            Console.CursorVisible = false;
            Console.Write($"Press {AnsiColors.LightBlue}(p){AnsiColors.DarkGrey} to sort by {projectString}, ");
            Console.Write($"{AnsiColors.LightBlue}(d){AnsiColors.DarkGrey} to sort by {dueDateString},");
            Console.Write($"{AnsiColors.LightBlue}(enter){AnsiColors.DarkGrey} to quit >");

            var key = Console.ReadKey().Key;
            //var keyString = key.Key;
            Console.CursorVisible = true;


            switch (key)
            {
                case ConsoleKey.P:
                    TaskSortOrder = SortOrder.Project;
                    break;
                case ConsoleKey.D:
                    TaskSortOrder = SortOrder.DueDate;
                    break;
                case ConsoleKey.Enter:
                    return false;

            };
            Console.CursorVisible = true;
            return true;
        }

        private void AddTasks()
        {
            if (TaskManager == null)
            {
                throw new NullReferenceException("Taskmanager is not initialised");
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Add new task (q to exit)\n--------------------------\n");

                Console.ForegroundColor = ConsoleColor.White;
                if (!StringInput("Name", out string taskName))
                    break;
                if (!StringInput("Project", out string projectName))
                    break;
                if (!DateInput("DueDate", out DateTime dueDate))
                    break;

                TaskManager.AddTask(taskName, projectName, dueDate);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Task '{taskName}' Added!");
                WaitForAnyKey("Press any key to enter a new task");

            }
        }
    }

}
