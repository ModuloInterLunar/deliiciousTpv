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

        public override string ToString() => $"Id: { id }, Name: { name }, Type: { type }, Price: { price }, Description: { description }, IngredientQties: { GetIngredients() }, Image: { image }";
        public string GetIngredients() => ingredientQties.Aggregate("", (acc, cur) => acc += $"{cur}\n");

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

        public void UpdateData(Dish updatedDish)
        {
            name = updatedDish.name;
            type = updatedDish.type;
            price = updatedDish.price;
            description = updatedDish.description;
            image = updatedDish.image;
            if (ingredientQties == null)
            {
                ingredientQties = updatedDish.ingredientQties;
                return;
            }
            updatedDish.ingredientQties.ForEach(updatedIngredientQty =>
            {
                IngredientQty ingredientQty = ingredientQties.Find(ingredientQty => ingredientQty.Ingredient == updatedIngredientQty.Ingredient);
                if (ingredientQty != null)
                    ingredientQty.UpdateData(updatedIngredientQty);
                else
                    ingredientQties.Add(updatedIngredientQty);
            });
        }
    }
}
