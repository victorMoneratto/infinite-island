using System.Windows;
using DasGame;

namespace Editor
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
            using (var game = new HueGame())
            {
                game.Run();
            }

            Shutdown(0);
        }
    }
}