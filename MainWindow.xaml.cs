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
            // STATUS
            StatusFilter.Items.Clear();
            StatusFilter.Items.Add("All");
            foreach (var s in _allSubscribers.Select(x => x.Status)
                                             .Distinct()
                                             .Where(x => !string.IsNullOrEmpty(x)))
                StatusFilter.Items.Add(s);
            StatusFilter.SelectedIndex = 0;

            // COMPANY
            CompanyFilter.Items.Clear();
            CompanyFilter.Items.Add("All");
            foreach (var c in _allSubscribers.Select(x => x.CompanyName)
                                              .Distinct()
                                              .Where(x => !string.IsNullOrEmpty(x)))
                CompanyFilter.Items.Add(c);
            CompanyFilter.SelectedIndex = 0;

            LoadAreaFilter("All");
        }

        private void LoadAreaFilter(string company)
        {
            AreaFilter.Items.Clear();
            AreaFilter.Items.Add("All");

            var areas = _allSubscribers
                .Where(s => company == "All" || s.CompanyName == company)
                .Select(s => s.AreaName)
                .Distinct()
                .Where(a => !string.IsNullOrEmpty(a));

            foreach (var area in areas)
                AreaFilter.Items.Add(area);

            AreaFilter.SelectedIndex = 0;
        }

        private void CompanyFilter_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedCompany = CompanyFilter.SelectedItem?.ToString() ?? "All";
            LoadAreaFilter(selectedCompany);
            ApplyFilters(null, null);
        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            var search = SearchBox.Text?.ToLower() ?? "";
            var status = StatusFilter.SelectedItem?.ToString();
            var company = CompanyFilter.SelectedItem?.ToString();
            var area = AreaFilter.SelectedItem?.ToString();

            var filtered = _allSubscribers.Where(s =>
                // SEARCH
                (
                    (s.SubscriberName ?? "").ToLower().Contains(search) ||
                    (s.PhoneNumber1 ?? "").Contains(search) ||
                    (s.PhoneNumber2 ?? "").Contains(search) ||
                    (s.NickName ?? "").ToLower().Contains(search)
                )
                // STATUS
                && (status == "All" || s.Status == status)
                // COMPANY
                && (company == "All" || s.CompanyName == company)
                // AREA
                && (area == "All" || s.AreaName == area)
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
