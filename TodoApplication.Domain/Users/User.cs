using TodoApplication.Domain.Abstractions;
using TodoApplication.Domain.Users.Events;

namespace TodoApplication.Domain.Users;

public sealed class User:Entity
{
    private readonly List<Role> _roles = new();

    private User(Guid id, string firstName, string lastName, string email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private User()
    {
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    
    public static User Create(string firstName, string lastName, string email)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
    
}