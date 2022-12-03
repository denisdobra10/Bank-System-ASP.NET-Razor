using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankSystem.Data;
using BankSystem.Models;

namespace BankSystem.Pages.User
{
    public class EraseModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public EraseModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Account account { get; set; }

        public void OnGet()
        {
            account = Account.LoggedInAccount;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            account = Account.LoggedInAccount;
            Account.LoggedInAccount = null;

            if (account != null)
            {
                _context.Account.Remove(account);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/User/Erase", new {id = "success"});

        }
    }
}
