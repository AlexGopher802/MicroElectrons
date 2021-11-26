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
using System.Windows.Shapes;
using RestSharp;
using MicroElectronsManager.Logics;
using MicroElectronsManager.Models;

namespace MicroElectronsManager
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();

        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = apiClient.Get<List<String>>(new RestRequest("user/postlist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                CbPosts.ItemsSource = response.Data;
                DpBirthday.DisplayDateEnd = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();

                var response = apiClient.Post(new RestRequest("user/registry")
                    .AddJsonBody(new
                    {
                        LastName = TbLastName.Text.ToString(),
                        FirstName = TbFirstName.Text.ToString(),
                        Patronymic = TbPatronymic.Text.ToString(),
                        Birthday = DpBirthday.SelectedDate.Value.ToString("dd.MM.yyyy"),
                        Post = CbPosts.SelectedItem.ToString(),
                        Login = TbLogin.Text.ToString(),
                        Password = TbPassword.Password.ToString(),
                        Salary = TbSalary.Text.ToString()
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                MessageBox.Show("Сотрудник зарегистрирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                (this.Owner as HRmanagerWindow).GridEmployeeWrite();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidForm()
        {
            if (TbLastName.Text.Trim() == "") { throw new Exception("Введите фамилию"); }
            if (TbFirstName.Text.Trim() == "") { throw new Exception("Введите имя"); }
            if (TbPatronymic.Text.Trim() == "") { throw new Exception("Введите отчество"); }
            if (DpBirthday.SelectedDate == null) { throw new Exception("Введите дату рождения"); }
            if (CbPosts.SelectedItem == null) { throw new Exception("Выберите должность"); }
            if (TbLogin.Text.Trim() == "") { throw new Exception("Введите логин"); }
            if (TbPassword.Password.Trim() == "") { throw new Exception("Введите пароль"); }
            if (TbSalary.Text.Trim() == "") { throw new Exception("Введите зарплату"); }
            int a = 0;
            if (!int.TryParse(TbSalary.Text.Trim(), out a))
            {
                throw new Exception("Некорректный ввод зарплаты");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
