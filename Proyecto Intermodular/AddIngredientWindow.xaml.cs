using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para AddIngredientWindow.xaml
    /// </summary>
    public partial class AddIngredientWindow : Window
    {
        private Ingredient ingredient;
        private Ingredient updatedIngredient;
        private float quantity;
        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        public Ingredient Ingredient { get => ingredient; set => ingredient = value; }
        public Ingredient UpdatedIngredient { get => updatedIngredient; set => updatedIngredient = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!float.TryParse(tbQuantity.Text, out quantity))
            {
                MessageBox.Show("Entre un número entero o decimal válido!");
                return;
            }
            if (ingredient != null)
            {
                UpdateIngredient();
                return;
            }
            CreateIngredient();
        }


        private async void CreateIngredient()
        {
            ingredient = new();
            GetData();
            try
            {
                updatedIngredient = await DeliiApi.CreateIngredient(ingredient);
            } 
            catch (AlreadyInUseException)
            {
                ingredient = null;
                MessageBox.Show("Este ingrediente ya existe!");
                return;
            }
            Close();
        }

        private async void UpdateIngredient()
        {
            GetData();
            try
            {
                updatedIngredient = await DeliiApi.UpdateIngredient(ingredient);
            }
            catch (AlreadyInUseException)
            {
                MessageBox.Show("Este ingrediente ya existe!");
                return;
            }
            Close();
        }

        private void GetData()
        {
            ingredient.Name = tbName.Text;
            ingredient.Quantity = quantity;
            ingredient.Measure = cbMeasure.Text;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (ingredient != null)
            {
                tbName.Text = ingredient.Name;
                tbQuantity.Text = ingredient.Quantity.ToString("#.##");
                cbMeasure.Text = ingredient.Measure;
                btnSave.Content = "Modificar Ingrediente";
            }
        }
    }
}
