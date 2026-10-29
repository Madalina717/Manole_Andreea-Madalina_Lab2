using Manole_Andreea_Madalina_Lab2.Models;

namespace Manole_Andreea_Madalina_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
