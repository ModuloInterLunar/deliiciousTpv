using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public abstract class DishModel
    {
        private string id;
        private string name;
        private string type;
        private float quantity;
        private IngredientQtyModel ingredients;
        private float price;
        private string description;
        private string image;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public IngredientQtyModel Ingredients { get => ingredients; set => ingredients = value; }
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
            ingredients = new IngredientQtyModel(food.Ingredients);
        }

        protected DishModel()
        {

        }
    }
}
