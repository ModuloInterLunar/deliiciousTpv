using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
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
