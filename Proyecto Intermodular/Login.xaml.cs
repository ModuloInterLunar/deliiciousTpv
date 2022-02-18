using System;
using System.Net.Http;
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
            catch (HttpRequestException)
            {
                MessageBox.Show("Fallo de conexión.\nPor favor, compruebe la conexión con la API!", "Fallo de Conexión");
            }
            catch (ItemNotFoundException)
            {
                string errorMessage = "El nombre de usuario es inválido.";
                borderUserName.Background = new SolidColorBrush(Colors.Red);
                txtBoxUserName.ToolTip = errorMessage;
                MessageBox.Show(errorMessage);
            }
            catch (WrongCredentialsException)
            {
                string errorMessage = "Las credenciales introducidas son inválidas";
                borderUserName.Background = new SolidColorBrush(Colors.Red);
                passwdInput.border.Background = new SolidColorBrush(Colors.Red);
                txtBoxUserName.ToolTip = errorMessage;
                MessageBox.Show(errorMessage);
            }
            catch(DeliiApiException ex)
            {
                MessageBox.Show($"Api error\nError message: {ex.Message}");
            }
        }

        // Para mostrar teclado de Windows
        private void btnKeyBoard_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
