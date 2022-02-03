using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Proyecto_Intermodular.models;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para IngredientQtyItem.xaml
    /// </summary>
    public partial class IngredientQtyItem : UserControl
    {

        public IngredientQtyItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string IngredientName { set => lblName.Content = value; get => lblName.Content.ToString(); }
        public string IngredientMeasure { set => lblMeasure.Content = value; get => lblMeasure.Content.ToString(); }
        public string IngredientQuantity { set => inputQuantity.Text = value; get => inputQuantity.Text; }
        public Ingredient BaseIngredient { set; get; }
    }
}
