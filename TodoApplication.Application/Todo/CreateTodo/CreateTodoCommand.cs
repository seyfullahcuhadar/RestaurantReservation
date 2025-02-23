using MediatR;
using TodoApplication.Domain.Abstractions;

namespace TodoApplication.Application.Todo.CreateTodo;

public record CreateTodoCommand(string Title, string Description) : IRequest<Result<Guid>>;

