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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System.Windows.Threading;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para KitchenItem.xaml
    /// </summary>
    public partial class KitchenItem : UserControl
    {

        public KitchenItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public String TableKitchen { set => lblTableKitchen.Content = value; get => lblTableKitchen.Content.ToString(); }

        public String EmployeeKitchen { set => lblEmployeeKitchen.Content = value; get => lblEmployeeKitchen.Content.ToString(); }
        
        public String DishKitchen { set => lblDishKitchen.Content = value; get => lblDishKitchen.Content.ToString(); }

        public String TimerKitchen { set => lblTimerKitchen.Content = value; get => lblTimerKitchen.Content.ToString(); }

        public String DescriptionKitchen { set => lblDescriptionKitchen.Text = value; get => lblDescriptionKitchen.Text.ToString(); }

        public ImageSource ImageKitchen { set => imgImageKitchen.Source = value; get => imgImageKitchen.Source; }

        public ImageSource DishImage { set => imgDishImage.Source = value; get => imgDishImage.Source; }
    }
}
        