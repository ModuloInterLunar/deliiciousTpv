using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        bool distribution = true;
        int borderSize = 25;
        List<Table> tables;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            tables = new();
            tables.Add(new Table(1, 10, 10));
            tables.Add(new Table(3, 40, 40));

            tables.ForEach(table => AddTable(table));
        }

        private void AddTable(Table table)
        {
            Border border = new();
            border.Background = (SolidColorBrush) new BrushConverter().ConvertFromString("#FF7AA0CD");
            border.CornerRadius = new(10);
            border.Width = borderSize;
            border.Height = borderSize;
            border.Name = $"brdTable{table.Id}";
            Canvas.SetLeft(border, table.PosX);
            Canvas.SetTop(border, table.PosY);

            border.MouseMove += new MouseEventHandler((object sender, MouseEventArgs e) => {

                if (e.LeftButton != MouseButtonState.Pressed) return;

                if (distribution) DragDrop.DoDragDrop(border, new DataObject(DataFormats.Serializable, border), DragDropEffects.Move);
                else MessageBox.Show("New ticket.");
            });

            Label label = new();
            label.Content = table.Id;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;

            border.Child = label;

            cnvTables.Children.Add(border);
            table.Border = border;
        }

        private void CnvTables_Drop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if(data is UIElement element)
            {
                Point dropPos = e.GetPosition(cnvTables);
                double offset = borderSize / 2;

                double left = (dropPos.X > cnvTables.ActualWidth - borderSize) ? cnvTables.ActualWidth - borderSize : 
                              (dropPos.X < borderSize / 2) ? 0 : dropPos.X - offset;

                double top = (dropPos.Y > cnvTables.ActualHeight - borderSize) ? cnvTables.ActualHeight - borderSize :
                             (dropPos.Y < borderSize / 2) ? 0 : dropPos.Y - offset;

                Canvas.SetLeft(element, left);
                Canvas.SetTop(element, top);
            }
        }

        private void BtnDistribution_Click(object sender, RoutedEventArgs e) => distribution = !distribution;

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size oldSize = e.PreviousSize;
            Size newSize = e.NewSize;

            foreach(Border border in cnvTables.Children)
            {
                Point oldPoint = new(Canvas.GetLeft(border), Canvas.GetTop(border));
                Point newPoint = new(oldPoint.X * newSize.Width / oldSize.Width, oldPoint.Y * newSize.Height / oldSize.Height);

                if (newPoint.X + borderSize > cnvTables.ActualWidth) 
                    newPoint.X = cnvTables.ActualWidth - borderSize;
                if (newPoint.Y + borderSize > cnvTables.ActualHeight) 
                    newPoint.Y = cnvTables.ActualHeight - borderSize;

                Canvas.SetLeft(border, newPoint.X);
                Canvas.SetTop(border, newPoint.Y);
            }
        }
    }
}
