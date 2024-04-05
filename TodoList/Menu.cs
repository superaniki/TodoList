namespace TodoList
{
    public abstract class Menu
    {
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

        protected static void WaitForAnyKey(string message = "Press any key", ConsoleColor color = ConsoleColor.Blue)
        {
            Console.ForegroundColor = color;
            Console.Write($"\n < {message} >");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        protected void PrintTaskList(bool useIndex = false)
        {
            if (TaskManager == null)
            {
                throw new NullReferenceException("Taskmanager is not initialised");
            }

            var tasks = TaskManager.GetTaskList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Name".PadRight(20) + "Is done");
            Console.WriteLine("------------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;

            int index = 1;
            foreach (Task task in tasks)
            {
                if (useIndex)
                    Console.Write(index + ") ");
                Console.Write(task.Label.PadRight(20));
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(task.IsDone ? "X" : " ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("]");

                index++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------");
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

