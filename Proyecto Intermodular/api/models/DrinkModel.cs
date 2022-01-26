using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class DrinkModel
    {
        private string id;
        private float price;
        private string description;
        private float quantity;
        public DrinkModel(Drink drink)
        {
            Id = drink.Id;
            Price = drink.Price;
            Description = drink.Description;
            quantity = drink.Quantity;
        }

        public DrinkModel() : base() { }

        public float Quantity { get => quantity; set => quantity = value; }
        public string Id { get => id; set => id = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
    }
}
