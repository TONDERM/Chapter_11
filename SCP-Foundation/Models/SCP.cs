using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SCP_Foundation.Models
{
    [Table("SCPs",Schema ="Information")]
    public class SCP
    {
        public int SCPID { get; set; }

        [Required(ErrorMessage = "&nbsp No name?")]
        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "Containment Procedures")]
        [StringLength(5000, MinimumLength = 8, ErrorMessage = "Please provide more details on containment")]
        public string ContainmentProcedure { get; set; }

        [StringLength(5000, MinimumLength = 8, ErrorMessage = "Please elaborate on the description")]
        public string Description { get; set; }

        [Display(Name = "ID Number")]
        [Required(ErrorMessage = "&nbsp SCP ID number is required.")]
        [RegularExpression("^(SCP)-[0-9]{4}$", ErrorMessage = "&nbsp Please enter a valid Id Number")]
        [Column("SCP ID#")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Please select Risk Class.")]
        [Display(Name = "Risk Class")]
        public string RiskId { get; set; }
        public Risk Risk { get; set; }

        [Required(ErrorMessage = "Please select Containment Class.")]
        [Display(Name = "Containment Class")]

        public string ContainId { get; set; }
        public Contain Contain { get; set; }

        [Required(ErrorMessage = "Please select Disruption Class.")]
        [Display(Name = "Disruption Class")]
        [Column("Disruption Class")]
        public string DisruptionId { get; set; }
        public Disruption Disruption { get; set; }

        [Display(Name = "Classified Level")]
        [Range(1,6, ErrorMessage="Select Classification Level")]
        public int ClassifiedId { get; set; }
        public Classified Classified { get; set; }

    }
}

