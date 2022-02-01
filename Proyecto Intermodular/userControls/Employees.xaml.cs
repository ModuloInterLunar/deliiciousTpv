using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        public Employees()
        {
            InitializeComponent();
        }

        private bool checkInputs()
        {
            for(int i = 0; i < inputsGrid.Children.Count; i++)
            {
                if (inputsGrid.Children[i] is TextBoxHint textBoxHint && isEmpty(textBoxHint.Text))
                    return false;
                else if (inputsGrid.Children[i] is PasswordInput password && isEmpty(password.Text))
                    return false;
            }

            return true;
        }

        private bool isEmpty(string str) => str == null || str == "";

        private async void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            /*string name = txtBoxName.Text;
            string surname = txtBoxSurname.Text;
            string dni = txtBoxDni.Text;
            string user = txtBoxUser.Text;
            string password = passwdInput.Text;
            string passwordConf = passwdInputConfirm.Text;
            string role = cbRole.Text;
            bool isAdmin = false;
            string confDni = @"^\d{8}[A-Z]|[XYZ]\d{7}[A-Z]$";


            if (name == "" || surname == "" || dni == "" || user == "" || password == "" || passwordConf == "")
            {
                MessageBox.Show("Error, no pueden haber campos vacíos");
                return;
            }*/

            bool isAdmin = false;
            string confDni = @"^\d{8}[A-Z]|[XYZ]\d{7}[A-Z]$"; 

            if(!checkInputs())
            {
                MessageBox.Show("Error, no pueden haber campos vacíos");
                return;
            }

            if (cbRole.Text == "Administrador") isAdmin = true;


            if (!Regex.IsMatch(txtBoxDni.Text, confDni))
            {
                MessageBox.Show("El DNI no es válido");
                return;
            }

            if (passwdInput.Text.Length < 5)
            {
                MessageBox.Show("La contraseña debe de tener al menos 5 caracteres");
                return;
            }

            if (passwdInput.Text != passwdInputConfirm.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden, vuelva a introducir la contraseña");
                return;
            }


            Employee employee = new() 
            { 
                Username = txtBoxName.Text,
                Dni = txtBoxDni.Text,
                Name = txtBoxName.Text,
                Surname = txtBoxSurname.Text,
                IsAdmin = isAdmin
            };

            Employee createdEmployee = await DeliiApi.CreateEmployee(employee);
            MessageBox.Show(createdEmployee.ToString());
        }
    }
}
