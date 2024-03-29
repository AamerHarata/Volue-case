using Volue_case.AppConfigurations;
using Volue_case.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(builder.Environment.IsDevelopment() ? "" : "http://*:80");

// Add services to the container.

ConfigureDb.Configure(builder.Services, builder.Configuration.GetConnectionString("Postgres"));

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
ConfigureServices.Configure(builder.Services);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
await DbInitializer.InitializeDatabase(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
