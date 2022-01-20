using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Order
    {
        string id;
        string ticket;
        string dish;
        bool isServed;
        bool isIncluded;
        string employee;

        public Order(string id, string ticket, string dish, string employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            isServed = false;
            isIncluded = false;
            this.employee = employee;
        }

        public Order(string id, string ticket, string dish, bool isServed, bool isIncluded, string employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.isServed = isServed;
            this.isIncluded = isIncluded;
            this.employee = employee;
        }

        public string Id { get => id; set => id = value; }
        public string Ticket { get => ticket; set => ticket = value; }
        public string Dish { get => dish; set => dish = value; }
        public bool IsServed { get => isServed; set => isServed = value; }
        public bool IsIncluded { get => isIncluded; set => isIncluded = value; }
        public string Employee { get => employee; set => employee = value; }
    }
}
