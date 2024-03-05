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
using DTOs;

namespace predavaccontroller.Controllers
{
    
    // [Authorize(Roles ="Predavac,Organizator")]
    [ApiController]
    [Route("[controller]")]
    public class PredavacController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public PredavacController(KonferencijaContext context)
        {
            Context = context;
        }

        [Authorize(Roles ="Organizator")]
        [Route("DodajPredavaca")]
        [HttpPost]
        public async Task<ActionResult> DodajPredavaca([FromBody] Predavac predavac)
        {
            string Email=(predavac.Ime+"."+predavac.Prezime+".pr@gmail.com").ToLower();
            string Pasword=(predavac.Ime+"."+predavac.Prezime).ToLower();
            if(predavac.Telefon<=9 && predavac.Telefon>=11)
            {
                return BadRequest("Niste uneli ispravan telefon");
            }
            try
            {  
                Zvanje zvanje=await this.Context.Zvanje.Where(x=>x.ID==predavac.ZvanjeID).FirstOrDefaultAsync();
				if(zvanje == null)
					return NotFound("Zvanje ne postoji");
				
                Oblast oblast=await this.Context.Oblast.Where(x=>x.ID==predavac.OblastID).FirstOrDefaultAsync();
											
				if(oblast == null)
					return NotFound("Oblast ne postoji");

                predavac.ArchiveFlag=0;
				predavac.Email=Email;
				predavac.Lozinka=Pasword;
	               
                Context.Predavaci.Add(predavac);
                await Context.SaveChangesAsync();
                return Ok(predavac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]  
        [Route("VratiSvePredavacePerPage")]
        [HttpGet]
        public async Task<ActionResult> SvePredavacePerPage([FromQuery] int pagenumber)
        {
            //TrenutniDatum=DateTime.Now;     
            const int elPerPage=5;
            try
            {
                var predavaci=Context.Predavaci
                    .Include(p=>p.Oblast)
                    .Include(p=>p.PredavacPredmet)
                    .Where(p=>p.ArchiveFlag==0);
                var predavac=await predavaci
                .Select(p=>
                new
                {   
                    ID=p.ID,
                    Ime=p.Ime+" "+p.Prezime,
                    Prezime=p.Prezime,
                    Opis=p.Opis,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                    Telefon="+381"+p.Telefon,
                    Oblast=p.Oblast.Name
                })
                .OrderBy(p=>p.Oblast)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                

                return Ok(predavac);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("VratiSvePredavace")]
        [HttpGet]
        public async Task<ActionResult> SviPredavaci()
        {
            //TrenutniDatum=DateTime.Now;     
            try
            {
                var predavaci=Context.Predavaci
                    .Include(p=>p.Oblast)
                    .Include(p=>p.PredavacPredmet)
                    .Where(p=>p.ArchiveFlag==0);
                var predavac=await predavaci
                .Select(p=>
                new
                {   
                    ID=p.ID,
                    Ime=p.Ime + " " + p.Prezime,
                    Opis=p.Opis,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                    Telefon="+381"+p.Telefon,
                    Grad=p.Grad,
                    Oblast=p.Oblast.Name
                })
                .OrderBy(p=>p.Oblast)
                .ToListAsync();

                return Ok(predavac);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]  
        [Route("VratiJegdnogPredavaca")]
        [HttpGet]
        public async Task<ActionResult> JedanPredavac([FromQuery] int idnumber)
        {
            //TrenutniDatum=DateTime.Now;     
            try
            {
                var i=0;
                var s=0;
                double prosek=0;
                Predavac predavacc=await Context.Predavaci.Where(x=>x.ID==idnumber).FirstOrDefaultAsync();
                var feedbacks=await Context.Feedbacks.Select(p=>new{p.Ocena,p.KometarisaniPredavac}).Where(x=>x.KometarisaniPredavac==predavacc).ToListAsync();
                if(feedbacks.Any())
                {
                foreach(var f in feedbacks){
                    i++;
                    s+=f.Ocena;
                }
                prosek=s/i;
                }

                var predavaci=Context.Predavaci
                    .Include(p=>p.Oblast)
                    .Include(p=>p.PredavacPredmet);
                var predavac=await predavaci.Where(p=>p.ID==idnumber)
                .Select(p=>
                new
                {   
                    ID=p.ID,
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    Opis=p.Opis,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                    Telefon=p.Telefon,
                    Oblast=p.Oblast.Name,
                    Zvanje=p.Zvanje.Name,
                    Grad=p.Grad,
                    ocena= (int)Math.Round(prosek),
                })
                .ToListAsync();

                return Ok(predavac);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("PrikaziPredavacaPoOblasti")]
        [HttpGet]
        public async Task<ActionResult> PrrikaziPoOblasti([FromQuery] int OblastID)
        {
            try
            {   
                var predavaci=Context.Oblast
                    .Include(p=>p.OblastPredavac);
                var predavac=await predavaci
                .Where(p=>p.ID==OblastID)
                .Select(p=>
                new
                {
                    Oblast=p.Name,
                    Predavac=p.OblastPredavac.Select(q=>
                        new
                        {
                            Ime=q.Ime + " " + q.Prezime,
                            Telefon="+381"+q.Telefon,
                            Zvanje=q.Zvanje.Name,
                            Opis=q.Opis,
                            Email=q.Email,
                            Grad=q.Grad,
                        }
                    )
                }).ToListAsync();

                return Ok(predavac);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [Route("IzmeniPredavaca")]
        // [HttpPut]
        // public async Task<ActionResult> IzmeniPredavaca(int predavacid,string ime,string prezime,string Opis,int Telefon,string Email,string Password,string Grad,int OblastID,int ZvanjeID)
        // {
        //     if(Password!=null)
        //     {
        //         if(Password.Length<8)
        //         {
        //         foreach(char c in Password)
        //         {
        //             if(c.Equals(".")||c.Equals("@"))
        //             {
        //                 break;
        //             }
        //             else
        //             {
        //                 return BadRequest("Niste uneli jedan od specijalnih karaktera : . ili @");
        //             }
                
        //         }
        //         return BadRequest("Premalo karaktera");
  
        //         }
        //     }
        //     if(Email!=null)
        //     {
        //     if(Email.Length<8)
        //     {
        //         foreach(char c in Email)
        //         {
        //             if(c.Equals("@"))
        //             {
        //                 break;
        //             }
        //             else
        //             {
        //                 return BadRequest("Niste uneli ispravan Email");
        //             }
                
        //         }
        //         return BadRequest("Premalo karaktera");
        //     }
        //     }
        //     if(Telefon!=0)
        //     {
        //     if(Telefon<=9 && Telefon>=11)
        //     {
        //         return BadRequest("Niste uneli ispravan telefon");
        //     }
        //     }
        //     try
        //     {   Predavac predavac=await this.Context.Predavaci.Where(x=>x.ID==predavacid).FirstOrDefaultAsync();
        //         Zvanje zvanje=await this.Context.Zvanje.Where(x=>x.ID==ZvanjeID).FirstOrDefaultAsync();
        //         Oblast oblast=await this.Context.Oblast.Where(x=>x.ID==OblastID).FirstOrDefaultAsync();
        //         if(ime!=null && ime!=predavac.Ime)
        //         {
        //         predavac.Ime=ime;
        //         }
        //         if(prezime!=null)
        //         {
        //         predavac.Prezime=prezime;
        //         }
        //         if(Opis!=null)
        //         {
        //         predavac.Opis=Opis;
        //         }
        //         if(Email!=null )
        //         {
        //         predavac.Email=Email;
        //         }
        //         if(Password!=null)
        //         {
        //         predavac.Lozinka=Password;
        //         }
        //         if(Telefon!=0)
        //         {
        //         predavac.Telefon=Telefon;
        //         }
        //         if(Grad!=null)
        //         {
        //         predavac.Grad=Grad;
        //         }
        //         if(OblastID!=0)
        //         {
        //         predavac.Oblast=oblast;
        //         }
        //         if(ZvanjeID!=0)
        //         {
        //         predavac.Zvanje=zvanje;
        //         }
        //         await Context.SaveChangesAsync();
        //         return Ok($"Predavac je izmenjen!");
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        [AllowAnonymous]
        [Route("SearchPretragaPredavaca")]
        [HttpGet]
        public async Task<ActionResult> SearchPretragaPredavaca(string searchString)
        {
            //TrenutniDatum=DateTime.Now;     

            try
            {
                var predavaci=Context.Predavaci
                    .Include(p=>p.Oblast);
                if(!String.IsNullOrEmpty(searchString))
                {
                var predavac=await predavaci
                .Where(p=>p.Ime.Contains(searchString) || p.Prezime.Contains(searchString))
                .Select(p=>
                new
                {    ID=p.ID,
                    Ime=p.Ime +" "+ p.Prezime,

                    Opis=p.Opis,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                    Telefon=p.Telefon,
                    Oblast=p.Oblast.Name,
                    Zvanje=p.Zvanje.Name,
                    Grad=p.Grad,
                }).ToListAsync();
                return Ok(predavac);
                }
                return Ok(predavaci);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles ="Organizator")]
        [Route("UkloniPredavacaSaPredavanja")]
        [HttpPut]
        public async Task<ActionResult> UklonniPredavaca(int pid)
        {
            if(pid<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                Predavac predavac=new Predavac();
                var predavanja=Context.Predavanja
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja.Where(x=>x.ID==pid).FirstOrDefaultAsync();
                if(predavanje!=null )
                {
                    if(predavanje.Predavac!=null)
                    {
                    predavanje.Predavac=null;
                    await Context.SaveChangesAsync();
                    return Ok(predavanje);
                    }
                    else
                    {
                       return NotFound("Ne postoji predavac koga bi uklonili");
                    }
                } 
                else
                {
                    return NotFound("Predavanje nije pronadjeno!");
                }             
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles ="Organizator")]
        [Route("DodajPredavacaNaPredavanje")]
        [HttpPut]
        public async Task<ActionResult> DodajPredavacaNaPredavanje(int pid,int predavacid)
        {
            if(pid<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                Predavac predavac=await Context.Predavaci.Where(x=>x.ID==predavacid).FirstOrDefaultAsync();
                var predavanja=Context.Predavanja
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja.Where(x=>x.ID==pid).FirstOrDefaultAsync();
                if(predavanje!=null )
                {
                    if(predavanje.Predavac==null)
                    {
                    predavanje.Predavac=predavac;
                    await Context.SaveChangesAsync();
                    return Ok(predavanje);
                    }
                    else
                    {
                       return NotFound("Ne postoji predavac koga bi uklonili");
                    }
                } 
                else
                {
                    return NotFound("Predavanje nije pronadjeno!");
                }             
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize(Roles ="Organizator")]
        [Route("IzbrisatiPredavaca/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
             if(id<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var predavac = await Context.Predavaci
                    .Include(x=>x.ZahteviPredavac)
                    .Include(x=>x.Reporteedbacks)
                    .Include(x=>x.Feedbacks)
                    .Include(x=>x.PredavacPredmet)
                    .Where(x=>x.ID==id).FirstOrDefaultAsync();
                var zahtev=await this.Context.ZahteviPredavac
                    .Include(x=>x.Predavac
                    ).Where(x=>x.Predavac==predavac).ToListAsync();
                foreach(ZahtevPredavac z in zahtev){
                    Context.ZahteviPredavac.Remove(z);
                }
                 var reportfeedbacks=await this.Context.ReportFeedbacks
                    .Include(x=>x.Feedback)
                    .Where(x=>x.Komentar==predavac).ToListAsync();
                foreach(ReportFeedback rf in reportfeedbacks){
                    Context.ReportFeedbacks.Remove(rf);
                }
                 var feedbacks=await this.Context.Feedbacks
                    .Where(x=>x.KometarisaniPredavac==predavac).ToListAsync();
                foreach(Feedback f in feedbacks){
                    Context.Feedbacks.Remove(f);
                }
                Context.Predavaci.Remove(predavac);
                await Context.SaveChangesAsync();
                return Ok(predavac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmenitiPredavacaNova/{predavacID}")]
        [HttpPut]
        [Authorize(Roles = "Organizator,Predavac")]
        public async Task<ActionResult> IzmenitiPredavacaNova(int predavacID, [FromBody] Predavac predavac)
        {
           try
            {  
                
                Predavac p = await Context.Predavaci.Where(x=>x.ID==predavacID).FirstOrDefaultAsync();
                if(string.IsNullOrWhiteSpace(predavac.Ime)){
                    predavac.Ime=p.Ime;
                }else if(string.IsNullOrWhiteSpace(predavac.Prezime)){
                    predavac.Prezime=p.Prezime;
                }else if(string.IsNullOrWhiteSpace(predavac.Opis)){
                    predavac.Opis=p.Opis;
                }else if(predavac.Telefon<=0){
                    predavac.Telefon=p.Telefon;
                }else if(string.IsNullOrWhiteSpace(predavac.Grad)){
                    predavac.Grad=p.Grad;
                }else if(string.IsNullOrWhiteSpace(predavac.Email)){
                    predavac.Email=p.Email;
                }else if(string.IsNullOrWhiteSpace(predavac.Lozinka)){
                    predavac.Lozinka=p.Lozinka;
                }
                    p.Ime = predavac.Ime;
                    p.Prezime = predavac.Prezime;
                    p.Opis = predavac.Opis;
                    p.Telefon = predavac.Telefon;
                    p.Grad = predavac.Grad;
                    p.Email = predavac.Email;
                    p.Lozinka=predavac.Lozinka;

                await Context.SaveChangesAsync();

                return Ok(predavac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("PrikaziOblasti")]
        [HttpGet]
        public async Task<ActionResult> PrrikaziOblasti()
        {
            try
            {   
                var oblasti=Context.Oblast;
                var oblast=await oblasti
                .Select(p=>
                new
                {
                    ID=p.ID,
                    Oblast=p.Name,
                }).ToListAsync();

                return Ok(oblast);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("PrikaziZvanja")]
        [HttpGet]
        public async Task<ActionResult> PrrikaziZvanja()
        {
            try
            {   
                var zvanja=Context.Zvanje;
                var zvanje=await zvanja
                .Select(p=>
                new
                {
                    id=p.ID,
                    name=p.Name,
                }).ToListAsync();

                return Ok(zvanja);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("PrikaziSale")]
        [HttpGet]
        public async Task<ActionResult> PrrikaziSale()
        {
            try
            {   
                var sale=Context.Sala;
                var sala=await sale
                .Select(p=>
                new
                {
                    ID=p.ID,
                    Sala=p.Ime,
                    Kapacitet=p.Kapacitet
                }).ToListAsync();

                return Ok(sala);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("daLiJeOdslusan/{predavacId}/{slusalacId}")]
        [HttpGet]
        public async Task<ActionResult> daLiJeOdslusan(int predavacId,int slusalacId)
        {
            try
            { 
                var pom=false;
                var slusalac=await Context.Slusaoci.Where(a=>a.ID==slusalacId).FirstOrDefaultAsync();
                var predavac=await Context.Predavaci.Where(a=>a.ID==predavacId).FirstOrDefaultAsync();

                var zahtevi=await Context.ZahteviSlusalac.Include(q=>q.Predavanje).Where(p=>p.Predavanje.Datum<DateTime.Now && p.Status.Equals("Odobren")&&p.Slusalac==slusalac&&p.Predavanje.Predavac==predavac).ToListAsync();
                if(zahtevi.Any()){pom=true;}

                return Ok(pom);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}