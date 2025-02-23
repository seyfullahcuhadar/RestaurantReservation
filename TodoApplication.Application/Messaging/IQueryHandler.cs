using MediatR;
using TodoApplication.Domain.Abstractions;

namespace TodoApplication.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
