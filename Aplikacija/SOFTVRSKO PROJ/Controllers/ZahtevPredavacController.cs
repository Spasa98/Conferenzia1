using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ZahtevPredavaca.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ZahtevPredavacController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public ZahtevPredavacController(KonferencijaContext context)
        {
            Context = context;
        }

        [Authorize(Roles ="Predavac")]
        [Route("DodajZahtevPredavaca/{PredavacID}/{PredavanjeID}/{tipzahteva}")]
        [HttpPost]
        public async Task<ActionResult> DodajZahtev(int PredavacID,int PredavanjeID,string tipzahteva)
        {
            try
            {   var zahtevPredavac=new ZahtevPredavac();
                var predavac=await this.Context.Predavaci.Where(x=>x.ID==PredavacID).FirstOrDefaultAsync();
                if(predavac==null)
                {
                    return BadRequest("Pogresan id predavaca!!");
                }
                var predavanje=await this.Context.Predavanja.Where(x=>x.ID==PredavanjeID).FirstOrDefaultAsync();
                if(predavanje==null)
                {
                    return BadRequest("Pogresan id predavanja!!");
                }
                zahtevPredavac.PrijavaOdjava=tipzahteva;
                zahtevPredavac.Status="neobradjen";
                zahtevPredavac.Datum=DateTime.Now;
                zahtevPredavac.Predavac=predavac;
                zahtevPredavac.Predavanje=predavanje;
                Context.ZahteviPredavac.Add(zahtevPredavac);
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
    }
    [Authorize(Roles ="Organizator")]
    [Route("OdbijZahtevPredavaca")]
    [HttpPut]
    public async Task<ActionResult> OdbijZahtev([FromQuery]int ZahtevID)
    { 
        try
            { 
            ZahtevPredavac zahtev=await this.Context.ZahteviPredavac.Where(x=>x.ID==ZahtevID).FirstOrDefaultAsync();
            if(zahtev==null)
            {
                return BadRequest("Ne postoji ovajzahtev!!");
            }
            zahtev.PrijavaOdjava="odbijen";
            zahtev.Status="obradjen";
            Context.ZahteviPredavac.Update(zahtev);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles ="Organizator")]
    [Route("OdobriZahtevPredavaca")]
    [HttpPut]
    public async Task<ActionResult> OdobriZahtev([FromQuery] int ZahtevID)
    { 
        try
            { 
            var zahtevi=Context.ZahteviPredavac
                .Include(x=>x.Predavac)
                .Include(x=>x.Predavanje);
            var zahtev=await zahtevi
                .Include(x=>x.Predavac)
                .Include(x=>x.Predavanje)
                .Where(x=>x.ID==ZahtevID)
                .FirstOrDefaultAsync();
            if(zahtev==null)
                return BadRequest("Ne postoji zahtev!");
            var predavanje=await Context.Predavanja.Where(x=>x.ID==zahtev.Predavanje.ID).FirstOrDefaultAsync();
            if(predavanje==null)
                return NotFound("Ne postoji predavanje!");
            if(zahtev.PrijavaOdjava=="prijava")
            {
                predavanje.Predavac=zahtev.Predavac;
                foreach (ZahtevPredavac z in zahtevi)
                {
                    if(z.Predavanje==predavanje && z!=zahtev && z.PrijavaOdjava=="prijava")
                    {
                        z.Status="obradjen";
                        z.PrijavaOdjava="odbijen";
                        Context.ZahteviPredavac.Update(z);
                        // Context.SaveChanges();
                    }
                }
            }
            if(zahtev.PrijavaOdjava=="odjava")
            {
                predavanje.Predavac=null;
            }
            zahtev.PrijavaOdjava="odobren";
            zahtev.Status="obradjen";
            Context.Predavanja.Update(predavanje);
            Context.ZahteviPredavac.Update(zahtev);
            await Context.SaveChangesAsync();
            return Ok(zahtev);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
     }
        [Authorize(Roles = "Organizator")]
        [Route("PrikaziSveZahtevePredavaca")]
        [HttpGet]
        public async Task<ActionResult> PrikaziSveZahtevePredavaca()
        {
        try
            {   
            var zahtevi=Context.ZahteviPredavac
                .Include(p=>p.Predavac)
                .Include(p=>p.Predavanje);
            var zahtev=await zahtevi
                .Where(x=>x.Predavac!=null && x.Predavanje!=null && x.Status=="neobradjen")
                .Select(p=>
            new
            {   
                idzahtev=p.ID,
                text=p.PrijavaOdjava,
                Status=p.Status,
                ceoDatum=p.Datum,
                Datum=p.Datum.ToLongDateString(),
                Vreme=p.Datum.ToShortTimeString(),
                Predavanje=p.Predavanje.ID,
                imePredavanje=p.Predavanje.Naziv,
                idPredavaca=p.Predavac.ID,
                imePredavaca=p.Predavac.Ime +" "+p.Predavac.Prezime,
            }).ToListAsync();
            return Ok(zahtev);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //jednog
        [Authorize(Roles = "Organizator,Predavac")]
        [Route("PrikaziZahtevePredavaca/{predavacid}/{pagenumber}")]
        [HttpGet]
         public async Task<ActionResult> PrikaziZahtevePredavaca(int predavacid, int pagenumber)
        {
           const int elPerPage=3;
            try
            {   
                var zahtevi=Context.ZahteviPredavac
                    .Include(p=>p.Predavac)
                    .Include(p=>p.Predavanje);

                 int ukupno=0;
                foreach (ZahtevPredavac z in zahtevi.Where(x=>x.Predavac.ID==predavacid))
                {
                    ukupno=ukupno+1;
                }
                var zahtev=await zahtevi
                .Where(x=>x.Predavac.ID==predavacid && x.Predavanje!=null)
                .Select(p=>
                new
                {  
                    ukupno=ukupno,
                    idzahtev=p.ID,
                    text=p.PrijavaOdjava,
                    Status=p.Status,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    Predavanje=p.Predavanje.ID,
                    imePredavanje=p.Predavanje.Naziv,
                    idPredavaca=p.Predavac.ID,
                    imePredavaca=p.Predavac.Ime +" "+p.Predavac.Prezime,
    
                })
                .OrderByDescending(p=>p.ceoDatum)
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
        [Authorize(Roles = "Organizator,Predavac")]
        [Route("IzbrisatiZahtev/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var zahtev = await Context.ZahteviPredavac.FindAsync(id);
                //string ime=predavac.Ime+" "+predavac.Prezime;
                Context.ZahteviPredavac.Remove(zahtev);
                await Context.SaveChangesAsync();
                return Ok(zahtev);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator,Predavac")]  
        [Route("VratiSveZahtevePredavacaPerPage")]
        [HttpGet]
        public async Task<ActionResult> VratiSveZahtevePredavacaPerPage([FromQuery] int pagenumber)
        {
            const int elPerPage=10;
            try
            {   
                var zahtevi=Context.ZahteviPredavac
                    .Include(p=>p.Predavac)
                    .Include(p=>p.Predavanje);
                int ukupno=0;
                foreach (ZahtevPredavac z in zahtevi)
                {
                    ukupno=ukupno+1;
                }
                var zahtev=await zahtevi
                .Where(x=>x.Predavac!=null && x.Predavanje!=null && x.Status=="neobradjen")
                .Select(p=>
                new
                {   
                    ukupno=ukupno,
                    idzahtev=p.ID,
                    text=p.PrijavaOdjava,
                    Status=p.Status,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    Predavanje=p.Predavanje.ID,
                    imePredavanje=p.Predavanje.Naziv,
                    idPredavaca=p.Predavac.ID,
                    imePredavaca=p.Predavac.Ime +" "+p.Predavac.Prezime,
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

       
   }     //dodajemo predavaca ako ne postoji na predavanju
        //ili ga updajtujemo zavino od updateoradd parametra
}