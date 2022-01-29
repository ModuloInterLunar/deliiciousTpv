using System;
using System.Collections.Generic;
using Proyecto_Intermodular.api;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Cosntructor por defecto, se llamará cuando creamos un ticket nuevo
        public Ticket()
        {
            /*total = 0.0f;
            text = "";
            isPaid = false;
            orders = new();*/
        }


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

        public async Task AddOrder(Order order, Ticket actualTicket)
        {
            orders.Add(order);
            actualTicket = await DeliiApi.UpdateTicket(actualTicket);
        }
        public async Task RemoveOrder(Order order, Ticket actualTicket)
        {
            DeliiApi.RemoveOrder(order);
            orders.Remove(order);
            actualTicket = await DeliiApi.UpdateTicket(actualTicket);
        }
    }
}
