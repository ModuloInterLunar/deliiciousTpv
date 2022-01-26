namespace Proyecto_Intermodular.models
{
    public class IngredientQtyModel
    {
        private string ingredient;
        private float quantity;

        public string Ingredient { get => ingredient; set => ingredient = value; }
        public float Quantity { get => quantity; set => quantity = value; }

        public IngredientQtyModel() { }

        public IngredientQtyModel(IngredientQty ingredientQty)
        {
            ingredient = ingredientQty.Ingredient.Id;
            quantity = ingredientQty.Quantity;
        }
    }
}
