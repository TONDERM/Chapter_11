using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SCP_Foundation.Models
{
    public class Classified
    {
        [Display(Name = "Classification")]
        public int ClassifiedId { get; set; }

        public string ClassificationLevel { get; set; }
    }
}
