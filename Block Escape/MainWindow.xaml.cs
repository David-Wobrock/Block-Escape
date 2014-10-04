using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Block_Escape
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor : build the window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Envent : Play button : launches the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event_Play(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            Visibility = Visibility.Hidden;
            gameWindow.Show();
            gameWindow.StartGame();

            new Thread(new ParameterizedThreadStart(IsGameWindowOpened)).Start(gameWindow);
        }

        private void IsGameWindowOpened(object gameW)
        {
            // Cast the gameW object into a GameWindow (the method needs to look like delegate)
            GameWindow gameWindow = gameW as GameWindow;

            // While the window is opened, cannot do anything
            while (gameWindow.IsVisible) { }

            // Ask main thread to set the window to visible
            Application.Current.Dispatcher.Invoke(SetToVisible);
        }

        /// <summary>
        /// Set the window visible
        /// </summary>
        private void SetToVisible()
        {
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Event : Quit button : close application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event_CloseApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
