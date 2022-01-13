using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Proyecto_Intermodular.api;

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
            // Muestra u oculta los carácteres de la contraseña
            // Si la contraseña estaba oculta, asignamos el valor del PasswordBox al TextBox y ponemos la propiedad
            // visibility en visible para el TextBox y en collapsed para el PasswordBox (al revés si la contraseña estuviese mostrándose).
            // Además cambiamos el icono.
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

        // LOGIN
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeliiAPI.Login(txtBoxUserName.Text, showPassword ? txtBoxPasswordShow.Text : txtBoxPasswordHide.Password);
                lblError.Visibility = Visibility.Collapsed;
            }
            catch(InvalidCredentialsException ex)
            {
                lblError.Content = ex.Message;
                lblError.Visibility = Visibility.Visible;
            }
            catch(DeliiApiException ex)
            {
                MessageBox.Show($"Fatal Error.");
            }
        }

        // Para mostrar teclado de Windows
        private void btnKeyBoard_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
