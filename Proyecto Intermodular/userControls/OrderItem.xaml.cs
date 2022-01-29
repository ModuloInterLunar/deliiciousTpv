﻿using System.Windows.Controls;
using System.Windows.Media;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para OrderModel.xaml
    /// </summary>
    public partial class OrderItem : UserControl
    {
        public OrderItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string DishName { set => lblDishName.Text = value; get => lblDishName.Text.ToString(); }
        public ImageSource DishImage { set => imgDish.Source = value; get => imgDish.Source; }
        public string DishPrice { set => lblPrice.Content = value; get => lblPrice.Content.ToString(); }
        public string DescriptionInput { set => txtBoxDescription.Text = value; get => txtBoxDescription.Text; }
    }
}
