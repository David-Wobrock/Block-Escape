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

namespace Block_Escape
{
    /// <summary>
    /// Logique d'interaction pour HowToPlayWindow.xaml
    /// </summary>
    public partial class HowToPlayWindow : Window
    {
        public HowToPlayWindow()
        {
            InitializeComponent();
        }

        private void Event_KeyPressed_Close(object sender, KeyEventArgs e)
        {
            Close();
        }
    }
}
