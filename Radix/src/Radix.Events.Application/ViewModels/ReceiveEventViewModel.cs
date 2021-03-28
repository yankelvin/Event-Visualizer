using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Radix.Events.Application.ViewModels
{
    public class ReceiveEventViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Timestamp { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Tag { get; set; }

        [JsonPropertyName("Valor")]
        public string Value { get; set; }
    }
}
