using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Sala")]
    public class Sala
    {
        [Key]
        public int ID {get; set;}
        public string Ime{get;set;}
        public int Kapacitet{get;set;}
        public virtual List<Predavanje> Predavanje {get; set;}
    }
}