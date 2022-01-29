using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api.models;
using System.Linq;

namespace Proyecto_Intermodular.api
{
    public static class DeliiApi
    {
        private static string URL = "http://localhost:8080/";
        private static string API_URL = URL + "api/";

        public static async Task<Employee> GetEmployeeFromToken()
        {
            string uri = API_URL + "employees/-1";
            string employeeJson = await DeliiApiClient.Get(uri);

            Employee employee = JsonSerializer.Deserialize<Employee>(employeeJson, DeliiApiClient.GetJsonOptions());
            return employee;
        }

        public static async Task<string> Login(string username, string password)
        {
            string uri = URL + "login";
            Authentifier auth = new Authentifier(username, password);

            try
            {
                string tokenJson = await DeliiApiClient.Post(uri, auth);
                TokenResponse tokenResponse = JsonSerializer.Deserialize<TokenResponse>(tokenJson, DeliiApiClient.GetJsonOptions());

                return tokenResponse.Token;
            }
            catch (DeliiApiException ex)
            {
                if (ex.Message.Contains("Employee not found"))
                    throw new UserNotFoundException(ex.Message);
                if (ex.Message.Contains("Invalid password"))
                    throw new WrongCredentialsException(ex.Message);
                throw new DeliiApiException(ex.Message);
            }
        }

        public static async Task<Employee> CreateEmployee(Employee employee)
        {
            string uri = API_URL + "employees";
            string employeeJson = await DeliiApiClient.Post(uri, employee);

            Employee emp = JsonSerializer.Deserialize<Employee>(employeeJson, DeliiApiClient.GetJsonOptions());

            return emp;
        }

        public static async Task<Ticket> GetTicket(string ticketId)
        {
            string uri = $"{API_URL}tickets/{ticketId}";
            string ticketJson = await DeliiApiClient.Get(uri);

            Ticket ticket = JsonSerializer.Deserialize<Ticket>(ticketJson, DeliiApiClient.GetJsonOptions());
            return ticket;
        }

        public static async Task<List<Employee>> GetAllEmployees()
        {
            string uri = API_URL + "employees";
            string employeesJson = await DeliiApiClient.Get(uri);

            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(employeesJson, DeliiApiClient.GetJsonOptions());

            return employees;
        }

        public static async Task<List<Table>> GetAllTables()
        {
            string uri = API_URL + "tables";
            string tablesJson = await DeliiApiClient.Get(uri);

            List<Table> tables = JsonSerializer.Deserialize<List<Table>>(tablesJson, DeliiApiClient.GetJsonOptions());

            return tables;
        }

        public static async Task<Table> CreateTable(Table table)
        {
            string uri = API_URL + "tables";

            try
            {
                TableModel tableModel = new TableModel(table);
                string createdTableJson = await DeliiApiClient.Post(uri, tableModel);
                Table createdTable = JsonSerializer.Deserialize<Table>(createdTableJson, DeliiApiClient.GetJsonOptions());

                return createdTable;
            }
            catch (DeliiApiException ex)
            {
                if (ex.Message.Contains("Can't create table!"))
                    throw new UserNotFoundException(ex.Message);
                throw new DeliiApiException(ex.Message);
            }
        }

        public static async Task<Order> CreateOrder(Order order)
        {
            string uri = API_URL + "orders";

            try
            {
                OrderModel orderModel = new(order);

                string createdOrderJson = await DeliiApiClient.Post(uri, orderModel);
                Order createdOrder = JsonSerializer.Deserialize<Order>(createdOrderJson, DeliiApiClient.GetJsonOptions());

                return createdOrder;
            }
            catch (DeliiApiException ex)
            {
                if (ex.Message.Contains("Can't create table!"))
                    throw new UserNotFoundException(ex.Message);
                throw new DeliiApiException(ex.Message);
            }
        }

        public static async Task<Table> UpdateTable(Table table)
        {
            string uri = API_URL + "tables/" + table.Id;
            TableModel tableModel = new TableModel(table);
            string updatedTableJson = await DeliiApiClient.Patch(uri, tableModel);
            Table updatedTable = JsonSerializer.Deserialize<Table>(updatedTableJson, DeliiApiClient.GetJsonOptions());

            return updatedTable;
        }

        public static async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            string uri = $"{API_URL}tickets/{ticket.Id}";
            TicketModel ticketModel = new TicketModel(ticket);
            string updatedTicketJson = await DeliiApiClient.Patch(uri, ticketModel);
            Ticket updatedTicket = JsonSerializer.Deserialize<Ticket>(updatedTicketJson, DeliiApiClient.GetJsonOptions());

            return updatedTicket;
        }

        public static async void RemoveTable(Table table)
        {
            string uri = API_URL + "tables/" + table.Id;

            await DeliiApiClient.Delete(uri);
        }

        public static async void RemoveOrder(Order order)
        {
            string uri = $"{API_URL}orders/{order.Id}";

            await DeliiApiClient.Delete(uri);
        }

        public static async Task<List<Order>> GetAllOrders()
        {
            string uri = API_URL + "orders";
            string ordersJson = await DeliiApiClient.Get(uri);

            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(ordersJson, DeliiApiClient.GetJsonOptions());

            return orders;
        }

        public static async Task<List<Dish>> GetAllDishes()
        {
            string uri = API_URL + "dishes";
            string dishesJson = await DeliiApiClient.Get(uri);

            List<Dish> dishes = JsonSerializer.Deserialize<List<Dish>>(dishesJson, DeliiApiClient.GetJsonOptions());

            return dishes;
        }

        internal static async Task<Ticket> CreateTicket()
        {
            string uri = API_URL + "tickets";
            TicketModel ticketModel = new();
            try
            {
                string createdTicketJson = await DeliiApiClient.Post(uri, ticketModel);
                Ticket createdTicket = JsonSerializer.Deserialize<Ticket>(createdTicketJson, DeliiApiClient.GetJsonOptions());

                return createdTicket;
            }
            catch (DeliiApiException ex)
            {
                if (ex.Message.Contains("Can't create table!"))
                    throw new UserNotFoundException(ex.Message);
                throw new DeliiApiException(ex.Message);
            }
        }
    }

    class Authentifier
    {
        private string username;
        private string password;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public Authentifier(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }


    class JsonErrorResponse
    {
        private string message;
        public string Message { get => message; set => message = value; }
    }

    class TokenResponse
    {
        private string token;
        public string Token { get => token; set => token = value; }
    }
}
