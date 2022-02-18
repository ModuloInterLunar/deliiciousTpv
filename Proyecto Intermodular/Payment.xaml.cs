using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Payment : Window
    {
        private Ticket ticket;
        private Table table;

        public Payment(Table table, Ticket ticket)
        {
            InitializeComponent();
            this.table = table;
            this.ticket = ticket;
            LoadTicketData();
        }


        private void LoadTicketData()
        {
            ticket.Orders.ForEach(order => ticket.Total += order.Dish.Price);
            ucTicketItem.TotalPrice = ticket.PriceFormatted;
            ucTicketItem.EmployeeName = ticket.Orders[0].Employee.FullName;
            ucTicketItem.IVA = ticket.IVAFormatted;
            ucTicketItem.PriceNoIVA = ticket.PriceNoIVAFormatted;
            ucTicketItem.Hour = ticket.Hour;
            ucTicketItem.Date = ticket.Date;
            ucTicketItem.AddOrders(ticket.Orders);
        }

        private async void btnPay_Click(object sender, RoutedEventArgs e)
        {
            ticket.Orders.ForEach(order => ticket.Total += order.Dish.Price);
            ticket.IsPaid = true;
            Ticket updatedTicket = await DeliiApi.UpdateTicket(ticket);
            table.ActualTicket = null;
            Table updatedTable = await DeliiApi.UpdateTable(table);
            Close();
        }
    }
}
