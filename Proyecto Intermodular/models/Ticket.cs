using System;
using System.Collections.Generic;
using Proyecto_Intermodular.api;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Intermodular.userControls;

namespace Proyecto_Intermodular.models
{
    public class Ticket
    {
        private string id;
        private double total;
        private string text;
        private bool isPaid;
        private string createdAt;
        private string updatedAt;
        private List<Order> orders;
        private TicketItem ticketItem;

        // Cosntructor por defecto, se llamará cuando creamos un ticket nuevo
        
        public Ticket() { }


        public Ticket(string id, double total, string text, bool isPaid, List<Order> orders){
            this.id = id;
            this.total = total;
            this.text = text;
            this.isPaid = isPaid;
            this.orders = orders;
        }

        public string Id { get => id; set => id = value; }
        public double Total { get => total; set => total = value; }
        public string Text { get => text; set => text = value; }
        public bool IsPaid { get => isPaid; set => isPaid = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public List<Order> Orders { get => orders; set => orders = value; }
        public TicketItem TicketItem { get => ticketItem; set => ticketItem = value; }
        public string PriceFormatted => total.ToString("0.00") + "€";
        public string PriceNoIVAFormatted => (total / 1.1).ToString("0.00") + "€";
        public string IVAFormatted => (total - total / 1.1).ToString("0.00") + "€";
        public string Hour => updatedAt.Substring(11, 5);
        public string Date => updatedAt.Substring(0, 10);

        public async Task AddOrder(Order order)
        {
            if (orders == null) orders = new();
            orders.Add(order);
            await DeliiApi.UpdateTicket(this);
        }
        public async Task RemoveOrder(Order order)
        {
            await DeliiApi.RemoveOrder(order);
            orders.Remove(order);
            await DeliiApi.UpdateTicket(this);
        }

        public void UpdateData(Ticket updatedTicket)
        {
            total = updatedTicket.total;
            text = updatedTicket.text;
            isPaid = updatedTicket.isPaid;
            createdAt = updatedTicket.createdAt;
            updatedAt = updatedTicket.updatedAt;
            if (orders == null)
            {
                orders = updatedTicket.orders;
                return;
            }
            if (updatedTicket.orders == null)
            {
                orders = null;
                return;
            }
            updatedTicket.orders.ForEach(updatedOrder =>
            {
                Order order = orders.Find(order => order.Id == updatedOrder.Id);
                if (order != null)
                    order.UpdateData(updatedOrder);
                else
                    orders.Add(updatedOrder);
            });

            // Returns the orders from this list if they are in the updated list
            orders = orders.FindAll(order =>
                updatedTicket.orders.Find(updatedOrders =>
                    order.Id == updatedOrders.Id
                ) != null
            );
        }
    }
}
