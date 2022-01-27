using Proyecto_Intermodular.api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Food : Dish
    {
        private List<IngredientQty> ingredientQties;
        public Food(string id, string name, float price, string description, List<IngredientQty> ingredientQties) : base(id, name, price, description)
        {
            this.ingredientQties = ingredientQties;
        }

        public Food(FoodOrDrink food)
        {
            Id = food.Id;
            Name = food.Name;
            Price = food.Price;
            Description = food.Description;
            ingredientQties = food.IngredientQties;
        }

        public Food() { }

        public override string ToString()
        {
            string ingredients = "";
            ingredientQties.ForEach(ingredientQty => ingredients += ingredientQty.Ingredient.Name + ": " + ingredientQty.Quantity + ", " );
            return $"Id: {Id}, Name: {Name}, Price: {Price}, Description: {Description}, IngredientQties: { ingredients }";
        }

        public List<IngredientQty> IngredientQties { get => ingredientQties; set => ingredientQties = value; }
    }
}
