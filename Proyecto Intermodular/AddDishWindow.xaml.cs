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
using System.Windows.Shapes;
using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        private Dish dish;
        private Dish updatedDish;

        private List<IngredientQty> dishIngredients = new();

        public AddDishWindow()
        {
            InitializeComponent();
        }

        public Dish Dish { get => dish; set => dish = value; }
        public Dish UpdatedDish { get => updatedDish; set => updatedDish = value; }

        private bool IsEmpty(string str) => str is null or "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dishIngredients.Count == 0)
            {
                MessageBox.Show($"Error, el plato debe tener al menos 1 ingrediente.");
                return;
            }

            if (IsEmpty(tbName.Text) || IsEmpty(tbPrice.Text))
            {
                MessageBox.Show($"Error, no puede haber ningún campo vacío.");
                return;
            }

            if (!double.TryParse(tbPrice.Text, out double price))
            {
                MessageBox.Show($"Error, el precio debe ser un número.");
                return;
            }

            if (dish != null)
            {
                UpdateDish();
                return;
            }

            CreateDish();
        }

        private async void CreateDish()
        {
            dish = new();
            GetData();
            try
            {
                updatedDish = await DeliiApi.CreateDish(dish);
            }
            catch (AlreadyInUseException)
            {
                dish = null;
                MessageBox.Show("Este plato ya existe!");
                return;
            }
            Close();
        }

        private async void UpdateDish()
        {
            GetData();
            try
            {
                updatedDish = await DeliiApi.UpdateDish(dish);
            }
            catch (AlreadyInUseException)
            {
                MessageBox.Show("Este ingrediente ya existe!");
                return;
            }
            Close();
        }

        private void GetData()
        {
            dish.Name = tbName.Text;
            dish.Type = cbDishType.Text == "Comida" ? "Food" : "Drink";

            dish.Price = double.Parse(tbPrice.Text);
            dish.Description = tbDescription.Text;
            dish.IngredientQties = dishIngredients;
            dish.Image = tbImageURL.Text;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (dish != null)
            {
                tbName.Text = dish.Name;
                cbDishType.Text = dish.Type == "Food" ? "Comida" : "Bebida";
                tbPrice.Text = dish.Price.ToString("0.00");
                tbDescription.Text = dish.Description;
                tbImageURL.Text = dish.Image;
                btnSave.Content = "Modificar Plato";
                dishIngredients = dish.IngredientQties;
                btnIngredientsQty.ToolTip = dish.GetIngredients();
            }
        }

        private void btnIngredientsQty_Click(object sender, RoutedEventArgs e)
        {
            IngredientSelector ingredientSelector = new()
            {
                IngredientsQtiesPreviouslyAdded = dishIngredients
            };
            
            ingredientSelector.btnSendIngredients.Click += (object sender, RoutedEventArgs e) =>
            {
                bool isValidOperation = true;
                dishIngredients.Clear();
                btnIngredientsQty.ToolTip = "";

                ingredientSelector.IngredientsQtyItemSelected.ForEach(ingredientQtyItem =>
                {
                    if (!double.TryParse(ingredientQtyItem.IngredientQuantity, out double quantity))
                    {
                        dishIngredients.Clear();
                        MessageBox.Show($"Error, la cantidad de {ingredientQtyItem.IngredientName} debe ser un número.");
                        isValidOperation = false;
                        return;
                    }

                    dishIngredients.Add(new()
                    {
                        Ingredient = ingredientQtyItem.BaseIngredient,
                        Quantity = quantity
                    });
                });

                if (isValidOperation)
                {
                    btnIngredientsQty.ToolTip = $"Actuales:\n{dishIngredients.Aggregate("", (acc, cur) => acc += $"{cur}\n")}";
                    ingredientSelector.Close();
                }
            };

            ingredientSelector.ShowDialog();
        }
    }
}
