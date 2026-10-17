using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Manole_Andreea_Madalina_Lab2.Models;

namespace Manole_Andreea_Madalina_Lab2.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly Manole_Andreea_Madalina_Lab2Context _context;

        public CreateModel(Manole_Andreea_Madalina_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Author.Add(Author);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}