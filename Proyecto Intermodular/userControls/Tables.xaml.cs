using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Tables.xaml
    /// </summary>
    public partial class Tables : UserControl
    {
        private List<Table> tables;
        private Table selectedTable;
        private bool isEditingTableLayout;

        public Tables()
        {
            InitializeComponent();
            GenerateCanvasTable();
        }

        public async void GenerateCanvasTable()
        {
            if (isEditingTableLayout) return;
            cnvTables.Children.Clear();
            tables = await DeliiApi.GetAllTables();
            tables.ForEach(table => CreateTable(table));
        }

        private void CreateTable(Table table)
        {
            Label label = new();
            label.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7AA0CD");
            label.Content = table.Id;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.MaxWidth = 100;
            label.Width = table.Width;
            label.Height = table.Height;
            label.AllowDrop = true;
            cnvTables.Children.Add(label);
            table.Label = label;
            table.UpdateRelativePosition(cnvTables.ActualWidth, cnvTables.ActualHeight);
            Canvas.SetLeft(label, table.PosXRelative);
            Canvas.SetTop(label, table.PosYRelative);

            label.MouseMove += new MouseEventHandler((object sender, MouseEventArgs e) => {
                if (e.LeftButton != MouseButtonState.Pressed) return;

                SelectTable(table);
                if (isEditingTableLayout) DragDrop.DoDragDrop(label, new DataObject(DataFormats.Serializable, label), DragDropEffects.Move);
            });
        }

        private void SelectTable(Table table)
        {
            selectedTable = table;
        }
        private void DeleteTable(Table table)
        {
            DeliiApi.RemoveTable(table);
            cnvTables.Children.Remove(table.Label);
            tables.Remove(table);
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            isEditingTableLayout = !isEditingTableLayout;
        }

        private void cnvTables_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Label label) MoveTable(e, label);
        }

        private void MoveTable(DragEventArgs e, Label label)
        {
            Point dropPos = e.GetPosition(cnvTables);
            Size offset = new(label.Width / 2, label.Height / 2);

            Table table = GetTable(label);

            double left = (dropPos.X > cnvTables.ActualWidth - offset.Width)
                            ? cnvTables.ActualWidth - offset.Width * 2
                            : (dropPos.X < offset.Width)
                                ? 0
                                : dropPos.X - offset.Width;

            double top = (dropPos.Y > cnvTables.ActualHeight - offset.Height)
                            ? cnvTables.ActualHeight - offset.Height * 2
                            : (dropPos.Y < offset.Height)
                                ? 0
                                : dropPos.Y - offset.Height;

            Point newPoint = new(left, top);
            table.SetPosition(newPoint, cnvTables.ActualWidth, cnvTables.ActualHeight);
            Canvas.SetLeft(label, left);
            Canvas.SetTop(label, top);
        }

        private Table GetTable(UIElement element) => tables.Find(table => table.Label == element);
        
        private async void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            Table table = await DeliiApi.CreateTable(new Table(0, 0));
            tables.Add(table);
            Application.Current.Dispatcher.Invoke(() => CreateTable(table));
        }
    }
}
