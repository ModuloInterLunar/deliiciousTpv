using Proyecto_Intermodular.userControls;

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

        public Order(string id, string ticket, Dish dish, bool isServed, bool isIncluded, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.hasBeenServed = isServed;
            this.isIncluded = isIncluded;
            this.employee = employee;
        }

        public Order(string id, string ticket, Dish dish, bool isServed, bool isIncluded, string description, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.hasBeenServed = isServed;
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
            
        }
        public OrderItem OrderItem { get => orderItem; set => orderItem = value; }
        public bool HasBeenCoocked { get => hasBeenCoocked; set => hasBeenCoocked = value; }
    }
}
