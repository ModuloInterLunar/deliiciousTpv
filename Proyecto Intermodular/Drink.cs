using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular
{
    class Drink : Dish
    {
        float quantity;
        public Drink(float quantity, string id, float price, string description) : base(id, price, description)
        {
            this.quantity = quantity;
        }

        public float Quantity { get => quantity; set => quantity = value; }
    }
}
