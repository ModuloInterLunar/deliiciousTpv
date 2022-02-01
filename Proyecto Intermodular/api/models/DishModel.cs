using Proyecto_Intermodular.models;
using System.Collections.Generic;

namespace Proyecto_Intermodular.api.models
{
    public class DishModel
    {
        private string id;
        private string name;
        private string type;
        private List<IngredientQtyModel> ingredientQties;
        private double price;
        private string description;
        private string image;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public List<IngredientQtyModel> IngredientsQties { get => ingredientQties; set => ingredientQties = value; }
        public double Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }

        public DishModel(Dish dish)
        {
            id = dish.Id;
            name = dish.Name;
            ingredientQties = dish.IngredientQties.ConvertAll(ingredientQty => new IngredientQtyModel(ingredientQty));
            price = dish.Price;

        }

        public DishModel() { }
    }
}
