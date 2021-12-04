using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroElectronsApi.Models;
using MicroElectronsApi.Logics;
using MicroElectronsApi.Models.Data;

namespace MicroElectronsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public SupplyController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление поставки
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Add(SupplyData supplyData)
        {
            try
            {
                Supply supply = new Supply()
                {
                    IsSell = supplyData.IsSell,
                    DateSupply = DateTime.Now,
                    Counterparty = _context.Counterparties.Where(c => c.Name == supplyData.CounterpartyName).FirstOrDefault()
                };
                _context.Supplies.Add(supply);

                foreach (var i in supplyData.Products)
                {
                    SupplyCompo supplyCompos = new SupplyCompo()
                    {
                        Product = _context.Products.Where(p => p.Name == i.Name).FirstOrDefault(),
                        Supply = supply,
                        Quantity = i.Quantity,
                        Summa = i.Price
                    };
                    _context.SupplyCompos.Add(supplyCompos);
                }
                _context.SaveChanges();

                var result = new
                {
                    Date = supply.DateSupply,
                    Counterparty = supply.Counterparty.Name,
                    SellOrBuy = (supply.IsSell.Value) ? "Продажа" : "Покупка"
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список поставок
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllList()
        {
            try
            {
                var result = (from supply in _context.Supplies
                              select new
                              {
                                  SupplyId = supply.Id,
                                  Counterparty = supply.Counterparty.Name,
                                  Date = supply.DateSupply.ToString("dd.MM.yyyy"),
                                  SellOrBuy = (supply.IsSell.Value) ? "Продажа" : "Покупка",
                                  Summa = GetSummById(_context.SupplyCompos.Where(s => s.SupplyId == supply.Id).ToList())
                              }).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Supplys not finded");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Возвращает общую сумму поставки по её id
        /// </summary>
        /// <param name="supplyId">id поставки</param>
        /// <returns></returns>
        public static double GetSummById(List<SupplyCompo> supplyCompos)
        {
            double result = 0.0;

            foreach (var i in supplyCompos)
            {
                result += i.Summa;
            }

            return result;
        }

        /// <summary>
        /// Подробная информация о поставке по id
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult MoreInfoById(SupplyData supplyData)
        {
            try
            {
                var result = (from supply in _context.Supplies
                              where supply.Id == supplyData.SupplyId
                              select new
                              {
                                  SupplyId = supply.Id,
                                  Counterparty = supply.Counterparty.Name,
                                  Date = supply.DateSupply.ToString("dd.MM.yyyy"),
                                  SellOrBuy = (supply.IsSell.Value) ? "Продажа" : "Покупка",
                                  Summa = GetSummById(_context.SupplyCompos.Where(s => s.SupplyId == supply.Id).ToList()),
                                  Products = (from product in _context.SupplyCompos
                                              where product.SupplyId == supply.Id
                                              select new ProductData()
                                              {
                                                  ProductId = product.ProductId,
                                                  Name = product.Product.Name,
                                                  CategoryName = product.Product.Category.Name,
                                                  Quantity = product.Quantity,
                                                  Price = product.Summa
                                              }).ToList()
                              }).FirstOrDefault();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
