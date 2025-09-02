using Microsoft.AspNetCore.Mvc;
using ExpensesApp.Repositories;
using ExpensesApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ExpensesApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Expense> _expenseRepository;
    private readonly IMongoDatabase _database;

    public AppController(
        IRepository<User> userRepository,
        IRepository<Category> categoryRepository,
        IRepository<Expense> expenseRepository,
        IMongoDatabase database)   // inject DB directly
    {
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _expenseRepository = expenseRepository;
        _database = database;
    }

    // ✅ Health check (works without any data)
    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        try
        {
            var command = new BsonDocument("ping", 1);
            var result = await _database.RunCommandAsync<BsonDocument>(command);
            return Ok(new { status = "ok", mongoResponse = result.ToString() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { status = "error", message = ex.Message });
        }
    }

    // ===== USERS =====
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers() => Ok(await _userRepository.GetAllAsync());

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        await _userRepository.AddAsync(user);
        return Ok(user);
    }

    // ===== CATEGORIES =====
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories() => Ok(await _categoryRepository.GetAllAsync());

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        await _categoryRepository.AddAsync(category);
        return Ok(category);
    }

    // ===== EXPENSES =====
    [HttpGet("expenses")]
    public async Task<IActionResult> GetExpenses() => Ok(await _expenseRepository.GetAllAsync());

    [HttpPost("expenses")]
    public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
    {
        await _expenseRepository.AddAsync(expense);
        return Ok(expense);
    }
}
