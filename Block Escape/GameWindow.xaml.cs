using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using GameCore;
using System.Windows.Threading;
using System.Threading;

namespace Block_Escape
{
    /// <summary>
    /// Logique d'interaction pour GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private static Boolean FirstPlaying = true;

        private Game game = null;

        private Thread GameLoopThread = null;

        /// <summary>
        /// GameWindow constructor, initializing everything
        /// </summary>
        public GameWindow()
        {
            // The window and its components
            InitializeComponent();

            // The game
            InitializeGame();

            // Show how to play
            if (FirstPlaying)
            {
                ShowHowToPlayWindow();
                FirstPlaying = false;
            }
        }

        /// <summary>
        /// Shows the How To Play Window (with play instructions)
        /// </summary>
        private void ShowHowToPlayWindow()
        {
            HowToPlayWindow HowToPlayWindow = new HowToPlayWindow();
            HowToPlayWindow.ShowDialog();
        }

        /// <summary>
        /// Initialize the game
        /// </summary>
        private void InitializeGame()
        {
            game = new Game();
        }

        /// <summary>
        /// Updates the UI
        /// </summary>
        public void updateUI()
        {
            // Clear the canvas
            mCanvasPrincipal.Children.Clear();
            mCanvasPrincipal.Background = Brushes.White;

            // Display my objects
            foreach (UIObject o in game.getDisplayedObjects())
            {
                Image im = new Image();
                im.Source = new BitmapImage(o.getImageUri());

                Canvas.SetLeft(im, o.X);
                Canvas.SetTop(im, o.Y);
                mCanvasPrincipal.Children.Add(im);
            }

            // Add the score displays
            DisplayScores();

            // Refresh the canvas to be sure it displays
            mCanvasPrincipal.Refresh();
            mCanvasPrincipal.UpdateLayout();
        }

        /// <summary>
        /// Display the scores on the UI
        /// </summary>
        private void DisplayScores()
        {
            // Score 
            TextBlock textBlockScore = new TextBlock();
            textBlockScore.Text = String.Format("Score : {0}", game.Score);
            textBlockScore.Style = Application.Current.FindResource("TextBlockStyle") as Style;
            Canvas.SetTop(textBlockScore, 20);
            Canvas.SetLeft(textBlockScore, 20);
            mCanvasPrincipal.Children.Add(textBlockScore);

            // Highscore
            TextBlock textBlockHighscore = new TextBlock();
            textBlockHighscore.Text = String.Format("Highscore : {0}", game.Highscore);
            textBlockHighscore.Style = Application.Current.FindResource("TextBlockStyle") as Style;
            Canvas.SetTop(textBlockHighscore, 40);
            Canvas.SetLeft(textBlockHighscore, 20);
            mCanvasPrincipal.Children.Add(textBlockHighscore);
        }

        /// <summary>
        /// Start the main game loop
        /// </summary>
        public void StartGame()
        {
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();
        }

        /// <summary>
        /// The game loop
        /// </summary>
        private void GameLoop()
        {
            while (true)
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(VerifyPressedKeys);
                    game.Turn(a);

                    Application.Current.Dispatcher.Invoke(updateUI);

                    if (game.VerifyEndOfGame())
                    {
                        EndOfGame();
                        break;
                    }

                    Thread.Sleep(20);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// The list of actions that will apply next
        /// </summary>
        private List<GameCore.Action> a = null;

        /// <summary>
        /// Checks the pressed keys
        /// </summary>
        private void VerifyPressedKeys()
        {
            a = new List<GameCore.Action>();

            if (Keyboard.IsKeyDown(Key.Up))
                a.Add(GameCore.Action.Jump);

            if (Keyboard.IsKeyDown(Key.Right))
                a.Add(GameCore.Action.Right);
            else if (Keyboard.IsKeyDown(Key.Left))
                a.Add(GameCore.Action.Left);

            if (a.Count == 0)
                a.Add(GameCore.Action.None);
        }

        /// <summary>
        /// Handles the end of the game
        /// </summary>
        private void EndOfGame()
        {
            String message = String.Format("End of game.\nThanks for playing.\nYour score : {0}", game.Score);
            if (game.Score >= game.Highscore)
                message += "\nThat's a new highscore !";

            MessageBox.Show(message, "End of game", MessageBoxButton.OK);
            Application.Current.Dispatcher.Invoke(Close);
        }

        /// <summary>
        /// Event : when the application is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (GameLoopThread.IsAlive || GameLoopThread.ThreadState == ThreadState.Running)
                GameLoopThread.Abort();

            if (game.Score >= game.Highscore)
                HighscoreFileHandler.getInstance().SaveHighscore(game.Score);
        }
    }
}
