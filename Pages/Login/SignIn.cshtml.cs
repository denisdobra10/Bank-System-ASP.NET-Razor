using BankSystem.Data;
using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace BankSystem.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly BankSystem.Data.BankSystemContext _context;


        [BindProperty]
        public Credential credential { get; set; }




        public LoginModel(BankSystemContext context)
        {
            _context = context;
        }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Account logInAccount = DatabaseHelper.Login(_context, credential.Username, credential.Password);

            if (logInAccount != null)
            {
                Account.LoggedInAccount = logInAccount;
                return RedirectToPage("/User/Index");
            }

            Account.LoggedInAccount = null;
            return Page();
        }


        
        public class Credential
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }   

        }
    }
}
