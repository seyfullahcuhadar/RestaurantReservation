
using System.Threading;
using System.Threading.Tasks;
using RestaurantReservation.Domain.Abstractions;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
