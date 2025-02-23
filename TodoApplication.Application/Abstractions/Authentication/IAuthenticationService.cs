

using TodoApplication.Domain.Abstractions;
using TodoApplication.Domain.Users;

namespace TodoApplication.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
