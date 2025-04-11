using Microsoft.AspNetCore.Identity;

namespace EticaretAPI.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
