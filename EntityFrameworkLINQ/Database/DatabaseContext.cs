using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLINQ.Database
{
    public  class DatabaseContext : DbContext
    {
        private readonly string _connectionString;
        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;   
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_connectionString);
        }
        public DbSet<CarsModels> CarsModels { get; set; }
    }

    public class CarsModels
    { 
        public int Id { get; set; } 
        public string make { get; set; }
        public string model { get; set; }   
        public string year { get; set; }   
    }
}
