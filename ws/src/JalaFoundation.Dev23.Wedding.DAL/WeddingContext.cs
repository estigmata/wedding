using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WeddingContext : DbContext
    {
        // Your context has been configured to use a 'WeddingContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'JalaFoundation.Dev23.Wedding.DAL.WeddingContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WeddingContext' 
        // connection string in the application configuration file.
        public WeddingContext()
            : base("name=WeddingContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Product> Products { get; set; }
        public DbSet<Couple> Couples { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<PresentList> PresentLists { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<Dedicatory> Dedicatories { get; set; }
    }
}