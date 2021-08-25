using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FinalProject.Models;

namespace FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FinalProject.Models.Collection> Collection { get; set; }
        public DbSet<FinalProject.Models.Item> Item { get; set; }
        public DbSet<FinalProject.Models.Comment> Comment { get; set; }
        public DbSet<FinalProject.Models.Like> Like { get; set; }
        public DbSet<FinalProject.Models.Tag> Tag { get; set; }
    }
}
