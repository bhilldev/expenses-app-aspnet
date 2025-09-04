using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpensesApp.Models;
using ExpensesApp.Repositories;
using ExpensesApp.Dtos;

namespace ExpensesApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IExpenseRepository _expenseRepository;

    // Hard-coded categories
    private readonly List<Category> _categories = new()
    {
        new Category { Id = "1", Name = "Food" },
        new Category { Id = "2", Name = "Transport" },
        new Category { Id = "3", Name = "Entertainment" },
        new Category { Id = "4", Name = "Utilities" }
    };

    public AppController(
        IUserRepository userRepository,
        IExpenseRepository expenseRepository)
    {
        _userRepository = userRepository;
        _expenseRepository = expenseRepository;
    }

    // ===== PING =====
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("Server is running!");

    // ===== USERS =====
    [HttpGet("users")]
    [Authorize] // Only authenticated users can see all users
    public async Task<IActionResult> GetUsers() =>
        Ok(await _userRepository.GetAllAsync());

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        // Check if username already exists
        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
            return BadRequest("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email
        };

        await _userRepository.AddAsync(user);
        return Ok(user);
    }

    // ===== CATEGORIES =====
    [HttpGet("categories")]
    [Authorize] // Only authenticated users can access categories
    public IActionResult GetCategories() => Ok(_categories);

    // ===== EXPENSES =====
    [HttpGet("expenses")]
    [Authorize]
    public async Task<IActionResult> GetExpenses() =>
        Ok(await _expenseRepository.GetAllAsync());

    [HttpGet("expenses/filter")]
    [Authorize]
    public async Task<IActionResult> GetExpensesFiltered(
        [FromQuery] string? range = "all",
        [FromQuery] DateTime? start = null,
        [FromQuery] DateTime? end = null)
    {
        IEnumerable<Expense> expenses = range?.ToLower() switch
        {
            "lastweek" => await _expenseRepository.GetLastWeekExpensesAsync(),
            "pastmonth" => await _expenseRepository.GetPastMonthExpensesAsync(),
            "past3months" => await _expenseRepository.GetPast3MonthsExpensesAsync(),
            "custom" when start.HasValue && end.HasValue =>
                await _expenseRepository.GetExpensesByDateRangeAsync(start.Value, end.Value),
            _ => await _expenseRepository.GetAllAsync()
        };

        return Ok(expenses);
    }

    [HttpPost("expenses")]
    [Authorize]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        // Validate category exists
        if (!_categories.Any(c => c.Id == dto.CategoryId))
            return BadRequest("Invalid category ID.");

        // Use user ID from JWT claim if available
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (userId == null)
            return Unauthorized();

        var expense = new Expense
        {
            UserId = userId,
            CategoryId = dto.CategoryId,
            AmountCents = dto.AmountCents,
            Date = dto.Date,
            Description = dto.Description
        };

        await _expenseRepository.AddAsync(expense);
        return Ok(expense);
    }
}

