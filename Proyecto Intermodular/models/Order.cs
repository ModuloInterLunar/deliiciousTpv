namespace Proyecto_Intermodular.models
{
    public class Order
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

        public Order(string id, string ticket, string dish, string description, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            isServed = false;
            isIncluded = false;
            this.description = description;
            this.employee = employee;
        }

        public Order(string id, string ticket, string dish, bool isServed, bool isIncluded, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.isServed = isServed;
            this.isIncluded = isIncluded;
            this.employee = employee;
        }

        public Order(string id, string ticket, string dish, bool isServed, bool isIncluded, string description, Employee employee)
        {
            this.id = id;
            this.ticket = ticket;
            this.dish = dish;
            this.isServed = isServed;
            this.isIncluded = isIncluded;
            this.description = description;
            this.employee = employee;
        }

        public Order() { }

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
