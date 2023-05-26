using Infrastracture;
using lb6_server.Models.Dto;
using lb6_server.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace lb6.Views
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Window
    {
        private IInternalHttpService _internalHttpService;
        private string _serverUrl = @"http://localhost:5229/api/v1/identity";
        private XmlSerializer _userRegisterSerialier;
        private readonly HttpClient _client;
        public SignUpPage()
        {
            _internalHttpService = new InternalHttpService();
            _client = new HttpClient();
            _userRegisterSerialier = new XmlSerializer(typeof(UserDto));
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var userRequest = new UserDto() { Name = UserNameTextBox.Text, Password = PasswordTextBox.Text };
            var result = await _internalHttpService.SendRequest<XmlSerializer, UserDto, SuccessfulResultResponseWithData<Guid>>(_serverUrl, "register", HttpMethod.Post, _userRegisterSerialier, userRequest);
            if (result.Result.IsSuccessful)
            {
                new MainWindow(userName:UserNameTextBox.Text, id: result!.Data).Show();
            }
            else
            {
                MessageBox.Show(result.Result.Message);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userRequest = new UserDto() { Name = UserNameTextBox.Text, Password = PasswordTextBox.Text };
            var result = await _internalHttpService.SendRequest<XmlSerializer, UserDto, SuccessfulResultResponseWithData<Guid>>(_serverUrl, "login", HttpMethod.Post, _userRegisterSerialier, userRequest);
            if (result.Result.IsSuccessful)
            {
                new MainWindow(userName: UserNameTextBox.Text, id: result.Data).Show();
            }
            else
            {
                MessageBox.Show(result.Result.Message);
            }
        }
    }
}
