using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular
{
    class Table
    {
        private static int DEFAULT_SIZE = 25;
        private int id;
        private double posXPercent;
        private double posYPercent;
        private double posX;
        private double posY;
        private double width;
        private double height;
        private Border border;

        public Table(int id, double posXPercent, double posYPercent, double width, double height)
        {
            this.id = id;
            this.posXPercent = posXPercent;
            this.posYPercent = posYPercent;
            this.width = width;
            this.height = height;
        }

        public Table(int id, double posXPercent, double posYPercent)
        {
            this.id = id;
            this.posXPercent = posXPercent;
            this.posYPercent = posYPercent;
            width = DEFAULT_SIZE;
            height = DEFAULT_SIZE;
        }

        public int Id { get => id; set => id = value; }
        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }
        public Border Border { get => border; set => border = value; }
        public double PosXPercent { get => posXPercent; set => posXPercent = value; }
        public double PosYPercent { get => posYPercent; set => posYPercent = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public override string ToString() => $"ID: {id}, BORDER: ({Canvas.GetLeft(border)}, {Canvas.GetTop(border)}, {border.Name})";

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

        public void ChangeTableSize(double widthIncrement = 0, double heightIncrement = 0)
        {
            width += widthIncrement;
            height += heightIncrement;
            border.Width = width;
            border.Height = height;
        }

        private void CorrectOutOfFrame(double frameWidth, double frameHeight)
        {
            if (posX + border.Width / 2 > frameWidth)
                posX = frameWidth - border.Width / 2;
            if (posY + border.Height / 2 > frameHeight)
                posY = frameHeight - border.Height / 2;
        }
    }
}
