using System;
using System.Threading;
using System.Threading.Tasks;
using TodoApplication.Application.Abstractions.Authentication;
using TodoApplication.Application.Messaging;
using TodoApplication.Domain.Abstractions;
using TodoApplication.Domain.Users;

namespace TodoApplication.Application.Users.RegisterUser;

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
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName, request.Email);
        var result =await _authenticationService.RegisterAsync(user, request.Password);
        
        return user.Id;
    }
}