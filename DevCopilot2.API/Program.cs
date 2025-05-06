using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevCopilot2.API.Modules;
using DevCopilot2.DataLayer.Context;
using DevCopilot2.DataLayer.Repository;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
BindAppConfiguration(builder);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // Disable automatic validation response
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    }); c.AddSecurityRequirement(new OpenApiSecurityRequirement { {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { }
        }
    });
});

#region DB context config
builder.Services.AddDbContext<DevCopilot2DbContext>(options =>
{
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DevCopilot2ConnectionString"),
    sqlServerOptions => sqlServerOptions.CommandTimeout(6000));
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddExceptionHandler(options =>
{
    options.AllowStatusCode404Response = true;
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
    rlo.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(supportedCultures[0]);
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


ConfigureAuthentication(builder);
RegisterServices(builder.Services);

var app = builder.Build();

//use the following code for enabling automigration.
//*******WARNING: if you enable auto migration every time that you change sth related to db , it would recreate and data loss will happen**********
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
//    dbContext.Database.EnsureCreated();
//}
// Configure the HTTP request pipeline.app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});
if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();

}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication(); // Ensure this is before UseAuthorization
app.UseAuthorization();
app.UseRequestLocalization();
app.MapControllers();

app.Run();

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


#region Config

static void BindAppConfiguration(WebApplicationBuilder builder)
{
    var env = builder.Environment;
    builder.Configuration
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();


    builder.Services.AddOptions<AppConfig>()
    .BindConfiguration(nameof(AppConfig));


    var config = builder.Configuration.Get<AppConfig>()!;

    if (config == null)
    {
        throw new Exception("Application settings not found");
    }
}

#endregion
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var config = builder.Configuration.Get<AppConfig>()!;

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddPolicyScheme(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, option =>
    {
        option.ForwardDefaultSelector = context =>
        {
            return "default";
        };
    })
    
    .AddJwtBearer("default", options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = config.DefaultJwtSettings.ValidateAudience,
            ValidateIssuer = config.DefaultJwtSettings.ValidateIssuer,
            ValidateIssuerSigningKey = config.DefaultJwtSettings.ValidateIssuerSigningKey,
            ValidateLifetime = config.DefaultJwtSettings.ValidateLifetime,
            ValidIssuer = config.DefaultJwtSettings.Issuer,
            ValidAudience = config.DefaultJwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.DefaultJwtSettings.SecurityKey)),
            ClockSkew = TimeSpan.Zero,
        };
    });
}