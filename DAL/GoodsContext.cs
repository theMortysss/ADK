using DataAccessLibrary.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary
{
    public class GoodsContext : DbContext
    {
        
        public DbSet<Goods> goods { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Sale> sales { get; set; }
        public DbSet<Supplie> supplies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GSSDB.mdf;Integrated Security=True;Connect Timeout=30");
    }
}
