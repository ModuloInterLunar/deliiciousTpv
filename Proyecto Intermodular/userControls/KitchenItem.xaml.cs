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
    /// Lógica de interacción para KitchenItem.xaml
    /// </summary>
    public partial class KitchenItem : UserControl
    {
        List<Order> orders;
        int timerCount = 0;
        public KitchenItem()
        {
            InitializeComponent();
            GenerateOrders();
        }

        public String TableKitchen { set => lblTableKitchen.Content = value; get => lblTableKitchen.Content.ToString(); }

        public String EmployeeKitchen { set => lblEmployeeKitchen.Content = value; get => lblEmployeeKitchen.Content.ToString(); }

        public String DishKitchen { set => lblDishKitchen.Content = value; get => lblDishKitchen.Content.ToString(); }

        public String TimerKitchen { set => lblTimerKitchen.Content = value; get => lblTimerKitchen.Content.ToString(); }

        public ImageSource ImageKitchen { set => imgImageKitchen.Source = value; get => imgImageKitchen.Source; }

        private async void GenerateOrders()
        {
            List<Order> allOrders = await DeliiApi.GetAllOrders();
            List <Order> notServedOrders = allOrders.FindAll(order => !order.HasBeenServed && order.Dish.Type == "Food");
            notServedOrders.ForEach(order => CreateOrder(order)); 
            
        }

        private void CreateOrder(Order order)
        {
            TableKitchen = "Mesa: " + order.Table;
            EmployeeKitchen = "Camarero: " + order.Employee.FullName;
            DishKitchen = "Plato: " + order.Dish.Name;
            if (order.HasBeenCoocked == false)
            {
                ImageKitchen = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/113/113339.png"));
            }else
            {
                ImageKitchen = new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/1046/1046874.png"));
            }
            
            StartTimer();
            
            btnCookingKitchen.Click += (object sender, RoutedEventArgs e) =>
            {
                ImageKitchen = new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/1046/1046874.png"));
                timerCount = 0;
                StartTimer();
            };
        }

        public void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += TimerTick;
            timer.Start();
            
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timerCount++;
            TimerKitchen = "Tiempo transcurrido: " + timerCount + "s.";
        }

    }
}
