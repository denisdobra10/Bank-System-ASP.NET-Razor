using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankSystem.Data;
using BankSystem.Models;

namespace BankSystem.Pages.User
{
    public class ModifyModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        [BindProperty]
        public Account account { get; set; } = default!;

        public ModifyModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
            account = Account.LoggedInAccount;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            account.AccountCreatedData = Account.LoggedInAccount.AccountCreatedData;

            _context.Account.Update(account);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Account.LoggedInAccount = account;
            return RedirectToPage("/User/Modify", new {id="success"});
        }

        private bool AccountExists(int id)
        {
          return _context.Account.Any(e => e.Id == id);
        }
    }
}
