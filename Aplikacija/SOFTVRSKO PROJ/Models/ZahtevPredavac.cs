using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("ZahtevPredavac")]
    public class ZahtevPredavac
    {
        [Key]
        public int ID {get; set;}

        public string PrijavaOdjava {get; set;}
        
        public DateTime Datum {get; set;}

        [Required]
        public string Status {get; set;}
        [JsonIgnore]
        public virtual Predavac Predavac {get; set;}
        [JsonIgnore]
        public virtual Predavanje Predavanje {get; set;}

    }
}