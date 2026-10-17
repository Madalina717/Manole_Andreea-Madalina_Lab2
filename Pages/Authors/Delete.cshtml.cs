using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manole_Andreea_Madalina_Lab2.Models;

namespace Manole_Andreea_Madalina_Lab2.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly Manole_Andreea_Madalina_Lab2Context _context;

        public DeleteModel(Manole_Andreea_Madalina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Author.FirstOrDefaultAsync(m => m.ID == id);
            if (author == null) return NotFound();

            Author = author;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Author.FindAsync(id);
            if (author != null)
            {
                Author = author;
                _context.Author.Remove(Author);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}