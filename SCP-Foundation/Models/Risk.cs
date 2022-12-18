using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SCP_Foundation.Models
{
    public class Risk
    {
        [Display(Name = "Risk Class")]
        public string RiskId { get; set; }
        public string Rclass { get; set; }
    }
}
