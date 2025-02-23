
using TodoApplication.Domain.Abstractions;
using TodoApplication.Domain.Users;

namespace TodoApplication.Domain.Todo;

public class Todo:Entity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}