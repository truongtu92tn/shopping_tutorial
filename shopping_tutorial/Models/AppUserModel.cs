using Microsoft.AspNetCore.Identity;

namespace shopping_tutorial.Models
{
    public class AppUserModel: IdentityUser
    {
        public string? Occupation {  get; set; }
    }
}
