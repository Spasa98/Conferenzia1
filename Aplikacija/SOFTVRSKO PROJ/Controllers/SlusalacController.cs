using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Proba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlusalacController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public SlusalacController(KonferencijaContext context)
        {
            Context = context;
        }

        [AllowAnonymous]
        [Route("FeedbackoviSlusaoca/{slusalacID}")]
        [HttpGet]
        public async Task<ActionResult> FeedbackoviSlusaoca(int slusalacID)
        {
           try
            {   
                var feedbackovi=Context.Feedbacks;
                   var slusalac=await Context.Slusaoci.Where(s=>s.ID==slusalacID).FirstOrDefaultAsync();
                var ffeedback=await feedbackovi
                .Where(p=>p.SlusalacKomentarise==slusalac).Select(q=>new{
                            Opis=q.Opis,
                            Datum=q.Datum.ToLongDateString(),
                            Ocena=q.Ocena,
                            Predavac=q.KometarisaniPredavac.Ime +" "+ q.KometarisaniPredavac.Prezime,
                })
                .ToListAsync();

                return Ok(ffeedback);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("VratiSlusaoca/{slusalacID}")]
        [HttpGet]
        public async Task<ActionResult> VratiSlusaoca(int slusalacID)
        {
           try
            {   
                Slusalac s=await Context.Slusaoci.Where(x=>x.ID==slusalacID).FirstOrDefaultAsync();

                return Ok(s);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Slusalac,Organizator")]
        [Route("PoslatiZahtevi/{slusalacID}")]
        [HttpGet]
        public async Task<ActionResult> PoslatiZahtevi(int slusalacID)
        {
           try
            {   
                var zahtevi=Context.Slusaoci
                    .Include(p=>p.ZahteviSlusalac).ThenInclude(p=>p.Predavanje); 
                var zahtev=await zahtevi
                .Where(p=>p.ID==slusalacID)
                .Select(p=>
                new
                {
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    Email=p.Email,
                    Zahtevi=p.ZahteviSlusalac.Select(q=>
                        new
                        {
                            Text=q.Text,
                            Datum=q.Datum.ToLongDateString(),
                            Status=q.Status,
                            Predavanje=q.Predavanje.Naziv,
                            
                        }
                    )
                }).ToListAsync();

                return Ok(zahtev);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
      
        [Route("BrojStranica/{str}/{id}")]
        [HttpGet]
        public  ActionResult BrojStranica(string str,int id)
        {
            
            try
            {   
                var slusalac= Context.Slusaoci.Where(q=>q.ID==id).FirstOrDefault();
                return Ok(Context.ZahteviSlusalac.Where(q=>q.Status.Equals(str)&&q.Slusalac==slusalac&&q.Predavanje.Datum>DateTime.Now).Include(w=>w.Predavanje).Count());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [Authorize(Roles = "Slusalac")]
        [Route("TrazeniZahtevi/{slusa}/{status}/{pagenumber}")]
        [HttpGet]
        public async Task<ActionResult> TrazeniZahtevi(int slusa,string status,int pagenumber)
        {
            const int elPerPage=3;
           try
            {   
                var slusalac= Context.Slusaoci.Where(q=>q.ID==slusa).FirstOrDefault();
                var zahtevi=Context.ZahteviSlusalac.Where(w=>w.Slusalac==slusalac && w.Status.Equals(status)&&w.Predavanje.Datum>DateTime.Now).Include(e=>e.Predavanje);
                var zahtev=await zahtevi
                .Select(p=>
                new
                { 
                    id=p.ID,
                    predavanje=p.Predavanje.Naziv,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    
                }).OrderBy(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                return Ok(zahtev);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [Route("DodajSlusaoca")]
        [HttpPost]
        public async Task<ActionResult> DodajSlusaoca([FromBody]Slusalac slusalac)
        {
           try
            {
         
                Context.Slusaoci.Add(slusalac);
                await Context.SaveChangesAsync();

                return Ok(slusalac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Slusalac,Organizator")]
        [Route("IzmeniSlusaoca/{slusalacID}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniSlusaoca(int slusalacID,[FromBody] Slusalac slusalac)
        {
           try
            {  

                Slusalac s=await Context.Slusaoci.Where(x=>x.ID==slusalacID).FirstOrDefaultAsync();
                if(string.IsNullOrWhiteSpace(slusalac.Ime)){
                    slusalac.Ime=s.Ime;
                }else if(string.IsNullOrWhiteSpace(slusalac.Prezime)){
                    slusalac.Prezime=s.Prezime;
                }else if(slusalac.Telefon<=0){
                    slusalac.Telefon=s.Telefon;
                }else if(string.IsNullOrWhiteSpace(slusalac.Email)){
                    slusalac.Email=s.Email;
                }else if(string.IsNullOrWhiteSpace(slusalac.Lozinka)){
                    slusalac.Lozinka=s.Lozinka;
                }
                    s.Ime=slusalac.Ime;
                    s.Prezime=slusalac.Prezime;
                    s.Email=slusalac.Email;
                    s.Telefon=slusalac.Telefon;
                    s.Lozinka=slusalac.Lozinka;
                
                
                await Context.SaveChangesAsync();

                return Ok(slusalac);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("IzbrisiSlusaoca/{slusalacID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiSlusaoca(int slusalacID)
        {
            if(slusalacID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var slusalac = await Context.Slusaoci
                    .Where(x=>x.ID==slusalacID).FirstOrDefaultAsync();
                if(slusalac==null)
                {
                    return BadRequest("Ne postoji slusalac");
                }
                var zahtev=await this.Context.ZahteviSlusalac
                    .Where(x=>x.Slusalac==slusalac).ToListAsync();
                foreach(ZahtevSlusalac z in zahtev){
                    Context.ZahteviSlusalac.Remove(z);
                }
                 var feedbacks=await this.Context.Feedbacks
                    .Where(x=>x.SlusalacKomentarise==slusalac).ToListAsync();
                foreach(Feedback f in feedbacks){
                    var reportfeedbacks=await this.Context.ReportFeedbacks
                    .Where(x=>x.Feedback==f).ToListAsync();
                    foreach(ReportFeedback rf in reportfeedbacks)
                    {
                         Context.ReportFeedbacks.Remove(rf);
                    }
                    Context.Feedbacks.Remove(f);
                }
                Context.Slusaoci.Remove(slusalac);
                await Context.SaveChangesAsync();
                return Ok(slusalac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // [Route("SlusaociPoStranicama/{stranica}/{predavanjeID}/{poStranici}")]
        // [HttpGet]
        // public async Task<ActionResult> SlusaociPoStranicama(int stranica,int predavanjeID,int poStranici)
        // {
            
        //     try
        //     {   
                
        //         Predavanje predavanje= await Context.Predavanja.Where(p=>p.ID==predavanjeID).FirstOrDefaultAsync();
                
        //         var zahtevi = Context.ZahteviSlusalac.Where(q=>q.Predavanje==predavanje&&q.Status.Equals("Odobren"));
        //         int ukupno=zahtevi.Count();
        //         var brStranica =(ukupno+(poStranici-1))/poStranici;
        //         var slusaoci=  Context.ZahteviSlusalac.Include(q=>q.Slusalac).Where(p=>p.Predavanje==predavanje&&p.Status.Equals("Odobren"));
        //         //var slusaoci=  Context.Slusaoci.Include(q=>q.ZahteviSlusalac.Where(p=>p.Predavanje==predavanje&&p.Status.Equals("Odobren")));
        //         //var s=await slusaoci.Where(q=>q.ZahteviSlusalac.)
        //         var s=await slusaoci.Select(q=>new{
        //             Ime=q.Slusalac.Ime,
        //             Prezime=q.Slusalac.Prezime,
        //         }).ToListAsync();
                
        //         List<Object> trazeni=new List<Object>();
        //         trazeni.Add(brStranica);
        //         for(var start=stranica*poStranici;start<(stranica+1)*poStranici&&start<ukupno;start++){
        //             trazeni.Add(s.ElementAt(start));
        //         }

        //         return Ok(trazeni);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        //U PREDAVANJA ===========================================================
        // [Route("PredavanjaPoStranicama/{stranica}/{poStranici}")]
        // [HttpGet]
        // public async Task<ActionResult> PredavanjaPoStranicama(int stranica,int poStranici)
        // {
            
        //     try
        //     {   
        //         var zahtevi = Context.Predavanja;
        //         int ukupno=zahtevi.Count();
        //         var brStranica =(ukupno+(poStranici-1))/poStranici;

        //         var s=await zahtevi.Select(q=>new{
        //             Predavanje=q.Naziv,
        //             Datum=q.Datum,
        //         }).ToListAsync();
                
        //         List<Object> trazeni=new List<Object>();
        //         trazeni.Add(brStranica);
        //         for(var start=stranica*poStranici;start<(stranica+1)*poStranici&&start<ukupno;start++){
        //             trazeni.Add(s.ElementAt(start));
        //         }

        //         return Ok(trazeni);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        //br slobodnih mesta za predavanje
        
        [Authorize(Roles = "Slusalac,Organizator")]
        [Route("PredavanjaSlusaoca/{slusalacID}")]
        [HttpGet]
        public async Task<ActionResult> PredavanjaSlusaoca(int slusalacID)
        {
            
            try
            {   
                Slusalac slusalac= Context.Slusaoci.Where(q=>q.ID==slusalacID).FirstOrDefault();
                var zahtevi=Context.ZahteviSlusalac.Include(q=>q.Predavanje).Where(p=>p.Status.Equals("Odobren")&&p.Slusalac==slusalac);
                var aa=await zahtevi.Select(s=>new{
                    id=s.ID,
                    Naziv=s.Predavanje.Naziv,
                    Opis=s.Predavanje.Opis,
                    Sala=s.Predavanje.sala.Ime,
                    Datum=s.Datum.ToLongDateString(),
                    PocetakPredavanja=s.Datum.ToString("HH:mm"),
                    KrajPredavanja=s.Datum.AddHours(1).ToString("HH:mm"),

                }).ToListAsync();
                return Ok(aa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("OdslusanaPredavanja/{slusalacID}")]
        [HttpGet]
        public async Task<ActionResult> OdslusanaPredavanja(int slusalacID)
        {
            
            try
            {   
                Slusalac slusalac= Context.Slusaoci.Where(q=>q.ID==slusalacID).FirstOrDefault();
                var zahtevi=Context.ZahteviSlusalac.Include(q=>q.Predavanje).Where(p=>p.Predavanje.Datum<DateTime.Now && p.Status.Equals("Odobren")&&p.Slusalac==slusalac);
                var aa=await zahtevi.Select(s=>new{
                    Id=s.Predavanje.ID,
                    Naziv=s.Predavanje.Naziv,
                    Opis=s.Predavanje.Opis,
                    Predavac=s.Predavanje.Predavac.Ime +" "+s.Predavanje.Predavac.Prezime,
                    Sala=s.Predavanje.sala.Ime,
                    Datum=s.Predavanje.Datum.ToLongDateString(),
                    PocetakPredavanja=s.Datum.ToString("HH:mm"),
                    KrajPredavanja=s.Datum.AddHours(1).ToString("HH:mm"),

                }).ToListAsync();
                return Ok(aa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PredstojecaPredavanja/{slusalacID}")]
        [HttpGet]
        public async Task<ActionResult> PredstojecaPredavanja(int slusalacID)
        {
            
            try
            {   
                Slusalac slusalac= Context.Slusaoci.Where(q=>q.ID==slusalacID).FirstOrDefault();
                var zahtevi=Context.ZahteviSlusalac.Include(q=>q.Predavanje).ThenInclude(s=>s.Predavac).Where(p=>p.Predavanje.Datum>DateTime.Now && p.Status.Equals("Odobren")&&p.Slusalac==slusalac);
                var aa=await zahtevi.Select(s=>new{
                    Id=s.Predavanje.ID,
                    Naziv=s.Predavanje.Naziv,
                    Opis=s.Predavanje.Opis,
                    Predavac=s.Predavanje.Predavac.Ime+" "+s.Predavanje.Predavac.Prezime,
                    Sala=s.Predavanje.sala.Ime,
                    Datum=s.Predavanje.Datum.ToLongDateString(),
                    PocetakPredavanja=s.Datum.ToString("HH:mm"),
                    KrajPredavanja=s.Datum.AddHours(1).ToString("HH:mm"),

                }).ToListAsync();
                return Ok(aa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("VratiJednogSlusaoca")]
        [HttpGet]
        public async Task<ActionResult> VratiJednogSlusaoca([FromQuery] int idnumber)
        {   
            try
            {
                var slusaoci = Context.Slusaoci;
                var slusalac = await slusaoci.Where(p=>p.ID==idnumber)
                .Select(p=>
                new
                {   
                    ID=p.ID,
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    Telefon="0"+p.Telefon,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                })
                .ToListAsync();

                return Ok(slusalac);
            }
            catch(Exception e)
            {
                 return BadRequest(e.Message);
            }
        }
        
        [Authorize(Roles = "Slusalac")]
        [Route("NeprijavljenaPredavanja/{slusalacID}/{pagenumber}")]
        [HttpGet]
        public async Task<ActionResult> NeprijavljenaPredavanja(int slusalacID,int pagenumber)
        {
            
            try
            {   
                
                
                var elPerPage=10;
                Slusalac slusalac= Context.Slusaoci.Where(q=>q.ID==slusalacID).FirstOrDefault();
                var zahtevi=Context.ZahteviSlusalac.Include(q=>q.Predavanje).Where(p=>p.Slusalac==slusalac);
                var aa=await zahtevi.Select(s=>new{
                    ID=s.Predavanje.ID,
                }).ToListAsync();
                var sva= Context.Predavanja.Where(q=>q.Predavac!=null).Include(p=>p.Oblast)
                    .Include(p=>p.sala).Include(p=>p.Predavac).Include(q=>q.sala).Include(a=>a.ZahteviSlusalac).ToList();
                foreach(var a in aa){
                    Predavanje p=await Context.Predavanja.Where(q=>q.ID==a.ID).FirstOrDefaultAsync();
                    sva.Remove(p);
                }
                var brstrana=sva.Count();
                var povratna=sva.Select(p=>
                new
                {   
                    brstrana=brstrana,
                    brzahteva=p.ZahteviSlusalac.Where(q=>q.Status=="Odobren").Count(),
                    kapacitet=p.sala.Kapacitet,
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name,
                })
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToList();
                return Ok(povratna);
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }
        [Route("BrStranaSlusaoca")]
        [HttpGet]
        public  ActionResult BrStranaSlusaoca()
        {
            
            try
            {   
               
                return Ok(Context.Slusaoci.Count());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous] 
        [Route("VratiSveSlusaocePerPage")]
        [HttpGet]
        public async Task<ActionResult> VratiSveSlusaocePerPage([FromQuery] int pagenumber)
        {
            //TrenutniDatum=DateTime.Now;     
            const int elPerPage=10;
            try
            {
                var slusaoci=Context.Slusaoci;
                var slusalac=await slusaoci
                .Select(p=>
                new
                {   
                    ID=p.ID,
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                    Telefon="+381"+p.Telefon,
                })
                .OrderBy(p=>p.Ime)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                return Ok(slusalac);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PredavanjaProfilSlusaoca/{slusalacID}/{strana}")]
        [HttpGet]
        public async Task<ActionResult> PredavanjaProfilSlusaoca(int slusalacID,int strana)
        {
            const int poStrani=3;
            try
            {   
                Slusalac slusalac= Context.Slusaoci.Where(q=>q.ID==slusalacID).FirstOrDefault();
                var zahtevi=Context.ZahteviSlusalac.Include(q=>q.Predavanje).ThenInclude(s=>s.Predavac).Where(p=>p.Predavanje.Datum>DateTime.Now && p.Status.Equals("Odobren")&&p.Slusalac==slusalac);
                var aa=await zahtevi.Select(s=>new{
                    Id=s.Predavanje.ID,
                    Naziv=s.Predavanje.Naziv,
                    Opis=s.Predavanje.Opis,
                    Predavac=s.Predavanje.Predavac.Ime+" "+s.Predavanje.Predavac.Prezime,
                    Sala=s.Predavanje.sala.Ime,
                    Datum=s.Predavanje.Datum.ToLongDateString(),
                    PocetakPredavanja=s.Datum.ToString("HH:mm"),
                    KrajPredavanja=s.Datum.AddHours(1).ToString("HH:mm"),
                    ceoDatum=s.Datum

                }).OrderByDescending(p=>p.ceoDatum)
                .Skip((strana-1)*poStrani)
                .Take(poStrani).ToListAsync();
                
                return Ok(aa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [Route("PredavanjaSlusaoca/{slusalacID}")]
        // [HttpGet]
        // public async Task<ActionResult> PredavanjaSlusaoca(int slusalacID)
        // {
            
        //     try
        //     {   
        //         Slusalac slusalac=await Context.Slusaoci.Where(q=>q.ID==slusalacID).FirstOrDefaultAsync();
        //         var zahtevi=Context.ZahteviSlusalac.Include(q=>q.Predavanje).Where(p=>p.Status.Equals("Odobren")&&p.Slusalac==slusalac);
        //         var aa=await zahtevi.Select(s=>new{
        //             Naziv=s.Predavanje.Naziv,
        //             Opis=s.Predavanje.Opis,
        //             Sala=s.Predavanje.sala.Ime,
        //             Datum=s.Datum.ToLongDateString(),
        //             PocetakPredavanja=s.Datum.ToString("HH:mm"),
        //             KrajPredavanja=s.Datum.AddHours(1).ToString("HH:mm"),

        //         }).ToListAsync();
        //         return Ok(aa);
        //     }
            // catch (Exception e)
            // {
            //     return BadRequest(e.Message);
            // }
        // }
    }
}