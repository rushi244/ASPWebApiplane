using ASPWebApiplane.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPWebApiplane.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(): base("DefaultConnection")
        {
                
        }
        public DbSet<Employee> Employee { get; set; }   
    }

}