using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Menu
    {
        private string id;
        private float price;
        private List<Dish> dishes;
        private string image;

        public Menu() { }

        public string Id { get => id; set => id = value; }
        public float Price { get => price; set => price = value; }
        public List<Dish> Dishes { get => dishes; set => dishes = value; }
        public string Image { get => image; set => image = value; }
    }
}
