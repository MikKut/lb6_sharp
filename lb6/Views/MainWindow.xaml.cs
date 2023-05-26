using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastracture;
using System.Xml.Serialization;
using lb6_server.Models.Dto;
using lb6_server.Models.Requests;
using lb6_server.Models.Responses;

namespace lb6.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _id;
        private readonly string _userName;
        private readonly string _password;
        private IInternalHttpService _internalHttpService;
        private string _serverUrl = "http://localhost:5229/api/v1/banking";
        private XmlSerializer _userBalanceSerializer;
        private XmlSerializer _userWithAmountSerializer;
        private readonly HttpClient _client;
        public MainWindow(string userName, Guid id)
        {
            _internalHttpService = new InternalHttpService();
            _client = new HttpClient();
            _userBalanceSerializer = new XmlSerializer(typeof(GetUserBalanceRequest));
            _userWithAmountSerializer = new XmlSerializer(typeof(MoneyOperationDto));
            InitializeComponent();
            _userName = userName;
            _id = id.ToString();
            IdLabel.Content = id.ToString();
            UserNameLabel.Content = userName;
        }

        private async void GetBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            var balanceRequest = new GetUserBalanceRequest() { Id = _id };
            var result = await _internalHttpService.SendRequest<XmlSerializer, GetUserBalanceRequest, GetBalanceResponse>(_serverUrl, "getbalance", HttpMethod.Post, _userBalanceSerializer, balanceRequest);
            BalanceLabel.Content = result.Balance.ToString();

            if (!result.Result.IsSuccessful)
            {
                StatusLabel.Content = result.Result.Message;
                MessageBox.Show(result.Result.Message);
            }
        }

        private async void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            var depositRequest = new MoneyOperationDto() { Id = _id, Amount = Convert.ToDecimal(DepositTextBox.Text) };
            var result = await _internalHttpService.SendRequest<XmlSerializer, MoneyOperationDto, SuccessfulResultResponse>(_serverUrl, "Deposit", HttpMethod.Post, _userWithAmountSerializer, depositRequest);
            if (!result.Result.IsSuccessful)
            {
                StatusLabel.Content = result.Result.Message;
                MessageBox.Show(result.Result.Message);
            }
        }

        private async void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            var depositRequest = new MoneyOperationDto() { Id = _id, Amount = Convert.ToDecimal(WithdrawalTextBox.Text) };
            var result = await _internalHttpService.SendRequest<XmlSerializer, MoneyOperationDto, SuccessfulResultResponse>(_serverUrl, "Withdraw", HttpMethod.Post, _userWithAmountSerializer, depositRequest);
            if (!result.Result.IsSuccessful)
            {
                StatusLabel.Content = result.Result.Message;
                MessageBox.Show(result.Result.Message);
            }
        }


        private string GetStarsForPassword(int passwordLength)
        {
            StringBuilder passwordWithStars = new StringBuilder(passwordLength);
            for(int i = 0; i< passwordLength; i++)
            {
                passwordWithStars.Append('*');
            }

            return passwordWithStars.ToString();
        }
    }
}
