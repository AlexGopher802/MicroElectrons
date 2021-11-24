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
    public class VisitorController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public VisitorController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Запись в журнал посещений (вход)
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WriteEntry(VisitorData visitorData)
        {
            try
            {
                Visitor visitor = new Visitor()
                {
                    LastName = visitorData.VisitorLastName,
                    FirstName = visitorData.VisitorFirstName,
                    Patronymic = visitorData.VisitorPatronymic,
                    Passport = visitorData.Passport
                };
                _context.Visitors.Add(visitor);

                VisitorJournal visitorJournal = new VisitorJournal()
                {
                    DateTimeEntry = DateTime.Now,
                    EmployeeEntry = _context.Employees.Where(e => e.Id == visitorData.EmployeeEntryId).FirstOrDefault(),
                    Visitor = visitor
                };
                _context.VisitorJournals.Add(visitorJournal);
                _context.SaveChanges();

                var result = new
                {
                    DateTime = visitorJournal.DateTimeEntry,
                    VisitorName = $"{visitorData.VisitorLastName} {visitorData.VisitorFirstName} {visitorData.VisitorPatronymic}",
                    EmployeeName = $"${visitorJournal.EmployeeEntry.LastName} {visitorJournal.EmployeeEntry.FirstName} {visitorJournal.EmployeeEntry.Patronymic}"
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Запись в журнал посещений (выход)
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WriteExit(VisitorData visitorData)
        {
            try
            {
                VisitorJournal visitorJournal = _context.VisitorJournals.Where(j => j.Visitor.LastName == visitorData.VisitorLastName &&
                                                                                    j.Visitor.FirstName == visitorData.VisitorFirstName &&
                                                                                    j.Visitor.Patronymic == visitorData.VisitorPatronymic &&
                                                                                    j.DateTimeExit == null).FirstOrDefault();

                visitorJournal.DateTimeExit = DateTime.Now;
                visitorJournal.EmployeeExit = _context.Employees.Where(e => e.Id == visitorData.EmployeeExitId).FirstOrDefault();
                _context.VisitorJournals.Update(visitorJournal);
                _context.SaveChanges();

                var result = new
                {
                    DateTime = visitorJournal.DateTimeExit,
                    VisitorName = $"{visitorData.VisitorLastName} {visitorData.VisitorFirstName} {visitorData.VisitorPatronymic}",
                    EmployeeName = $"${visitorJournal.EmployeeExit.LastName} {visitorJournal.EmployeeExit.FirstName} {visitorJournal.EmployeeExit.Patronymic}"
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
