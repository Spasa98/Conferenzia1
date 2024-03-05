using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Oblast")]
    public class Oblast
    {
        [Key]
        public int ID {get; set;}
        public string Name{get;set;}
        [JsonIgnore]
        public virtual List<Predavanje> OblastPredavanje {get; set;}
        [JsonIgnore]
        public virtual List<Predavac> OblastPredavac {get; set;}
    }
}