using TodoApplication.Domain.Todo;

namespace TodoApplication.Infrastructure.Repositories;

public class TodoRepository:Repository<Todo>,ITodoRepository
{
    public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task AddAsync(Todo todo, CancellationToken cancellationToken)
    {
        if (todo is null)
        {
            throw new ArgumentNullException(nameof(todo), "Todo cannot be null");
        }

        Add(todo);
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}