using Microsoft.AspNetCore.Identity;

namespace EMS.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public Employee? Employee { get; set; }
    }
}
