using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public abstract class Dish
    {
        private string id;
        private float price;
        private string description;

        protected Dish(string id, float price, string description)
        {
            this.id = id;
            this.price = price;
            this.description = description;
        }

        public string Id { get => id; set => id = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
    }
}
