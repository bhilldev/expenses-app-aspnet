using ExpensesApp.Repositories;
using ExpensesApp.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ===== MongoDB Setup =====
var connectionString = builder.Configuration.GetConnectionString("MongoDb")
                       ?? "mongodb://localhost:27017"; // fallback

var client = new MongoClient(connectionString);
var database = client.GetDatabase("ExpenseTracker");

// Register MongoDB + repositories
builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddScoped<IRepository<User>>(sp => new Repository<User>(database, "Users"));
builder.Services.AddScoped<IRepository<Category>>(sp => new Repository<Category>(database, "Categories"));
builder.Services.AddScoped<IRepository<Expense>>(sp => new Repository<Expense>(database, "Expenses"));

// ===== ASP.NET Core Setup =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== Middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

