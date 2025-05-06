using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevCopilot2.Web.Modules;
using DevCopilot2.DataLayer.Context;
using DevCopilot2.DataLayer.Repository;
using DevCopilot2.Domain.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using DevCopilot2.Web.MiddleWares;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

#region Services

#region data protection

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + "\\wwwroot\\Auth\\"))
    .SetApplicationName("DevCopilot2Project")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/sign-in";
    options.LogoutPath = "/log-out";
    options.ExpireTimeSpan = TimeSpan.FromDays(180);
    options.SlidingExpiration = true;
});

#endregion

#region Resources

builder.Services.AddRequestLocalization(rlo =>
{
    List<CultureInfo> supportedCultures = new()
    {
        new("fa-IR"),
        new("en-US"),
    };
    rlo.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    rlo.SupportedCultures = supportedCultures;
    rlo.SupportedUICultures = supportedCultures;
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddControllersWithViews()
     .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

#endregion

builder.Services.AddMvc();

builder.Services.AddControllersWithViews();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = new[] { "text/plain" };
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

RegisterServices(builder.Services);

#region DbContext Config

builder.Services.AddDbContext<DevCopilot2DbContext>(options =>
{
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DevCopilot2ConnectionString"),
    sqlServerOptions => sqlServerOptions.CommandTimeout(10));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#endregion

#region html encoder

builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

#endregion

#endregion

#region App


var app = builder.Build();
//use the following code for enabling automigration.
//*******WARNING: if you enable auto migration every time that you change sth related to db , it would recreate and data loss will happen**********
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
//    dbContext.Database.EnsureCreated();
//}
// Configure the HTTP request pipeline.
//remove this code for the production
//app.UseDeveloperExceptionPage();
#region Not Found

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404) context.Response.Redirect("/404");
});

#endregion
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    //  app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler("/404");
}

//The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();
app.UseHttpsRedirection();
StaticFileOptions options = new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static files for 30 days
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
        ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
    },
    ContentTypeProvider = new FileExtensionContentTypeProvider()
};
((FileExtensionContentTypeProvider)options.ContentTypeProvider).Mappings.Add(new KeyValuePair<string, string>(".glb", "model/gltf-buffer"));

app.UseStaticFiles(options);
app.UseMiddleware<SynchronousIOMiddleware>();
app.UseResponseCompression();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization();
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


#region AddIoC

void RegisterServices(IServiceCollection services)
{
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
         .ConfigureContainer<ContainerBuilder>(builder =>
         {
             builder.RegisterModule(new AutofacModule());
         });
    services.AddScoped(typeof(ICrudRepository<,>), typeof(CrudRepository<,>));
}

#endregion


app.Run();

#endregion