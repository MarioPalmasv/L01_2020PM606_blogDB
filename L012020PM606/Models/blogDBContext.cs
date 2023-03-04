using Microsoft.EntityFrameworkCore;
namespace L012020PM606.Models
{
    public class blogDBContext: DbContext
    {
        public blogDBContext(DbContextOptions<blogDBContext> options): base(options)
        {

        }

        public DbSet<usuarios> usuarios { get; set; }

        public DbSet<comentarios> comentarios { get; set; }

        public DbSet<publicaciones> publicaciones { get; set; }


    }
}
