using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        bool distribution;
        bool isDroppingOverOtherTable;
        List<Table> tables;
        Table selectedTable;
        bool showPassword = false;

        List<Order> orders;
        Border myBorder1 = new Border();
        
        StackPanel stackPanel = new StackPanel();


        Employee currentUser;


        public MainWindow()
        {
            InitializeComponent();
            if (currentUser == null)
                GetCurrentUser();
            else
                UpdateUI();

            GenerateCanvasTables();
            GenerateOrders();
        }


        private async void GetCurrentUser()
        {
            try
            {
                //DeliiAPI.GetEmployeeFromToken().ContinueWith(task =>
                //{
                //    if (task.IsFaulted)
                //        MessageBox.Show(task.Exception.Message);
                //    currentUser = task.Result;
                //    ApplicationState.SetValue("current_user", currentUser);
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {
                //        UpdateUI();
                //    });
                //}); 

                currentUser = await DeliiApi.GetEmployeeFromToken();
                ApplicationState.SetValue("current_user", currentUser);
                Application.Current.Dispatcher.Invoke(() =>
                {
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

        #region Tab 1

        private void CreateTable(Table table)
        {
            Border border = new();
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7AA0CD");
            border.CornerRadius = new(10);
            border.Name = $"border{table.Id}";
            border.MaxWidth = 100;
            border.Width = table.Width;
            border.Height = table.Height;
            border.AllowDrop = true;
            Canvas.SetLeft(border, table.PosX);
            Canvas.SetTop(border, table.PosY);

            border.MouseMove += new MouseEventHandler((object sender, MouseEventArgs e) => {
                if (e.LeftButton != MouseButtonState.Pressed) return;

                SelectTable(table);
                if (distribution) DragDrop.DoDragDrop(border, new DataObject(DataFormats.Serializable, border), DragDropEffects.Move);
            });

            /*
            border.Drop += new DragEventHandler((object sender, DragEventArgs e) => {
                Border borderForDelete = (Border)e.Data.GetData(DataFormats.Serializable);
                MessageBox.Show(GetTable(borderForDelete).ToString());
                if (border == borderForDelete) return;

                table.ChangeTableSize(borderForDelete.Width, 0);

                Table tableForDelete = GetTable(borderForDelete);
                DeleteTable(tableForDelete);
                SelectTable(table);
            });
            */

            Label label = new();
            label.Content = table.Id;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;

            border.Child = label;

            cnvTables.Children.Add(border);
            table.Border = border;
        }

        private void SelectTable(Table table)
        {
            selectedTable = table;
            lblTableSelected.Content = $"Table Selected: {selectedTable.Id}";
        }

        private void DeleteTable(Table table)
        {
            isDroppingOverOtherTable = true;
            cnvTables.Children.Remove(table.Border);
            tables.Remove(table);
        }

        private Table GetTable(UIElement element)
        {
            foreach (Table table in tables)
                if (table.Border == element) return table;

            return null;
        }

        private void MoveTable(DragEventArgs e, Border border)
        {
            Point dropPos = e.GetPosition(cnvTables);
            Size offset = new(border.Width / 2, border.Height / 2);

            Table table = GetTable(border);

            double left = (dropPos.X > cnvTables.ActualWidth - offset.Width * 2) ? cnvTables.ActualWidth - offset.Width * 2 :
                            (dropPos.X < offset.Width) ? 0 : dropPos.X - offset.Width;

            double top = (dropPos.Y > cnvTables.ActualHeight - offset.Height * 2) ? cnvTables.ActualHeight - offset.Height * 2 :
                            (dropPos.Y < offset.Height) ? 0 : dropPos.Y - offset.Height;

            Point newPoint = new(left, top);
            table.SetPosition(newPoint, cnvTables.ActualWidth, cnvTables.ActualHeight);
            Canvas.SetLeft(border, left);
            Canvas.SetTop(border, top);
        }

        private void CnvTables_Drop(object sender, DragEventArgs e)
        {
            if (isDroppingOverOtherTable)
            {
                isDroppingOverOtherTable = false;
                return;
            }

            object data = e.Data.GetData(DataFormats.Serializable);
            if(data is Border border) MoveTable(e, border);
        }

        private void CnvTables_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Border border) MoveTable(e, border);
        }

        private void BtnDistribution_Click(object sender, RoutedEventArgs e) => distribution = !distribution;

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (tables == null ) return;
            tables.ForEach(table =>
            {
                table.UpdatePosition(cnvTables.ActualWidth, cnvTables.ActualHeight);

                Canvas.SetLeft(table.Border, table.PosX);
                Canvas.SetTop(table.Border, table.PosY);
            });
        }

        private void BtnSaveDistribution_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            //string availableId = "1";
            //bool isIdTaken = true;

            //while (isIdTaken)
            //    if (tables.Find(table => table.Id == availableId) == null) isIdTaken = false;
            //    else availableId++;

            //Table table = new(availableId, 0.10, 0.10);

            //tables.Add(table);
            //CreateTable(table);
        }

        private void BntDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable == null) return;
            DeleteTable(selectedTable);
            lblTableSelected.Content = "Table Selected:";
        }
        #endregion


        #region Cocina
        private async void GenerateCanvasTables()
        {
            tables = await DeliiApi.GetAllTables();

            tables.ForEach(table => CreateTable(table));

            
        }

        private void GenerateOrders()
        {
            orders = new();
            orders.Add(new Order("1", "1", "Fabada", false, false, "Djessy"));
            orders.Add(new Order("2", "2", "Spaghetti", false, false, "Alvaro"));
            orders.Add(new Order("3", "3", "Porritos", false, false, "Matías"));
            orders.Add(new Order("4", "4", "Hamburguesa", false, false, "Saul"));

            orders.ForEach(order => CreateOrder(order));
        }

        private void CreateOrder(Order order)
        {

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            Border myBorder1 = new Border();
            myBorder1.Background = Brushes.SkyBlue;
            myBorder1.BorderBrush = Brushes.Black;
            myBorder1.BorderThickness = new Thickness(1);

            myBorder1.Child = stackPanel;

            Label label1 = new Label();
            label1.Content = "Ticket:";
            Label label2 = new Label();
            label2.Content = order.Ticket;
            Label label3 = new Label();
            label3.Content = "Plato:";
            Label label4 = new Label();
            label4.Content = order.Dish;
            Label label5 = new Label();
            label5.Content = "Mesa:";
            Label label6 = new Label();
            label6.Content = "Camarero:";
            Label label7 = new Label();
            label7.Content = order.Employee;

            Button btnCookedDish = new Button();
            btnCookedDish.Click += (Object sender, RoutedEventArgs e) => {
                btnCookedDish.Content = "En espera";
                myBorder1.Background = Brushes.Green;
            };

            btnCookedDish.Content = "Cocinando";
            stackPanel.Children.Add(label1);
            stackPanel.Children.Add(label2);
            stackPanel.Children.Add(label3);
            stackPanel.Children.Add(label4);
            stackPanel.Children.Add(label5);
            stackPanel.Children.Add(label6);
            stackPanel.Children.Add(label7);
            stackPanel.Children.Add(btnCookedDish);
            panelKitchen.Children.Add(myBorder1);
        }
        #endregion


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
            string confDni = @"\d{8}[A-Z]|[XYZ]\d{7}[A-Z]";

            if (role == "Administrador")
            {
                isAdmin = true;
            }

            if (name == "" || surname == "" || dni == "" || user == "" || password == "" || passwordConf == "") {
                MessageBox.Show("Error, no pueden haber campos vacíos");
                return;
            }

            if (!Regex.IsMatch(dni, confDni))
            {
                MessageBox.Show("El DNI no es válido");
                return;
            }

            if (password.Length < 5) {
                MessageBox.Show("La contraseña debe de tener al menos 5 caracteres");
                return;

            }

            if (password != passwordConf) {
                MessageBox.Show("Las contraseñas no coinciden, vuelva a introducir la contraseña");
                return;
            }


            Employee employee = new Employee(user, dni, name, surname, password, isAdmin);
            
            Employee createdEmployee = await DeliiApi.CreateEmployee(employee);
            MessageBox.Show(createdEmployee.ToString());
        }
        #endregion

    }
}
