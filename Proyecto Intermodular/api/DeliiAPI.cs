﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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

        public static async Task<List<Employee>> GetAllEmployees()
        {
            string uri = API_URL + "employees";
            string employeesJson = await DeliiApiClient.Get(uri);

            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(employeesJson, DeliiApiClient.GetJsonOptions());

            return employees;
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
