using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroElectronsApi.Models;
using MicroElectronsApi.Logics;
using MicroElectronsApi.Models.Data;

namespace MicroElectronsApi.Models.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterpartyController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public CounterpartyController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление контрагента
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Add(Counterparty counterpartyData)
        {
            try
            {
                Counterparty counterparty = new Counterparty()
                {
                    Name = counterpartyData.Name,
                    Tin = counterpartyData.Tin,
                    Address = counterpartyData.Address,
                    Bic = counterpartyData.Bic
                };
                _context.Counterparties.Add(counterparty);
                _context.SaveChanges();

                var result = (from counter in _context.Counterparties
                              where counter.Name == counterpartyData.Name && counter.Tin == counterpartyData.Tin
                              select new
                              {
                                  Id = counter.Id,
                                  Name = counter.Name,
                                  Tin = counter.Tin,
                                  Address = counter.Address,
                                  Bic = counter.Bic
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Counterparty was not added.");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Поиск контерагента по наименованию
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult FindByName(Counterparty counterpartyData)
        {
            try
            {
                var result = (from counter in _context.Counterparties
                              where counter.Name == counterpartyData.Name
                              select new
                              {
                                  Id = counter.Id,
                                  Name = counter.Name,
                                  Tin = counter.Tin,
                                  Address = counter.Address,
                                  Bic = counter.Bic
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Counterparty was not finded.");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список наименований контрагентов
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllNameList()
        {
            try
            {
                var result = (from counterparty in _context.Counterparties
                              select new String(counterparty.Name)).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
