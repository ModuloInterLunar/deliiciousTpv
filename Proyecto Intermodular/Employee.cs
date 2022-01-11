using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular
{
    class Employee
    {
        private string id;
        private string username;
        private string dni;
        private string name;
        private string surname;
        private string password;
        private bool isAdmin;

        public string Id { get => id; set => id = value;}
        public string Username { get => username; set => username = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

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

        public Employee()
        {

        }

    }
}
