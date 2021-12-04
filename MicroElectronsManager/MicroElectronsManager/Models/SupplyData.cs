using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronsManager.Models
{
    public class SupplyData
    {
        public int SupplyId { get; set; }
        public string Counterparty { get; set; }
        public string Date { get; set; }
        public string SellOrBuy { get; set; }
        public int Summa { get; set; }
        public List<ProductData> Products { get; set; }
    }
}
