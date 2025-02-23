using System;
using System.Threading;
using System.Threading.Tasks;
using RestaurantReservation.Application.Abstractions.Authentication;
using RestaurantReservation.Application.Messaging;
using RestaurantReservation.Domain.Abstractions;

namespace RestaurantReservation.Application.Users.RegisterUser;

public class RegisterUserCommandHandler:ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
    }
    public  Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
   

        return null;
    }
}