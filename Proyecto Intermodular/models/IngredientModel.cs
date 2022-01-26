using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class IngredientModel
    {
        private string id;
        private string name;
        private float quantity;

        public IngredientModel(Ingredient ingredient)
        {
            id = ingredient.Id;
            name = ingredient.Name;
            quantity = ingredient.Quantity;
        }

        public IngredientModel() { }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Quantity { get => quantity; set => quantity = value; }
    }
}
