using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Proyecto_Intermodular.models;
using Proyecto_Intermodular.api;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Proyecto_Intermodular
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        int timerStage;

        List<Uri> timerImagesUris = new()
        {
            new Uri("/Proyecto Intermodular;component/images/timer/timer_3.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_4.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_5.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_6.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_7.png", UriKind.Relative),
            new Uri("/Proyecto Intermodular;component/images/timer/timer_2.png", UriKind.Relative)
        };
        List<ImageSource> timerImages = new();

        Employee currentUser;

        public MainWindow()
        {
            InitializeComponent();
            StartTimer();
        }



        //private async void showDishes()
        //{
        //    List<Dish> dishes = await DeliiApi.GetAllDishes();

        //    MessageBox.Show(dishes[0].ToString());
        //    //MessageBox.Show((dishes[0] as Food).ToString());
        //}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ucTables.UpdateTablesPosition();
        }

        public void StartTimer()
        {
            LoadTimerImages();
            timerStage = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void LoadTimerImages()
        {
            timerImagesUris.ForEach(uri => timerImages.Add(new BitmapImage(uri)));
        }
        private void TimerTick(object sender, EventArgs e)
        {
            imgTimer.Source = timerImages[timerStage];
            if (timerStage == 5)
            {
                UpdateDataset();
                timerStage = 0;
            }
            else
                timerStage++;
        }


        public void UpdateDataset()
        {
            if (currentUser == null)
                GetCurrentUser();
            else
                UpdateUI();

            ucTables.UpdateCanvasTables();
            ucIngredients.UpdateStackIngredients();
            ucDishes.UpdateLayout();
            ucKitchen.UpdateKitchen();
            ucBar.UpdateKitchen();
            ucTickets.UpdateStackTickets();
            //GenerateOrders();
        }


        private async void GetCurrentUser()
        {
            try
            {
                currentUser = await DeliiApi.GetEmployeeFromToken();
                ApplicationState.SetValue("current_user", currentUser);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ucTables.CurrentUser = currentUser;
                    if (currentUser.IsAdmin) 
                        ShowAdminTabs();
                    UpdateUI();
                });
                
            }
            catch (DeliiApiException ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void ShowAdminTabs()
        {
            adminTabEmployees.Visibility = Visibility.Visible;
            adminTabIngredients.Visibility = Visibility.Visible;
            adminTabDishes.Visibility = Visibility.Visible;
        }

        private void UpdateUI()
        {
            currentUser = ApplicationState.GetValue<Employee>("current_user");
            Title = (currentUser == null)
                    ? "Empleado: default user"
                    : "Empleado: " + currentUser.FullName;
        }
        
        
    }
}
