using System;
using System.ComponentModel.DataAnnotations;

namespace Radix.Events.Application.ViewModels
{
    public class ReceiveEventViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan Timestamp { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Valor { get; set; }
    }
}
