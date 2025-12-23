using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CableSubscriberApp
{
    public partial class MainWindow : Window
    {
        private readonly JsonDataService _dataService = new JsonDataService();
        private ObservableCollection<Subscriber> _allSubscribers;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            LoadFilters();
        }

        private void LoadData()
        {
            _allSubscribers = new ObservableCollection<Subscriber>(_dataService.Load());
            SubscriberGrid.ItemsSource = _allSubscribers;
        }

        private void LoadFilters()
        {
            StatusFilter.Items.Add("All");
            foreach (var s in _allSubscribers.Select(x => x.Status).Distinct().Where(x => !string.IsNullOrEmpty(x)))
                StatusFilter.Items.Add(s);

            AreaFilter.Items.Add("All");
            foreach (var a in _allSubscribers.Select(x => x.AreaName).Distinct().Where(x => !string.IsNullOrEmpty(x)))
                AreaFilter.Items.Add(a);

            CompanyFilter.Items.Add("All");
            foreach (var c in _allSubscribers.Select(x => x.CompanyName).Distinct().Where(x => !string.IsNullOrEmpty(x)))
                CompanyFilter.Items.Add(c);

            StatusFilter.SelectedIndex = 0;
            AreaFilter.SelectedIndex = 0;
            CompanyFilter.SelectedIndex = 0;
        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            var searchText = SearchBox.Text?.ToLower() ?? "";
            var status = StatusFilter.SelectedItem?.ToString();
            var area = AreaFilter.SelectedItem?.ToString();
            var company = CompanyFilter.SelectedItem?.ToString();

            var filtered = _allSubscribers.Where(s =>
                // SEARCH
                (
                    (s.SubscriberName ?? "").ToLower().Contains(searchText) ||
                    (s.PhoneNumber1 ?? "").Contains(searchText) ||
                    (s.PhoneNumber2 ?? "").Contains(searchText) ||
                    (s.NickName ?? "").ToLower().Contains(searchText) ||
                    s.RentAmount.ToString().Contains(searchText)
                )
                // STATUS
                && (status == "All" || s.Status == status)
                // AREA
                && (area == "All" || s.AreaName == area)
                // COMPANY
                && (company == "All" || s.CompanyName == company)
            ).ToList();

            SubscriberGrid.ItemsSource = filtered;
        }

        private void NewSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SubscriberDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.Subscriber.Id = _allSubscribers.Any()
                    ? _allSubscribers.Max(x => x.Id) + 1
                    : 1;

                _allSubscribers.Add(dialog.Subscriber);
                _dataService.Save(_allSubscribers);
                LoadFilters();
                ApplyFilters(null, null);
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
                    LoadFilters();
                    ApplyFilters(null, null);
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
                    LoadFilters();
                    ApplyFilters(null, null);
                }
            }
        }
    }
}
