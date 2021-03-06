using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private Employee currentUser;

        public Employee CurrentUser { get => currentUser; set => currentUser = value; }

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
                    table.UpdateData(updatedTable, cnvTables.ActualWidth, cnvTables.ActualHeight);
                else
                {
                    tables.Add(updatedTable);
                    CreateTable(updatedTable);
                }
            });

            RemoveDeletedTables(updatedTables);

            if (!tables.Contains(selectedTable))
                SelectTable(null);
            if (selectedTable != null)
                loadOrders();
        }

        private void RemoveDeletedTables(List<Table> updatedTables)
        {
            tables = tables.FindAll(table => {
                Table updatedTable = updatedTables.Find(updatedTable => table.Id == updatedTable.Id);
                if (updatedTable == null)
                {
                    cnvTables.Children.Remove(table.Border);
                    return false;
                }
                return true;
            });
        }

        private void CreateTable(Table table)
        {
            Border border = new();
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7AA0CD");
            Label label = new();
            label.Content = table.Id;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            border.Child = label;
            border.MaxWidth = 100;
            border.Width = table.Width;
            border.Height = table.Height;
            border.CornerRadius = new CornerRadius(50);
            border.AllowDrop = true;
            border.ToolTip = $"Mesa número {table.Id}";
            cnvTables.Children.Add(border);
            table.Border = border;
            table.UpdateRelativePosition(cnvTables.ActualWidth, cnvTables.ActualHeight);
            Canvas.SetLeft(border, table.PosXRelative);
            Canvas.SetTop(border, table.PosYRelative);

            border.MouseLeftButtonUp += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
            {
                SelectTable(table);
            });

            border.MouseMove += new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (e.LeftButton != MouseButtonState.Pressed) return;

                SelectTable(table);
                if (isEditingTableLayout) DragDrop.DoDragDrop(border, new DataObject(DataFormats.Serializable, border), DragDropEffects.Move);
            });
        }
        private void SelectTable(Table table)
        {
            if (selectedTable != null)
            {
                selectedTable.Border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7AA0CD");

                if (selectedTable.ActualTicket != null && selectedTable.ActualTicket.Orders != null)
                {
                    selectedTable.ActualTicket.Orders.ForEach(order =>
                    {
                        stackOrders.Children.Remove(order.OrderItem);
                        order.OrderItem = null;
                    });
                    stackOrders.Children.Clear(); // this is just to make sure the list is empty
                }
            }
            selectedTable = table;
            if (selectedTable == null)
            {
                lblSelectedTable.Content = $"MESA SELECCIONADA:";
                return;
            }
            selectedTable.Border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF92C0F7");
            lblSelectedTable.Content = $"MESA SELECCIONADA: {selectedTable.Id}";
            loadOrders();
        }

        private void UnSelectTable(Table table)
        {
            selectedTable.Border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7AA0CD");
            selectedTable = null;
            stackOrders.Children.Clear();
            lblSelectedTable.Content = "MESA SELECCIONADA:";
        }

        private void DeleteTable(Table table)
        {
            if (table == null) return;
            if (table.ActualTicket != null)
            {
                if (table.ActualTicket.Orders != null)
                {
                    table.ActualTicket.Orders.ForEach(async order => await DeliiApi.RemoveOrder(order));
                }
                DeliiApi.RemoveTicket(table.ActualTicket);
            }
            DeliiApi.RemoveTable(table);
            cnvTables.Children.Remove(table.Border);
            tables.Remove(table);
        }

        private void MoveTable(DragEventArgs e, Border border)
        {
            Point dropPos = e.GetPosition(cnvTables);
            Size offset = new(border.Width / 2, border.Height / 2);
            Size cnvSize = new(cnvTables.ActualWidth, cnvTables.ActualHeight);

            Table table = GetTable(border);
            table.MoveLabel(dropPos, offset, cnvSize);
        }

        private Table GetTable(UIElement element) => tables.Find(table => table.Border == element);

        public void UpdateTablesPosition()
        {
            if (tables == null) return;
            tables.ForEach(table => table.UpdateRelativePosition(cnvTables.ActualWidth, cnvTables.ActualHeight));
        }

        public void loadOrders()
        {

            if (selectedTable.ActualTicket == null || selectedTable.ActualTicket.Orders == null)
            {
                btnGetTicket.Visibility = Visibility.Collapsed;
                stackOrders.Children.Clear();
                return;
            }
            List<Order> orders = selectedTable.ActualTicket.Orders;
            btnGetTicket.Visibility = (orders.Count == 0) ? Visibility.Collapsed : Visibility.Visible;
            RemoveOldOrderItems(orders);
            orders.ForEach(order => CreateOrderItem(order));
        }

        private void RemoveOldOrderItems(List<Order> orders)
        {
            List<OrderItem> orderItemsToRemove = new();
            foreach (OrderItem orderItem in stackOrders.Children)
            {
                if (!orders.Exists(order => order.OrderItem == orderItem)) orderItemsToRemove.Add(orderItem);
            }
            orderItemsToRemove.ForEach(orderItem => stackOrders.Children.Remove(orderItem));
        }
        #endregion

        #region Eventos
        private void cnvTables_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Border border) MoveTable(e, border);
        }

        private async void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            Table table = await DeliiApi.CreateTable(new Table(0.5, 0.5));
            tables.Add(table);
            Application.Current.Dispatcher.Invoke(() => CreateTable(table));
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            isEditingTableLayout = !isEditingTableLayout;
            imgMove.Source = new BitmapImage(new Uri(isEditingTableLayout ? "/Proyecto Intermodular;component/images/move_enabled_icon.png" : "/Proyecto Intermodular;component/images/move_icon.png", UriKind.Relative));
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteTable(selectedTable);
            UnSelectTable(selectedTable);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e) => tables.ForEach(async table => await DeliiApi.UpdateTable(table));
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            UpdateCanvasTables();
            Table selectedTable = this.selectedTable;
            SelectTable(null);
            SelectTable(selectedTable);

        }
        private async void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable == null)
            {
                MessageBox.Show("Selecciona una mesa primero!");
                return;
            }
            if (selectedTable.ActualTicket == null)
            {
                Ticket ticket = await DeliiApi.CreateTicket();
                selectedTable.ActualTicket = ticket;
                await DeliiApi.UpdateTable(selectedTable);
            }

            DishSelector dishSelector = new();


            dishSelector.btnSendOrders.Click += (object sender, RoutedEventArgs e) =>
            {
                if (dishSelector.OrderItems == null) return;

                dishSelector.OrderItems.ForEach(async orderItem =>
                {
                    // En principio, no tendría que entrar nunca en el catch, porque tenemos que hacer que se desactive
                    // el botón cuando no haya suficiente cantidad TODO
                    try
                    {
                        await DeliiApi.ReduceIngredientQuantity(orderItem.Dish);
                    }
                    catch (NotEnoughStockException e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }

                    Order order = await DeliiApi.CreateOrder(new()
                    {
                        Dish = orderItem.Dish,
                        Ticket = selectedTable.ActualTicket.Id,
                        HasBeenCooked = false,
                        HasBeenServed = false,
                        Description = orderItem.txtBoxDescription.Text,
                        Employee = CurrentUser,
                        Table = selectedTable.Id
                    });


                    CreateOrderItem(order);
                    await selectedTable.ActualTicket.AddOrder(order);

                    dishSelector.Close();
                });
            };
            dishSelector.ShowDialog();
        }

        private void CreateOrderItem(Order order)
        {
            if (order.OrderItem != null)
                return;

            string dishImageUrl = (order.Dish.Image == null || order.Dish.Image == "") ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : order.Dish.Image;
            order.OrderItem = new()
            {
                Color = order.GetColorFromState(),
                DishName = order.Dish.Name,
                DishPrice = order.Dish.formattedPrice,
                Description = order.Description,
                DishImage = new BitmapImage(new Uri(dishImageUrl))
            };

            order.OrderItem.btnDelete.Click += async (object sender, RoutedEventArgs e) =>
            {
                if (order.OrderItem.IsDeleting) return;
                order.OrderItem.IsDeleting = true;
                try {
                    await selectedTable.ActualTicket.RemoveOrder(order);
                }
                catch (ItemNotFoundException)
                {
                    MessageBox.Show("Ya se había borrado");
                }
                stackOrders.Children.Remove(order.OrderItem);
            };

            stackOrders.Children.Add(order.OrderItem);
        }

        private void btnGetTicket_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable.ActualTicket.Orders.FindIndex(order => order.HasBeenServed == false) != -1)
            {
                MessageBox.Show("Solo se puede pedir la cuenta cuando todos los platos han sido servidos!");
                return;
            }
            Payment payment = new(selectedTable, selectedTable.ActualTicket);
            payment.ShowDialog();
        }
    #endregion


    }
}