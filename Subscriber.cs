using System.ComponentModel;

namespace CableSubscriberApp
{
    public class Subscriber : INotifyPropertyChanged
    {
        private int _id;
        private string _subscriberName;
        private string _nickName;
        private int _rentAmount;
        private string _status;
        private string _areaName;
        private string _companyName;
        private string _address;
        private string _phoneNumber1;
        private string _phoneNumber2;
        private string _setTopBoxNumber;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string SubscriberName
        {
            get => _subscriberName;
            set { _subscriberName = value; OnPropertyChanged(nameof(SubscriberName)); }
        }

        public string NickName
        {
            get => _nickName;
            set { _nickName = value; OnPropertyChanged(nameof(NickName)); }
        }

        public int RentAmount
        {
            get => _rentAmount;
            set { _rentAmount = value; OnPropertyChanged(nameof(RentAmount)); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string AreaName
        {
            get => _areaName;
            set { _areaName = value; OnPropertyChanged(nameof(AreaName)); }
        }

        public string CompanyName
        {
            get => _companyName;
            set { _companyName = value; OnPropertyChanged(nameof(CompanyName)); }
        }

        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public string PhoneNumber1
        {
            get => _phoneNumber1;
            set { _phoneNumber1 = value; OnPropertyChanged(nameof(PhoneNumber1)); }
        }

        public string PhoneNumber2
        {
            get => _phoneNumber2;
            set { _phoneNumber2 = value; OnPropertyChanged(nameof(PhoneNumber2)); }
        }

        public string SetTopBoxNumber
        {
            get => _setTopBoxNumber;
            set { _setTopBoxNumber = value; OnPropertyChanged(nameof(SetTopBoxNumber)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
