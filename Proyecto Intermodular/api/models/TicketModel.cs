using Proyecto_Intermodular.models;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Intermodular.api.models
{
    public class TicketModel
    {
        private string id;
        private double total;
        private string text;
        private bool isPaid;
        private List<string> orders;

        public TicketModel() { }


        public TicketModel(Ticket ticket){
            id = ticket.Id;
            total = ticket.Total;
            text = ticket.Text;
            isPaid = ticket.IsPaid;
            orders = ticket.Orders.Select(order => order.Id).ToList() ?? new List<string>();
        }

        public string Id { get => id; set => id = value; }
        public double Total { get => total; set => total = value; }
        public string Text { get => text; set => text = value; }
        public bool IsPaid { get => isPaid; set => isPaid = value; }
        public List<string> Orders { get => orders; set => orders = value; }
    }
}
