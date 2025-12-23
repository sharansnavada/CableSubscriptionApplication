using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CableSubscriberApp
{
    public partial class MainWindow : Window
    {
        private readonly JsonDataService _dataService = new JsonDataService();
        private List<Subscriber> _allSubscribers;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _allSubscribers = _dataService.Load();

            if (_allSubscribers.Count == 0)
            {
                _allSubscribers.Add(new Subscriber
                {
                    Id = 1,
                    SubscriberName = "Gopi",
                    NickName = "HANDI",
                    RentAmount = 270,
                    Status = "Active",
                    AreaName = "Channappana Palya",
                    CompanyName = "CHANNAPPANA PALYA",
                    Address = ""
                });

                _dataService.Save(_allSubscribers);
            }

            SubscriberGrid.ItemsSource = _allSubscribers;
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = SearchBox.Text.ToLower();

            SubscriberGrid.ItemsSource = _allSubscribers.Where(s =>
                (s.SubscriberName ?? "").ToLower().Contains(text) ||
                (s.NickName ?? "").ToLower().Contains(text) ||
                s.RentAmount.ToString().Contains(text) ||
                (s.Status ?? "").ToLower().Contains(text) ||
                (s.AreaName ?? "").ToLower().Contains(text) ||
                (s.CompanyName ?? "").ToLower().Contains(text) ||
                (s.Address ?? "").ToLower().Contains(text)
            ).ToList();
        }

        private void NewSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SubscriberDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.Subscriber.Id = _allSubscribers.Count + 1;
                _allSubscribers.Add(dialog.Subscriber);
                _dataService.Save(_allSubscribers);
                SubscriberGrid.ItemsSource = _allSubscribers;
            }
        }

        private void EditSubscriber_Click(object sender, RoutedEventArgs e)
        {
            if (SubscriberGrid.SelectedItem is Subscriber selected)
            {
                var dialog = new SubscriberDialog(selected);
                if (dialog.ShowDialog() == true)
                {
                    _dataService.Save(_allSubscribers);
                    SubscriberGrid.ItemsSource = _allSubscribers;
                }
            }
        }

        private void DeleteSubscriber_Click(object sender, RoutedEventArgs e)
        {
            if (SubscriberGrid.SelectedItem is Subscriber selected)
            {
                if (MessageBox.Show("Delete selected subscriber?",
                    "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _allSubscribers.Remove(selected);
                    _dataService.Save(_allSubscribers);
                    SubscriberGrid.ItemsSource = _allSubscribers;
                }
            }
        }
    }
}
