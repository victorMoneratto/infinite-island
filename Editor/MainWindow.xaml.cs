using System.Windows;
using DasGame;

namespace Editor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            HueGame.World.Gravity = -HueGame.World.Gravity;
        }
    }
}