using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WVMS.Model;
using WVMS.Model.Message;

namespace WVMS.Core.DB
{
    public class MyDbContext : IdentityDbContext<User>
    { 
        public MyDbContext(DbContextOptions<MyDbContext> options)    : base(options)
        {

        }
        
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Configsys> Configsyss { get; set; }
        public virtual DbSet<Customer>  Customers { get; set; }
        public virtual DbSet<Stockin> Stockins { get; set; }
        public virtual DbSet<Stockindetail> Stockindetails { get; set; }
        public virtual DbSet<Reservoirarea> Reservoirareas { get; set; }
    }
}
