namespace InfiniteIsland.Game
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var game = new InfiniteIsland())
            {
                game.Window.Title = "Infinite Island";
                game.Run();
            }
        }
    }
}