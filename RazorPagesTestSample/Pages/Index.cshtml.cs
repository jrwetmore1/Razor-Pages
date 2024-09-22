using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTestSample.Data;

namespace RazorPagesTestSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Message> Messages { get; set; }

        [BindProperty]
        public Message NewMessage { get; set; }

        public async Task OnGetAsync()
        {
            Messages = await _db.GetMessagesAsync();
        }

        public async Task<IActionResult> OnPostAddMessageAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _db.AddMessageAsync(NewMessage);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAllMessagesAsync()
        {
            await _db.DeleteAllMessagesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteMessageAsync(int id)
        {
            await _db.DeleteMessageAsync(id);
            return RedirectToPage();
        }
    }
}
