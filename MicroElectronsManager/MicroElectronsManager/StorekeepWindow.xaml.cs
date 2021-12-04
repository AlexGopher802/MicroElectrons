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
    /// Логика взаимодействия для StorekeepWindow.xaml
    /// </summary>
    public partial class StorekeepWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public StorekeepWindow()
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
            DataGridStorageWrite();
        }

        public void DataGridStorageWrite()
        {
            try
            {
                DataGridStorage.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<ProductData>>(new RestRequest("product/AllStorageList"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridStorage.ItemsSource = response.Data;
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

        private void CmChangeMethod_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new ChangeStorageMethodWindow() { Owner = this, productData = DataGridStorage.SelectedItem as ProductData }.Show();
        }

        private void DataGridStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmChangeMethod.IsEnabled = DataGridStorage.SelectedItem != null;
        }
    }
}
