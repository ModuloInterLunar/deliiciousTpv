using System;
using System.Collections.Generic;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Linq;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Dishes.xaml
    /// </summary>
    public partial class Dishes : UserControl
    {
        // dishes store all the database dishes
        List<Dish> dishes;

        public Dishes()
        {
            InitializeComponent();
            // We generate the list of dishes
            UpdateStackDishes();
        }

        // Método para actualizar la lita de platos
        public async void UpdateStackDishes()
        {
            // Get all dishes from database
            List<Dish> updatedDishes = await DeliiApi.GetAllDishes();
            updatedDishes = updatedDishes.OrderBy(dish => dish.Name).ToList();

            // Si dishes es nulo es porque es la primera vez que se ejecuta el método
            // como no hay nada que actualizar simplemente los generamos
            if(dishes == null)
            {
                dishes = updatedDishes;
                dishes.ForEach(dish => GenerateDishItem(dish));
                return;
            }

            // si no es nulo tenemos que actualizar los campos del dish (si este estaba antes)
            updatedDishes.ForEach(updatedDish =>
            {
                // Busa un dish en los antiguos que tenga el mismo id que el updated dish
                Dish dish = dishes.Find(dish => dish.Id == updatedDish.Id);
                if (dish != null)
                {
                    // si lo encuentra lo actualiza
                    dish.UpdateData(updatedDish);
                    UpdateDishItem(dish);
                }
                else
                {
                    // si no lo encuentra es que es nuevo, entonces lo crea
                    dishes.Add(updatedDish);
                    GenerateDishItem(updatedDish);
                }
            });

            // Elimina los dishes antiguos (los que estaban en dishes, pero no en updated dishes)
            RemoveOldDishes(updatedDishes);
        }

        // Método para eliminar dishes antiguos
        private void RemoveOldDishes(List<Dish> updatedDishes)
        {
            // Asignamos a la variable dishes otra lista que se genera a partir de dishes
            dishes = dishes.FindAll(dish =>
            {
                // Busca en la lista de dishes si hay alguno con el mismo id que el dish actual
                Dish updatedDish = updatedDishes.Find(updatedDish => dish.Id == updatedDish.Id);
                if (updatedDish == null)
                {
                    // Si no lo hay lo elimina de la lista de dishes (de elementos gráficos)
                    stackDishes.Children.Remove(dish.DishItem);
                    // Y devuelve false para que no se añada a la lista del find all
                    return false;
                }

                // si hay alguno devuelve true para que si se añada al find all
                return true;
            });
        }

        // Actualiza los datos de un dish item
        private void UpdateDishItem(Dish dish)
        {
            string dishImageUrl = (dish.Image == null || dish.Image == "") ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : dish.Image;

            dish.DishItem.DishName = dish.Name;
            dish.DishItem.DishPrice = dish.formattedPrice;
            try
            {
                dish.DishItem.DishImage = new BitmapImage(new Uri(dishImageUrl));
            }
            catch
            {
                dish.DishItem.DishImage = new BitmapImage(new Uri("https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg"));
            }
            dish.DishItem.ToolTip = dish.GetFullDescription();
        }

        // Crea un DishItem y se lo asigna a un Dish
        private void GenerateDishItem(Dish dish)
        {
            // asigna la url de la image
            string dishImageUrl = (dish.Image == null || dish.Image == "") ? "https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg" : dish.Image;
            // crea el DishItem
            DishItem dishItem = new()
            {
                DishName = dish.Name,
                DishPrice = dish.formattedPrice,
                Margin = new(5),
                ToolTip = dish.GetFullDescription()
            };

            try
            {
                dishItem.DishImage = new BitmapImage(new Uri(dishImageUrl));
            }
            catch 
            {
                dishItem.DishImage = new BitmapImage(new Uri("https://barradeideas.com/wp-content/uploads/2019/09/fast-food.jpg"));
            }
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
            // lo añade al stack panel
            stackDishes.Children.Add(dishItem);
            
        }

        // Abre la ventana de creación de un Dish
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDishWindow addDishWindow = new();
            addDishWindow.ShowDialog();
            UpdateStackDishes();
        }
    }
}
