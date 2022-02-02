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

        public string Text { set => txtBox.Text = value; get => txtBox.Text; }
        public string Hint { set; get; }
    }
}
