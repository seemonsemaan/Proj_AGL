using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proj_AGL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj_AGL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
