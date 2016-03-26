namespace DedCore
{
    public enum LineEnding
    {
        LF,
        CRLF,
    }

    public class Line
    {
        public string Data;
        public LineEnding Ending;

        public Line(string data, LineEnding ending)
        {
            Data = data;
            Ending = ending;
        }
    }
}
