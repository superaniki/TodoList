using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList
{
    public static class AnsiColors
    {
        public const string UnderLined = "\x1B[4m";
        public const string ResetAll = "\x1B[0m";
        public const string ResetDecorations = "\x1B[24m";
        public const string Green = "\x1B[32m";
        public const string DarkGrey = "\x1B[90m";
        public const string White = "\x1B[97m";
        public const string LightBlue = "\x1B[94m";
        public const string LightYellow = "\x1B[94m";
        // Add more constants here...
    }
}