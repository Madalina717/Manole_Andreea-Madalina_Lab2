using Microsoft.EntityFrameworkCore;

public class Manole_Andreea_Madalina_Lab2Context(DbContextOptions<Manole_Andreea_Madalina_Lab2Context> options) : DbContext(options)
{
    public DbSet<Manole_Andreea_Madalina_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Manole_Andreea_Madalina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
}
