using ODataDemo.Models;
using ODataDemo.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace ODataDemo.Controllers
{
    public class EmployeesController : ODataController
    {
        DatabaseContext context = new DatabaseContext();
        [EnableQuery] // bu metodun sorgulanabilir olduğunu belirtiyoruz
        public IQueryable<Employee> Get()
        {
            //isimlendirme burada önemli
            return context.Employees;
        }
        public IHttpActionResult GetName([FromODataUri] int key)
        {
            var emp = context.Employees.Find(key);
            if (emp==null)
            {
                return NotFound();
            }
            return Ok(emp.Name);


        }
    }
}
