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
            UpdateCanvasTables();
        }

        #region Funciones
        public async void UpdateCanvasTables()
        {
            if (isEditingTableLayout) return;

            List<Table> updatedTables = await DeliiApi.GetAllTables();

            if (tables == null)
            {
                tables = updatedTables;
                tables.ForEach(table => CreateTable(table));
                return;
            }


            updatedTables.ForEach(updatedTable =>
            {
                Table table = tables.Find(table => table.Id == updatedTable.Id);
                if (table != null)
                {
                    table.UpdateData(updatedTable);
                    UpdateLabel(table);
                }
                else
                {
                    tables.Add(updatedTable);
                    CreateTable(updatedTable);
                }
            });

            RemoveOldTables(updatedTables);

            if (!tables.Contains(selectedTable))
                SelectTable(null);
        }

        private void RemoveOldTables(List<Table> updatedTables)
        {
            List<Table> tablesToRemove = new();
            tables.ForEach(table =>
            {
                Table updatedTable = updatedTables.Find(updatedTable => updatedTable.Id == table.Id);
                if (updatedTable == null)
                {
                    cnvTables.Children.Remove(table.Label);
                    tablesToRemove.Add(table);
                }
            });

            tablesToRemove.ForEach(tableToRemove => tables.Remove(tableToRemove));
        }
        private void UpdateLabel(Table table)
        {
            Label label = table.Label;
            label.Width = table.Width;
            label.Height = table.Height;
            table.UpdateRelativePosition(cnvTables.ActualWidth, cnvTables.ActualHeight);
            Canvas.SetLeft(label, table.PosXRelative);
            Canvas.SetTop(label, table.PosYRelative);
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

            label.MouseLeftButtonUp += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
            {
                SelectTable(table);
            });
            label.MouseMove += new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (e.LeftButton != MouseButtonState.Pressed) return;

                SelectTable(table);
                if (isEditingTableLayout) DragDrop.DoDragDrop(label, new DataObject(DataFormats.Serializable, label), DragDropEffects.Move);
            });

        }
        private void SelectTable(Table table)
        {
            selectedTable = table;
            if (selectedTable == null)
            {
                // lblTableSelected.Content = $"Table Selected: ";
                return;
            }
            // lblTableSelected.Content = $"Table Selected: {selectedTable.Id}";
        }

        private void DeleteTable(Table table)
        {
            DeliiApi.RemoveTable(table);
            cnvTables.Children.Remove(table.Label);
            tables.Remove(table);
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
        public void UpdateTablesPosition()
        {
            if (tables == null) return;
            tables.ForEach(table =>
            {
                table.UpdateRelativePosition(cnvTables.ActualWidth, cnvTables.ActualHeight);
                Canvas.SetLeft(table.Label, table.PosXRelative);
                Canvas.SetTop(table.Label, table.PosYRelative);
            });
        }
        #endregion


        #region Eventos
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            isEditingTableLayout = !isEditingTableLayout;
        }

        private void cnvTables_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Label label) MoveTable(e, label);
        }

        private async void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            Table table = await DeliiApi.CreateTable(new Table(0, 0));
            tables.Add(table);
            Application.Current.Dispatcher.Invoke(() => CreateTable(table));
        }
        #endregion

    }
}
