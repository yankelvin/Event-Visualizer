using Radix.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Radix.Events.Application.ViewModels
{
    public class EventViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Value { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Country { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Region Region { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string SensorName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan Timestamp { get; set; }

        public Status Status { get; set; }
    }
}
