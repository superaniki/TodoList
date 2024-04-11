namespace TodoList
{
    public abstract class Menu
    {
        public enum SortOrder
        {
            Group,
            DueDate
        }
        public TaskManager? TaskManager { set; get; } = null;

        public abstract void PrintMenu();

        public abstract Command InputCommand();

        protected static string PromptInput(string label = "")
        {
            string? input = null;
            while (String.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{(label != "" ? label + " " : "")}> ");
                input = Console.ReadLine();
            }

            return input;
        }

        protected static bool DateInput(string taskName, out DateTime dueDate)
        {
            while (true)
            {
                string? input = PromptInput(taskName);

                if (input != null)
                {
                    if (input.Trim() == "q")
                    {
                        dueDate = new();
                        return false;
                    }
                    else if (input.Trim() != "" && DateTime.TryParse(input, out DateTime date))
                    {
                        dueDate = date;
                        return true;
                    }
                }
            }
        }

        protected static bool StringInput(string taskName, out string data)
        {
            while (true)
            {
                string? input = PromptInput(taskName);

                if (input != null)
                {
                    if (input.Trim() == "q")
                    {
                        data = "";
                        return false;
                    }
                    else if (input.Trim() != "")
                    {
                        data = input;
                        return true;
                    }
                }
            }
        }

        protected static void WaitForAnyKey(string message = "Press any key", ConsoleColor color = ConsoleColor.Blue)
        {
            Console.ForegroundColor = color;
            Console.Write($"\n < {message} >");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        private void PrintRow(String[] data, int padding = 20, bool useCheckMark = false, bool checkMark = false, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.ForegroundColor = foregroundColor;
            foreach (string label in data)
            {
                Console.Write(label.PadRight(padding));
            }
            if (useCheckMark)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(checkMark ? "X" : " ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("]");
            }
            Console.Write("\n");
        }

        private void PrintHorizontalLine()
        {
            Console.WriteLine("------------------------------------------------------------------------------");
        }

        protected void PrintTaskList(bool useIndex = false, SortOrder sortBy = SortOrder.DueDate)
        {
            if (TaskManager == null)
            {
                throw new NullReferenceException("Taskmanager is not initialised");
            }

            var tasks = TaskManager.GetTaskList();
            Console.ForegroundColor = ConsoleColor.White;
            PrintRow(["Name", "Project", "Due Date", "Is Done"], 20);
            PrintHorizontalLine();

            int index = 1;
            foreach (Task task in tasks)
            {
                if (useIndex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(index + ") ");
                }

                PrintRow([task.Label, task.Project, task.DueDate.ToShortDateString()], 20, true, task.IsDone, ConsoleColor.Magenta);
                index++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            PrintHorizontalLine();
        }

        public static void PrintErrorMessage(string message, bool useKeyWait = true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*** " + message + " ***");
            if (useKeyWait)
                WaitForAnyKey();
        }

        public static void PrintSuccessMessage(string message, bool useKeyWait = true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** " + message + " ***");
            if (useKeyWait)
                WaitForAnyKey();
        }
    }
}

