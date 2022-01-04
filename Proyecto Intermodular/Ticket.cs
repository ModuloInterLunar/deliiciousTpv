using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular
{
    class Ticket
    {
        string id;
        float total;
        string text;
        bool isPaid;

        // Cosntructor por defecto, se llamará cuando creamos un ticket nuevo
        public Ticket()
        {
            id = ""; // Esto hay que cambiarlo por un id disponible que nos de la base de datos.
            total = 0.0f;
            text = "";
            isPaid = false;
        }

        // Este constructor se usará si el ticket creado lo sacamos de la base de datos.
        public Ticket(string id, float total, string ticket, bool isPaid){
            this.Id = id;
            this.total = total;
            this.text = ticket;
            this.isPaid = isPaid;
        }

        public string Id { get => id; set => id = value; }
        public float Total { get => total; set => total = value; }
        public bool IsPaid { get => isPaid; set => isPaid = value; }
        public string Text { get => text; set => text = value; }
    }
}
