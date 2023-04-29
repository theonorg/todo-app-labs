using Microsoft.AspNetCore.Mvc;
using TodoWebapp.Models;
using TodoWebapp.Services;

namespace TodoWebapp.Controllers;

    
public class TodoController : Controller
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodoController> _logger;
    public TodoController(ITodoService todoService, ILogger<TodoController> logger)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [ActionName("Index")]
    public async Task<IActionResult> Index()
    {
        return View(await _todoService.GetTodosAsync());
    }

    [ActionName("Create")]
    public IActionResult Create()
    {
        _logger.LogInformation("Creating new todo");
        return View();
    }

    [HttpPost]
    [ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync([Bind("Title,IsComplete")] Todo todo)
    {
        _logger.LogInformation($"Creating new todo: {todo}");
        if (ModelState.IsValid)
        {
            _logger.LogInformation($"Adding new todo: Model Valid");
            todo.IsComplete = false;
            await _todoService.AddTodoAsync(todo);
            _logger.LogInformation($"Added new todo: {todo}");
            return RedirectToAction("Index");
        }

        return View(todo);
    }

    [HttpPost]
    [ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync([Bind("Id,Title,IsComplete")] Todo todo)
    {
        if (ModelState.IsValid)
        {
            await _todoService.UpdateTodoAsync(todo.Id, todo);
            return RedirectToAction("Index");
        }

        return View(todo);
    }

    [ActionName("Edit")]
    public async Task<ActionResult> EditAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        Todo? todo = await _todoService.GetTodoAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        return View(todo);
    }

    [ActionName("Delete")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        Todo? todo = await _todoService.GetTodoAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        return View(todo);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] int id)
    {
        await _todoService.DeleteTodoAsync(id);
        return RedirectToAction("Index");
    }

    [ActionName("Details")]
    public async Task<ActionResult> DetailsAsync(int id)
    {
        return View(await _todoService.GetTodoAsync(id));
    }
}

