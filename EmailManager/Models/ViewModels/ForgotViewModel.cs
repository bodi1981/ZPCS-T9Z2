using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
