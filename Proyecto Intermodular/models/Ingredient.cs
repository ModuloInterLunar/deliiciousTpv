using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Ingredient
    {
        private string id;
        private string name;
        private float quantity;
        public Ingredient() { }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Quantity { get => quantity; set => quantity = value; }

        internal void UpdateData(Ingredient updatedIngredient)
        {
            name = updatedIngredient.id;
            quantity = updatedIngredient.quantity;
        }
    }
}
