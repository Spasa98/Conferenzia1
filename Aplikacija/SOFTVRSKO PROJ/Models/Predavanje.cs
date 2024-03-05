using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Predavanje")]
    public class Predavanje
    {
        [Key]
        public int ID {get; set;}

        [Required]
        public string Naziv {get; set;}

        [Required]
        public string Opis {get; set;}

        public DateTime Datum {get; set;}
                
        [JsonIgnore]
        public Sala sala {get; set;}

        [JsonIgnore]
        public virtual Oblast Oblast {get; set;}
        [JsonIgnore]
        public virtual Predavac Predavac { get; set; }

        [JsonIgnore]
        public virtual List<ZahtevSlusalac> ZahteviSlusalac {get; set;}
        [JsonIgnore]
        public virtual List<ZahtevPredavac> ZahteviPredavac {get; set;}
    }
}