using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System.Windows.Threading;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Kitchen.xaml
    /// </summary>
    public partial class Kitchen : UserControl
    {

        List<Order> notServedOrders;
        List<Order> notServedFoodOrders;
        Border emptyKitchen;
        public Kitchen()
        {
            InitializeComponent();
            GenerateOrders();
        }

        private async void GenerateOrders()
        {
            this.notServedOrders = await DeliiApi.GetAllOrdersNotServed();
            notServedFoodOrders = notServedOrders.FindAll(order => order.Dish.Type == "Food");
            notServedFoodOrders.ForEach(order => CreateOrder(order));
            generateEmptyKitchenBorder();
        }

        private void generateEmptyKitchenBorder()
        {
            emptyKitchen = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            emptyKitchen.Child = new Label()
            {
                Content = "👩‍🍳 No hay pedidos pendientes!",
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center
            };
        }

        private void CreateOrder(Order order)
        {
            int timerCount = 0;
            string dishImageUrl = (order.Dish.Image == "" || order.Dish.Image == null) ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : order.Dish.Image;
            string url = !order.HasBeenCooked ? "https://image.flaticon.com/icons/png/512/113/113339.png" : "https://cdn-icons-png.flaticon.com/512/1046/1046874.png";
            KitchenItem kitchenItem = new KitchenItem()
            {
                TableKitchen = "Mesa: " + order.Table,
                EmployeeKitchen = "Camarero: " + order.Employee.FullName,
                DishKitchen = "Plato: " + order.Dish.Name,
                TimerKitchen = "Tiempo transcurrido: " + timerCount + " s.",
                DescriptionKitchen = "Descripción: " + order.Description,
                ImageKitchen = new BitmapImage(new Uri(url))
            };

            try
            {
                kitchenItem.DishImage = new BitmapImage(new Uri(dishImageUrl));
            }
            catch
            {
                kitchenItem.DishImage = new BitmapImage(new Uri("https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg"));
            }

            StartTimer();

            void StartTimer()
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Tick += TimerTick;
                timer.Start();
            }

            void TimerTick(object sender, EventArgs e)
            {
                kitchenItem.TimerKitchen = "Tiempo transcurrido: " + timerCount + " s.";
                timerCount++;
            }

            kitchenItem.btnCookingKitchen.Click += (object sender, RoutedEventArgs e) =>
            {
                if (order.HasBeenCooked == false)
                {
                    BtnOrderCooked(order, kitchenItem);
                    UpdateKitchen();
                }
                else
                {
                    BtnOrderServed(order, kitchenItem);
                    UpdateKitchen();
                }

            };
            order.KitchenItem = kitchenItem;
            stackKitchen.Children.Add(kitchenItem);
        }

        public async void BtnOrderCooked(Order order, KitchenItem kitchenItem)
        {
            order.HasBeenCooked = true;

            order.UpdateData(await DeliiApi.UpdateOrder(order));
            kitchenItem.ImageKitchen = new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/1046/1046874.png"));
        }

        public async void BtnOrderServed(Order order, KitchenItem kitchenItem)
        {
            order.HasBeenServed = true;

            order.UpdateData(await DeliiApi.UpdateOrder(order));
            kitchenItem.ImageKitchen = new BitmapImage(new Uri("https://icon-library.com/images/dish-icon/dish-icon-8.jpg"));
        }

        public async void UpdateKitchen()
        {
            List<Order> updatedOrders = await DeliiApi.GetAllOrdersNotServed();

            List<Order> updatedFoodOrders = updatedOrders.FindAll(order => order.Dish.Type == "Food");

            updatedFoodOrders.ForEach(updatedFoodOrder =>
            {
                Order order = notServedFoodOrders.Find(order => order.Id == updatedFoodOrder.Id);
                if (order != null)
                {
                    order.UpdateData(updatedFoodOrder);
                }
                else
                {
                    notServedFoodOrders.Add(updatedFoodOrder);
                    CreateOrder(updatedFoodOrder);
                }
            });

            RemoveServedOrders(updatedFoodOrders);

            if (notServedFoodOrders.Count == 0)
            {
                if (!stackKitchen.Children.Contains(emptyKitchen))
                    stackKitchen.Children.Add(emptyKitchen);
            } 
            else
            {
                if (stackKitchen.Children.Contains(emptyKitchen))
                    stackKitchen.Children.Remove(emptyKitchen);
            }
        }

        private void RemoveServedOrders(List<Order> updatedFoodOrders)
        {
            notServedFoodOrders = notServedFoodOrders.FindAll(order =>
            {
                Order updatedOrder = updatedFoodOrders.Find(updatedOrder => order.Id == updatedOrder.Id);
                if (updatedOrder == null)
                {
                    stackKitchen.Children.Remove(order.KitchenItem);
                    return false;
                }
                return true;
            });
        }

        private Brush GetColorFromOrderState(Order order)
        {
            Brush color = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFDDDD");
            if (order.HasBeenCooked) color = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFE8CC");
            if (order.HasBeenServed) color = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDDFFDD");
            return color;
        }
    }
}
