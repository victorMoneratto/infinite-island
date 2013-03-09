namespace DasGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (HueGame game = new HueGame())
                game.Run();
        }
    }
}