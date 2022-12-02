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
    public class BalanceModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;


        public List<Transaction> transactions { get; set; }


        public BalanceModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
            transactions = DatabaseHelper.GetTransactionsByCardNumber(_context, Account.LoggedInAccount.CardNumber);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        

        //public async Task<IActionResult> OnPostAsync()
        //{
        //  if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Transaction.Add(Transaction);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
