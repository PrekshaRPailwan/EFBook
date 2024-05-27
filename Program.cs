using EFBook.Models;
using EFBook.Services;
using EFBook.Services.Interface;
using EFBook.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Add DbContext to the services with SQL Server configuration.
builder.Services.AddDbContext<EbookManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));

// Register the IBookService and its implementation
builder.Services.AddScoped<IBookService, BookService>();
// Register the IAuthorService and its implementation
builder.Services.AddScoped<IAuthorService, AuthorService>();

// Add Swagger/OpenAPI services
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

app.MapControllers(); // map the controller routes

app.Run();
