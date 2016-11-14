using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MVC2_with_a_class_library.Models;

namespace MVC2_with_a_class_library.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }

        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.EmployeeId == id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.SaveEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            //SampleDBContext db = new SampleDBContext();
            //ViewBag.Departments = new SelectList(db.Departmetns, "Id", "Name");           
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Employee employee = new Employee();
            TryUpdateModel(employee);
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.AddEmployee(employee);

                return RedirectToAction("Index");
            }
            return View();
            //public ActionResult Create(FormCollection formCollection)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        Employee employee = new Employee();
            //        employee.EmployeeId = int.Parse(formCollection["EmployeeId"]);
            //        employee.Name = formCollection["Name"];
            //        employee.Gender = formCollection["Gender"];
            //        employee.City = formCollection["City"];

            //        EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            //        employeeBusinessLayer.AddEmployee(employee);

            //        return RedirectToAction("Index");
            //    }
            //    return View();
            //string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //SqlConnection connection = new SqlConnection(connectionString);
            //try
            //{
            //    connection.Open();
            //    string id = formCollection["EmployeeId"];
            //    string name = formCollection["Name"];
            //    string gender = formCollection["Gender"];
            //    string city = formCollection["City"];

            //    string qry = "insert into tblEmployee(EmployeeId,Name,Gender,City) values (" + id + ",'" + name + "','" + gender + "','" + city + "')";
            //    SqlCommand cmd = new SqlCommand(qry,connection);
            //    cmd.ExecuteNonQuery();
            //}
            //catch
            //{ }
            //finally
            //{
            //    connection.Close();
            //}
        }
    }
}