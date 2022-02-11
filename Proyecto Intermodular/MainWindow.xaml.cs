using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<Order> orders;
        int timerStage;

        List<Uri> timerImagesUris = new()
        {
            new Uri("/Proyecto Intermodular;component/images/timer/timer_3.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_4.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_5.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_6.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_7.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_2.png", UriKind.Relative)
        };
        List<ImageSource> timerImages = new();

        Employee currentUser;

        public MainWindow()
        {
            InitializeComponent();
            StartTimer();
        }

        //private async void showDishes()
        //{
        //    List<Dish> dishes = await DeliiApi.GetAllDishes();

        //    MessageBox.Show(dishes[0].ToString());
        //    //MessageBox.Show((dishes[0] as Food).ToString());
        //}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ucTables.UpdateTablesPosition();
        }

        public void StartTimer()
        {
            LoadTimerImages();
            timerStage = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void LoadTimerImages()
        {
            timerImagesUris.ForEach(uri => timerImages.Add(new BitmapImage(uri)));
        }
        private void TimerTick(object sender, EventArgs e)
        {
            imgTimer.Source = timerImages[timerStage];
            if (timerStage == 5)
            {
                UpdateDataset();
                timerStage = 0;
            }
            else
                timerStage++;
        }


        public void UpdateDataset()
        {
            if (currentUser == null)
                GetCurrentUser();
            else
                UpdateUI();

            ucTables.UpdateCanvasTables();
            ucIngredients.UpdateStackIngredients();
            ucDishes.UpdateLayout();
            ucKitchen.UpdateKitchen();
            //GenerateOrders();
        }


        private async void GetCurrentUser()
        {
            try
            {
                currentUser = await DeliiApi.GetEmployeeFromToken();
                ApplicationState.SetValue("current_user", currentUser);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ucTables.CurrentUser = currentUser;
                    UpdateUI();
                });
            }
            catch (DeliiApiException ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void UpdateUI()
        {
            currentUser = ApplicationState.GetValue<Employee>("current_user");
            Title = (currentUser == null)
                    ? "Empleado: default user"
                    : "Empleado: " + currentUser.FullName;
        }
        

        #region Cocina

        /*private async void GenerateOrders()
        {
            List<Order> updatedOrders = await DeliiApi.GetAllOrders();
            updatedOrders = updatedOrders.FindAll(order => !order.HasBeenServed);
            if (orders == null)
            {
                orders = updatedOrders;
                orders.ForEach(order => CreateOrder(order));
                return;
            }

            updatedOrders.ForEach(updatedOrder =>
            {
                Order order = orders.Find(order => order.Id == updatedOrder.Id);
                if (order != null)
                { 
                    order.UpdateData(updatedOrder);
                }
                else
                {
                    orders.Add(updatedOrder);
                    CreateOrder(updatedOrder);
                }
            });

            RemoveOldOrders(updatedOrders);
        }

        private void RemoveOldOrders(List<Order> updatedOrders)
        {
            orders = orders.FindAll(order => {
                Order updatedOrder = updatedOrders.Find(updatedOrder => order.Id == updatedOrder.Id);
                if (updatedOrder == null)
                {
                    ucKitchen.Children.Remove(order.Border);
                    return false;
                }
                return true;
            });
        }

        private void CreateOrder(Order order)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            Border border = new Border();
            order.Border = border;
            border.Background = Brushes.SkyBlue;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new(1);

            border.Child = stackPanel;

            Label lblTicket = new Label();
            lblTicket.Content = $"Ticket: {order.Ticket}";
            Label lblDish = new();
            lblDish.Content = $"Plato: {order.Dish}";
            Label lblTable = new();
            lblTable.Content = $"Mesa: {order.Table}";
            Label lblEmployee = new();
            lblEmployee.Content = $"Camarero: {order.Employee.FullName}";

            Button btnCookedDish = new Button();
            btnCookedDish.Click += (object sender, RoutedEventArgs e) => {
                btnCookedDish.Content = "En espera";
                border.Background = Brushes.Green;
            };

            btnCookedDish.Content = "Cocinando";
            stackPanel.Children.Add(lblTicket);
            stackPanel.Children.Add(lblDish);
            stackPanel.Children.Add(lblTable);
            stackPanel.Children.Add(lblEmployee);
            stackPanel.Children.Add(btnCookedDish);
            panelKitchen.Children.Add(border);
        }*/
        #endregion
        /*

        #region Crear Empleado

        private async void BtnCrearEmpleado(object sender, RoutedEventArgs e)
        {
            string name = txtBoxName.Text;
            string surname = txtBoxSurname.Text;
            string dni = txtBoxDni.Text;
            string user = txtBoxUser.Text;
            string password = passwdInput.Text;
            string passwordConf = passwdInputConfirm.Text;
            string role = cbRole.Text;
            bool isAdmin = false;
            string confDni = @"^\d{8}[A-Z]|[XYZ]\d{7}[A-Z]$";

            if (role == "Administrador")
            {
                isAdmin = true;
            }

            if (name == "" || surname == "" || dni == "" || user == "" || password == "" || passwordConf == "") 
            {
                MessageBox.Show("Error, no pueden haber campos vacíos");
                return;
            }

            if (!Regex.IsMatch(dni, confDni))
            {
                MessageBox.Show("El DNI no es válido");
                return;
            }

            if (password.Length < 5) 
            {
                MessageBox.Show("La contraseña debe de tener al menos 5 caracteres");
                return;

            }

            if (password != passwordConf) 
            {
                MessageBox.Show("Las contraseñas no coinciden, vuelva a introducir la contraseña");
                return;
            }


            Employee employee = new Employee(user, dni, name, surname, password, isAdmin);
            
            Employee createdEmployee = await DeliiApi.CreateEmployee(employee);
            MessageBox.Show(createdEmployee.ToString());
        }
        #endregion
        */
    }
}
