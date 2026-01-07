using KarapinhaAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Interfaces;
using Shared.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});




// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<SeedHelpers>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUtilizadorInterface, UtilizadorRepository>();
builder.Services.AddScoped<ICategoriaInterface, CategoriaRepository>();
builder.Services.AddScoped<IServicoInterface, ServicoRepository>();
builder.Services.AddScoped<IMarcacaoInterface, MarcacaoRepository>();
builder.Services.AddScoped<IProfissionalInterface, ProfissionalRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedHelpers>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
