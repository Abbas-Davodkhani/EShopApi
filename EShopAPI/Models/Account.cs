using System.ComponentModel.DataAnnotations;

namespace EShopAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
