using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Domains
{
    public class Email
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole adresat jest wymagane.")]
        [Display(Name = "Adresat")]
        [MaxLength(50)]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Pole nadawca jest wymagane.")]
        [RegularExpression(@"^(([a-zA-Z\-0-9\.]+@)([a-zA-Z\-0-9\.]+)[;]*)+$", ErrorMessage = "Pole Nadawca nie jest prawidłowym adresem e-mail.")]
        [Display(Name = "Nadawca")]
        [MaxLength(250)]
        public string Reciepient { get; set; }

        [Display(Name = "Temat")]
        [MaxLength(50)]
        public string Subject { get; set; }

        [Display(Name = "Treść wiadomości")]
        [MaxLength(500)]
        public string Message { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}