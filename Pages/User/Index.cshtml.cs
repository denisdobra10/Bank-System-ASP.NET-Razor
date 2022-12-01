using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.User
{
    public class IndexModel : PageModel
    {

        public Account account = Account.LoggedInAccount;



        public void OnGet()
        {
        }
    }
}
