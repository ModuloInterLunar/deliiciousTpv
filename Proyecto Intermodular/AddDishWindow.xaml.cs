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
using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        private Dish dish;
        private Dish updatedDish;
        public AddDishWindow()
        {
            InitializeComponent();
        }
        public Dish Dish { get => dish; set => dish = value; }
        public Dish UpdatedDish { get => updatedDish; set => updatedDish = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dish != null)
            {
                UpdateDish();
                return;
            }
            CreateDish();
        }


        private async void CreateDish()
        {
            dish = new();
            GetData();
            try
            {
                updatedDish = await DeliiApi.CreateDish(dish);
            }
            catch (AlreadyInUseException)
            {
                dish = null;
                MessageBox.Show("Este plato ya existe!");
                return;
            }
            Close();
        }

        private async void UpdateDish()
        { 
            GetData();
            try
            {
                updatedDish = await DeliiApi.UpdateDish(dish);
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
            double price;
            if (!double.TryParse(tbPrice.Text, out price))
            {
                MessageBox.Show("Entre un precio entero o decimal válido!");
                return;
            }

            dish.Name = tbName.Text;
            if (cbDishType.Text == "Comida") 
            {
                dish.Type = "Food";
            } 
            else
            {
                dish.Type = "Drink";
            }
            
            dish.Price = price;
            dish.Description = tbDescription.Text;
            dish.Image = tbImageURL.Text;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (dish != null)
            {
                tbName.Text = dish.Name;
                cbDishType.Text = dish.Type;
                tbPrice.Text = dish.Price.ToString("0.00");
                tbDescription.Text = dish.Description;
                tbImageURL.Text = dish.Image;
                btnSave.Content = "Modificar Plato";
            }
        }
    }
}
