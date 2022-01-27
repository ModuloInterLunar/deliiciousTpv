using Proyecto_Intermodular.models;
using System.Collections.Generic;

namespace Proyecto_Intermodular.api.models
{
    public abstract class DishModel
    {
        private string id;
        private string name;
        private string type;
        private float quantity;
        private List<IngredientQtyModel> ingredientQties;
        private float price;
        private string description;
        private string image;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public List<IngredientQtyModel> IngredientsQties { get => ingredientQties; set => ingredientQties = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }

        protected DishModel(Drink drink)
        {
            id = drink.Id;
            name = drink.Name;
            quantity = drink.Quantity;
        }

        protected DishModel(Food food)
        {
            id = food.Id;
            name = food.Name;
            ingredientQties = food.IngredientQties.ConvertAll(ingredientQty => new IngredientQtyModel(ingredientQty));
        }

        protected DishModel() { }
    }
}
