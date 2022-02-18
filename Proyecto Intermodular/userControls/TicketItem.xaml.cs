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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para TicketItem.xaml
    /// </summary>
    public partial class TicketItem : UserControl
    {
        public TicketItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string PriceNoIVA { set => priceNoIVA.Text = value; get => priceNoIVA.Text; }
        public string IVA { set => iva.Text = value; get => iva.Text; }
        public string TotalPrice { set => totalPrice.Text = value; get => totalPrice.Text; }
        public string EmployeeName { set => employeeName.Text = value; get => employeeName.Text; }
        public string Hour { set => lblHora.Content = value; get => lblHora.Content.ToString(); }
        public string Date { set => lblFecha.Content = value; get => lblFecha.Content.ToString(); }

        public void AddOrders(List<Order> orders)
        {
            List<IGrouping<string, Order>> ordersGrouped = orders.GroupBy(order => order.Dish.Id).ToList();

            ordersGrouped.ForEach(orderIGrouping =>
            {
                List<Order> orders = orderIGrouping.ToList();

                orderNameStackPanel.Children.Add(new Label() 
                {
                    Content = orders.Count + " x " + orders[0].Dish.Name
                });
                orderPriceStackPanel.Children.Add(new Label()
                {
                    Content = orders[0].Dish.formattedPrice
                });
                orderImportStackPanel.Children.Add(new Label()
                {
                    Content = (orders[0].Dish.Price * orders.Count).ToString("0.00") + "€"
                });
            });
        }
    }
}
