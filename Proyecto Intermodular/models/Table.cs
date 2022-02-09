using System;
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
        private double width = DEFAULT_SIZE;
        private double height = DEFAULT_SIZE;
        private Ticket actualTicket;
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
        }


        public string Id { get => id; set => id = value; }
        public double PosXRelative { get => posXRelative; set => posXRelative = value; }
        public double PosYRelative { get => posYRelative; set => posYRelative = value; }
        public Border Border { get => border; set => border = value; }
        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public Ticket ActualTicket { get => actualTicket; set => actualTicket = value; }

        public override string ToString() => $"ID: {id}, LABEL: ({Canvas.GetLeft(border)}, {Canvas.GetTop(border)}, {border.Name})";

        public void SetPosition(Point newPoint, double cnvWidth, double cnvHeight)
        {
            posX = newPoint.X / cnvWidth;
            posY = newPoint.Y / cnvHeight;
            UpdateLabelCoordenates(newPoint.X, newPoint.Y);
        }

        public void UpdateRelativePosition(double newCanvasWidth, double newCanvasHeight)
        {
            posXRelative = posX * newCanvasWidth;
            posYRelative = posY * newCanvasHeight;
            CorrectOutOfFrame(newCanvasWidth, newCanvasHeight);
            UpdateLabelCoordenates();
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
            if (posXRelative + border.Width > frameWidth)
                posXRelative = frameWidth - border.Width;
            if (posYRelative + border.Height > frameHeight)
                posYRelative = frameHeight - border.Height;
        }

        internal void UpdateData(Table updatedTable, double frameWidth, double frameHeight)
        {
            height = updatedTable.Height;
            width = updatedTable.Width;
            posX = updatedTable.PosX;
            posY = updatedTable.PosY;
            if (updatedTable.ActualTicket == null)
                actualTicket = null;
            else if (actualTicket == null)
                actualTicket = updatedTable.actualTicket;
            else
                actualTicket.UpdateData(updatedTable.ActualTicket);
            border.Width = width;
            border.Height = height;
            UpdateRelativePosition(frameWidth, frameHeight);
        }

        public void UpdateLabelCoordenates()
        {
            Canvas.SetLeft(border, posXRelative);
            Canvas.SetTop(border, posYRelative);
        }

        public void UpdateLabelCoordenates(double left, double top)
        {
            Canvas.SetLeft(border, left);
            Canvas.SetTop(border, top);
        }

        public void MoveLabel(Point dropPos, Size offset, Size cnvSize)
        {
            double left = (dropPos.X > cnvSize.Width - offset.Width)
                ? cnvSize.Width - offset.Width * 2
                : (dropPos.X < offset.Width)
                    ? 0
                    : dropPos.X - offset.Width;

            double top = (dropPos.Y > cnvSize.Height - offset.Height)
                            ? cnvSize.Height - offset.Height * 2
                            : (dropPos.Y < offset.Height)
                                ? 0
                                : dropPos.Y - offset.Height;

            Point newPoint = new(left, top);
            SetPosition(newPoint, cnvSize.Width, cnvSize.Height);
        }
    }
}
