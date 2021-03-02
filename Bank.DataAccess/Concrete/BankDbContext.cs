using System;
using System.Collections.Generic;
using System.Text;
using Bank.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Bank.DataAccess.Concrete
{
    public class BankDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BankDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<RemOperation> RemOperations { get; set; }
        public DbSet<RemReceiver> RemReceivers { get; set; }
        public DbSet<SecQue> SecQues { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }


    }
}
