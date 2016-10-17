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

        public EmptyResult Remove(int id)
        {
            var employee = context.Employees.First(p => p.Id == id);
            context.Employees.Remove(employee);
            context.SaveChanges();

            return new EmptyResult();
        }

        public EmptyResult Insert(Employee model)
        {
            context.Employees.Add(model);
            context.SaveChanges();

            return new EmptyResult();
        }

        public EmptyResult Update(Employee model)
        {
            var employee = context.Employees.First(p => p.Id == model.Id);

            employee.Name = model.Name;
            employee.ParentId = model.ParentId;
            employee.PhotoUrl = model.PhotoUrl;
            employee.Title = model.Title;
            
            context.SaveChanges();

            return new EmptyResult();
        }

        public EmployeeController()
        {
            if (context.Employees.Count() == 0)
            {
                context.Employees.Add(new Employee()
                {
                    Id = 1,
                    ParentId = 0,
                    Name = "Name 1",
                    Title = "Title 1",
                    PhotoUrl = "/Images/img.jpg"
                });
                context.Employees.Add(new Employee()
                {
                    Id = 2,
                    ParentId = 1,
                    Name = "Name 2",
                    Title = "Title 2",
                    PhotoUrl = "/Images/img.jpg"
                });
                context.Employees.Add(new Employee()
                {
                    Id = 3,
                    ParentId = 1,
                    Name = "Name 3",
                    Title = "Title 3",
                    PhotoUrl = "/Images/img.jpg"
                });
            }
            context.SaveChanges();
        }
    }
}