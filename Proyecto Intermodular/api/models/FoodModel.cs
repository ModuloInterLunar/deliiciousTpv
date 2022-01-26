using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class FoodModel
    {
        private string id;
        private string name;
        private float price;
        private string description;
        private string image;
        private IngredientQty ingredients;

        public FoodModel(Food food)
        {
            Id = food.Id;
            Name = food.Name;
            Price = food.Price;
            Description = food.Description;
            ingredients = food.Ingredients;
        }

        public FoodModel() { }

        public IngredientQty Ingredients { get => ingredients; set => ingredients = value; }
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }
    }
}
