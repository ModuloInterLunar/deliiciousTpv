
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular
{
    class Table
    {
        public static int BORDER_SIZE = 25;
        private int id;
        private double posXPercent;
        private double posYPercent;
        private double posX;
        private double posY;
        private Border border;

        public Table(int id, double posXPercent, double posYPercent)
        {
            this.id = id;
            this.posXPercent = posXPercent;
            this.posYPercent = posYPercent;
        }

        public int Id { get => id; set => id = value; }
        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }
        public Border Border { get => border; set => border = value; }
        public double PosXPercent { get => posXPercent; set => posXPercent = value; }
        public double PosYPercent { get => posYPercent; set => posYPercent = value; }

        public void SetPosition(Point newPoint, double cnvWidth, double cnvHeight)
        {
            posXPercent = newPoint.X / cnvWidth;
            posYPercent = newPoint.Y / cnvHeight;
        }

        public void UpdatePosition(double newWidth, double newHeight)
        {
            posX = PosXPercent * newWidth;
            posY = PosYPercent * newHeight;
            CorrectOutOfFrame(newWidth, newHeight);
        }

        private void CorrectOutOfFrame(double frameWidth, double frameHeight)
        {
            if (posX + BORDER_SIZE > frameWidth)
                posX = frameWidth - BORDER_SIZE;
            if (posY + BORDER_SIZE > frameHeight)
                posY = frameHeight - BORDER_SIZE;
        }

    }
}
