using Proyecto_Intermodular.api.models;
using System;

namespace Proyecto_Intermodular.models
{
    public class IngredientQty
    {
        private Ingredient ingredient;
        private double quantity;

        public Ingredient Ingredient { get => ingredient; set => ingredient = value; }
        public double Quantity { get => quantity; set => quantity = value; }

        public override string ToString() => $"{ingredient.Name}, {quantity}";

        public IngredientQty() { }

        internal void UpdateData(IngredientQty updatedIngredientQty)
        {
            quantity = updatedIngredientQty.quantity;
            if (updatedIngredientQty.ingredient == null)
                ingredient = null;
            else if (ingredient == null)
                ingredient = updatedIngredientQty.ingredient;
            else
                ingredient.UpdateData(updatedIngredientQty.ingredient);
        }
    }
}
