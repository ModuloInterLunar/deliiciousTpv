using Proyecto_Intermodular.models;
using System.Windows.Controls;
using System.Windows.Media;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para OrderModel.xaml
    /// </summary>
    public partial class OrderItem : UserControl
    {
        private Dish dish;
        public OrderItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Dish Dish { set => dish = value; get => dish; }
        public string DishName { set => lblDishName.Text = value; get => lblDishName.Text.ToString(); }
        public ImageSource DishImage { set => imgDish.Source = value; get => imgDish.Source; }
        public string DishPrice { set => lblPrice.Content = value; get => lblPrice.Content.ToString(); }
        public string Description { set => txtBoxDescription.Text = value; get => txtBoxDescription.Text; }
        public bool IsDeleting { get; internal set; }
        public Brush Color { set => borderCard.Background = value; get => borderCard.Background; }
    }
}
