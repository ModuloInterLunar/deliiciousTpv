using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular
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

        public override string ToString()
        {
            return String.Format("id: {0}, username: {1}, dni: {2}, name: {3}, surname: {4}, password: {5}, isAdmin: {6}", 
                id, username, dni, name, surname, password, isAdmin );
        }
    }
}
