using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class OrderModel
    {
        private string id;
        private string ticket;
        private string dish;
        private bool isServed;
        private bool isIncluded;
        private string description;
        private string createdAt;
        private string updatedAt;
        private Employee employee;
        private string table;

        public OrderModel(Order order)
        {
            this.id = order.Id;
            this.ticket = order.Ticket;
            this.dish = order.Dish.Id;
            isServed = order.IsServed;
            isIncluded = order.IsIncluded;
            this.description = order.Description;
            this.employee = order.Employee;
        }

        public OrderModel() { }

        public string Id { get => id; set => id = value; }
        public string Ticket { get => ticket; set => ticket = value; }
        public string Dish { get => dish; set => dish = value; }
        public bool IsServed { get => isServed; set => isServed = value; }
        public bool IsIncluded { get => isIncluded; set => isIncluded = value; }
        public Employee Employee { get => employee; set => employee = value; }
        public string Description { get => description; set => description = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public string Table { get => table; set => table = value; }
    }
}
