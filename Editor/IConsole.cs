using System;

namespace Editor
{
    public interface IConsole
    {
        int CursorLeft { get; set; }
        int CursorTop { get; set; }

        ConsoleColor ForegroundColor { get; set; }
        ConsoleColor BackgroundColor { get; set; }
        void ResetColor();

        void Clear();
        void Write(char value);
        void Write(string value);

        ConsoleKeyInfo ReadKey(bool intercept);
    }

    public static class ConsoleExtensions
    {
        public static void Write(this IConsole console, string format, params object[] args)
        {
            console.Write(string.Format(format, args));
        }
    }
}
