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
    /// Логика взаимодействия для AddSupplyWindow.xaml
    /// </summary>
    public partial class AddSupplyWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();
        private List<ProductData> products = new List<ProductData>();

        public AddSupplyWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteListCounterparty();

                WriteListProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void WriteListCounterparty()
        {
            var response = apiClient.Get<List<string>>(new RestRequest("counterparty/AllNameList"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
            }

            CbCounterparty.ItemsSource = response.Data;
        }

        public void WriteListProducts()
        {
            var response = apiClient.Get<List<string>>(new RestRequest("product/AllNameList"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
            }

            CbProducts.ItemsSource = response.Data;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddProductInSupply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidProductForm();
                ProductData productData = new ProductData()
                {
                    Name = CbProducts.SelectedItem.ToString(),
                    Quantity = Convert.ToInt32(TbQuantity.Text),
                    Price = Convert.ToInt32(TbPrice.Text)
                };
                products.Add(productData);

                DataGridProductsWrite();
                ClearProductForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridProductsWrite()
        {
            DataGridProducts.ClearValue(ItemsControl.ItemsSourceProperty);
            DataGridProducts.ItemsSource = products;
        }

        private void ValidProductForm()
        {

        }

        private void ValidForm()
        {

        }

        private void ClearProductForm()
        {
            CbProducts.SelectedItem = null;
            TbQuantity.Text = "";
            TbPrice.Text = "";
        }

        private void BtnAddCounterparty_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
