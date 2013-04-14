namespace InfiniteIsland.Game
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (InfiniteIsland game = new InfiniteIsland())
                game.Run();
        }
    }
}