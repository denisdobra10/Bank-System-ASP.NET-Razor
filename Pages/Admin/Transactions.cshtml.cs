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

      public Transaction Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }
            else 
            {
                Transaction = transaction;
            }
            return Page();
        }
    }
}
