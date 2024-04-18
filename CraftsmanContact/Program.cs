using CraftsmanContact.Data;
using CraftsmanContact.Models;
using CraftsmanContact.Services.AuthService;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Craftsman Contact API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IOfferedServiceRepository, OfferedServiceRepository>();
builder.Services.AddScoped<IDealRepository, DealRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddDbContext<CraftsmanContactContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")));


//builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.SignIn.RequireConfirmedEmail = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<CraftsmanContactContext>()
    .AddDefaultUI();


var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["JWT:Issuer"];
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["JWT:Audience"];
var jwtSigningKey = Environment.GetEnvironmentVariable("JWT_SIGNINGKEY") ?? builder.Configuration["JWT:SigningKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
                options.DefaultScheme =
                    options.DefaultSignInScheme =
                        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(jwtSigningKey))
    };
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();   

//app.MapGroup("/identity").MapIdentityApi<AppUser>();

AddAdmin();

app.Run();


void AddAdmin()
{
    var tAdmin = CreateAdminIfNotExists();
    tAdmin.Wait();
}

async Task CreateAdminIfNotExists()
{
    var email = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? builder.Configuration["Admin:Email"];
    var firstname = Environment.GetEnvironmentVariable("ADMIN_FIRSTNAME") ?? builder.Configuration["Admin:FirstName"];
    var lastname = Environment.GetEnvironmentVariable("ADMIN_LASTNAME") ?? builder.Configuration["Admin:LastName"];
    var phone = Environment.GetEnvironmentVariable("ADMIN_PHONENUMBER") ?? builder.Configuration["Admin:PhoneNumber"];
    var pw = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? builder.Configuration["Admin:Password"];
    
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var adminInDb = await userManager.FindByEmailAsync(email);
    if (adminInDb == null)
    {
        var admin = new AppUser
        {
            UserName = "admin", 
            Email = email,
            FirstName = firstname,
            LastName = lastname,
            PhoneNumber = phone
        };
        var adminCreated = await userManager.CreateAsync(admin, pw);

        if (adminCreated.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}

public partial class Program { }
