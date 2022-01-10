using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular
{
    class Order
    {
        string id;
        string ticket;
        string dish;
        bool isServed;
        bool isIncluded;
        string staff;

        public Order(string id, string ticket, string dish, string staff)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            isServed = false;
            isIncluded = false;
            this.staff = staff;
        }

        public Order(string id, string ticket, string dish, bool isServed, bool isIncluded, string staff)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.isServed = isServed;
            this.isIncluded = isIncluded;
            this.staff = staff;
        }

        public string Id { get => id; set => id = value; }
        public string Ticket { get => ticket; set => ticket = value; }
        public string Dish { get => dish; set => dish = value; }
        public bool IsServed { get => isServed; set => isServed = value; }
        public bool IsIncluded { get => isIncluded; set => isIncluded = value; }
        public string Staff { get => staff; set => staff = value; }
    }
}
