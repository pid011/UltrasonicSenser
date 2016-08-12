using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SenserTest
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            this.Unloaded += MainPage_Unloaded;

            var test = Senser.Distance.GetDistance();
            textBlock.Text = $"{test}cm";

            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            var distance = Senser.Distance.GetDistance();
            textBlock.Text = $"{distance}cm";
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
            }
        }
    }
}