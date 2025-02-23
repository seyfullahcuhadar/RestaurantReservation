using MediatR;
using TodoApplication.Application.Todo.CreateTodo;
using TodoApplication.Domain.Abstractions;
using TodoApplication.Domain.Todo;


namespace TodoApplication.Application.Todo.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITodoRepository _todoRepository;

    public CreateTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository)
    {
        _unitOfWork = unitOfWork;
        _todoRepository = todoRepository;
    }

    public async Task<Result<Guid>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Domain.Todo.Todo
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };
        await _todoRepository.AddAsync(todo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(todo.Id);
    }
}

