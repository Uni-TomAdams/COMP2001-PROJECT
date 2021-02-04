using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace COMP2001_Authentication_Application.Models
{
    public class LoginResultModel
    {
        [Required]
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }

        [Required]
        [JsonPropertyName("Verified")]
        public string Verified { get; set; }
    }
}
