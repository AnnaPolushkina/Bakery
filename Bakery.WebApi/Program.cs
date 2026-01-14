using Bakery.Core.Services;
using Bakery.Infrastructure.Services;
using Bakery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Bakery.Infrastructure.Seed;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BakeryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BakeryDb")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();
    ProductSeeder.Seed(context);
}

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

