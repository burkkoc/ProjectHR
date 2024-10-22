using IKProject.Application;
using IKProject.Infrastructure;
using IKProject.Persistence;
using IKProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IK.Domain.Entities.Identity;
using IKProject.Application.Features.Querries.Login;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IKProject.Application.Interfaces.Services;
using IKProject.Infrastructure.GeneralServices;
using IKProject.Infrastructure.Services;
using IKProject.Application.Features.Querries;
using MediatR;
using IKProject.API.Filters;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IKProject API",
        Version = "v1",
        Description = "IKProject API swagger client."
    });
    c.SchemaFilter<EnumSchemaFilter>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Uygulama servislerini ekleyin
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration); 
builder.Services.AddHttpContextAccessor(); 

// DbContext yapýlandýrmasý
builder.Services.AddDbContext<IKProjectDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddCors(options =>
options.AddDefaultPolicy(policy =>
 policy.AllowAnyHeader()
 .AllowAnyMethod().WithOrigins("http://localhost:5173")
 .AllowCredentials()));

// Kimlik servislerini ekleyin
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<IKProjectDbContext>()
    .AddDefaultTokenProviders();

// MediatR servislerini ekleyin
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// LoginQueryHandler'ý DI konteynerine ekleyin
builder.Services.AddScoped<IRequestHandler<LoginQueryRequest, LoginQueryResponse>, LoginQueryHandler>();

builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IVerificationCodeService, VerificationCodeService>();
builder.Services.AddTransient<IRandomPasswordService, RandomPasswordService>();


var app = builder.Build();

// HTTP isteði yapýlandýrmasý
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IKProject API V1");
    c.RoutePrefix = string.Empty;
});
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();


