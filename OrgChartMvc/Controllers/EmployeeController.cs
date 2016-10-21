using OrgChartMvc.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrgChartMvc.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeesDbContext context = new EmployeesDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read()
        {
            var employees = context.Employees.ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }       

        public EmptyResult Update(List<Employee> model)
        {
            foreach (var employeeModel in model)
            {
                var employee = context.Employees.FirstOrDefault(p => p.Id == employeeModel.Id);

                if (employee == null)
                {
                    employee = new Employee();
                    context.Employees.Add(employee);
                }

                employee.Id = employeeModel.Id;
                employee.ParentId = employeeModel.ParentId;
                employee.Name = employeeModel.Name;
                employee.Title = employeeModel.Title;
                employee.PhotoUrl = employeeModel.PhotoUrl;
            }

            var modelIds = model.Select(p => p.Id);
            var removeEmployees = context.Employees.Where(p => !modelIds.Contains(p.Id));

            foreach (var employee in removeEmployees) {
                context.Employees.Remove(employee);
            }

            context.SaveChanges();

            return new EmptyResult();
        }

        public EmployeeController()
        {
            if (context.Employees.Count() == 0)
            {
                context.Employees.Add(new Employee()
                {
                    Id = "1",
                    ParentId = "",
                    Name = "Name 1",
                    Title = "Title 1",
                    PhotoUrl = "/Images/img.jpg"
                });
                context.Employees.Add(new Employee()
                {
                    Id = "2",
                    ParentId = "1",
                    Name = "Name 2",
                    Title = "Title 2",
                    PhotoUrl = "/Images/img.jpg"
                });
                context.Employees.Add(new Employee()
                {
                    Id = "3",
                    ParentId = "1",
                    Name = "Name 3",
                    Title = "Title 3",
                    PhotoUrl = "/Images/img.jpg"
                });
            }
            context.SaveChanges();
        }
    }
}