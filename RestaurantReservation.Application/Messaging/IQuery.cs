using MediatR;
using RestaurantReservation.Domain.Abstractions;

namespace RestaurantReservation.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}