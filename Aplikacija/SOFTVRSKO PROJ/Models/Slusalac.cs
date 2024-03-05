using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Slusalac")]
    public class Slusalac
    {
        [Key]
        public int ID {get; set;}

        [Required]
        public string Ime {get; set;}

        [Required]
        public string Prezime {get; set;}

        [Required]
        public string Email {get; set;}

        [Required]
        public int Telefon {get; set;}

        [Required]
        public string Lozinka {get; set;}
        public int ArchiveFlag {get;set;}
        //public int predavanjeId{get;set;}
        
        public virtual List<ZahtevSlusalac> ZahteviSlusalac {get; set;}
        public virtual List<Feedback> feedback {get; set;}
    }
}