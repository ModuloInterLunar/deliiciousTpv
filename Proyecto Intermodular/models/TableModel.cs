using System;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular.models
{
    public class TableModel
    {
        private string id;
        private double posX;
        private double posY;
        private double posXRelative;
        private double posYRelative;
        private double width;
        private double height;
        private string actualTicket;

        public TableModel(Table table)
        {
            id = table.Id;
            posX = table.PosX;
            posY = table.PosY;
            width = table.Width;
            height = table.Height;
            actualTicket = table.ActualTicket.Id;
        }

        public TableModel() { }


        public string Id { get => id; set => id = value; }
        public double PosXRelative { get => posXRelative; set => posXRelative = value; }
        public double PosYRelative { get => posYRelative; set => posYRelative = value; }
        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public string ActualTicket { get => actualTicket; set => actualTicket = value; }

    }
}
