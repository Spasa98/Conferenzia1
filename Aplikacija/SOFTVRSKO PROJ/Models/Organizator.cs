using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Organizator")]
    public class Organizator
    {
        [Key]
        public int ID {get; set;}
        [Required]
        public string Ime {get; set;}

        public string Prezime {get; set;}
        [Required]
        public string Email {get; set;}
        [Required]
        public string Lozinka {get; set;}
        [JsonIgnore]
        public virtual List<ZahtevSlusalac> ZahteviSlusalac {get; set;}
        [JsonIgnore]
        public virtual List<ZahtevPredavac> ZahteviPredavac {get; set;}
        //virtual public List<Predavanje> Predavanja {get; set;}

    }
}