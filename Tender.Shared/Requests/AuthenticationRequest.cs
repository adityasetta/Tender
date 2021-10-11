namespace Tender.Shared.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class AuthenticationRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
