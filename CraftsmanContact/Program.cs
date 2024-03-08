using CraftsmanContact.Data;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IOfferedServiceRepository, OfferedServiceRepository>();
builder.Services.AddScoped<IDealRepository, DealRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<CraftsmanContactContext, CraftsmanContactContext>();

//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
//builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<UsersContext>();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<UsersContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();   

app.MapGroup("/identity").MapIdentityApi<AppUser>();

app.Run();
