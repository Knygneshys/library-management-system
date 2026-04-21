using backend.Data;
using backend.Services.Implementations;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("SQLiteDatabase");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is empty!");
}

builder.Services.AddDbContext<LibraryDbContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddScoped<IAuthorServices, AuthorServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetService<LibraryDbContext>();
    
    if (dbContext is not null)
    {
        dbContext.Database.Migrate();
    }
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();