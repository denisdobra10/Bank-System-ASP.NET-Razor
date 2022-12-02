using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankSystem.Data;
using BankSystem.Models;

namespace BankSystem.Pages.User
{
    public class TransferModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public TransferModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Transaction transaction { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetupTransaction();

            int response = Transaction.TransferMoney(_context, transaction);
            if (response == 0)
            {
                _context.Transaction.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToPage("/User/TransferStatus", new { id = "success"});
            }

            return RedirectToPage("/User/TransferStatus", new {id = response});
        }



        private void SetupTransaction()
        {
            transaction.Date = DateTime.Now;

            transaction.FromUserInitialBalance = Account.LoggedInAccount.Balance;

            transaction.FromUserAfterBalance = Account.LoggedInAccount.Balance - transaction.MoneyAmount;

            transaction.ToUserInitialBalance = 999;

            transaction.ToUserAfterBalance = 111;

        }


    }
}
