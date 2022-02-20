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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Ingredients.xaml
    /// </summary>
    public partial class Ingredients : UserControl
    {
        List<Ingredient> ingredients;
        public Ingredients()
        {
            InitializeComponent();
            UpdateStackIngredients();
        }

        public async void UpdateStackIngredients()
        {
            List<Ingredient> updatedIngredients = await DeliiApi.GetAllIngredients();
            updatedIngredients = updatedIngredients.OrderBy(ingredient => ingredient.Name).ToList();

            if (ingredients == null)
            {
                ingredients = updatedIngredients;
                ingredients.ForEach(ingredient => CreateIngredientItem(ingredient));
                return;
            }

            updatedIngredients.ForEach(updatedIngredient =>
            {
                Ingredient ingredient = ingredients.Find(ingredient => ingredient.Id == updatedIngredient.Id);
                if (ingredient != null)
                {
                    ingredient.UpdateData(updatedIngredient);
                    UpdateIngredientItem(ingredient);
                }
                else
                {
                    ingredients.Add(updatedIngredient);
                    CreateIngredientItem(updatedIngredient);
                }
            });

            RemoveOldIngredients(updatedIngredients);
        }


        private void RemoveOldIngredients(List<Ingredient> updatedIngredients)
        {
            ingredients = ingredients.FindAll(ingredient =>
            {
                Ingredient updatedIngredient = updatedIngredients.Find(updatedIngredient => ingredient.Id == updatedIngredient.Id);
                if (updatedIngredient == null)
                {
                    stackIngredients.Children.Remove(ingredient.IngredientItem);
                    return false;
                }
                return true;
            });
        }

        private void UpdateIngredientItem(Ingredient ingredient)
        {
            ingredient.IngredientItem.IngredientName = ingredient.Name;
            ingredient.IngredientItem.Quantity = ingredient.FormattedQuantity;
        }

        public void CreateIngredientItem(Ingredient ingredient)
        {
            IngredientItem ingredientItem = new()
            {
                IngredientName = ingredient.Name,
                Quantity = ingredient.FormattedQuantity
            };
            stackIngredients.Children.Add(ingredientItem);
            ingredient.IngredientItem = ingredientItem;

            ingredientItem.btnModify.Click += (object sender, RoutedEventArgs e) =>
            {
                AddIngredientWindow addIngredientWindow = new()
                {
                    Title = "Modificar Ingrediente: " + ingredient.Name,
                    Ingredient = ingredient
                };
                addIngredientWindow.ShowDialog();
                UpdateStackIngredients();
            };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow addIngredientWindow = new();
            addIngredientWindow.ShowDialog();
            UpdateStackIngredients();
        }
    }
}
