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
    public class ProductController : ControllerBase
    {
        private MicroElectronsDBContext _context;

        public ProductController(MicroElectronsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление продукта
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Add(ProductData productData)
        {
            try
            {
                Product product = new Product()
                {
                    Name = productData.Name,
                    Category = _context.ProductCategories.Where(c => c.Name == productData.CategoryName).FirstOrDefault()
                };
                _context.Products.Add(product);
                _context.SaveChanges();

                var result = new
                {
                    Name = product.Name,
                    Category = product.Category
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Поиск продукта по наименованию
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult FindByName(ProductData productData)
        {
            try
            {
                var result = (from product in _context.Products
                              where product.Name == productData.Name
                              select new
                              {
                                  Name = product.Name,
                                  Category = product.Category.Name
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Product was not finded.");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список наименований продуктов
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllNameList()
        {
            try
            {
                var result = (from product in _context.Products
                              select new String(product.Name)).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Список наименований категорий
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllCategoryList()
        {
            try
            {
                var result = (from category in _context.ProductCategories
                              select new String(category.Name)).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
