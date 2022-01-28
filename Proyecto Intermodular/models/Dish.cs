using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Dish
    {
        private string id;
        private string name;
        private string type;
        private double price;
        private string description;
        private string image;
        private List<IngredientQty> ingredientQties;


        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public double Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }
        public List<IngredientQty> IngredientQties { get => ingredientQties; set => ingredientQties = value; }

        public override string ToString()
        {
            string ingredients = "";
            ingredientQties.ForEach(ingredientQty => ingredients += ingredientQty.Ingredient.Name + ": " + ingredientQty.Quantity + ", ");
            return $"Id: { id }, Name: { name }, Type: { type }, Price: { price }, Description: { description }, IngredientQties: { ingredients }, Image: { image }";
        }

        public Dish() { }

        public Dish(string id, string name, float price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public Dish(string id, string name, float price, string description, List<IngredientQty> ingredientQties)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
            this.ingredientQties = ingredientQties;
        }

    }
}
