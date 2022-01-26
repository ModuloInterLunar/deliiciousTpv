namespace Proyecto_Intermodular.models
{
    public class IngredientQty
    {
        private Ingredient ingredient;
        private float quantity;

        public Ingredient Ingredient { get => ingredient; set => ingredient = value; }
        public float Quantity { get => quantity; set => quantity = value; }

        public IngredientQty() { }
    }
}
