using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DatatableHtmlHelper.DataTableHelper;
using DatatableHtmlHelper.Models;

namespace DatatableHtmlHelper.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetData(DTParameters param)
        {
            var employees=new List<Employee>();
            for (int i = 0; i < 1; i++)
            {
                var employee=new Employee()
                {
                    Id = i,
                    Age = i,
                    Name = "name"+i,
                    Office = "office"+i,
                    Position = "position"+i,
                    Salary = i,
                    StartDate = DateTime.Now
                };
                employees.Add(employee);
            }
            return Json(new DTResult<Employee>()
            {
                data = employees,
                draw = param.Draw,
                recordsFiltered = employees.Count,
                recordsTotal = employees.Count
            },JsonRequestBehavior.AllowGet) ;
        }
    }
}