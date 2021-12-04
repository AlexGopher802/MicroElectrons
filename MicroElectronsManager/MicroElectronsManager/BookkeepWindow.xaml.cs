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
using Excel = Microsoft.Office.Interop.Excel;

namespace MicroElectronsManager
{
    /// <summary>
    /// Логика взаимодействия для BookkeepWindow.xaml
    /// </summary>
    public partial class BookkeepWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public BookkeepWindow()
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
            DataGridComeJournalWrite();
        }

        public void DataGridComeJournalWrite()
        {
            try
            {
                DataGridComeJournal.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<ComeJournalData>>(new RestRequest("comejournal/alllist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridComeJournal.ItemsSource = response.Data;
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

        private void CmExport_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            ex.Visible = true;
            ex.SheetsInNewWorkbook = 1;
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            ex.DisplayAlerts = false;
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            sheet.Name = $"Отчет за {DateTime.Now.ToString("dd.MM.yyyy")}";

            sheet.Cells[1, 1] = "Предмет";
            sheet.Cells[1, 2] = "Количество";
            sheet.Cells[1, 3] = "Дата";
            sheet.Cells[1, 4] = "Приход/расход";
            sheet.Cells[1, 5] = "Тип операции";

            for (int i = 0; i < DataGridComeJournal.Items.Count; i++)
            {
                var selectedRow = DataGridComeJournal.Items[i] as ComeJournalData;
                sheet.Cells[i + 2, 1] = selectedRow.SubjectName;
                sheet.Cells[i + 2, 2] = selectedRow.Quantity;
                sheet.Cells[i + 2, 3] = selectedRow.DateTimeConfirm;
                sheet.Cells[i + 2, 4] = selectedRow.IsCome;
                sheet.Cells[i + 2, 5] = selectedRow.Operation;
            }

            var headers = sheet.get_Range("A1", "E1");
            headers.Cells.Font.Size = 14;
            var aRange = sheet.get_Range("A1", "E100");
            aRange.Columns.AutoFit();
        }
    }
}
