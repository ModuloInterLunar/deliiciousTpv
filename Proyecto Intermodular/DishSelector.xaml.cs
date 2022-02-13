using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.userControls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para DishSelector.xaml
    /// </summary>
    public partial class DishSelector : Window
    {
        private List<Dish> dishes;
        private List<OrderItem> orderItems;
        private string typeFilter;

        public DishSelector()
        {
            InitializeComponent();
            OrderItems = new();
            GenerateAllDishes();
        }
        public List<OrderItem> OrderItems { get => orderItems; set => orderItems = value; }

        private void ReloadAllDishes()
        {
            if (dishesContainer == null) return;
            dishesContainer.Children.Clear();
            GenerateAllDishes();
        }


        private async void GenerateAllDishes()
        {
            dishes = await DeliiApi.GetAllDishes();
            dishes.ForEach(dish =>
            {
                if (typeFilter != null && dish.Type != typeFilter) return;
                DishItem dishItem = CreateDishItem(dish);
                dishesContainer.Children.Add(dishItem);
            });
        }

        private DishItem CreateDishItem(Dish dish)
        {
            string dishImageUrl = (dish.Image == "" || dish.Image == null) ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : dish.Image;

            DishItem dishItem = new()
            {
                DishImage = new BitmapImage(new Uri(dishImageUrl)),
                DishName = dish.Name,
                DishPrice = dish.formattedPrice,
                ToolTip = dish.GetFullDescription(),
                Margin = new(10),
            };

            try
            {
                dishItem.DishImage = new BitmapImage(new Uri(dishImageUrl));
            }
            catch
            {
                dishItem.DishImage = new BitmapImage(new Uri("https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg"));
            }

            dishItem.btnAddDish.Click += (object sender, RoutedEventArgs e) =>
            {
                OrderItem orderItem = new()
                {
                    DishName = dish.Name,
                    DishPrice = dish.formattedPrice,
                    DishImage = new BitmapImage(new Uri(dishImageUrl)),
                    Margin = new(5),
                    Dish = dish
                };

                orderItem.btnDelete.Click += (object sender, RoutedEventArgs e) =>
                {
                    OrderItems.Remove(orderItem);
                    selectedDishesContainer.Children.Remove(orderItem);
                };

                selectedDishesContainer.Children.Add(orderItem);
                OrderItems.Add(orderItem);
            };

            return dishItem;
        }

        private void cmbBoxDishType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();
            switch (selectedItem)
            {
                case "Todos":
                    typeFilter = null;
                    break;
                case "Comidas":
                    typeFilter = "Food";
                    break;
                case "Bebidas":
                    typeFilter = "Drink";
                    break;
            }
            ReloadAllDishes();
        }
    }
}
