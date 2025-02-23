using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RestaurantReservation.Infrastructure.Authentication.Models;

public class ApplicationUser:IdentityUser
{
    public List<string> Roles { get; set; }
}