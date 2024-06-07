using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Web_Crawler.Models;

namespace Web_Crawler.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("DbConnectionString") {}
        public DbSet<Product> Products { get; set; }
        public DbSet<FrequencyTokenInDescriptions> FrequencyTokenInDescriptions { get; set; }
    }
}