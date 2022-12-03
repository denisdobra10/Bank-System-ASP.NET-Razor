using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankSystem.Models;

namespace BankSystem.Data
{
    public class BankSystemContext : DbContext
    {
        public BankSystemContext (DbContextOptions<BankSystemContext> options)
            : base(options)
        {
        }

        public DbSet<BankSystem.Models.Account> Account { get; set; } = default!;

        public DbSet<BankSystem.Models.Transaction> Transaction { get; set; }

        public DbSet<BankSystem.Models.Deposit> Deposit { get; set; }
    }
}
