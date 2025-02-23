using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TodoApplication.Infrastructure.Authentication.Models;

public class ApplicationUser:IdentityUser
{
    public List<string> Roles { get; set; }
}