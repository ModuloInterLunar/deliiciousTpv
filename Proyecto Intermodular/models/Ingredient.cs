using Proyecto_Intermodular.userControls;
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
        private string measure = "uds.";
        private IngredientItem ingredientItem;
        public Ingredient() { }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public string Measure { get => measure; set => measure = value; }
        public string FormattedQuantity => quantity.ToString("#.##") + " " + Measure;
        public IngredientItem IngredientItem { get => ingredientItem; set => ingredientItem = value; }

        internal void UpdateData(Ingredient updatedIngredient)
        {
            name = updatedIngredient.name;
            quantity = updatedIngredient.quantity;
            measure = updatedIngredient.measure;
        }
    }
}
