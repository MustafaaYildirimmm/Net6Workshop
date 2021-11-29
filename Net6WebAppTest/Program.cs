using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Resources;
using System.Globalization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();


//builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
//{
//    var libraryPath = Path.GetFullPath(
//        Path.Combine(Environment.CurrentDirectory, "..", "MyClassLib"));

//    if (!Directory.Exists(libraryPath)) Directory.CreateDirectory(libraryPath);

//    options.FileProviders.Add(new PhysicalFileProvider(libraryPath));

//});



builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulture = new List<CultureInfo>()
    {
        new CultureInfo("en-EN"),
        new CultureInfo("tr-TR")
    };

    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    {
        // My custom request culture logic
        return new ProviderCultureResult("tr");
    }));

});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var localizeOptions = app.Services.GetService <IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizeOptions?.Value);

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


