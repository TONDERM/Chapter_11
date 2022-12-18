using System.ComponentModel.DataAnnotations;

namespace SCP_Foundation.Models
{
    public class Disruption
    {
        [Display(Name = "Disruption Class")]
        public string DisruptionId { get; set; }
        public string Dclass { get; set; }
    }
}
