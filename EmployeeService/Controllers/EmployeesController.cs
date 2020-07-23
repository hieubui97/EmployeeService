using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            using (ModelDbContext db = new ModelDbContext())
            {
                return db.Employees.ToList();
            }
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<Employee> Get(string gender = "all")
        {
            using (ModelDbContext db = new ModelDbContext())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return db.Employees.ToList();
                    case "male":
                        return db.Employees.Where(e => e.Gender.ToLower() == "male").ToList();
                    case "female":
                        return db.Employees.Where(e => e.Gender.ToLower() == "female").ToList();
                    default:
                        return db.Employees.ToList();
                }
            }
        }

        [Route("{id:int}")]
        public Employee GetByID(int id)
        {
            using (ModelDbContext db = new ModelDbContext())
            {
                var emp = db.Employees.Where(x => x.ID == id).FirstOrDefault();
                return emp;
            }
        }

        [Route("post")]
        public IActionResult Post([FromBody] Employee emp)
        {
            try
            {
                using (ModelDbContext db = new ModelDbContext())
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();

                    return Ok();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [Route("put/{id}")]
        public IActionResult Put(int id, [FromBody] Employee emp)
        {
            try
            {
                using (ModelDbContext db = new ModelDbContext())
                {
                    var x = db.Employees.FirstOrDefault(x => x.ID == id);

                    if (x == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        x.FirstName = emp.FirstName;
                        x.LastName = emp.LastName;
                        x.Gender = emp.Gender;
                        x.Salary = emp.Salary;

                        db.SaveChanges();
                        return Ok();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (ModelDbContext db = new ModelDbContext())
                {
                    var x = db.Employees.FirstOrDefault(x => x.ID == id);

                    if (x == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        db.Employees.Remove(x);
                        db.SaveChanges();

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
