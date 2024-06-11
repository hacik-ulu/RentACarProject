using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit;
using NETCore.MailKit.Core;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.WriteOperations;
using RentACarProject.Application.Features.RepositoryPattern;
using RentACarProject.Application.Interfaces.BlogInterfaces;
using RentACarProject.Application.Interfaces.CarDescriptionInterfaces;
using RentACarProject.Application.Interfaces.CarFeatureInterfaces;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Application.Interfaces.CarPricingInterfaces;
using RentACarProject.Application.Interfaces.CategoryInterfaces;
using RentACarProject.Application.Interfaces.EmailInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Interfaces.RentCarInterfaces;
using RentACarProject.Application.Interfaces.ReservationInterfaces;
using RentACarProject.Application.Interfaces.ReviewInterfaces;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;
using RentACarProject.Application.Interfaces.TagCloudInterfaces;
using RentACarProject.Application.Services;
using RentACarProject.Application.Tools;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using RentACarProject.Persistence.Repositories.BlogRepositories;
using RentACarProject.Persistence.Repositories.CarDescriptionRepositories;
using RentACarProject.Persistence.Repositories.CarFeatureRepositories;
using RentACarProject.Persistence.Repositories.CarPricingRepositories;
using RentACarProject.Persistence.Repositories.CarRepository;
using RentACarProject.Persistence.Repositories.CategoryRepositories;
using RentACarProject.Persistence.Repositories.CommentRepositories;
using RentACarProject.Persistence.Repositories.EmailRepositories;
using RentACarProject.Persistence.Repositories.GeneralRepository;
using RentACarProject.Persistence.Repositories.RentCarRepositories;
using RentACarProject.Persistence.Repositories.ReviewsRepositories;
using RentACarProject.Persistence.Repositories.StatisticsRepositories;
using RentACarProject.Persistence.Repositories.TagCloudRepositories;
using RentACarProject.WebApi.Hubs;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

// CORS and SignalR
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});
builder.Services.AddSignalR();


// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ValidAudience = JwtTokenDefaults.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ClockSkew = TimeSpan.Zero // Token s�resini tam olarak belirlemek i�in
    };
});


# region Registrations
// Add Services to the container. - These are using for Mediator - 
builder.Services.AddScoped<RentACarContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICarRepository), typeof(CarRepository));
builder.Services.AddScoped(typeof(IStatisticsRepository), typeof(StatisticsRepository));
builder.Services.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
builder.Services.AddScoped(typeof(ICarPricingRepository), typeof(CarPricingRepository));
builder.Services.AddScoped(typeof(ITagCloudRepository), typeof(TagCloudRepository));
builder.Services.AddScoped(typeof(IRentCarRepository), typeof(RentCarRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ICarFeatureRepository), typeof(CarFeatureRepository));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(CommentRepository<>));
builder.Services.AddScoped(typeof(ICarDescriptionRepository), typeof(CarDescriptionRepository));
builder.Services.AddScoped(typeof(IReviewRepository), typeof(ReviewRepository));
builder.Services.AddScoped(typeof(IEmailRepository), typeof(EmailRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));


//- These are using for CQRS -

// About Service
builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();
builder.Services.AddScoped<RemoveAboutCommandHandler>();

// Banner Service
builder.Services.AddScoped<GetBannerQueryHandler>();
builder.Services.AddScoped<GetBannerByIdQueryHandler>();
builder.Services.AddScoped<CreateBannerCommandHandler>();
builder.Services.AddScoped<UpdateBannerCommandHandler>();
builder.Services.AddScoped<RemoveBannerCommandHandler>();

// Banner Service
builder.Services.AddScoped<GetBrandQueryHandler>();
builder.Services.AddScoped<GetBrandByIdQueryHandler>();
builder.Services.AddScoped<CreateBrandCommandHandler>();
builder.Services.AddScoped<UpdateBrandCommandHandler>();
builder.Services.AddScoped<RemoveBrandCommandHandler>();

// Car Service
builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<UpdateCarCommandHandler>();
builder.Services.AddScoped<RemoveCarCommandHandler>();
builder.Services.AddScoped<GetCarWithBrandQueryHandler>();
builder.Services.AddScoped<GetLastFiveCarsWithBrandsQueryHandler>();

// Category Service
builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();
builder.Services.AddScoped<GetCategoryWithBlogCountHandler>();

// Contact Service
builder.Services.AddScoped<GetContactQueryHandler>();
builder.Services.AddScoped<GetContactByIdQueryHandler>();
builder.Services.AddScoped<CreateContactCommandHandler>();
builder.Services.AddScoped<UpdateContactCommandHandler>();
builder.Services.AddScoped<RemoveContactCommandHandler>();

// Email Service
// Email Service
// Email Service
builder.Services.AddMailKit(config =>
{
    config.UseMailKit(new MailKitOptions()
    {
        Server = builder.Configuration["Smtp:Host"],
        Port = int.Parse(builder.Configuration["Smtp:Port"]),
        //SenderName = "RAP�D RENT",
        SenderEmail = builder.Configuration["Smtp:UserName"],
        Account = builder.Configuration["Smtp:UserName"],
        Password = builder.Configuration["Smtp:Password"],
        Security = builder.Configuration.GetValue<bool>("Smtp:EnableSsl")
    });
});

#endregion

// Mediator
builder.Services.AddApplicationService(builder.Configuration);

// Fluent Validation
builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

// JWT Configuration
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CarHub>("/carhub");

app.Run();