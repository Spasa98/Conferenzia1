using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("ZahtevSlusalac")]
    public class ZahtevSlusalac
    {
        [Key]
        public int ID {get; set;}

        [Required]
        public string Text {get; set;}
        
        [Required]
        public DateTime Datum {get; set;}
        [Required]
        public string Status {get; set;}
        [JsonIgnore]
        public virtual Slusalac Slusalac {get; set;}
        [JsonIgnore]
        public virtual Predavanje Predavanje {get; set;}
    }
}