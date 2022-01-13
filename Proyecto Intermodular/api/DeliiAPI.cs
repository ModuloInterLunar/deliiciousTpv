using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace Proyecto_Intermodular.api
{
    public static class DeliiAPI
    {
        private static string API_URL = "http://localhost:8080/";
        public static Employee GetEmployeeFromToken()
        {
            string token = ApplicationState.GetValue<string>("token");
            string path = "api/employees/-1";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL + path);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string result = ReadResponse(httpResponse);

                Employee employee = JsonSerializer.Deserialize<Employee>(result, GetJsonOptions());

                return employee;
            }
            catch (WebException webException)
            {
                HandleWebException(webException);
                return null;
            }
        }

        public static string Login(string username, string password)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL + "login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonSerializer.Serialize(new
                {
                    username,
                    password
                }, 
                GetJsonOptions());

                streamWriter.Write(json);
            }

            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                
                string result = ReadResponse(httpResponse);

                TokenResponse tokenResponse = JsonSerializer.Deserialize<TokenResponse>(result, GetJsonOptions());

                return tokenResponse.Token;
            } 
            catch (WebException webException)
            {
                HandleWebException(webException);
            }
            return null;
        }

        public static Employee CreateEmployee(Employee employee)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/api/employees");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonSerializer.Serialize(employee, GetJsonOptions());

                streamWriter.Write(json);
            }


            string employeeJson = HandleResponse(httpWebRequest);

            Employee emp = JsonSerializer.Deserialize<Employee>(employeeJson, GetJsonOptions());

            return emp;

        }

        public static List<Employee> GetAllEmployees()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/api/employees");
            httpWebRequest.Method = "GET";

            string employeesJson = HandleResponse(httpWebRequest);

            Thread.Sleep(5000);

            Console.WriteLine("Not sleeping");

            if (employeesJson == null) return null;

            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(employeesJson, GetJsonOptions());

            return employees;
        }


        private static string HandleResponse(HttpWebRequest httpWebRequest)
        {
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string result = ReadResponse(httpResponse);
                return result;
            }
            catch (WebException webException)
            {
                return HandleWebException(webException);
            }
        }

        private static string HandleWebException(WebException webException)
        {
            HttpWebResponse httpResponse = (HttpWebResponse)webException.Response;
            string result = ReadResponse(httpResponse);
            try
            {
                JsonErrorResponse jsonErrorResponse = JsonSerializer.Deserialize<JsonErrorResponse>(result, GetJsonOptions());
                string error = jsonErrorResponse.Message;

                if (error == "Employee not found!")
                    throw new UserNotFoundException(error);
                else
                    throw new DeliiApiException(error);
            }
            catch
            {
                throw new DeliiApiException(result);
            }

        }

        private static string ReadResponse(HttpWebResponse httpResponse)
        {
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }

        }


        private static JsonSerializerOptions GetJsonOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return options;
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
}
