﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankSystem.Data;
using BankSystem.Models;

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
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Account.Add(GenerateAccount());
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }



        private Account GenerateAccount()
        {
            return new Account()
            {
                Id = account.Id,
                Username = account.Username,
                Password = account.Password,
                Name = account.Name,
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
