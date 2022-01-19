using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular
{
    public class Table
    {
        private readonly static int DEFAULT_SIZE = 25;
        private string id;
        private double posX;
        private double posY;
        private double posXRelative;
        private double posYRelative;
        private double width;
        private double height;
        private Border border;

        public Table(string id, double posX, double posY, double width, double height)
        {
            this.id = id;
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
        }

        public Table()
        {

        }

        public Table(string id, double posX, double posY)
        {
            this.id = id;
            this.posX = posX;
            this.posY = posY;
            width = DEFAULT_SIZE;
            height = DEFAULT_SIZE;
        }

        public string Id { get => id; set => id = value; }
        public double PosX { get => posXRelative; set => posXRelative = value; }
        public double PosY { get => posYRelative; set => posYRelative = value; }
        public Border Border { get => border; set => border = value; }
        public double PosXPercent { get => posX; set => posX = value; }
        public double PosYPercent { get => posY; set => posY = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public override string ToString() => $"ID: {id}, BORDER: ({Canvas.GetLeft(border)}, {Canvas.GetTop(border)}, {border.Name})";

        public void SetPosition(Point newPoint, double cnvWidth, double cnvHeight)
        {
            posX = newPoint.X / cnvWidth;
            posY = newPoint.Y / cnvHeight;
        }

        public void UpdatePosition(double newWidth, double newHeight)
        {
            posXRelative = PosXPercent * newWidth;
            posYRelative = PosYPercent * newHeight;
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
            if (posXRelative + border.Width / 2 > frameWidth)
                posXRelative = frameWidth - border.Width / 2;
            if (posYRelative + border.Height / 2 > frameHeight)
                posYRelative = frameHeight - border.Height / 2;
        }
    }
}
