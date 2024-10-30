using Counter.Models;
using Counter.Services;
using System.Diagnostics;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        List<CounterModel> counters = new();
        CounterService countersService;

        public MainPage()
        {
            InitializeComponent();

            countersService = new();
            LoadCounters();

        }

        private async void LoadCounters()
        {
            counters = await countersService.LoadCountersAsync();
            CounterCollectionView.ItemsSource = counters;
        }


        private async void AddNewCounter(object sender, EventArgs e)
        {
            try
            {
                string name = CounterName.Text; // name
                int iniValue = int.Parse(CounterIniValue.Text); // parse int
                if (String.IsNullOrEmpty(CounterColor.Text) || !CounterColor.Text.Contains("#") || CounterColor.Text.Length != 7)
                    throw new Exception();
                Color color = Color.FromArgb(CounterColor.Text); // parse


                CounterModel newCounter = new(name, color, iniValue);
                counters.Add(newCounter);
                
            }
            catch (Exception)
            {
                await DisplayAlert("Alert", "Enter Correct Data", "OK");
                return;
            }

                await countersService.SaveCountersAsync(counters);

                Refresh();
        }

        private void OnIncrementClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Value++;
            Refresh();
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Value--;
            Refresh();
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Reset();
            Refresh();
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counters.Remove(counter);
            Refresh();
        }

        private async void Refresh()
        {
            //Refresh
            await countersService.SaveCountersAsync(counters);
            LoadCounters();
            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }
    }

}
