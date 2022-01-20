using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    public class Employee
    {
        private string id;
        private string username;
        private string dni;
        private string name;
        private string surname;
        private string password;
        private bool isAdmin;

        public string Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public string FullName => Capitalize(name) + " " + Capitalize(surname);


        public override string ToString() => $"id: {id}, username: {username}, dni: {dni}, name: {name}, surname: {surname}, password: {password}, isAdmin: {isAdmin}";

        public Employee(string id, string username, string dni, string name, string surname, string password, bool isAdmin)
        {
            Id = id;
            Username = username;
            Dni = dni;
            Name = name;
            Surname = surname;
            Password = password;
            IsAdmin = isAdmin;
        }

        public Employee(string username, string dni, string name, string surname, string password, bool isAdmin)
        {
            Username = username;
            Dni = dni;
            Name = name;
            Surname = surname;
            Password = password;
            IsAdmin = isAdmin;
        }

        public Employee()
        {

        }

        private String Capitalize(string str) => char.ToUpper(str[0]) + str.Substring(1);
    }
}
