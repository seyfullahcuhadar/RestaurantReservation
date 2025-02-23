using MediatR;
using RestaurantReservation.Domain.Abstractions;

namespace RestaurantReservation.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
