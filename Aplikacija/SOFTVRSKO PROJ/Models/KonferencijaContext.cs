using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class KonferencijaContext:DbContext
    {
        public KonferencijaContext(DbContextOptions options):base(options){}

        public DbSet<Organizator> Organizator {get; set;}
        public DbSet<Predavac> Predavaci {get; set;}
        public DbSet<Slusalac> Slusaoci {get; set;}
        public DbSet<Feedback> Feedbacks {get; set;}
        public DbSet<ReportFeedback> ReportFeedbacks {get; set;}
        public DbSet<Predavanje> Predavanja {get; set;}
        public DbSet<ZahtevPredavac> ZahteviPredavac {get; set;}
        public DbSet<ZahtevSlusalac> ZahteviSlusalac {get; set;}
        public DbSet<Zvanje> Zvanje {get; set;}
        public DbSet<Oblast> Oblast {get; set;}
        public DbSet<Sala> Sala {get; set;}



    }
}