using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Zvanje")]
    public class Zvanje
    {
        [Key]
        public int ID {get; set;}
        public string Name{get;set;}
    }
}