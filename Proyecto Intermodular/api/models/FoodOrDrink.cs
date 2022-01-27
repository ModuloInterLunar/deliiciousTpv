using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class FoodOrDrink
    {
        private string id;
        private string name;
        private string type;
        private float quantity;
        private IngredientQty ingredients;
        private float price;
        private string description;
        private string image;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public IngredientQty Ingredients { get => ingredients; set => ingredients = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }

        protected FoodOrDrink() { }
    }
}
