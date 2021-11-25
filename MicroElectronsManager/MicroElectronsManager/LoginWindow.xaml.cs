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
    /// Логика взаимодействия для MainWindow.xaml
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
                    Password = TbPassword.Password.ToString()
                }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                this.Hide();
                TbLogin.Text = "";
                TbPassword.Password = "";
                switch (response.Data.Post)
                {
                    case "Бухгалтер":
                        new BookkeepWindow() { Owner = this, user = response.Data }.Show();
                        break;

                    case "Кладовщик":
                        new StorekeepWindow() { Owner = this, user = response.Data }.Show();
                        break;

                    case "HR менеджер":
                        new HRmanagerWindow() { Owner = this, user = response.Data }.Show();
                        break;

                    case "Вахтёр":
                        new VisitorWindow() { Owner = this, user = response.Data }.Show();
                        break;

                    case "Менеджер поставок":
                        new SupplyWindow() { Owner = this, user = response.Data }.Show();
                        break;

                    case "Начальник производства":
                        new ManufactureWindow() { Owner = this, user = response.Data }.Show();
                        break;

                    default:
                        this.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
