using System.Collections.Generic;
using System.Diagnostics;
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
        bool initializeResize = true; // Controla bug de window resize
        int borderSize = Table.BORDER_SIZE;
        List<Table> tables;

        public MainWindow()
        {
            InitializeComponent();

            tables = new();
            tables.Add(new Table(1, 0.10, 0.10));
            tables.Add(new Table(3, 0.40, 0.40));

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
                Table table = GetTableFromUiElement(element);

                double left = (dropPos.X > cnvTables.ActualWidth - borderSize) ? cnvTables.ActualWidth - borderSize :
                                (dropPos.X < offset) ? 0 : dropPos.X - offset;

                double top = (dropPos.Y > cnvTables.ActualHeight - borderSize) ? cnvTables.ActualHeight - borderSize :
                                (dropPos.Y < offset) ? 0 : dropPos.Y - offset;

                Point newPoint = new(left, top);
                table.SetPosition(newPoint, cnvTables.ActualWidth, cnvTables.ActualHeight);
                Canvas.SetLeft(element, left);
                Canvas.SetTop(element, top);
            }
        }

        private Table GetTableFromUiElement(UIElement element)
        {
            foreach (Table table in tables)
            {
                if (table.Border == element) return table;
            }
            return null;
        }

        private Table GetTableById(int id)
        {
            foreach (Table table in tables)
            {
                if (table.Id == id) return table;
            }
            return null;
        }

        private void BtnDistribution_Click(object sender, RoutedEventArgs e) => distribution = !distribution;

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach(Table table in tables)
            {
                table.UpdatePosition(cnvTables.ActualWidth, cnvTables.ActualHeight);

                Canvas.SetLeft(table.Border, table.PosX);
                Canvas.SetTop(table.Border, table.PosY);
            }
        }

        private void BtnSaveDistribution_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
