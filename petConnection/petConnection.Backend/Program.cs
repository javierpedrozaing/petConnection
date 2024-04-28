using Microsoft.EntityFrameworkCore;
using petConnection.Backend.Data;
using petConnection.Backend.UnitOfWork.Implementations;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder
    .Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DockerConnection"));

builder.Services.AddTransient<SeedDb>();
builder.Services.AddTransient<SeedDbUsers>();
builder.Services.AddTransient<SeedDbCountries>();
builder.Services.AddTransient<SeedDbArticles>();
builder.Services.AddTransient<SeedDbSuccessCases>();


builder.Services.AddScoped(typeof(IGenericRespository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();
builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();

builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IPetsUnitOfWork, PetsUnitOfWork>();

builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services.AddScoped<IArticlesUnitOfWork, ArticlesUnitOfWork>();

builder.Services.AddScoped<ISuccessCasesRepository, SuccessCasesRepository>();
builder.Services.AddScoped<ISuccessCasesUnitOfWork, SuccessCasesUnitOfWork>();


var app = builder.Build();

// injección manual
SeedData(app);

void SeedData(WebApplication app)
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopeFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();

        var serviceCountries = scope.ServiceProvider.GetService<SeedDbCountries>();
        serviceCountries!.SeedAsync().Wait();

        var serviceUsers = scope.ServiceProvider.GetService<SeedDbUsers>();
        serviceUsers!.SeedAsync().Wait();

        var serviceArticles = scope.ServiceProvider.GetService<SeedDbArticles>();
        serviceArticles!.SeedAsync().Wait();

        var serviceSuccessCases = scope.ServiceProvider.GetService<SeedDbSuccessCases>();
        serviceSuccessCases!.SeedAsync().Wait();
    }
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

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

