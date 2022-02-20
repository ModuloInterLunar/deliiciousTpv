using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Proyecto_Intermodular.userControls;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para IngredientSelector.xaml
    /// </summary>
    public partial class IngredientSelector : Window
    {
        List<Ingredient> ingredients;
        List<IngredientQtyItem> ingredientsQtyItemSelected = new();
        List<IngredientQty> ingredientsQtiesPreviouslyAdded = new();

        public List<IngredientQtyItem> IngredientsQtyItemSelected { get => ingredientsQtyItemSelected; set => ingredientsQtyItemSelected = value; }
        public List<IngredientQty> IngredientsQtiesPreviouslyAdded { get => ingredientsQtiesPreviouslyAdded; set => ingredientsQtiesPreviouslyAdded = value; }

        public IngredientSelector()
        {
            InitializeComponent();
            GenerateIngredientsList();
        }

        private async void GenerateIngredientsList()
        {
            ingredients = await DeliiApi.GetAllIngredients();
            ingredients.OrderBy(ingredient => ingredient.Name)
                .ToList()
                .ForEach(ingredient => CreateIngredientItem(ingredient));
        }

        private void CreateIngredientItem(Ingredient ingredient)
        {
            IngredientItem ingredientItem = new()
            {
                IngredientName = ingredient.Name,
                Quantity = $"Restante: {ingredient.FormattedQuantity}"
            };

            ((Image)ingredientItem.btnModify.Content).Source = new BitmapImage(new Uri("/Proyecto Intermodular;component/images/add_icon.png", UriKind.Relative));

            ingredientItem.btnModify.Click += (object sender, RoutedEventArgs e) =>
            {
                ingredientItem.IsEnabled = false;
                CreateIngredientQtyItem(ingredient, ingredientItem, null);
            };

            IngredientQty ingredientQtyPreviouslyAdded = ingredientsQtiesPreviouslyAdded.Find(ing => ing.Ingredient.Id == ingredient.Id);
            if(ingredientQtyPreviouslyAdded != null)
            {
                CreateIngredientQtyItem(ingredientQtyPreviouslyAdded.Ingredient, ingredientItem, ingredientQtyPreviouslyAdded.Quantity);
                ingredientItem.IsEnabled = false;
            }

            ingredientsPanel.Children.Add(ingredientItem);
            ingredient.IngredientItem = ingredientItem;
        }

        private void CreateIngredientQtyItem(Ingredient ingredient, IngredientItem ingredientItem, double? quantity)
        {
            IngredientQtyItem ingredientQtyItem = new()
            {
                IngredientName = ingredient.Name,
                IngredientMeasure = $"({ingredient.Measure})",
                BaseIngredient = ingredient,
                IngredientQuantity = quantity.ToString()?? "" 
            };

            ingredientQtyItem.btnDelete.Click += (object sender, RoutedEventArgs e) =>
            {
                selectedIngredientsPanel.Children.Remove(ingredientQtyItem);
                ingredientsQtyItemSelected.Remove(ingredientQtyItem);
                ingredientItem.IsEnabled = true;
            };

            selectedIngredientsPanel.Children.Add(ingredientQtyItem);
            ingredientsQtyItemSelected.Add(ingredientQtyItem);
        }
    }
}
