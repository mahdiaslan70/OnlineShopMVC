using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
namespace WebApplication6.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string? FullName { get; set; }
        
        public DateTime? RegisterDate { get; set; }= DateTime.Now;

        public ICollection<Order> Orders { get; set; }
    }
}
