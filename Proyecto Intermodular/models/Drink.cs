﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Drink : Dish
    {
        private float quantity;
        public Drink(string id, string name, float price, string description, float quantity) : base(id, name, price, description)
        {
            this.quantity = quantity;
        }

        public Drink() : base()
        {

        }

        public float Quantity { get => quantity; set => quantity = value; }
    }
}
