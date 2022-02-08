using Proyecto_Intermodular.models;

namespace Proyecto_Intermodular.api.models
{
    public class OrderModel
    {
        private string id;
        private string ticket;
        private string dish;
        private bool hasBeenServed;
        private bool hasBeenCooked;
        private bool isIncluded;
        private string description;
        private string createdAt;
        private string updatedAt;
        private string employee;
        private string table;

        public OrderModel(Order order)
        {
            this.id = order.Id;
            this.ticket = order.Ticket;
            this.dish = order.Dish.Id;
            this.hasBeenServed = order.HasBeenServed;
            this.hasBeenCooked = order.HasBeenCooked;
            this.isIncluded = order.IsIncluded;
            this.table = order.Table;
            this.description = order.Description;
            this.employee = order.Employee.Id;
        }

        public OrderModel() { }

        public string Id { get => id; set => id = value; }
        public string Ticket { get => ticket; set => ticket = value; }
        public string Dish { get => dish; set => dish = value; }
        public bool HasBeenServed { get => hasBeenServed; set => hasBeenServed = value; }
        public bool IsIncluded { get => isIncluded; set => isIncluded = value; }
        public string Employee { get => employee; set => employee = value; }
        public string Description { get => description; set => description = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public string Table { get => table; set => table = value; }
        public bool HasBeenCooked { get => hasBeenCooked; set => hasBeenCooked = value; }
    }
}
