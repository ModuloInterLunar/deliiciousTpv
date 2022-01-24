using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Food : Dish
    {
        private Dictionary<string, float> ingredients;
        public Food(string id, float price, string description, Dictionary<string, float> ingredients) : base(id, price, description)
        {
            this.ingredients = ingredients;
        }

        public Food()
        {

        }

        public Dictionary<string, float> Ingredients { get => ingredients; set => ingredients = value; }
    }
}
