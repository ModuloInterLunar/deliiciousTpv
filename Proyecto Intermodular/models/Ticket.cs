using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Ticket
    {
        private string id;
        private float total;
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


        public Ticket(string id, float total, string text, bool isPaid, List<Order> orders){
            this.id = id;
            this.total = total;
            this.text = text;
            this.isPaid = isPaid;
            this.orders = orders;
        }

        public string Id { get => id; set => id = value; }
        public float Total { get => total; set => total = value; }
        public string Text { get => text; set => text = value; }
        public bool IsPaid { get => isPaid; set => isPaid = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public List<Order> Orders { get => orders; set => orders = value; }

        public void AddOrder(Order order) => orders.Add(order);
        public void RemoveOrder(Order order) => orders.Remove(order);
    }
}
