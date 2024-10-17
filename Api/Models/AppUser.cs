using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Api.Models
{
    public class AppUser : IdentityUser
    {
        public int age { set; get; }

        public int Schoolid { get; set; }
        public virtual School School { set; get; }
    }
}
