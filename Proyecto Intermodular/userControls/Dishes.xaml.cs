using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Dishes.xaml
    /// </summary>
    public partial class Dishes : UserControl
    {
        List<Dish> dishes;

        public Dishes()
        {
            InitializeComponent();
            UpdateStackDishes();
        }

        public async void UpdateStackDishes()
        {
            List<Dish> updatedDishes = await DeliiApi.GetAllDishes();

            if(dishes == null)
            {
                dishes = updatedDishes;
                dishes.ForEach(dish => GenerateDishItem(dish));
                return;
            }

            updatedDishes.ForEach(updatedDish =>
            {
                Dish dish = dishes.Find(dish => dish.Id == updatedDish.Id);
                if (dish != null)
                {
                    dish.UpdateData(updatedDish);
                    UpdateDishItem(dish);
                }
                else
                {
                    dishes.Add(updatedDish);
                    GenerateDishItem(updatedDish);
                }
            });

            RemoveOldDishes(updatedDishes);
        }

        private void RemoveOldDishes(List<Dish> updatedDishes)
        {
            dishes = dishes.FindAll(dish =>
            {
                Dish updatedDish = updatedDishes.Find(updatedDish => dish.Id == updatedDish.Id);
                if (updatedDish == null)
                {
                    stackDishes.Children.Remove(dish.DishItem);
                    return false;
                }
                return true;
            });
        }

        private void UpdateDishItem(Dish dish)
        {
            string dishImageUrl = (dish.Image == null || dish.Image == "") ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : dish.Image;

            dish.DishItem.DishName = dish.Name;
            dish.DishItem.DishPrice = dish.formattedPrice;
            dish.DishItem.DishImage = new BitmapImage(new Uri(dishImageUrl));
            dish.DishItem.ToolTip = dish.GetFullDescription();
        }

        private void GenerateDishItem(Dish dish)
        {
            string dishImageUrl = (dish.Image == null || dish.Image == "") ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : dish.Image;

            DishItem dishItem = new()
            {
                DishName = dish.Name,
                DishImage = new BitmapImage(new Uri(dishImageUrl)),
                DishPrice = dish.formattedPrice,
                Margin = new(5),
                ToolTip = dish.GetFullDescription()
            };
            dishItem.btnAddDish.Visibility = Visibility.Collapsed;
            dishItem.btnModifyDish.Visibility = Visibility.Visible;

            dishItem.btnModifyDish.Click += (object sender, RoutedEventArgs e) =>
            {
                AddDishWindow addDishWindow = new()
                {
                    Title = "Modificar Plato: " + dish.Name,
                    Dish = dish
                };
                addDishWindow.ShowDialog();
                UpdateStackDishes();
            };

            dish.DishItem = dishItem;
            stackDishes.Children.Add(dishItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDishWindow addDishWindow = new();
            addDishWindow.ShowDialog();
            UpdateStackDishes();
        }
    }
}
