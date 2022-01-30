using Proyecto_Intermodular.userControls;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular.models
{
    public class Order
    {
        private string id;
        private string ticket;
        private Dish dish;
        private bool hasBeenServed;
        private bool isIncluded;
        private bool hasBeenCoocked;
        private string description;
        private string createdAt;
        private string updatedAt;
        private Employee employee;
        private string table;
        private Border border;
        private OrderItem orderItem;

        public Order(string id, string ticket, Dish dish, string description, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            hasBeenServed = false;
            isIncluded = false;
            this.description = description;
            this.employee = employee;
        }

        public Order(string id, string ticket, Dish dish, bool hasBeenServed, bool isIncluded, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.hasBeenServed = hasBeenServed;
            this.isIncluded = isIncluded;
            this.employee = employee;
        }

        public Order(string id, string ticket, Dish dish, bool hasBeenServed, bool isIncluded, string description, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.hasBeenServed = hasBeenServed;
            this.isIncluded = isIncluded;
            this.description = description;
            this.employee = employee;
        }

        public Order() { }

        public string Id { get => id; set => id = value; }
        public string Ticket { get => ticket; set => ticket = value; }
        public Dish Dish { get => dish; set => dish = value; }
        public bool HasBeenServed { get => hasBeenServed; set => hasBeenServed = value; }
        public bool IsIncluded { get => isIncluded; set => isIncluded = value; }
        public Employee Employee { get => employee; set => employee = value; }
        public string Description { get => description; set => description = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public string Table { get => table; set => table = value; }

        public void UpdateData(Order updatedOrder)
        {
            ticket = updatedOrder.ticket;
            if (updatedOrder.dish == null)
                dish = null;
            else if (dish == null)
                dish = updatedOrder.dish;
            else
                dish.UpdateData(updatedOrder.dish);
            hasBeenServed = updatedOrder.hasBeenServed;
            isIncluded = updatedOrder.isIncluded;
            if (updatedOrder.employee == null)
                employee = null;
            else if (employee == null)
                employee = updatedOrder.employee;
            else
                employee.UpdateData(updatedOrder.employee);
            description = updatedOrder.description;
            createdAt = updatedOrder.createdAt;
            updatedAt = updatedOrder.updatedAt;
            table = updatedOrder.table;

        }

        public OrderItem OrderItem { get => orderItem; set => orderItem = value; }
        public bool HasBeenCoocked { get => hasBeenCoocked; set => hasBeenCoocked = value; }
        public Border Border { get => border; set => border = value; }
    }
}
