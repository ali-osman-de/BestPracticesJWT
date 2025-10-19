using Microsoft.AspNetCore.Identity;

namespace BestPracticesJWT.Core.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => string.Join(" ", FirstName, LastName);
    public string City { get; set; }
}
