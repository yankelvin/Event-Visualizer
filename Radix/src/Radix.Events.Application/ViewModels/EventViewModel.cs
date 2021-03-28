using Radix.Core.Enums;
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
        public string TimeStamp { get; set; }

        public Status Status { get; set; }

        public EventViewModel(string id, string value, string country, Region region, string sensorName, string timeStamp)
        {
            Id = id;
            Value = value;
            Country = country;
            Region = region;
            SensorName = sensorName;
            TimeStamp = timeStamp;
        }
    }
}
