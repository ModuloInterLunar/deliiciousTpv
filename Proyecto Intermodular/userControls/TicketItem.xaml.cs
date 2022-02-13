﻿using System;
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
    /// Lógica de interacción para TicketItem.xaml
    /// </summary>
    public partial class TicketItem : UserControl
    {
        public TicketItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string PriceNoIVA { set => priceNoIVA.Text = value; get => priceNoIVA.Text; }
        public string IVA { set => iva.Text = value; get => iva.Text; }
        public string TotalPrice { set => totalPrice.Text = value; get => totalPrice.Text; }
        public string EmployeeName { set => employeeName.Text = value; get => employeeName.Text; }
    }
}
