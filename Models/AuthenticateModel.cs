using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AutentificacionModelo
    {
        [Required]
        public string usuario { get; set; }

        [Required]
        public string password { get; set; }
    }
}