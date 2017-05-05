using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MVCDemo.Models;
using BusinessLayer;

namespace MVCDemo.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index1(/*int departmentId*/)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();

            /*
            
            EmployeeContext employeeContext = new EmployeeContext();
            List<Employee> employees = employeeContext.Employees.Where(emp => emp.DepartmentId == departmentId).ToList();
            */
            return View(employees);

        }
        // GET: Employee
        /* public ActionResult Details(int id)
         {
             EmployeeContext employeeContext = new EmployeeContext();
             Employee employee = employeeContext.Employees.Single(emp => emp.EmployeeId == id);

             return View(employee);
         }*/

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Create_Post(Employee employee)
        {
            //            Employee employee = new Employee();
            //TryUpdateModel(employee);

            /*foreach(string key in formCollection.AllKeys)
            {
                Response.Write("Key - " + key + " ");
                Response.Write(formCollection[key]);
                Response.Write("<br/>");
            }
            return View();
            

            // args FormCollection formCollection
            Employee employee = new Employee();
            employee.Name = formCollection["Name"];
            employee.Gender = formCollection["Gender"];
            employee.City = formCollection["City"];
            employee.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"]);

            

            //args string name, string gender, string city, DateTime dateOfBirth
            Employee employee = new Employee();
            employee.Name = name;
            employee.Gender = gender;
            employee.City = city;
            employee.DateOfBirth = dateOfBirth;

    */



            if (ModelState.IsValid)
            {

                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.AddEmployee(employee);

                return RedirectToAction("Index1");
            }

            return View();
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_get(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.EmployeeID == id);

            return View(employee);

        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_post(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(x => x.EmployeeID == id);
            //include list - white list exclude list - black list
            //exclude UpdateModel<TModel>(TModel model, string prefix, string[], includeProperties, string[] excludeProperties)
            //Include UpdateModel<TModel>(TModel model, string[] includeProperties)
            UpdateModel<IEmployee>(employee);

            if (ModelState.IsValid)
            {
                
                employeeBusinessLayer.SaveEmployee(employee);

                return RedirectToAction("Index1");
            }
            return View(employee);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);
            return RedirectToAction("Index1");
        }

    }

}