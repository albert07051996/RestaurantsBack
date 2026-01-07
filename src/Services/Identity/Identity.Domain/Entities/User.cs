using BuildingBlocks.Shared.Domain;

namespace Identity.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    private User() { }

    public User(string email, string userName, string passwordHash, string firstName, string lastName, string? phoneNumber = null)
    {
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        IsActive = true;
    }

    public void UpdateProfile(string firstName, string lastName, string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        UpdateTimestamp();
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdateTimestamp();
    }

    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }

    public void AddRole(Role role)
    {
        if (!UserRoles.Any(ur => ur.RoleId == role.Id))
        {
            UserRoles.Add(new UserRole(Id, role.Id));
        }
    }
}
