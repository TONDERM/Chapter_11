using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPages.Models
{
    public class Client
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="First name is required")]
        [Display(Name ="First Name")]
        [Column("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [Column("Last Name")]
        public string LastName { get; set; }


        public string Address { get; set; }
        public string City { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only alphabetic letters are allowed.")]
        public string State { get; set; }

        [Display(Name ="Zipcode")]
        [Column("Zipcode")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage="Email is required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
