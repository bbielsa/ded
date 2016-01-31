namespace Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new Workspace(new DotNetConsole());
            ws.Run();
        }
    }
}
