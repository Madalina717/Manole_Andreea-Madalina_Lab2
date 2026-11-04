using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manole_Andreea_Madalina_Lab2.Models;

namespace Manole_Andreea_Madalina_Lab2.Pages.BorrowingPages;

public class DetailsModel : PageModel
{
    private readonly Manole_Andreea_Madalina_Lab2Context _context;

    public DetailsModel(Manole_Andreea_Madalina_Lab2Context context)
    {
        _context = context;
    }

    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        // Folosim .Include și .ThenInclude pentru a aduce datele despre Carte, Autor și Membru
        var borrowing = await _context.Borrowing
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .Include(b => b.Member)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (borrowing is null)
        {
            return NotFound();
        }
        else
        {
            Borrowing = borrowing;
        }

        return Page();
    }
}