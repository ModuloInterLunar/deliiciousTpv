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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para TextBoxHint.xaml
    /// </summary>
    public partial class TextBoxHint : UserControl
    {
        public TextBoxHint()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtBox.Text == Hint) txtBox.Text = "";
            txtBox.Foreground = Brushes.Black;
        }

        private void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(txtBox.Text);
            if (txtBox.Text == null || txtBox.Text == "")
            {
                txtBox.Text = Hint;
                txtBox.Foreground = (Brush) new BrushConverter().ConvertFrom("#A5A5A5");
            }
        }

        public string Text { get => txtBox.Text; }
        public string Hint { set; get; }
    }
}
