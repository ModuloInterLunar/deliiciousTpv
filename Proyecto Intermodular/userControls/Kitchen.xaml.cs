﻿using System;
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

        List<Order> allOrders;
        public Kitchen()
        {
            InitializeComponent();
            GenerateOrders();
        }

        private async void GenerateOrders()
        {
            allOrders = await DeliiApi.GetAllOrdersNotServed();
            List<Order> notServedOrders = allOrders.FindAll(order => order.Dish.Type == "Food");
            notServedOrders.ForEach(order => CreateOrder(order));
        }

        private void CreateOrder(Order order)
        {
            int timerCount = 0;
            string url = !order.HasBeenCooked ? "https://image.flaticon.com/icons/png/512/113/113339.png" : "https://cdn-icons-png.flaticon.com/512/1046/1046874.png";
            KitchenItem kitchenItem = new KitchenItem()
            {
                TableKitchen = "Mesa: " + order.Table,
                EmployeeKitchen = "Camarero: " + order.Employee.FullName,
                DishKitchen = "Plato: " + order.Dish.Name,
                TimerKitchen = "Tiempo transcurrido: " + timerCount + " s.",
                //DescriptionKitchen = "Descripción: " + order.Description,
                ImageKitchen = new BitmapImage(new Uri(url))
            };

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

            updatedOrders.ForEach(updatedOrder =>
            {
                Order order = allOrders.Find(order => order.Id == updatedOrder.Id);
                if (order != null)
                    order.UpdateData(updatedOrder);
                else
                {
                    allOrders.Add(updatedOrder);
                    CreateOrder(updatedOrder);
                }
            });

            RemoveServedOrders(updatedOrders);
        }

        private void RemoveServedOrders(List<Order> updatedOrders)
        {
            allOrders = allOrders.FindAll(order =>
            {
                Order updatedOrder = updatedOrders.Find(updatedOrder => order.Id == updatedOrder.Id);
                if (updatedOrder == null)
                {
                    stackKitchen.Children.Remove(order.KitchenItem);
                    return false;
                }
                return true;
            });
        }
    }
}
