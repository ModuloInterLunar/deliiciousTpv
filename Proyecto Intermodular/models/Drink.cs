using Proyecto_Intermodular.api.models;

namespace Proyecto_Intermodular.models
{
    public class Drink : Dish
    {
        private float quantity;
        public Drink(string id, string name, float price, string description, float quantity) : base(id, name, price, description)
        {
            this.quantity = quantity;
        }

        public Drink() : base() { }

        public Drink(FoodOrDrink drink)
        {
            Id = drink.Id;
            Name = drink.Name;
            Price = drink.Price;
            Description = drink.Description;
            quantity = drink.Quantity;
        }

        public float Quantity { get => quantity; set => quantity = value; }
    }
}
