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
    .AddEntityFrameworkStores<LibraryIdentityContext>();

// Add services to the container.
builder.Services.AddRazorPages();

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