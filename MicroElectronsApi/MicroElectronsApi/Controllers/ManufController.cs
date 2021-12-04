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
    public class ManufController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public ManufController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление задачи
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Add(ManufData manufData)
        {
            try
            {
                Models.Task task = new Models.Task()
                {
                    Warehouse = _context.Warehouses.Where(w => w.ProductId == manufData.ProductId).FirstOrDefault(),
                    Quantity = manufData.Quantity,
                    DateStart = DateTime.ParseExact(manufData.DateStart, "dd.MM.yyyy", null),
                    DateDeadline = DateTime.ParseExact(manufData.DateDeadline, "dd.MM.yyyy", null),
                    Employee = _context.Employees.Where(e => e.Id == manufData.EmployeeId).FirstOrDefault(),
                    Status = _context.TaskStatuses.Where(s => s.Name == "Выполняется").FirstOrDefault()
                };
                _context.Tasks.Add(task);
                _context.SaveChanges();

                var result = new
                {
                    Id = task.Id,
                    DateStart = task.DateStart,
                    DateDeadline = task.DateDeadline
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список задач
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllList()
        {
            try
            {
                var result = (from task in _context.Tasks
                              select new
                              {
                                  TaskId = task.Id,
                                  DateStart = task.DateStart.ToString("dd.MM.yyyy"),
                                  DateDeadline = task.DateDeadline.ToString("dd.MM.yyyy"),
                                  DateEnd = (task.DateEnd != null) ? task.DateEnd.Value.ToString("dd.MM.yyyy") : "",
                                  Quantity = task.Quantity,
                                  ProductId = task.Warehouse.ProductId,
                                  ProductName = task.Warehouse.Product.Name,
                                  EmployeeId = task.EmployeeId,
                                  Status = task.Status.Name
                              }).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Окончание задачи
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult End(ManufData manufData)
        {
            try
            {
                Models.Task task = _context.Tasks.Where(t => t.Id == manufData.TaskId).FirstOrDefault();
                task.DateEnd = DateTime.ParseExact(manufData.DateEnd, "dd.MM.yyyy", null);
                task.Status = (task.DateEnd.Value > task.DateDeadline)
                    ? _context.TaskStatuses.Where(s => s.Name == "Завершено с опозданием").FirstOrDefault()
                    : _context.TaskStatuses.Where(s => s.Name == "Завершено в срок").FirstOrDefault();
                _context.Tasks.Update(task);
                _context.SaveChanges();

                var result = task;

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
