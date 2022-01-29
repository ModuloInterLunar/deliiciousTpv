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

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para DishSelector.xaml
    /// </summary>
    public partial class DishSelector : Window
    {
        List<Dish> dishes;
        List<Dish> selectedDishes;

        public DishSelector()
        {
            InitializeComponent();
            selectedDishes = new();
            GenerateAllDishes();
        }

        public List<Dish> SelectedDishes { get => selectedDishes; set => selectedDishes = value; }

        private async void GenerateAllDishes()
        {
            dishes = await DeliiApi.GetAllDishes();
            dishes.ForEach(dish =>
            {
                DishItem dishItem = CreateDishItem(dish);
                dishesContainer.Children.Add(dishItem);
            });
        }

        private DishItem CreateDishItem(Dish dish)
        {
            string dishImageUrl = (dish.Image == "" || dish.Image == null) ? "https://upload.wikimedia.org/wikipedia/commons/6/62/NCI_Visuals_Food_Hamburger.jpg" : dish.Image;

            DishItem dishItem = new()
            {
                DishImage = new BitmapImage(new Uri(dishImageUrl)),
                DishName = dish.Name,
                DishPrice = $"{dish.Price} €",
                ToolTip = dish.GetIngredients(),
                Margin = new(10),
            };

            dishItem.btnAddDish.Click += (object sender, RoutedEventArgs e) =>
            {
                OrderItem orderItem = new()
                {
                    DishName = dish.Name,
                    DishPrice = $"{dish.Price} €",
                    DishImage = new BitmapImage(new Uri(dishImageUrl)),
                    Margin = new(5)
                };

                orderItem.btnDelete.Click += (object sender, RoutedEventArgs e) =>
                {
                    selectedDishes.RemoveAt(selectedDishesContainer.Children.IndexOf(orderItem));
                    selectedDishesContainer.Children.Remove(orderItem);
                };

                selectedDishesContainer.Children.Add(orderItem);
                selectedDishes.Add(dish);
            };

            return dishItem;
        }

        private void cmbBoxDishType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
