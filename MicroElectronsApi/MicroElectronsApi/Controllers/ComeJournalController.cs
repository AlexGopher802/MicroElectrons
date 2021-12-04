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
    public class ComeJournalController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public ComeJournalController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список записей прихода/расхода
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllList()
        {
            try
            {
                var result = (from comeJournal in _context.ComeJournals
                              select new
                              {
                                  JournalId = comeJournal.Id,
                                  SubjectName = comeJournal.SubjectName,
                                  Quantity = comeJournal.Quantity,
                                  DateTimeConfirm = comeJournal.DateTimeConfirm.ToString("dd.MM.yyyy"),
                                  IsCome = (comeJournal.IsCome.Value) ? "Приход" : "Расход",
                                  Operation = comeJournal.Operation.Name
                              }).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
