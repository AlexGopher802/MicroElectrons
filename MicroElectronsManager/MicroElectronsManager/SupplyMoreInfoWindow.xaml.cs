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
    /// Логика взаимодействия для SupplyMoreInfoWindow.xaml
    /// </summary>
    public partial class SupplyMoreInfoWindow : Window
    {
        public SupplyData supplyData;

        public SupplyMoreInfoWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbCounterpartyName.Text = supplyData.Counterparty;
            TbSellOrBuy.Text = supplyData.SellOrBuy;
            TbDate.Text = supplyData.Date;
            DataGridProducts.ClearValue(ItemsControl.ItemsSourceProperty);
            DataGridProducts.ItemsSource = supplyData.Products;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }
    }
}
