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
            List<Ticket> updatedTickets = await DeliiApi.GetAllTicketsPaid();


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
            ticket.TicketItem.TotalPrice = ticket.PriceFormatted;
            ticket.TicketItem.EmployeeName = ticket.Orders[0].Employee.FullName;
            ticket.TicketItem.IVA = ticket.IVAFormatted;
            ticket.TicketItem.PriceNoIVA = ticket.PriceNoIVAFormatted;
            ticket.TicketItem.Hour = ticket.Hour;
            ticket.TicketItem.Date = ticket.Date;
        }

        private void GenerateTicketItem(Ticket ticket)
        {
            TicketItem ticketItem = new()
            {
                TotalPrice = ticket.PriceFormatted,
                EmployeeName = ticket.Orders[0].Employee.FullName,
                Margin = new(10),
                IVA = ticket.IVAFormatted,
                PriceNoIVA = ticket.PriceNoIVAFormatted,
                Hour = ticket.Hour,
                Date = ticket.Date
            };

            ticketItem.AddOrders(ticket.Orders);
            ticket.TicketItem = ticketItem;
            stackTickets.Children.Add(ticketItem);
        }

    }
}
