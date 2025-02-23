using System;
using RestaurantReservation.Application.Messaging;

namespace RestaurantReservation.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password) : ICommand<Guid>;