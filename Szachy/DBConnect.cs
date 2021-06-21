using Microsoft.EntityFrameworkCore;

namespace Chess
{
    public class ChessDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-KUGR5BJ\ADONISCE30;Database=Chess;Trusted_Connection=True;");
            }
        }
        public virtual DbSet<Chess> Chess { get; set; }
        public virtual DbSet<WatchedGame> WatchedGame { get; set; }

    }
}