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
    public class WithdrawalsModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public WithdrawalsModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        public IList<Withdraw> Withdraw { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Withdraw != null)
            {
                Withdraw = await _context.Withdraw.ToListAsync();
            }
        }
    }
}
