using System;
using System.Threading.Tasks;
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

        // LOGIN
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtBoxUserName.Text;
                string password = passwdInput.Text;

                string token = await DeliiApi.Login(username, password);
                ApplicationState.SetValue("token", token);
                Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        MainWindow mainWindow = new();
                        mainWindow.Show();
                        Close();
                    }
                );
            }
            catch (UserNotFoundException ex)
            {
                borderUserName.Background = new SolidColorBrush(Colors.Red);
                txtBoxUserName.ToolTip = ex.Message;
            }
            catch(DeliiApiException ex)
            {
                MessageBox.Show($"Api error\nError message: {ex}");
            }
        }

        // Para mostrar teclado de Windows
        private void btnKeyBoard_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
