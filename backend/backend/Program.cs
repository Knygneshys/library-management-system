using System.Text.Json.Serialization;
using backend.Data;
using backend.Data.Seeding;
using backend.Data.Seeding.Seeders;
using backend.Services;
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

builder.Services.AddControllers()
    .AddJsonOptions(o => 
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddScoped<IAuthorServices, AuthorServices>();
builder.Services.AddScoped<IParcelLockerServices, ParcelLockerServices>();
builder.Services.AddScoped<ILockerServices, LockerServices>();
builder.Services.AddScoped<IPrintingHouseServices, PrintingHouseServices>();
builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IReservationServices, ReservationServices>();
builder.Services.AddScoped<ITaskGenerationServices, TaskGenerationServices>();
builder.Services.AddHostedService<TaskGenerationBackgroundService>();
builder.Services.AddScoped<ITaskService, TaskServices>();

builder.Services.AddScoped<ISeeder, PrintingHouseSeeder>();
builder.Services.AddScoped<ISeeder, PublisherSeeder>();
builder.Services.AddScoped<ISeeder, AuthorSeeder>();
builder.Services.AddScoped<ISeeder, GenreSeeder>();
builder.Services.AddScoped<ISeeder, BookSeeder>();
builder.Services.AddScoped<ISeeder, CopySeeder>();
builder.Services.AddScoped<ISeeder, ReservationSeeder>();
builder.Services.AddScoped<ISeeder, LoanSeeder>();
builder.Services.AddScoped<ISeeder, ParcelLockerSeeder>();

builder.Services.AddScoped<ApplicationSeeder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetService<LibraryDbContext>();

    if (dbContext is not null)
    {
        dbContext.Database.Migrate();

        var seeder = scope.ServiceProvider.GetRequiredService<ApplicationSeeder>();
        await seeder.SeedAllAsync(dbContext, scope.ServiceProvider);
    }
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();