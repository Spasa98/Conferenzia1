using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("ReportFeedback")]
    public class ReportFeedback
    {
        [Key]
        public int ID {get; set;}

        [Required]
        public DateTime Datum {get; set;}

        [Required]
        public string Opis {get; set;}

        [Required]
        public string Status {get; set;}

        [JsonIgnore]
        public virtual Predavac Komentar {get; set;}
        [JsonIgnore]
        public virtual Feedback Feedback {get; set;}
    }
}