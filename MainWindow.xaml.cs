using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CableSubscriberApp
{
    public partial class MainWindow : Window
    {
        private List<Subscriber> _allSubscribers;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var service = new JsonDataService();
            _allSubscribers = service.Load();

            if (_allSubscribers.Count == 0)
            {
                _allSubscribers.Add(new Subscriber
                {
                    Id = 1,
                    SubscriberName = "Gopi",
                    AreaName = "Channappana Palya"
                });

                service.Save(_allSubscribers);
            }

            SubscriberGrid.ItemsSource = _allSubscribers;
        }


        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SubscriberGrid.ItemsSource =
                _allSubscribers
                .Where(s => s.SubscriberName.ToLower()
                .Contains(SearchBox.Text.ToLower()))
                .ToList();
        }
    }
}
