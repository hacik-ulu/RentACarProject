using RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.WriteOperations;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Services;
using RentACarProject.Persistence.Context;
using RentACarProject.Persistence.Repositories.CarRepository;
using RentACarProject.Persistence.Repositories.GeneralRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<RentACarContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICarRepository), typeof(CarRepository));

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

// Category Service
builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();

// Contact Service
builder.Services.AddScoped<GetContactQueryHandler>();
builder.Services.AddScoped<GetContactByIdQueryHandler>();
builder.Services.AddScoped<CreateContactCommandHandler>();
builder.Services.AddScoped<UpdateContactCommandHandler>();
builder.Services.AddScoped<RemoveContactCommandHandler>();

// Mediator
builder.Services.AddApplicationService(builder.Configuration);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
