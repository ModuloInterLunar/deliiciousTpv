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
    /// Lógica de interacción para PasswordInput.xaml
    /// </summary>
    public partial class PasswordInput : UserControl
    {
        bool showPassword = false;
        public PasswordInput()
        {
            InitializeComponent();
        }

        private void btnShowHidePassword_Click(object sender, RoutedEventArgs e)
        {
            if (showPassword)
            {
                txtInputHide.Password = txtInputShow.Text;
                txtInputHide.Visibility = Visibility.Visible;
                txtInputShow.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtInputShow.Text = txtInputHide.Password;
                txtInputHide.Visibility = Visibility.Collapsed;
                txtInputShow.Visibility = Visibility.Visible;
            }

           
            imageShowHidePassword.Source = new BitmapImage(new Uri(showPassword ? "/Proyecto Intermodular;component/images/password_hide.png" : "/Proyecto Intermodular;component/images/password_show.png", UriKind.Relative));
            showPassword = !showPassword;
        }

        public string Text { set => txtInputHide.Password = value; get => showPassword ? txtInputShow.Text : txtInputHide.Password; }
    }
}
