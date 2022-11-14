using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPages.Models
{
    public class ClientService
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ClientID")]
        public int ClientID;
        public Client Client { get; set; }
        [ForeignKey("ServicesID")]
        public int ServicesID;
        public Services Services { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
