using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int ID {get; set;}
        [Required]
        public string Opis {get; set;}

        [Column(TypeName="date")]
        public DateTime Datum {get; set;}

        
        public int Tip {get; set;}//0-negative, 1-positive
        [Required]
        [Range(1,5)]
        public int Ocena {get; set;}
        //public int slusalacID {get;set;}
        //public int predavacID {get;set;}
        [JsonIgnore]
        public virtual Predavac KometarisaniPredavac {get; set;}
        [JsonIgnore]
        public virtual Slusalac SlusalacKomentarise {get; set;}
    }
}