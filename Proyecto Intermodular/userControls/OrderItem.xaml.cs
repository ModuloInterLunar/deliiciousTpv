using System.Windows;
using System.Windows.Controls;

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
        public string DishPrice { set => lblPrice.Content = value; get => lblPrice.Content.ToString(); }
        public string DescriptionInput { set => txtBoxDescription.Text = value; get => txtBoxDescription.Text; }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}
