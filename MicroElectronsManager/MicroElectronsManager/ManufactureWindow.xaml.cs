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
    /// Логика взаимодействия для ManufactureWindow.xaml
    /// </summary>
    public partial class ManufactureWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public ManufactureWindow()
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
            DataGridManufsWrite();
        }

        public void DataGridManufsWrite()
        {
            try
            {
                DataGridManufs.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<ManufData>>(new RestRequest("manuf/alllist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridManufs.ItemsSource = response.Data;
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

        private void CmEnd_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Задача действительно выполнена?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                var selectedTask = DataGridManufs.SelectedItem as ManufData;
                var response = apiClient.Post(new RestRequest("manuf/End")
                    .AddJsonBody(new
                    {
                        TaskId = selectedTask.TaskId,
                        DateEnd = DateTime.Now.ToString("dd.MM.yyyy")
                    }));

                DataGridManufsWrite();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CmAdd_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new AddTaskWindow() { Owner = this }.Show();
        }

        private void DataGridManufs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTask = DataGridManufs.SelectedItem as ManufData;
            CmEnd.IsEnabled = DataGridManufs.SelectedItem != null && selectedTask.DateEnd == "";
        }
    }
}
