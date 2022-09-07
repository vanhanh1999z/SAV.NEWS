using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NEWS.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SAV.NEWS",
        Version = "v1.0"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<SocialfireContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"), ServiceLifetime.Scoped);

builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://devsso.hmisp.com.vn";
                    options.MetadataAddress = "https://devsso.hmisp.com.vn/adfs/.well-known/openid-configuration";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudiences = new List<string>() { "76D2F288-D23C-45B3-8DF3-B67EA9C116BB" },
                    };
                });

#endregion Authentication

#region Authorization

var authPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser() // Remove if you don't need the user to be authenticated
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .Build();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("authenticatied", authPolicy);
    //options.DefaultPolicy = authPolicy;
});

#endregion Authorization

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(devCorsPolicy);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();