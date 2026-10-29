using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manole_Andreea_Madalina_Lab2.Models;


namespace Manole_Andreea_Madalina_Lab2.Pages.BookPages
{
    public class IndexModel : PageModel
    {
        private readonly Manole_Andreea_Madalina_Lab2Context _context;

        public IndexModel(Manole_Andreea_Madalina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; } = default!;
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;

            var booksIQ = from b in _context.Book
                          .Include(b => b.Author)
                          .Include(b => b.Publisher)
                          .Include(b => b.BookCategories)
                          .ThenInclude(b => b.Category)
                          .AsNoTracking()
                          select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                booksIQ = booksIQ.Where(s => s.Author.FirstName.Contains(searchString)
                                          || s.Author.LastName.Contains(searchString)
                                          || s.Title.Contains(searchString));
            }

            BookD.Books = await booksIQ.OrderBy(b => b.Title).ToListAsync();

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .Where(i => i.ID == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author.FullName).ToList();
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author.FullName).ToList();
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title).ToList();
                    break;
            }
        }
    }
}