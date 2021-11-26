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
    /// Логика взаимодействия для AddHolidayWindow.xaml
    /// </summary>
    public partial class AddHolidayWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public AddHolidayWindow()
        {
            InitializeComponent();
        }

        private void ValidForm()
        {
            if (DpDateStart.SelectedDate == null || DpDateEnd.SelectedDate == null)
            {
                throw new Exception("Укажите даты начала и конца отпуска.");
            }

            if (DpDateStart.SelectedDate.Value > DpDateEnd.SelectedDate.Value)
            {
                throw new Exception("Дата конца не может наступить быстрее даты начала.");
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();

                var response = apiClient.Post(new RestRequest("user/holiday")
                    .AddJsonBody(new { EmployeeId = user.EmployeeId,
                        DateStart = DpDateStart.SelectedDate.Value.ToString("dd.MM.yyyy"),
                        DateEnd = DpDateEnd.SelectedDate.Value.ToString("dd.MM.yyyy")
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                MessageBox.Show("Сотрудник отправлен в отпуск", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                (this.Owner as HRmanagerWindow).GridHolidayWrite();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbEmployeeName.Text = $"{user.LastName} {user.FirstName} {user.Patronymic}";
            DpDateStart.DisplayDateStart = DateTime.Now;
            DpDateEnd.DisplayDateStart = DateTime.Now;
        }
    }
}
