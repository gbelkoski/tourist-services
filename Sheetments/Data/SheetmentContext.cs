using Microsoft.EntityFrameworkCore;

namespace Sheetments
{
    public class SheetmentContext : DbContext
    {
        const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\gjorgji.belkoski.ALLOCATESOFTWAR\\source\\repos\\Sheetments\\Sheetments\\Sheetments.mdf;Integrated Security=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Used when instantiating db context outside IoC 
            if (ConnectionString != null)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ItemLedger> ItemLedger { get; set; }
        public virtual DbSet<ShippmentItem> ShippmentItem { get; set; }
    }
}
