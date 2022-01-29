using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Orders.xaml
    /// </summary>
    public partial class DishItem : UserControl
    {
        public DishItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ImageSource DishImage { set => imgDish.ImageSource = value; get => imgDish.ImageSource; }
        public string DishName { set => txtBlockDishName.Text = value; get => txtBlockDishName.Text; }
        public string DishPrice { set => txtBlockPrice.Text = value; get => txtBlockPrice.Text; }
    }
}
