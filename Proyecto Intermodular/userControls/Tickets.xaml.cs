using Proyecto_Intermodular.api;
using Proyecto_Intermodular.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Intermodular.userControls
{
    /// <summary>
    /// Lógica de interacción para Tickets.xaml
    /// </summary>
    public partial class Tickets : UserControl
    {
        List<Ticket> tickets;
        public Tickets()
        {
            InitializeComponent();
            UpdateStackTickets();
        }

        public async void UpdateStackTickets()
        {
            List<Ticket> receivedTickets = await DeliiApi.GetAllTickets();
            List<Ticket> updatedTickets = receivedTickets.FindAll(ticket => ticket.Total > 0);


            if (tickets == null)
            {
                tickets = updatedTickets;
                tickets.ForEach(ticket => GenerateTicketItem(ticket));
                return;
            }

            updatedTickets.ForEach(updatedTicket =>
            {
                Ticket ticket = tickets.Find(ticket => ticket.Id == updatedTicket.Id);
                if (ticket != null)
                {
                    ticket.UpdateData(updatedTicket);
                    UpdateTicketItem(ticket);
                }
                else
                {
                    tickets.Add(updatedTicket);
                    GenerateTicketItem(updatedTicket);
                }
            });

            RemoveOldTickets(updatedTickets);
        }

        private void RemoveOldTickets(List<Ticket> updatedTickets)
        {
            tickets = tickets.FindAll(ticket =>
            {
                Ticket updatedTicket = updatedTickets.Find(updatedTicket => ticket.Id == updatedTicket.Id);
                if (updatedTicket == null)
                {
                    stackTickets.Children.Remove(ticket.TicketItem);
                    return false;
                }
                return true;
            });
        }

        private void UpdateTicketItem(Ticket ticket)
        {
            ticket.TicketItem.TotalPrice = ticket.Total.ToString();
            ticket.TicketItem.EmployeeName = ApplicationState.GetValue<Employee>("current_user").FullName;
            ticket.TicketItem.PriceNoIVA = ticket.PriceNoIVAFormatted;
        }

        private void GenerateTicketItem(Ticket ticket)
        {
            TicketItem ticketItem = new()
            {
                TotalPrice = ticket.PriceFormatted,
                EmployeeName = ApplicationState.GetValue<Employee>("current_user").FullName,
                Margin = new(5),
                PriceNoIVA = ticket.PriceNoIVAFormatted
            };

            ticket.TicketItem = ticketItem;
            stackTickets.Children.Add(ticketItem);
        }
    }
}
