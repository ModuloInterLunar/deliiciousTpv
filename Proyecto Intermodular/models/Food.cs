using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Food : Dish
    {
        Dictionary<string, float> ingredients;
        public Food(Dictionary<string, float> ingredients, string id, float price, string description) : base(id, price, description)
        {
            this.ingredients = ingredients;
        }

        public Dictionary<string, float> Ingredients { get => ingredients; set => ingredients = value; }
    }
}
