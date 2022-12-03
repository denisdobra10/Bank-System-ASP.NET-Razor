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
    public class StatementModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        [BindProperty]
        public Account account { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<Deposit> Deposits { get; set; }
        public List<Withdraw> Withdrawals { get; set; }

        public StatementModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            account = Account.LoggedInAccount;

            Transactions = DatabaseHelper.GetTransactionsByCardNumber(_context, account.CardNumber);
            Deposits = DatabaseHelper.GetDepositsByCardNumber(_context, account.CardNumber);
            Withdrawals = DatabaseHelper.GetWithdrawalsByCardNumber(_context, account.CardNumber);

            return Page();
        }

        

    }
}
