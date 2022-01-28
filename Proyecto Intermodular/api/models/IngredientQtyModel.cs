using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class IngredientQtyModel
    {
        private string ingredient;
        private double quantity;

        public string Ingredient { get => ingredient; set => ingredient = value; }
        public double Quantity { get => quantity; set => quantity = value; }

        public IngredientQtyModel() { }

        public IngredientQtyModel(IngredientQty ingredientQty)
        {
            ingredient = ingredientQty.Ingredient.Id;
            quantity = ingredientQty.Quantity;
        }
    }
}
