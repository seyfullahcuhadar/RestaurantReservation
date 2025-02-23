using System;
using TodoApplication.Domain.Abstractions;

namespace TodoApplication.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;