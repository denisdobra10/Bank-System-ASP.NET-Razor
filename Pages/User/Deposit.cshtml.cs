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
    public class DepositModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;
        public List<Deposit> deposits { get; set; }

        public DepositModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
            deposits = DatabaseHelper.GetDepositsByCardNumber(_context, Account.LoggedInAccount.CardNumber);
        }

        [BindProperty]
        public DepositInfo depositInfo { get; set; }


        [BindProperty]
        public Account account { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {

            if(depositInfo.amount < 1)
            {
                // Display error
                return RedirectToPage("/User/Deposit", new { id = "error" });

            }

            account = Account.LoggedInAccount;
            account.Balance += depositInfo.amount;

            _context.Account.Update(account);
            _context.Deposit.Add(new Models.Deposit() { account = account, Amount = depositInfo.amount, Date = DateTime.Now });

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

            return RedirectToPage("/User/Deposit", new {id = "success"});
        }

        private bool AccountExists(int id)
        {
          return _context.Account.Any(e => e.Id == id);
        }


        public class DepositInfo
        {
            public float amount { get; set; }
        }
    }
}
