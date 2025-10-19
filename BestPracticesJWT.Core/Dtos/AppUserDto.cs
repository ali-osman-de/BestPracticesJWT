namespace BestPracticesJWT.Core.Dtos;

public class AppUserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => string.Join(" ", FirstName, LastName);
}
