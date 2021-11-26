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
    public class UserController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public UserController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список всех неуволенных сотрудников
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllList()
        {
            try
            {
                var result = (from user in _context.Users
                              where user.Employee.Status.Name != "Уволен"
                              join labor in _context.Labors on user.EmployeeId equals labor.EmployeeId
                              select new UserData()
                              {
                                  EmployeeId = user.EmployeeId,
                                  LastName = user.Employee.LastName,
                                  FirstName = user.Employee.FirstName,
                                  Patronymic = user.Employee.Patronymic,
                                  Birthday = user.Employee.Birthday.ToString("dd.MM.yyyy"),
                                  Status = user.Employee.Status.Name,
                                  Salary = labor.Salary,
                                  LaborDate = labor.DateConfirm.ToString("dd.MM.yyyy"),
                                  Post = user.Employee.Post.Name,
                                  Login = user.Login,
                                  Password = user.Password
                              }).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Регистрация сотрудника
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Registry(UserData userData)
        {
            try
            {
                Employee employee = new Employee()
                {
                    LastName = userData.LastName,
                    FirstName = userData.FirstName,
                    Patronymic = userData.Patronymic,
                    Birthday = DateTime.ParseExact(userData.Birthday, "dd.MM.yyyy", null),
                    Post = _context.Posts.Where(p => p.Name == userData.Post).FirstOrDefault(),
                    Status = _context.EmployeeStatuses.Where(s => s.Name == "Работает").FirstOrDefault()
                };
                _context.Employees.Add(employee);

                Labor labor = new Labor()
                {
                    Salary = userData.Salary,
                    DateConfirm = DateTime.Now,
                    Employee = employee
                };
                _context.Labors.Add(labor);

                User user = new User()
                {
                    Login = userData.Login,
                    Password = HashHandler.Sha256(userData.Password),
                    Employee = employee
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                var result = (from userDb in _context.Users
                              where userDb.Login == user.Login
                              select new
                              {
                                  UserId = userDb.Id,
                                  Login = userDb.Login,
                                  Post = userDb.Employee.Post.Name
                              }).FirstOrDefault();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Авторизация сотрудника
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Auth(UserData userData)
        {
            try
            {
                var result = (from user in _context.Users
                              where user.Login == userData.Login && user.Password == HashHandler.Sha256(userData.Password)
                              select new UserData()
                              {
                                  EmployeeId = user.EmployeeId,
                                  LastName = user.Employee.LastName,
                                  FirstName = user.Employee.FirstName,
                                  Patronymic = user.Employee.Patronymic,
                                  Birthday = user.Employee.Birthday.ToString("dd.MM.yyyy"),
                                  Status = user.Employee.Status.Name,
                                  Post = user.Employee.Post.Name,
                                  Login = user.Login,
                                  Password = user.Password
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Login and/or password invalid");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Увольнение сотрудника
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Dismissal(UserData userData)
        {
            try
            {
                Employee employee = _context.Employees.Where(e => e.Id == userData.EmployeeId).FirstOrDefault();
                if (employee == null)
                {
                    throw new Exception("That employee not exist");
                }

                Dismissal dismissal = new Dismissal()
                {
                    Employee = employee,
                    DateConfirm = DateTime.Now
                };
                _context.Dismissals.Add(dismissal);

                employee.Status = _context.EmployeeStatuses.Where(s => s.Name == "Уволен").FirstOrDefault();
                _context.Employees.Update(employee);
                _context.SaveChanges();

                var result = new
                {
                    EmployeeId = employee.Id,
                    Status = employee.Status.Name
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список должностей
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult PostList()
        {
            try
            {
                var result = (from post in _context.Posts
                              select new String(post.Name)).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Должности не найдены");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список трудовых договоров
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult LaborsList()
        {
            try
            {
                var result = (from labor in _context.Labors
                              select new LaborData()
                              {
                                  EmployeeId = labor.EmployeeId,
                                  LastName = labor.Employee.LastName,
                                  FirstName = labor.Employee.FirstName,
                                  Patronymic = labor.Employee.Patronymic,
                                  Post = labor.Employee.Post.Name,
                                  Date = labor.DateConfirm.ToString("dd.MM.yyyy")
                              }).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Трудовые договора не найдены");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список заявлений на увольнение
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult DismissList()
        {
            try
            {
                var result = (from dismiss in _context.Dismissals
                              select new DismissData()
                              {
                                  EmployeeId = dismiss.EmployeeId,
                                  LastName = dismiss.Employee.LastName,
                                  FirstName = dismiss.Employee.FirstName,
                                  Patronymic = dismiss.Employee.Patronymic,
                                  Post = dismiss.Employee.Post.Name,
                                  Date = dismiss.DateConfirm.ToString("dd.MM.yyyy")
                              }).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Заявления на увольнение не найдены");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список заявлений на отпуск
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult HolidayList()
        {
            try
            {
                var result = (from holiday in _context.Holidays
                              select new HolidayData()
                              {
                                  EmployeeId = holiday.EmployeeId,
                                  LastName = holiday.Employee.LastName,
                                  FirstName = holiday.Employee.FirstName,
                                  Patronymic = holiday.Employee.Patronymic,
                                  Post = holiday.Employee.Post.Name,
                                  DateStart = holiday.DateStart.ToString("dd.MM.yyyy"),
                                  DateEnd = holiday.DateEnd.ToString("dd.MM.yyyy")
                              }).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Заявления на отпуск не найдены");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Отпуск сотрудника
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Holiday(HolidayData holidayData)
        {
            try
            {
                Employee employee = _context.Employees.Where(e => e.Id == holidayData.EmployeeId).FirstOrDefault();
                if (employee == null)
                {
                    throw new Exception("That employee not exist");
                }

                Holiday holiday = new Holiday()
                {
                    Employee = employee,
                    DateStart = DateTime.ParseExact(holidayData.DateStart, "dd.MM.yyyy", null),
                    DateEnd = DateTime.ParseExact(holidayData.DateEnd, "dd.MM.yyyy", null)
                };
                _context.Holidays.Add(holiday);

                employee.Status = _context.EmployeeStatuses.Where(s => s.Name == "В отпуске").FirstOrDefault();
                _context.Employees.Update(employee);
                _context.SaveChanges();

                var result = new
                {
                    EmployeeId = employee.Id,
                    Status = employee.Status.Name
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
