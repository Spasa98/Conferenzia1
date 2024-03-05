using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Predavac")]
    public class Predavac
    {  
        [Key]
        public int ID {get; set;}
        [Required]
        public string Ime {get; set;}
        [Required]
        public string Prezime {get; set;}
        [Required]
        public string Opis {get; set;}

        public string Email {get; set;}

        public string Lozinka {get; set;}
        [Required]
        public int Telefon {get; set;}
        [Required]
        public string Grad {get; set;}
        
        public string PutanjaSlike {get; set;}
        public int ArchiveFlag {get;set;}
		
		[Required]
		public int ZvanjeID {get;set;}
        [JsonIgnore]
        public Zvanje Zvanje {get; set;}
        
		public int OblastID {get;set;}
        [JsonIgnore]
        public Oblast Oblast {get; set;}
        [JsonIgnore]
        public virtual List<Predavanje> PredavacPredmet {get; set;}
        [JsonIgnore]
        public virtual List<ZahtevPredavac> ZahteviPredavac {get; set;}
        [JsonIgnore]
        public virtual List<ReportFeedback> Reporteedbacks {get; set;}
        [JsonIgnore]
        public virtual List<Feedback> Feedbacks {get; set;}

    }
}