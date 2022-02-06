using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class MenuModel
    {
        private string id;
        private float price;
        private string name;
        private List<string> dishes;
        private string image;

        public MenuModel(Menu menu)
        {
            Id = menu.Id;
            Price = menu.Price;
            Dishes = menu.Dishes.ConvertAll(dish => dish.Id);
            Name = menu.Name;
        }

        public string Id { get => id; set => id = value; }
        public float Price { get => price; set => price = value; }
        public List<string> Dishes { get => dishes; set => dishes = value; }
        public string Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
    }
}
