﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        public MainWindow()
        {
            InitializeComponent();

            tables = new();
            tables.Add(new Table(1, 0.10, 0.10, 25, 25));
            tables.Add(new Table(3, 0.40, 0.40));

            tables.ForEach(table => CreateTable(table));
        }

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
            int availableId = 1;
            bool isIdTaken = true;

            while (isIdTaken)
                if (tables.Find(table => table.Id == availableId) == null) isIdTaken = false;
                else availableId++;

            Table table = new(availableId, 0.10, 0.10);

            tables.Add(table);
            CreateTable(table);
        }

        private void BntDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable == null) return;
            DeleteTable(selectedTable);
            lblTableSelected.Content = "Table Selected:";
        }

        private void BtnCrearEmpleado(object sender, RoutedEventArgs e)
        {
            string name = txtBoxName.Text;
            string surname = txtBoxSurname.Text;
            string dni = txtBoxDni.Text;
            string user = txtBoxUser.Text;
            string pass = showPassword ? txtBoxPass.Text : passBoxPass.Password;
            string confPass = showPassword ? txtBoxConfPass.Text : passBoxConfPass.Password;
            string role = cbRole.Text;
            bool isAdmin = false;
            string confDni = @"\d{8}[A-Z]|[XYZ]\d{7}[A-Z]";

            if (role == "Administrador")
            {
                isAdmin = true;
            }

            if (name == "" || surname == "" || dni == "" || user == "" || pass == "" || confPass == "") {
                MessageBox.Show("Error, no pueden haber campos vacíos");
            }

            if (pass != confPass) {
                MessageBox.Show("Las contraseñas no coinciden, vuelva a introducir la contraseña");
            }

            if (name!="" && surname!="" && dni!="" && user!="" && pass!="" && confPass!="" && pass==confPass && Regex.IsMatch(dni, confDni)) {
                Employee employee = new Employee(user, dni, name, surname, pass, isAdmin);
                MessageBox.Show(employee.ToString());
            }
            
        }

        private void btnShowHidePassword_Click(object sender, RoutedEventArgs e)
        {
            // Muestra u oculta los carácteres de la contraseña
            // Si la contraseña estaba oculta, asignamos el valor del PasswordBox al TextBox y ponemos la propiedad
            // visibility en visible para el TextBox y en collapsed para el PasswordBox (al revés si la contraseña estuviese mostrándose).
            // Además cambiamos el icono.
            if (showPassword)
            {
                passBoxPass.Password = txtBoxPass.Text;
                passBoxConfPass.Password = txtBoxConfPass.Text;
                passBoxPass.Visibility = Visibility.Visible;
                txtBoxPass.Visibility = Visibility.Collapsed;
                passBoxConfPass.Visibility = Visibility.Visible;
                txtBoxConfPass.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtBoxPass.Text = passBoxPass.Password;
                txtBoxConfPass.Text = passBoxConfPass.Password;
                passBoxPass.Visibility = Visibility.Collapsed;
                passBoxConfPass.Visibility = Visibility.Collapsed;
                txtBoxPass.Visibility = Visibility.Visible;
                txtBoxConfPass.Visibility = Visibility.Visible;
            }

            imageShowHidePassword.Source = new BitmapImage(new Uri(showPassword ? "./password_hide.png" : "./password_show.png", UriKind.Relative));
            showPassword = !showPassword;
        }

    }
}
