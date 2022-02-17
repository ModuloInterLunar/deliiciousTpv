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

        private bool CheckInputs()
        {
            for(int i = 0; i < inputsGrid.Children.Count; i++)
            {
                if (inputsGrid.Children[i] is TextBoxHint textBoxHint && IsEmpty(textBoxHint.Text))
                    return false;
                else if (inputsGrid.Children[i] is PasswordInput password && IsEmpty(password.Text))
                    return false;
            }

            return true;
        }

        private bool IsEmpty(string str) => str is null or "";

        private async void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool isAdmin = false;
            string confDni = @"^\d{8}[A-Z]|[XYZ]\d{7}[A-Z]$"; 

            if(!CheckInputs())
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
                Username = txtBoxUser.Text,
                Dni = txtBoxDni.Text,
                Name = txtBoxName.Text,
                Surname = txtBoxSurname.Text,
                Password = passwdInput.Text,
                IsAdmin = isAdmin
            };

            Employee createdEmployee = await DeliiApi.CreateEmployee(employee);
            MessageBox.Show($"Se ha creado correctamente el empleado '{employee.Username}'");
        }
    }
}
