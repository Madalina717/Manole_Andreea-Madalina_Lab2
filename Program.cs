using Microsoft.EntityFrameworkCore;
using Manole_Andreea_Madalina_Lab2.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Manole_Andreea_Madalina_Lab2Context")
    ?? throw new InvalidOperationException("Connection string 'Manole_Andreea_Madalina_Lab2Context' not found.");

builder.Services.AddDbContext<Manole_Andreea_Madalina_Lab2Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

// Adăugăm politica de autorizare bazată pe rolul de Admin (Pasul 11)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/BookPages");
    options.Conventions.AllowAnonymousToPage("/BookPages/Index");
    options.Conventions.AllowAnonymousToPage("/BookPages/Details");

    options.Conventions.AuthorizeFolder("/MemberPages", "AdminPolicy");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();