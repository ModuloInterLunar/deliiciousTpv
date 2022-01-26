﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public abstract class Dish
    {
        private string id;
        private string name;
        private string type;
        private float price;
        private string description;
        private string image;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }

        public Dish(string id, string name, float price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public Dish(string id, string name, float price, string description)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
        }

        protected Dish()
        {

        }
    }
}
