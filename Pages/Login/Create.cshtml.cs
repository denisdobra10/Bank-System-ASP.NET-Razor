using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankSystem.Data;
using BankSystem.Models;
using System.Security.Cryptography;

namespace BankSystem.Pages.Login
{
    public class CreateModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;

        public CreateModel(BankSystem.Data.BankSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account account { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            // Get the validation status code
            int result = ValidNewAccount(account.Username, account.Email);

            if (result != 0)
            {
                return RedirectToPage("/Login/Status", new {id = result});
            }

            // Create account
            _context.Account.Add(GenerateAccount());
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }



        private int ValidNewAccount(string userName, string email)
        {
            if (DatabaseHelper.UserExists(_context, account.Username))
                return 201;

            if (DatabaseHelper.EmailExists(_context, account.Email))
                return 202;


            return 0;
            
        }


        private Account GenerateAccount()
        {
            return new Account()
            {
                Id = account.Id,
                Username = account.Username,
                Password = Encryptor.MD5Hash(account.Password ),
                Name = account.Name,
                Gender = account.Gender,
                SecretQuestion = account.SecretQuestion,
                Email = account.Email,
                CardNumber = DatabaseHelper.GenerateCardNumber(_context),
                Pin = DatabaseHelper.GeneratePin(),
                Balance = 0.00f,
                AccountCreatedData = DateTime.Today
            };
        }
    }
}
