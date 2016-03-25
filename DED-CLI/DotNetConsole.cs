using System;

namespace DedCLI
{
    public class DotNetConsole : IConsole
    {
        public int CursorLeft
        {
            get { return Console.CursorLeft; }
            set { Console.CursorLeft = value; }
        }

        public int CursorTop
        {
            get { return Console.CursorTop; }
            set { Console.CursorTop = value; }
        }

        public ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public ConsoleColor BackgroundColor
        {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Write(char value)
        {
            Console.Write(value);
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public void Dispose()
        {
        }

        public void SetCursorPosition(int x, int y)
        {
            CursorLeft = x;
            CursorTop = y;
        }
    }
}
