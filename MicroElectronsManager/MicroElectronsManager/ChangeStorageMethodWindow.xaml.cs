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
    /// Логика взаимодействия для ChangeStorageMethodWindow.xaml
    /// </summary>
    public partial class ChangeStorageMethodWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();
        public ProductData productData;

        public ChangeStorageMethodWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbProductName.Text = productData.Name;
            WriteListMethods();
        }

        private void WriteListMethods()
        {
            try
            {
                var response = apiClient.Get<List<string>>(new RestRequest("product/AllStorageMethodList"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                CbMethods.ItemsSource = response.Data;
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

                var response = apiClient.Post(new RestRequest("product/ChangeStorageMethod")
                    .AddJsonBody(new
                    {
                        ProductId = productData.ProductId,
                        StorageMethod = CbMethods.SelectedItem.ToString()
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                (this.Owner as StorekeepWindow).DataGridStorageWrite();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidForm()
        {
            if (CbMethods.SelectedItem == null) { throw new Exception("Выберите способ хранения"); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
