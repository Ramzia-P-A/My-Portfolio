var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Enable routing
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Portfolio}/{action=Index}/{id?}");

app.Run();
