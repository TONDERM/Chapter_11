using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SCP_Foundation.Models
{
    public class Contain
    {
        [Display(Name = "Containment Class")]
        public string ContainId { get; set; }
        public string Cclass { get; set; }
    }
}
