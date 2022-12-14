using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankSystem.Data;
using BankSystem.Models;

namespace BankSystem.Pages.Admin
{
    public class TransactionsModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public TransactionsModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transaction != null)
            {
                Transaction = await _context.Transaction.ToListAsync();
            }
        }
    }
}
