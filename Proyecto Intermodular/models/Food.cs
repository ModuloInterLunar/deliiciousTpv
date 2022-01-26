using Proyecto_Intermodular.api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Food : Dish
    {
        private IngredientQty ingredients;
        public Food(string id, string name, float price, string description, IngredientQty ingredients) : base(id, name, price, description)
        {
            this.ingredients = ingredients;
        }

        public Food(DishModel food)
        {
            Id = food.Id;
            Name = food.Name;
            Price = food.Price;
            Description = food.Description;
            ingredients = food.Ingredients;
        }

        public Food()
        {

        }

        public IngredientQty Ingredients { get => ingredients; set => ingredients = value; }
    }
}
