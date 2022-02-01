using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            /*
            if(txtBox.Text == Hint) txtBox.Text = "";
            txtBox.Foreground = Brushes.Black;
            */
        }

        private void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            /*
            if (txtBox.Text is null or "")
            {
                txtBox.Text = Hint;
                txtBox.Foreground = (Brush) new BrushConverter().ConvertFrom("#A5A5A5");
            }
            */
        }

        public string Text { set => txtBox.Text = value; get => txtBox.Text; }
        public string Hint { set; get; }
    }
}
