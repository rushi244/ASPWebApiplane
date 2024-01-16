using ASPWebApiplane.Data;
using ASPWebApiplane.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ASPWebApiplane.Controllers
{
    //[Route("api/[controller]")]
            
    public class ValuesController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET api/values
        
        [System.Web.Http.HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (ApplicationDbContext entities = new ApplicationDbContext())
                {
                    return Ok(entities.Employee.ToList());
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // GET api/values/5
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult GetEmployeeDetail(int id)
        {
            Employee EmpDetails = context.Employee.Find(id);
            if (EmpDetails == null)
            {
                return NotFound();
            }

            return Ok(EmpDetails);
        }

        [System.Web.Http.HttpPost]
        // POST api/values
        public HttpResponseMessage post([FromBody] Employee employee)
        {
            try
            {
                using (ApplicationDbContext entities = new ApplicationDbContext())
                {
                    entities.Employee.Add(employee);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [System.Web.Mvc.HttpPut]
        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody] Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var EmployeeData =context.Employee.Find(id);
            if (EmployeeData == null)
            {
                return BadRequest("Sorry, seems something wrong. Couldn't determinerecord to update.");
            }
            context.Employee.AddOrUpdate(emp);
            context.SaveChanges();
            return Ok(emp);
        }

        // DELETE api/values/5
        [System.Web.Mvc.HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Employee emp = context.Employee.FirstOrDefault(u=>u.ID==id);
            if (emp == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
           context.Employee.Remove(emp);
            context.SaveChanges();
            var response = new HttpResponseMessage();
                response.Headers.Add("DeleteMessage", "Succsessfuly Deleted!!!");
            return response;
        }
    }
}
