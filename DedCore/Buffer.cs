using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedCore
{
    public class Buffer
    {
        public LineEnding DefaultEnding = LineEnding.LF;
        public List<Line> Lines = new List<Line>();

        public Buffer()
        {
            Lines.Add(new Line("", LineEnding.LF));
        }

        public Buffer(string str)
        {
            int cur = 0;
            int beg = 0;
            int numLF = 0;
            int numCRLF = 0;

            while (cur < str.Length)
            {
                if (str[cur] == '\n')
                {
                    Lines.Add(new Line(str.Substring(beg, cur - beg), LineEnding.LF));
                    cur += 1;
                    beg = cur;
                    numLF++;
                }
                else if (str[cur] == '\r' && cur + 1 < str.Length && str[cur + 1] == '\n')
                {
                    Lines.Add(new Line(str.Substring(beg, cur - beg), LineEnding.CRLF));
                    cur += 2;
                    beg = cur;
                    numCRLF++;
                }
                else
                {
                    cur++;
                }
            }

            if (numCRLF > numLF)
                DefaultEnding = LineEnding.CRLF;

            if (Lines.Count == 0)
                Lines.Add(new Line(str.Substring(beg, cur - beg), DefaultEnding));
            else
                Lines.Add(new Line(str.Substring(beg, cur - beg), Lines.Last().Ending));
        }

        public void MergeLine(int line)
        {
            Lines[line - 1].Data += Lines[line].Data;
            Lines.RemoveAt(line);
        }

        public void SplitLine(Cursor where)
        {
            var line = Lines[where.Line];
            var rest = line.Data.Substring(where.Column);
            line.Data = line.Data.Substring(0, where.Column);
            Lines.Insert(where.Line + 1, new Line(rest, line.Ending));
        }

        public void InsertAt(Cursor where, string what)
        {
            var line = Lines[where.Line];
            line.Data = line.Data.Insert(where.Column, what);
        }

        public void RemoveAt(Cursor where, int count)
        {
            var line = Lines[where.Line];
            line.Data = line.Data.Remove(where.Column - 1, count);
        }

        override public string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Lines.Count; i++)
            {
                sb.Append(Lines[i].Data);

                if (i != Lines.Count - 1)
                {
                    switch (Lines[i].Ending)
                    {
                        case LineEnding.LF:
                            sb.Append('\n');
                            break;
                        case LineEnding.CRLF:
                            sb.Append("\r\n");
                            break;
                    }
                }
            }

            return sb.ToString();
        }
    }
}
