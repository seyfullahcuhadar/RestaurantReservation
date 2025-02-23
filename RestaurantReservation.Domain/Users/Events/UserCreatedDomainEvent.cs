using System;
using RestaurantReservation.Domain.Abstractions;

namespace RestaurantReservation.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;