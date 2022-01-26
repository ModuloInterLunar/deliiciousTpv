using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class TicketModel
    {
        private string id;
        private float total;
        private string text;
        private bool isPaid;
        private string createdAt;
        private string updatedAt;
        private List<string> orders;

        public TicketModel() { }


        public TicketModel(Ticket ticket){
            id = ticket.Id;
            total = ticket.Total;
            text = ticket.Text;
            isPaid = ticket.IsPaid;
            orders = ticket.Orders.Select(order => order.Id).ToList();
        }

        public string Id { get => id; set => id = value; }
        public float Total { get => total; set => total = value; }
        public string Text { get => text; set => text = value; }
        public bool IsPaid { get => isPaid; set => isPaid = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public List<string> Orders { get => orders; set => orders = value; }
    }
}
