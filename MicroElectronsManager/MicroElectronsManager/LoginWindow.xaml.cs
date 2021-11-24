using System;
using System.Collections.Generic;
using System.Linq;
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
using RestSharp;
using MicroElectronsManager.Logics;
using MicroElectronsManager.Models;

namespace MicroElectronsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = apiClient.Post<UserData>(new RestRequest("user/auth")
                .AddJsonBody(new
                {
                    Login = TbLogin.Text.ToString(),
                    Password = TbPassword.Text.ToString()
                }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }
                
                MessageBox.Show($"{response.Data.LastName} {response.Data.FirstName} {response.Data.Patronymic}, {response.Data.Post}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
