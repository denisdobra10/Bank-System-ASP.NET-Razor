using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.User
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            Account.LoggedInAccount = null;

            return RedirectToPage("/Index");
        }
    }
}
