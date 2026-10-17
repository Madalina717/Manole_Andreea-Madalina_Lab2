using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manole_Andreea_Madalina_Lab2.Models;

namespace Manole_Andreea_Madalina_Lab2.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly Manole_Andreea_Madalina_Lab2Context _context;

        public DetailsModel(Manole_Andreea_Madalina_Lab2Context context)
        {
            _context = context;
        }

        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Author.FirstOrDefaultAsync(m => m.ID == id);
            if (author == null) return NotFound();

            Author = author;
            return Page();
        }
    }
}