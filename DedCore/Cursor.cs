using System;
using System.Linq;

namespace DedCore
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right,
    }

    public class Cursor
    {
        public int Line;
        public int Column;

        public Cursor(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public bool AtStart(Buffer buf)
        {
            return Line == 0 && Column == 0;
        }

        public bool AtEnd(Buffer buf)
        {
            return Line == buf.Lines.Count - 1 && Column == buf.Lines.Last().Data.Length;
        }

        public bool AtFirstLine(Buffer buf)
        {
            return Line == 0;
        }

        public bool AtLastLine(Buffer buf)
        {
            return Line == buf.Lines.Count - 1;
        }

        public bool AtFirstColumn(Buffer buf)
        {
            return Column == 0;
        }

        public bool AtLastColumn(Buffer buf)
        {
            return Column == buf.Lines[Line].Data.Length;
        }

        public Cursor Move(Buffer buf, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    if (AtFirstLine(buf))
                        return new Cursor(0, 0);
                    else
                        return new Cursor(Line - 1, Math.Min(Column, buf.Lines[Line - 1].Data.Length));
                case MoveDirection.Down:
                    if (AtLastLine(buf))
                        return new Cursor(Line, buf.Lines[Line].Data.Length);
                    else
                        return new Cursor(Line + 1, Math.Min(Column, buf.Lines[Line + 1].Data.Length));
                case MoveDirection.Left:
                    if (AtStart(buf))
                        return this;
                    else if (AtFirstColumn(buf))
                        return new Cursor(Line - 1, buf.Lines[Line - 1].Data.Length);
                    else
                        return new Cursor(Line, Column - 1);
                case MoveDirection.Right:
                    if (AtEnd(buf))
                        return this;
                    else if (AtLastColumn(buf))
                        return new Cursor(Line + 1, 0);
                    else
                        return new Cursor(Line, Column + 1);
            }

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Cursor other = obj as Cursor;
            if (other == null)
            {
                return false;
            }

            return Line == other.Line && Column == other.Column;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Line.GetHashCode();
            hash = hash * 23 + Column.GetHashCode();
            return hash;
        }
    }
}
