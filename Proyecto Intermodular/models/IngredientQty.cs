using Proyecto_Intermodular.api.models;

namespace Proyecto_Intermodular.models
{
    public class IngredientQty
    {
        private Ingredient ingredient;
        private double quantity;

        public Ingredient Ingredient { get => ingredient; set => ingredient = value; }
        public double Quantity { get => quantity; set => quantity = value; }

        public IngredientQty() { }

    }
}
