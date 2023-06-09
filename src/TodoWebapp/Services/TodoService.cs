using TodoWebapp.Models;

namespace TodoWebapp.Services;

public class TodoService : ITodoService
{
    private readonly HttpClient _client;
    private readonly ILogger<TodoService> _logger;
    public TodoService(HttpClient client, IConfiguration configuration, ILogger<TodoService> logger)
    {
        client.BaseAddress = new Uri(configuration["Services:TodoAPI"]);
        _client = client;
        _logger = logger;
    }
    public async Task<Todo?> AddTodoAsync(Todo todo)
    {
        _logger.LogInformation($"Adding todo: {todo}");
        var response = await _client.PostAsJsonAsync("", todo);
        Log(response);
        return await response.Content.ReadFromJsonAsync<Todo>();
    }

    public async Task<Todo?> DeleteTodoAsync(int id)
    {
        _logger.LogInformation($"Deleting todo with id {id}");
        var response = await _client.DeleteAsync($"{id}");
        Log(response);
        return await response.Content.ReadFromJsonAsync<Todo>();
    }

    public async Task<Todo?> UpdateTodoAsync(int id, Todo todo)
    {
        _logger.LogInformation($"Updating todo with id {id}");
        var response = await _client.PutAsJsonAsync($"{id}", todo);
        Log(response);
        return await response.Content.ReadFromJsonAsync<Todo>();
    }

    public async Task<Todo?> GetTodoAsync(int id)
    {
        _logger.LogInformation($"Getting todo with id {id}");
        var response = await _client.GetAsync($"{id}");
        Log(response);
        return await response.Content.ReadFromJsonAsync<Todo>();
    }

    public async Task<IEnumerable<Todo>?> GetTodosAsync()
    {
        _logger.LogInformation("Getting all todos");
        var response = await _client.GetAsync("");
        Log(response);
        return await response.Content.ReadFromJsonAsync<IEnumerable<Todo>>();
    }

    public Task ToggleStatusAsync(int id, Todo todo)
    {
        throw new NotImplementedException();
    }

    private void Log(HttpResponseMessage response)
    {
        _logger.LogInformation($"Call to {response.RequestMessage?.RequestUri} with verb {response.RequestMessage?.Method} ended with status code {response.StatusCode}");
        _logger.LogInformation($"Response content: {response.Content.ReadAsStringAsync().Result}");
    }
}