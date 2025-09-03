using ExpensesApp.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ===== Load MongoDB settings safely =====
var connectionString = builder.Configuration.GetValue<string>("MongoConnectionString")
                       ?? throw new InvalidOperationException("MongoConnectionString is missing!");
var databaseName = builder.Configuration.GetValue<string>("DatabaseName")
                       ?? throw new InvalidOperationException("DatabaseName is missing!");

// ===== MongoDB client & database =====
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase(databaseName);

// ===== Register repositories =====
builder.Services.AddSingleton<IUserRepository>(new UserRepository(mongoDatabase));
builder.Services.AddSingleton<IExpenseRepository>(new ExpenseRepository(mongoDatabase));

// ===== Add controllers & Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== Middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
