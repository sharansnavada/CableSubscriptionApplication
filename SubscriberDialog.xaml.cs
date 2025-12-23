using System.Windows;

namespace CableSubscriberApp
{
    public partial class SubscriberDialog : Window
    {
        public Subscriber Subscriber { get; private set; }

        public SubscriberDialog()
        {
            InitializeComponent();
            Subscriber = new Subscriber();
        }

        public SubscriberDialog(Subscriber existing) : this()
        {
            Subscriber = existing;

            NameBox.Text = existing.SubscriberName;
            NickNameBox.Text = existing.NickName;
            RentBox.Text = existing.RentAmount.ToString();
            StatusBox.Text = existing.Status;
            AreaBox.Text = existing.AreaName;
            CompanyBox.Text = existing.CompanyName;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Subscriber.SubscriberName = NameBox.Text;
            Subscriber.NickName = NickNameBox.Text;
            Subscriber.RentAmount = int.TryParse(RentBox.Text, out var r) ? r : 0;
            Subscriber.Status = StatusBox.Text;
            Subscriber.AreaName = AreaBox.Text;
            Subscriber.CompanyName = CompanyBox.Text;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
