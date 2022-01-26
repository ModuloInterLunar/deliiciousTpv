using System.Windows.Controls;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para OrderModel.xaml
    /// </summary>
    public partial class OrderModel : UserControl
    {
        public OrderModel()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string DishName { set => lblDishName.Content = value; get => lblDishName.Content.ToString(); }
        public string EmployeeName { set => lblEmployeeName.Content = value; get => lblEmployeeName.Content.ToString(); }
        public bool IsServed { set => ckBoxIsServed.IsChecked = value; get => ckBoxIsServed.IsChecked.Value; }
    }
}
