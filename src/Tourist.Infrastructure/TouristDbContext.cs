using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Tourist.Domain;

namespace Tourist.Infrastructure;
public class TouristDbContext //: DbContext
{
    public TouristDbContext()//(DbContextOptions options) : base(options)
    {
        SQLiteConnection sqlite_conn;
        sqlite_conn = CreateConnection();
        CreateTable(sqlite_conn);
        InsertData(sqlite_conn);
    }
    static SQLiteConnection CreateConnection()
      {

         SQLiteConnection sqlite_conn;
         // Create a new database connection:
         sqlite_conn = new SQLiteConnection("Data Source=Tourist.db;Version=3;New=True;Compress=True;");
         // Open the connection:
         try
         {
            sqlite_conn.Open();
         }
         catch (Exception ex)
         {

         }
         return sqlite_conn;
      }

     static void CreateTable(SQLiteConnection conn)
      {
         SQLiteCommand sqlite_cmd;
         string Createsql = @"CREATE TABLE Customer
            (Id VARCHAR(20), Name VARCHAR(255), Address VARCHAR(255))";
         string Createsql1 = @"CREATE TABLE SampleTable1
            (Col1 VARCHAR(20), Col2 INT)";
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = Createsql;
         sqlite_cmd.ExecuteNonQuery();
        //  sqlite_cmd.CommandText = Createsql1;
        //  sqlite_cmd.ExecuteNonQuery();
      }

      static void InsertData(SQLiteConnection conn)
      {
         SQLiteCommand sqlite_cmd;
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = @"INSERT INTO Customer
            (Name, Address) VALUES ('Инекс Огица', 'бб');";
         sqlite_cmd.ExecuteNonQuery();
         sqlite_cmd.CommandText = @"INSERT INTO Customer
            (Name, Address) VALUES ('Хотел Тино', 'кеј Македонија бр. 56');";
         sqlite_cmd.ExecuteNonQuery();
         sqlite_cmd.CommandText = @"INSERT INTO Customer
            (Name, Address) VALUES ('Хотел лебед', 'кеј Македонија бр. 67');";
         sqlite_cmd.ExecuteNonQuery();
        sqlite_cmd.CommandText = @"INSERT INTO Customer
            (Name, Address) VALUES ('Хотел гарден', 'кеј Македонија бр. 71');";
         sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = @"INSERT INTO Customer
            (Name, Address) VALUES ('Хотел милениум палас', 'кеј Македонија бр. 65');";
         sqlite_cmd.ExecuteNonQuery();
        sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = @"INSERT INTO Customer
            (Name, Address) VALUES ('Вила жика', 'кеј Македонија бр. 61');";
         sqlite_cmd.ExecuteNonQuery();


        //  sqlite_cmd.CommandText = @"INSERT INTO Customer
        //     (Col1, Col2) VALUES ('Test3 Text3 ', 3);";
        //  sqlite_cmd.ExecuteNonQuery();
      }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Customer>(entity => 
    //     {
    //         entity.HasKey(e => e.Id);
    //         entity.Property(e => e.Id);
    //         entity.Property(e => e.Name).HasMaxLength(255);
    //         entity.Property(e => e.Address).HasMaxLength(255);
    //         entity.HasData(
    //             new Customer
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Name = "Инекс Олгица",
    //                 Address = "bb"
    //             },
    //             new Customer
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Name = "Хотел Тино",
    //                 Address = "кеј Македонија бр. 56"
    //             },
    //             new Customer
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Name = "Хотел Лебед",
    //                 Address = "кеј Македонија бр. 87"
    //             },
    //             new Customer
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Name = "Хотел Гарден",
    //                 Address = "кеј Македонија бр. 89"
    //             },
    //             new Customer
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Name = "Хотел Милениум Палас",
    //                 Address = "кеј Македонија бр. 80"
    //             },
    //             new Customer
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Name = "Вила Жика",
    //                 Address = "кеј Македонија бр. 66"
    //             }
    //         );
    //     });
    // }
}