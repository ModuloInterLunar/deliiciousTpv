using System.Windows.Controls;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para IngredientItem.xaml
    /// </summary>
    public partial class IngredientItem : UserControl
    {
        public IngredientItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string IngredientName { set => lblName.Content = value; get => lblName.Content.ToString(); }
        public string Quantity { set => lblQuantity.Content = value; get => lblQuantity.Content.ToString(); }

        
    }
}
