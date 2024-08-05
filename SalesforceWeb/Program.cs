using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Events;
using SalesforceWeb.Profiles;
using SalesforceWeb.Services;
using SalesforceWeb.Services.IServices;
using MagicVilla_Web.Services;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760, retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container with appropriate lifetimes
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(DtoProfile));
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor(); // Scoped service for HTTP request context

builder.Services.AddScoped<IOAuthService, OAuthService>();
builder.Services.AddScoped<IPractitionerService, PractitionerService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<ISalesforceService, SalesforceService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddHttpClient<IRelatedAccountService, RelatedAccountService>();
builder.Services.AddHttpClient<IServiceListingService, ServiceListingService>();

// Register other dependencies
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add session and authentication services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.SlidingExpiration = true;
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvcCore().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopRight,
    CloseButton = true,
    PreventDuplicates = true,
    NewestOnTop = true,
    TapToDismiss = true,
    HideDuration = 5000,
},
new NToastNotifyOption()
{
    DefaultSuccessTitle = "Success",
    DefaultSuccessMessage = "Operation successful",
    DefaultErrorTitle = "Error",
    DefaultErrorMessage = "Operation failed",
    DefaultInfoTitle = "Info",
    DefaultInfoMessage = "Information",
    DefaultWarningTitle = "Warning",
    DefaultWarningMessage = "Warning",
    ScriptSrc = "lib/toastr.js/toastr.min.js",
    StyleHref = "lib/toastr.js/toastr.min.css"
});


var app = builder.Build();


// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
