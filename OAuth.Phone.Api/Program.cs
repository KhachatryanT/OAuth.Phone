using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Api.Application;
using OAuth.Phone.DataAccess.EF.Postgres;
using OAuth.Phone.Infrastructure.Implementation;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.UseCases;
using OAuth.Phone.Utils;
using OAuth.Phone.Utils.Settings;

const string authenticationScheme = Defaults.AuthenticationScheme;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddOptions<ConfirmationCodeSettings>()
	.Bind(builder.Configuration.GetSection(ConfirmationCodeSettings.Section));
builder.Services.AddOptions<AuthenticationCodeSettings>()
	.Bind(builder.Configuration.GetSection(AuthenticationCodeSettings.Section));
builder.Services.AddOptions<TokenSettings>()
	.Bind(builder.Configuration.GetSection(TokenSettings.Section));

builder.Services.AddDbContext<IDbContext, AppDbContext>(o =>
	o.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddAuthentication(authenticationScheme)
	.AddCookie(authenticationScheme, o =>
	{
		o.ReturnUrlParameter = "RedirectUrl";
		o.LoginPath = "/login";
	});

builder.Services.AddUseCases();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

await app.EnsureMigrationAsync<AppDbContext>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAppExceptionHandler(app.Environment);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();