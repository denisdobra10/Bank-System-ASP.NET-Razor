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
using static BankSystem.Pages.User.DepositModel;
using System.Security.Principal;

namespace BankSystem.Pages.User
{
    public class WithdrawModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public List<Withdraw> withdrawals { get; set; }


        public WithdrawModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
            withdrawals = DatabaseHelper.GetWithdrawalsByCardNumber(_context, Account.LoggedInAccount.CardNumber);
        }


        [BindProperty]
        public WithdrawInfo withdrawalInfo { get; set; }

        [BindProperty]
        public Account account { get; set; } = default!;



        public async Task<IActionResult> OnPostAsync()
        {

            if (withdrawalInfo.amount < 1)
            {
                // Display error
                return RedirectToPage("/User/Withdraw", new { id = "error1" });
            }

            if (withdrawalInfo.amount > Account.LoggedInAccount.Balance)
            {
                // Display error
                return RedirectToPage("/User/Withdraw", new { id = "error2" });
            }

            account = Account.LoggedInAccount;
            account.Balance -= withdrawalInfo.amount;

            _context.Account.Update(account);
            _context.Withdraw.Add(new Models.Withdraw() { account = account, Amount = withdrawalInfo.amount, Date = DateTime.Now });


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

            return RedirectToPage("/User/Withdraw", new { id = "success" });
        }


        private bool AccountExists(int id)
        {
          return _context.Account.Any(e => e.Id == id);
        }


        public class WithdrawInfo
        {
            public float amount { get; set; }
        }
    }
}
