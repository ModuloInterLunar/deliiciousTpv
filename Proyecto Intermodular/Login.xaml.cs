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

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        bool showPassword = false;

        public Login()
        {
            InitializeComponent();
        }

        private void btnShowHidePassword_Click(object sender, RoutedEventArgs e)
        {
            if (showPassword)
            {
                txtBoxPasswordHide.Password = txtBoxPasswordShow.Text;
                txtBoxPasswordHide.Visibility = Visibility.Visible;
                txtBoxPasswordShow.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtBoxPasswordShow.Text = txtBoxPasswordHide.Password;
                txtBoxPasswordHide.Visibility = Visibility.Collapsed;
                txtBoxPasswordShow.Visibility = Visibility.Visible;
            }

            imageShowHidePassword.Source = new BitmapImage(new Uri(showPassword ? "./password_hide.png" : "./password_show.png", UriKind.Relative));
            showPassword = !showPassword;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // LOGIN
        }
    }
}
