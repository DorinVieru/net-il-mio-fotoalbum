using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : DbContext
    {
        private const string SqlServer = "Data Source=localhost;Initial Catalog=photos;Integrated Security=True;Trust Server Certificate=True";
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SqlServer);
        }
    }
}
