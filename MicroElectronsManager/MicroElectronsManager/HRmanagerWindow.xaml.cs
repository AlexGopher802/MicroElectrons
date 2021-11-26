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
    /// Логика взаимодействия для HRmanagerWindow.xaml
    /// </summary>
    public partial class HRmanagerWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public HRmanagerWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbWelcome.Text = $"Здравствуйте, {user.FirstName} {user.Patronymic}";
            GridEmployeeWrite();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GridEmployeeWrite()
        {
            try
            {
                DataGridEmployee.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<UserData>>(new RestRequest("user/alllist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }
                
                DataGridEmployee.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridLaborWrite()
        {
            try
            {
                DataGridLabor.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<LaborData>>(new RestRequest("user/laborslist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridLabor.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridDismissWrite()
        {
            try
            {
                DataGridDismiss.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<DismissData>>(new RestRequest("user/dismisslist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridDismiss.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void GridHolidayWrite()
        {
            try
            {
                DataGridHoliday.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<HolidayData>>(new RestRequest("user/holidaylist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridHoliday.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CmAdd_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new AddEmployeeWindow() { Owner = this }.Show();
        }

        private void CmDismiss_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridEmployee.SelectedItem == null)
                {
                    throw new Exception("Никакой пользователь не выбран");
                }

                var selectedUser = DataGridEmployee.SelectedItem as UserData;
                if (MessageBox.Show($"Вы точно хотите уволить сотрудника: {selectedUser.LastName} {selectedUser.FirstName} {selectedUser.Patronymic}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question)
                    == MessageBoxResult.Yes)
                {
                    var response = apiClient.Post(new RestRequest("user/dismissal")
                        .AddJsonBody(new { EmployeeId = selectedUser.EmployeeId }));

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                    }

                    MessageBox.Show("Сотрудник успешно уволен", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);

                    GridEmployeeWrite();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CmHoliday_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridEmployee.SelectedItem == null)
                {
                    throw new Exception("Никакой пользователь не выбран");
                }

                var selectedUser = DataGridEmployee.SelectedItem as UserData;

                if (selectedUser.Status == "В отпуске")
                {
                    throw new Exception("Сотрудник уже в отпуске");
                }

                this.IsEnabled = false;
                new AddHolidayWindow() { Owner = this, user = selectedUser }.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ContentsClear()
        {
            MenuEmployee.IsEnabled = true;
            MenuLabors.IsEnabled = true;
            MenuDismiss.IsEnabled = true;
            MenuHoliday.IsEnabled = true;

            ContentEmployee.Visibility = Visibility.Collapsed;
            ContentLabors.Visibility = Visibility.Collapsed;
            ContentDismiss.Visibility = Visibility.Collapsed;
            ContentHoliday.Visibility = Visibility.Collapsed;
        }

        private void MenuEmployee_Click(object sender, RoutedEventArgs e)
        {
            ContentsClear();
            MenuEmployee.IsEnabled = false;
            ContentEmployee.Visibility = Visibility.Visible;
            GridEmployeeWrite();
        }

        private void MenuLabors_Click(object sender, RoutedEventArgs e)
        {
            ContentsClear();
            MenuLabors.IsEnabled = false;
            ContentLabors.Visibility = Visibility.Visible;
            GridLaborWrite();
        }

        private void MenuDismiss_Click(object sender, RoutedEventArgs e)
        {
            ContentsClear();
            MenuDismiss.IsEnabled = false;
            ContentDismiss.Visibility = Visibility.Visible;
            GridDismissWrite();
        }

        private void MenuHoliday_Click(object sender, RoutedEventArgs e)
        {
            ContentsClear();
            MenuHoliday.IsEnabled = false;
            ContentHoliday.Visibility = Visibility.Visible;
            GridHolidayWrite();
        }

        private void DataGridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmDismiss.IsEnabled = DataGridEmployee.SelectedItem != null;
            CmHoliday.IsEnabled = DataGridEmployee.SelectedItem != null;
        }
    }
}
