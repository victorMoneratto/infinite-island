namespace InfiniteIsland
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            bool fullscreen = args.Length > 0 && args[0] == "fullscreen";
            using (var game = new InfiniteIsland(fullscreen))
            {
                game.Window.Title = "Infinite Island";
                game.Run();
            }
        }
    }
}