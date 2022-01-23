using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Intermodular.models
{
    public class Table
    {
        private readonly static int DEFAULT_SIZE = 30;
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

        public Table(double posX, double posY)
        {
            this.posX = posX;
            this.posY = posY;
            width = DEFAULT_SIZE;
            height = DEFAULT_SIZE;
        }


        public string Id { get => id; set => id = value; }
        public double PosXRelative { get => posXRelative; set => posXRelative = value; }
        public double PosYRelative { get => posYRelative; set => posYRelative = value; }
        public Border Border { get => border; set => border = value; }
        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public override string ToString() => $"ID: {id}, BORDER: ({Canvas.GetLeft(border)}, {Canvas.GetTop(border)}, {border.Name})";

        public void SetPosition(Point newPoint, double cnvWidth, double cnvHeight)
        {
            posX = newPoint.X / cnvWidth;
            posY = newPoint.Y / cnvHeight;
        }

        public void UpdatePosition(double newCanvasWidth, double newCanvasHeight)
        {
            posXRelative = PosX * newCanvasWidth;
            posYRelative = PosY * newCanvasHeight;
            CorrectOutOfFrame(newCanvasWidth, newCanvasHeight);
        }

        public void ChangeTableSize(double newWidth = 0, double newHeight = 0)
        {
            width += newWidth;
            height += newHeight;
            border.Width = width;
            border.Height = height;
        }

        private void CorrectOutOfFrame(double frameWidth, double frameHeight)
        {
            if (posXRelative + border.Width > frameWidth)
                posXRelative = frameWidth - border.Width;
            if (posYRelative + border.Height > frameHeight)
                posYRelative = frameHeight - border.Height;
        }
    }
}
