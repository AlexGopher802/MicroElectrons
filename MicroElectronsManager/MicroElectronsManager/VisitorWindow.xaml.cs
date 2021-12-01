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
    /// Логика взаимодействия для VisitorWindow.xaml
    /// </summary>
    public partial class VisitorWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public VisitorWindow()
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
            DataGridVisitorWrite();
        }

        public void DataGridVisitorWrite()
        {
            try
            {
                DataGridVisitors.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<VisitorData>>(new RestRequest("visitor/alllist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridVisitors.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Посетитель действительно вышел?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }

                var selectedVisistor = DataGridVisitors.SelectedItem as VisitorData;

                var response = apiClient.Post(new RestRequest("visitor/writeexit")
                    .AddJsonBody(new
                    {
                        EmployeeExitId = user.EmployeeId,
                        VisitorLastName = selectedVisistor.VisitorLastName,
                        VisitorFirstName = selectedVisistor.VisitorFirstName,
                        VisitorPatronymic = selectedVisistor.VisitorPatronymic
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridVisitorWrite();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridVisitors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedVisistor = DataGridVisitors.SelectedItem as VisitorData;
            CmExit.IsEnabled = selectedVisistor != null && selectedVisistor.DateTimeExit == null;
            CmWhoEntry.IsEnabled = selectedVisistor != null;
            CmWhoExit.IsEnabled = selectedVisistor != null && selectedVisistor.DateTimeExit != null;
        }

        private void CmWhoEntry_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(DataGridVisitors.SelectedItem as VisitorData).EmployeeEntryName}", "Кто впустил?", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CmWhoExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(DataGridVisitors.SelectedItem as VisitorData).EmployeeExitName}", "Кто выпустил?", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
