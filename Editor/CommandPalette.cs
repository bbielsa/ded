using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Editor
{
    public class CommandArgs : EventArgs
    {
        public string Command;

        public CommandArgs(string command)
        {
            Command = command;
        }
    }

    public class CommandPalette : ITextInput
    {
        public event EventHandler<CommandArgs> CommandRecieved;

        public int X;
        public int Y;
        public int Width;
        public int Height;
        public bool Focused { get; private set; }

        private Cursor cursor;
        private Buffer buffer;

        public CommandPalette(int x, int y, int w)
        {
            Focused = true;

            X = x;
            Y = y;
            Width = w;
            Height = 2;

            buffer = new Buffer();
            cursor = new Cursor(0, 0);
        }

        public void Clear()
        {
            buffer = new Buffer();
            cursor = new Cursor(0, 0);
        }

        public void Render()
        {
            if (!Focused)
                return;

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            // Draw box

            for(int y = Y; y < Y + Height - 1; y++)
            {
                Console.SetCursorPosition(X, y);
                Console.Write(" ");
                Console.SetCursorPosition(X + Width - 1, y);
                Console.Write(" ");
            }

            Console.SetCursorPosition(X, Y + Height - 1);
            Console.Write(new string(' ', Width));

            // Before rohan yells at me because he thought I forgot to change the foreground color:
            // it was a design choice, retard

            Console.BackgroundColor = ConsoleColor.Black;

            // Clear text
            Console.SetCursorPosition(X + 1, Y + Height - 2);
            Console.Write(new string(' ', Width - 2));

            Console.SetCursorPosition(X + 1, Y);
            Console.Write(buffer);
        }

        public void ActivateCursor()
        {
            //Debug.Print("ActivateCursor()");
            Console.SetCursorPosition(X + 1 + cursor.Column, Y);
        }

        public void Hide()
        {
            Focused = false;
        }

        public void Show()
        {
            Focused = true;
            Render();
        }

        public bool HandleInput(EditorInput input)
        {
            switch (input)
            {
                case EditorInput.Backspace:
                    if (cursor.Column == 0)
                        return true;
                    buffer.RemoveAt(cursor, 1);
                    cursor.Column--;
                    return true;
                case EditorInput.DownArrow:
                case EditorInput.End:
                    cursor.Column = buffer.Lines[0].Data.Length;
                    return false;
                // IMO, the cursor should go to the first column, not the first non-whitespace 
                case EditorInput.UpArrow:
                case EditorInput.Home:
                    cursor.Column = 0;
                    return true;
                case EditorInput.LeftArrow:
                    cursor.Column = Math.Max(0, cursor.Column - 1);
                    return false;
                case EditorInput.RightArrow:
                    cursor.Column = Math.Min(Width - 2, cursor.Column + 1);
                    return false;
                case EditorInput.Enter:
                    {
                        if (CommandRecieved != null)
                            CommandRecieved(this, new CommandArgs(buffer.Lines[0].Data));
                   
                        Hide();
                        Clear();
                        return true;
                    }
                default:
                    return false;
            }
        }

        public bool HandleInput(string input)
        {
            if (buffer.Lines[0].Data.Length + input.Length > Width - 2)
                return false;

            buffer.InsertAt(cursor, input);
            cursor.Column += input.Length;
            
            return true;
        }

        public bool AdjustWindow()
        {
            return false;
        }
    }
}
