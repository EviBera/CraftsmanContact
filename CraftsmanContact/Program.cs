using CraftsmanContact.Data;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IOfferedServiceRepository, OfferedServiceRepository>();
builder.Services.AddScoped<IDealRepository, DealRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<CraftsmanContactContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")));

//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
//builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<CraftsmanContactContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("*");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();   

app.MapGroup("/identity").MapIdentityApi<AppUser>();

app.Run();

public partial class Program { }
