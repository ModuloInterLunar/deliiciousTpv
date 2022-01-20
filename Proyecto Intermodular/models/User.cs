using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular.models
{
    class User
    {
        private String token;
        private String username;
        private bool isAdmin;
        private String id;

        public string Token { get => token; set => token = value; }
        public string Username { get => username; set => username = value; }
        public bool Isadmin { get => isAdmin; set => isAdmin = value; }
        public string Id { get => id; set => id = value; }

    }
}
