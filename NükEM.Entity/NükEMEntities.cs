namespace NükEM.Entity
{
    using NükEM.Entity.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection.Emit;

    public class NükEMEntities : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NükEM.Entity.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public NükEMEntities()
            : base("name=NükEMEntities")
        {
            Database.SetInitializer(new DataInitializer());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context121>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
     

        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<PersonnelDetail> PersonnelDetails { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<SalaryCalculationConstant> SalaryCalculationConstants { get; set; }
        public virtual DbSet<TaxBracket> TaxBrackets { get; set; }

 
    }
}