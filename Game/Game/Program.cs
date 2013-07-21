namespace InfiniteIsland
{
    internal static class Program
    {
        private static void Main()
        {
            using (var game = new InfiniteIsland())
            {
                game.Window.Title = "Infinite Island";
                game.Run();
            }
        }
    }
}