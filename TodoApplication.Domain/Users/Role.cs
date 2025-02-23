using System.Collections.Generic;

namespace TodoApplication.Domain.Users;

public class Role
{
    public static readonly Role Registered = new(1, "Registered");

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
    
    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
}