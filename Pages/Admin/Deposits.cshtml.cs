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
    public class DepositsModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public DepositsModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        public IList<Deposit> Deposit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Deposit != null)
            {
                Deposit = await _context.Deposit.ToListAsync();
            }
        }
    }
}
