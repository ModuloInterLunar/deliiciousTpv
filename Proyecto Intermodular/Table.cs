
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular
{
    class Table
    {

        private int id;
        private double posXPercent;
        private double posYPercent;
        private double posX;
        private double posY;
        private Border border;

        public Table(int id, double posX, double posY)
        {
            this.id = id;
            this.posX = posX;
            this.posY = posY;
        }

        public int Id { get => id; set => id = value; }
        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }
        public Border Border { get => border; set => border = value; }

        private void UpdatePosition(double newWidth, double newHeight)
        {
            posX = posXPercent * newWidth;
            posY = posYPercent * newHeight;
        }

    }
}
