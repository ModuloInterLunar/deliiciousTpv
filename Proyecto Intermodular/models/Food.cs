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

        public Food()
        {

        }

        public IngredientQty Ingredients { get => ingredients; set => ingredients = value; }
    }
}
