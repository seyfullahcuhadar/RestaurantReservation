
namespace TodoApplication.Domain.Todo;

public interface ITodoRepository
{
    Task AddAsync(Todo todo, CancellationToken cancellationToken);
    // Diğer gerekli metotlar eklenebilir (Get, Update, Delete vs.)
}

