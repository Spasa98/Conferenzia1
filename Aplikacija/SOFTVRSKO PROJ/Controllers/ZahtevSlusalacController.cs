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
    public class ZahtevSlusalacController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public ZahtevSlusalacController(KonferencijaContext context)
        {
            Context = context;
        }
        [Route("BrojStranica/{str}")]
        [HttpGet]
        public  ActionResult BrojStranica(string str)
        {
            
            try
            {   
                var s="";

                if(str.Equals("Neobradjene")){
                    s="Neobradjen";
                }else if(str.Equals("Odobrene")){
                    s="Odobren";
                }else{
                    s="Odbijen";
                }
                return Ok(Context.ZahteviSlusalac.Where(q=>q.Status.Equals(s)).Count());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Slusalac")]
        [Route("NovZahtev/{slusalacID}/{predavanjeID}")]
        [HttpPost]
        public async Task<ActionResult> NovZahtev(int slusalacID,int predavanjeID)
        {
           try
            {  
                
                var predavanje = Context.Predavanja.Where(p => p.ID == predavanjeID).FirstOrDefault();
                var slusalac = Context.Slusaoci.Where(p => p.ID == slusalacID).FirstOrDefault();
                if(predavanje==null||slusalac==null){return BadRequest("Nevalidni unos");}
                var zahtev=Context.ZahteviSlusalac.Where(p=>p.Predavanje==predavanje&&p.Slusalac==slusalac).FirstOrDefault();
                if(zahtev!=null){return BadRequest("Zahtev postoji");}
                ZahtevSlusalac z = new ZahtevSlusalac{
                
                    Text = "",
                    Datum = DateTime.Now,
                    Status = "Neobradjen",
                    Predavanje = predavanje,
                    Slusalac= slusalac,
                };

                Context.ZahteviSlusalac.Add(z);
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("OdobravanjeZahteva/{zahtevID}")]
        [HttpPut]
        public async Task<ActionResult> OdobravanjeZahteva(int zahtevID)
        {
           try
            {  
                ZahtevSlusalac zahtev=await this.Context.ZahteviSlusalac.Where(x=>x.ID==zahtevID).FirstOrDefaultAsync();
                zahtev.Status = "Odobren";
                zahtev.Datum=DateTime.Now;
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("OdbijanjeZahteva/{zahtevID}")]
        [HttpPut]
        public async Task<ActionResult> OdbijanjeZahteva(int zahtevID)
        {
           try
            {  
                ZahtevSlusalac zahtev=await this.Context.ZahteviSlusalac.Where(x=>x.ID==zahtevID).FirstOrDefaultAsync();
                zahtev.Status = "Odbijen";
                zahtev.Datum=DateTime.Now;
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Slusalac,Organizator")]
        [Route("ObrisiZahtev/{zahtevID}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiZahtev(int zahtevID)
        {
            if(zahtevID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var zahtev = await Context.ZahteviSlusalac.FindAsync(zahtevID);
                Context.ZahteviSlusalac.Remove(zahtev);
                await Context.SaveChangesAsync();
                return Ok(zahtev);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("PromeniZahtev/{zahtevID}/{predavanjeID}")]
        [HttpPut]
        public async Task<ActionResult> PromeniZahtev(int zahtevID,int predavanjeID)
        {
            if(zahtevID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                ZahtevSlusalac zahtev=await Context.ZahteviSlusalac.Where(x=>x.ID==zahtevID).FirstOrDefaultAsync();
                var predavanje = Context.Predavanja.Where(p => p.ID == predavanjeID).FirstOrDefault();
                if(zahtev.Status.Equals("Odobren")){
                    return BadRequest("Zahtev obradjen");
                }
                zahtev.Datum = DateTime.Now;
                zahtev.Predavanje = predavanje;
                await Context.SaveChangesAsync();

                return Ok($"Uspešno promenjen zahtev");
                
                          
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Slusalac,Organizator")]
        [Route("ZahteviPoStranicama/{stranica}/{predavanjeID}/{poStranici}")]
        [HttpGet]
        public async Task<ActionResult> ZahteviPoStranicama(int stranica,int predavanjeID,int poStranici)
        {
            
            try
            {   
                
                Predavanje predavanje= await Context.Predavanja.Where(p=>p.ID==predavanjeID).FirstOrDefaultAsync();
                
                var zahtevi = await Context.ZahteviSlusalac.Where(q=>q.Predavanje==predavanje&&q.Status.Equals("Neobradjen")).ToListAsync();
                int ukupno=zahtevi.Count();
                var brStranica =(ukupno+(poStranici-1))/poStranici;
                var slusaoci=  Context.ZahteviSlusalac.Include(q=>q.Slusalac).Where(p=>p.Predavanje==predavanje&&p.Status.Equals("Neobradjen"));
                //var slusaoci=  Context.Slusaoci.Include(q=>q.ZahteviSlusalac.Where(p=>p.Predavanje==predavanje&&p.Status.Equals("Odobren")));
                //var s=await slusaoci.Where(q=>q.ZahteviSlusalac.)
                var s=await slusaoci.Select(q=>new{
                    Ime=q.Slusalac.Ime,
                    Prezime=q.Slusalac.Prezime,
                }).ToListAsync();
                
                List<Object> trazeni=new List<Object>();
                for(var start=stranica*poStranici;start<(stranica+1)*poStranici&&start<ukupno;start++){
                    trazeni.Add(s.ElementAt(start));
                }

                return Ok(trazeni);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("PrikaziNeobradjeneZahteveSlusaoca/{pagenumber}")]
        [HttpGet]
         public async Task<ActionResult> PrikaziNeobradjeneZahteveSlusaoca(int pagenumber)
        {
           //DateTime TrenutniDatum=DateTime.Now;
           //Console.WriteLine(TrenutniDatum.ToString());
           const int elPerPage=10;
            try
            {   
                var zahtevi=Context.ZahteviSlusalac.Where(x=> x.Predavanje!=null&&x.Status.Equals("Neobradjen")&&x.Predavanje.Datum>DateTime.Now)
                    .Include(p=>p.Slusalac)
                    .Include(p=>p.Predavanje);
                    
                var brstrana=zahtevi.Count();
                var zahtev=await zahtevi
                .Where(x=> x.Predavanje!=null)
                .Select(p=>
                new
                {
                    brstr=brstrana,
                    idZahtevSlusalac=p.ID,
                    TipZahteve=p.Text,
                    Status=p.Status,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    IDPredavanja=p.Predavanje.ID,
                    nazivPredavanje=p.Predavanje.Naziv,
                    idSlusaoca=p.Slusalac.ID,
                    imeSlusaoca=p.Slusalac.Ime + " " + p.Slusalac.Prezime,
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

        [Authorize(Roles = "Organizator")]
        [Route("PrikaziOdobreneZahteveSlusaoca/{pagenumber}")]
        [HttpGet]
         public async Task<ActionResult> PrikaziOdobreneZahteveSlusaoca(int pagenumber)
        {
           //DateTime TrenutniDatum=DateTime.Now;
           //Console.WriteLine(TrenutniDatum.ToString());
           const int elPerPage=10;
            try
            {   
                var zahtevi=Context.ZahteviSlusalac.Where(x=> x.Predavanje!=null&&x.Status.Equals("Odobren")&&x.Predavanje.Datum>DateTime.Now)
                    .Include(p=>p.Slusalac)
                    .Include(p=>p.Predavanje);
                    
                var brstrana=zahtevi.Count();
                var zahtev=await zahtevi
                .Select(p=>
                new
                {
                    brstr=brstrana,
                    idZahtevSlusalac=p.ID,
                    TipZahteve=p.Text,
                    Status=p.Status,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    IDPredavanja=p.Predavanje.ID,
                    nazivPredavanje=p.Predavanje.Naziv,
                    idSlusaoca=p.Slusalac.ID,
                    imeSlusaoca=p.Slusalac.Ime + " " + p.Slusalac.Prezime,
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
        
        [Authorize(Roles = "Organizator")]
        [Route("PrikaziOdbijeneZahteveSlusaoca/{pagenumber}")]
        [HttpGet]
         public async Task<ActionResult> PrikaziOdbijeneZahteveSlusaoca(int pagenumber)
        {
           //DateTime TrenutniDatum=DateTime.Now;
           //Console.WriteLine(TrenutniDatum.ToString());
           const int elPerPage=10;
            try
            {   
                var zahtevi=Context.ZahteviSlusalac.Where(x=> x.Predavanje!=null&&x.Status.Equals("Odbijen")&&x.Predavanje.Datum>DateTime.Now)
                    .Include(p=>p.Slusalac)
                    .Include(p=>p.Predavanje);
                    
                var brstrana=zahtevi.Count();
                var zahtev=await zahtevi
                .Select(p=>
                new
                {
                    brstr=brstrana,
                    idZahtevSlusalac=p.ID,
                    TipZahteve=p.Text,
                    Status=p.Status,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    IDPredavanja=p.Predavanje.ID,
                    nazivPredavanje=p.Predavanje.Naziv,
                    idSlusaoca=p.Slusalac.ID,
                    imeSlusaoca=p.Slusalac.Ime + " " + p.Slusalac.Prezime,
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
        [Route("VratiSveZahteveSlusaocaPerPage")]
        [HttpGet]
        public async Task<ActionResult> VratiSveZahteveSlusaocaPerPage([FromQuery] int pagenumber)
        {
            const int elPerPage=10;
            try
            {   
                var zahtevi=Context.ZahteviSlusalac
                    .Include(p=>p.Slusalac)
                    .Include(p=>p.Predavanje);
                var zahtev=await zahtevi
                .Where(x=> x.Predavanje!=null  && x.Status=="neobradjen")
                .Select(p=>
                new
                {   
                    idZahtevSlusalac=p.ID,
                    TipZahteve=p.Text,
                    Status=p.Status,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    IDPredavanja=p.Predavanje.ID,
                    nazivPredavanje=p.Predavanje.Naziv,
                    idSlusaoca=p.Slusalac.ID,
                    imeSlusaoca=p.Slusalac.Ime + " " + p.Slusalac.Prezime,
                })
                .OrderBy(p=>p.ceoDatum)
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
    }
}