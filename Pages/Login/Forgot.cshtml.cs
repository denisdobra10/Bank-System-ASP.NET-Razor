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
using System.Security.Cryptography;

namespace BankSystem.Pages.Login
{
    public class ForgotModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public ForgotModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account account { get; set; } = default!;

        [BindProperty]
        public Credentials credentials { get; set; }




        public async Task<IActionResult> OnPostAsync()
        {
            int result = CredentialsCheck();

            if (result != 0)
                return RedirectToPage("/Login/Forgot", new { id = result });

            account = DatabaseHelper.GetAccountByUsername(_context, credentials.Username);
            account.Password = Encryptor.MD5Hash(credentials.NewPassword);

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

            return RedirectToPage("/Login/Forgot", new {id=result});
        }

        private bool AccountExists(int id)
        {
          return _context.Account.Any(e => e.Id == id);
        }


        private int CredentialsCheck()
        {
            var foundAcc = DatabaseHelper.GetAccountByUsername(_context, credentials.Username);

            if(foundAcc == null)
                return 301;

            if (foundAcc.SecretQuestion != credentials.SecurityQuestion)
                return 302;

            if (!credentials.NewPassword.Equals(credentials.ConfirmNewPassword))
                return 303;


            return 0;

        }


        public class Credentials
        {
            public string Username { get; set; }
            public string SecurityQuestion { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmNewPassword { get; set; }
        }
    }
}
