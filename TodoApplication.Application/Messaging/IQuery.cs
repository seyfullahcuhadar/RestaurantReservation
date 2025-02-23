using MediatR;
using TodoApplication.Domain.Abstractions;

namespace TodoApplication.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}