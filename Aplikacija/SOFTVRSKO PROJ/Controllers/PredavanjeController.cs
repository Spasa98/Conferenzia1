using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;


namespace Proba.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PredavanjeController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public PredavanjeController(KonferencijaContext context)
        {
   
            Context = context;
        }

        
        #region Predavanja

        // [Route("DodajPredavanje")]
        // [HttpPost]
        // public async Task<ActionResult> DodajPredavanje([FromBody] Predavanje predavanje)
        // {
        //     try
        //     {   
               
        //         Sala sala=await this.Context.Sala.Where(x=>x.ID==predavanje.SalaID).FirstOrDefaultAsync();
        //         if(sala == null)
		// 			return NotFound("Sala ne postoji");
        //         Oblast oblast=await this.Context.Oblast.Where(x=>x.ID==predavanje.OblastID).FirstOrDefaultAsync();
        //         if(oblast == null)
		// 			return NotFound("Oblat ne postoji");
           
        //         predavanje.Oblast=oblast;
        //         predavanje.sala=sala;              
        //         Context.Predavanja.Add(predavanje);
        //         await Context.SaveChangesAsync();
        //         return Ok($"Dodato je predavanje o : {predavanje.Naziv}");
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        //  [Route("DodajPredavanje/{naziv}/{opis}/{PredavacID}/{SalaID}/{OblastID}/{datum}")]
        // [HttpPost]
        // public async Task<ActionResult> DodajPredavanje(string naziv,string opis,int PredavacID,int SalaID,int OblastID,DateTime datum)
        // {
        //     try
        //     {   
        //         // this.datum=DateTime.now;
        //         Sala sala=await this.Context.Sala.Where(x=>x.ID==SalaID).FirstOrDefaultAsync();
        //         Oblast oblast=await this.Context.Oblast.Where(x=>x.ID==OblastID).FirstOrDefaultAsync();
        //         Predavanje predavanje=new Predavanje();
        //         predavanje.Naziv=naziv;
        //         predavanje.Opis=opis;
        //         Predavac predavac=await this.Context.Predavaci.Where(x=>x.ID==PredavacID).FirstOrDefaultAsync();
        //         predavanje.Predavac=predavac;
        //         predavanje.Oblast=oblast;
        //         predavanje.sala=sala;
        //         predavanje.Datum=datum;               
        //         Context.Predavanja.Add(predavanje);
        //         await Context.SaveChangesAsync();
        //         return Ok($"Dodato je predavanje o : {predavanje.Naziv}");
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        [Authorize(Roles = "Organizator")]
        [Route("DodajPredavanje/{naziv}/{opis}/{SalaID}/{OblastID}/{pom}/{PredavacID}/{datum}")]
        [HttpPost]
        public async Task<ActionResult> DodajPredavanjeBezPredavaca(string naziv,string opis,int SalaID,int OblastID,bool pom,int PredavacID,DateTime datum)
        {
            try
            {   
                // this.datum=DateTime.now;
                Sala sala=await this.Context.Sala.Where(x=>x.ID==SalaID).FirstOrDefaultAsync();
                Oblast oblast=await this.Context.Oblast.Where(x=>x.ID==OblastID).FirstOrDefaultAsync();
                Predavanje predavanje=new Predavanje();
                predavanje.Naziv=naziv;
                predavanje.Opis=opis;
                if(pom==true)
                {
                Predavac predavac=await this.Context.Predavaci.Where(x=>x.ID==PredavacID).FirstOrDefaultAsync();
                predavanje.Predavac=predavac;
                }
				predavanje.Datum=datum;
                predavanje.Oblast=oblast;
                predavanje.sala=sala;              
                Context.Predavanja.Add(predavanje);
                await Context.SaveChangesAsync();
                return Ok(predavanje);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Predavac")]
        [Route("PredavanjaZaKojaJeMogucZahtev")]
        [HttpGet]
        public async Task<IActionResult> PredavanjaBezPredavaca([FromQuery] int pagenumber,int pid)
        {
            DateTime trenutnidatum=DateTime.Now;
            int elPerPage=3;
            try
            {
                var predavac=await Context.Predavaci
                .Include(x=>x.ZahteviPredavac).ThenInclude(x=>x.Predavanje)
                .Include(x=>x.Oblast)
                .Where(q=>q.ID==pid)
                .FirstOrDefaultAsync();

                var mojiZahteviIds= predavac.ZahteviPredavac.Select(x=>x.ID).ToList();

                if(predavac == null)
                    return NotFound("Predavac ne postoji!");

                var predavanja =await Context.Predavanja
                .Include(x=>x.ZahteviPredavac)
                .Include(x=>x.sala)
                .Include(x=>x.Oblast)
                .Where(x=>x.Predavac == null && x.Oblast == predavac.Oblast && x.Datum>trenutnidatum)
                .ToListAsync();

                predavanja = predavanja
                .Where(x => !mojiZahteviIds.Intersect(x.ZahteviPredavac.Select(x=>x.ID)).Any())
                .ToList();

                var rezultat = predavanja
                .Select(p=>
                new
                {
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Sala=p.sala.Ime,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name,
                    OblstID=p.Oblast.ID
                })
                .OrderBy(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToList();
            
                 return Ok(rezultat); 

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            

        }
        
        [AllowAnonymous]
        [Route("VratiSvaPredavanja")]
        [HttpGet]
        public async Task<ActionResult> SvaPredavanja([FromQuery] int pagenumber,int pom)
        {                    
            //TrenutniDatum=DateTime.Now;
            Predavanje predavanjee=new Predavanje();
            const int elPerPage=10;

            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);

                var predavanje= predavanja
                .Select(p=>
                new
                {
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                });
                //sortiranje 1-oblast,2-predavac
                if(pom==1){
                var predavanjeSort=await predavanje.OrderBy(x=>x.Oblast).Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage).ToListAsync();
                return Ok(predavanjeSort); 
                }   
                else if(pom==2)
                {
                var predavanjeSort=await predavanje.OrderBy(x=>x.Predavac).Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage).ToListAsync();
                 return Ok(predavanjeSort); 
                }
                else if(pom==3)
                {
                var predavanjeSort=await predavanje.OrderBy(x=>x.ceoDatum).Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage).ToListAsync();
                 return Ok(predavanjeSort); 
                }
                else
                return Ok(predavanje.Skip((pagenumber-1)*elPerPage).Take(elPerPage).OrderByDescending(x=>x.Naziv));  

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("SvaPredavanjaBezPredavaca")]
        [HttpGet]
        public async Task<ActionResult> SvaPredavanjaBezPredavaca()
        {
            //TrenutniDatum=DateTime.Now;     
            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Predavac==null)
                .Select(p=>
                new
                {   ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                }).ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("SearchPretragaPredavanja")]
        [HttpGet]
        public async Task<ActionResult> SearchPretragaPredavanja(string searchString)
        {
            //TrenutniDatum=DateTime.Now;     

            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                if(!String.IsNullOrEmpty(searchString))
                {
                var predavanje=await predavanja
                .Where(p=>p.Naziv.Contains(searchString) || p.Predavac.Ime.Contains(searchString) || p.Predavac.Prezime.Contains(searchString))
                .Select(p=>
                new
                {   ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                }).ToListAsync();
                return Ok(predavanje); 
                }
                
                return Ok(predavanja);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("JednoPredavanje/{predavanjeID}")]
        [HttpGet]
        public async Task<ActionResult> JednoPredavanje(int predavanjeID)
        {
            //TrenutniDatum=DateTime.Now;     
            var broj=0;
            try
            {
                var zahtevi=Context.ZahteviSlusalac.ToList();
                foreach(ZahtevSlusalac z in zahtevi)
                {
                    if(z.Status=="Odobren")
                    {
                        broj++;
                    }
                } 
                var predavanje=await this.Context.Predavanja
                    .Include(x=>x.Predavac)
                    .Include(x=>x.Oblast)
                    .Include(x=>x.sala)
                    .Include(x=>x.ZahteviSlusalac)
                    .Where(x=>x.ID==predavanjeID && x.Predavac!=null)
                .Select(p=>
                new
                {  
                    brslusaoca=broj,
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    PredavacID=p.Predavac.ID,
                    Sala=p.sala.Ime,
                    SalaID=p.sala.ID,
                    Kapacitet=p.sala.Kapacitet,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name,
                    OblastID=p.Oblast.ID
                }).FirstOrDefaultAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("JednoPredavanjeBezPredavaca/{predavanjeID}")]
        [HttpGet]
        public async Task<ActionResult> JednoPredavanjeBezPredavaca(int predavanjeID)
        {
            //TrenutniDatum=DateTime.Now;     
            var broj=0;
            try
            {
                var zahtevi=Context.ZahteviSlusalac.ToList();
                foreach(ZahtevSlusalac z in zahtevi)
                {
                    if(z.Status=="Odobren")
                    {
                        broj++;
                    }
                } 
                var predavanje=await this.Context.Predavanja
                    .Include(x=>x.Oblast)
                    .Include(x=>x.sala)
                    .Where(x=>x.ID==predavanjeID)
                .Select(p=>
                new
                { 
                    brslusaoca=broj,
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Sala=p.sala.Ime,
                    Kapacitet=p.sala.Kapacitet,
                    SalaID=p.sala.ID,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name,
                    OblastID=p.Oblast.ID
                }).FirstOrDefaultAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [AllowAnonymous]
        [Route("PrikaziPredavanjaPoOblasti")]
        [HttpGet]
         public async Task<ActionResult> PrrikaziPoOblasti([FromQuery] int OblastID)
        {
            try
            {   
                var predavanja=Context.Oblast
                    .Include(p=>p.OblastPredavanje)
                    .ThenInclude(p=>p.sala)
                    .Include(p=>p.OblastPredavanje)
                    .ThenInclude(p=>p.Predavac); 
                var predavanje=await predavanja
                .Where(p=>p.ID==OblastID)
                .Select(p=>
                new
                {
                    Oblast=p.Name,
                    Predavanja=p.OblastPredavanje.Select(q=>
                        new
                        {
                             ID=p.ID,
                            Naziv=q.Naziv,    
                            Opis=q.Opis,
                            Predavac=q.Predavac.Ime +" "+ q.Predavac.Prezime,
                            Sala=q.sala.Ime,
                            Datum=q.Datum.ToLongDateString(),
                            PocetakPredavanja=q.Datum.ToString("HH:mm"),
                            KrajPredavanja=q.Datum.AddHours(1).ToString("HH:mm"),
                        }
                    )
                }).ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [Route("PrikaziPredavanjaPoPredavacu/{PredavacID}")]
        [HttpGet]
         public async Task<ActionResult> PrrikaziPoPredavacu(int PredavacID,int pagenumber,int elPerPage)
        {
            //int p =new int();
            try
            {   
                Predavac predavac=Context.Predavaci.Where(q=>q.ID==PredavacID).FirstOrDefault();
                var predavanje=await Context.Predavanja
                .Where(p=>p.Predavac==predavac)
                .Select(q=>
                new
                {
                   
                            ID=q.ID,
                            Naziv=q.Naziv,    
                            Opis=q.Opis,
                            Sala=q.sala.Ime,
                            ceoDatum=q.Datum,
                            Datum=q.Datum.ToLongDateString(),
                            PocetakPredavanja=q.Datum.ToString("HH:mm"),
                            KrajPredavanja=q.Datum.AddHours(1).ToString("HH:mm"),
                        
                    
                })  
                .OrderByDescending(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [Authorize(Roles = "Predavac")]
        [Route("PrikaziPredstojecihPredavanjaPoPredavacu/{PredavacID}")]
        [HttpGet]
         public async Task<ActionResult> PrrikazPredstojecihPredavanjaZaPredavaca(int PredavacID,int pagenumber,int elPerPage)
        {
           DateTime TrenutniDatum=DateTime.Now;
           Console.WriteLine(TrenutniDatum.ToString());
            try
            {   
                Predavac predavac=Context.Predavaci.Where(q=>q.ID==PredavacID).FirstOrDefault();
                var predavanje=await Context.Predavanja
                .Where(p=>p.Predavac==predavac&&p.Datum>TrenutniDatum)
                .Select(q=>
                new
                {
                            ID=q.ID,
                            Naziv=q.Naziv,    
                            Opis=q.Opis,
                            Sala=q.sala.Ime,
                            Datum=q.Datum.ToLongDateString(),
                            ceoDatum=q.Datum,
                            PocetakPredavanja=q.Datum.ToString("HH:mm"),
                            KrajPredavanja=q.Datum.AddHours(1).ToString("HH:mm"),
                        
                    
                })
                .OrderByDescending(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [Authorize(Roles = "Predavac")]
        [Route("PrikaziProslihPredavanjaPoPredavacu/{PredavacID}")]
        [HttpGet]
         public async Task<ActionResult> PrrikazProslihPredavanjaZaPredavaca(int PredavacID,int pagenumber,int elPerPage)
        {
           DateTime TrenutniDatum=DateTime.Now;
           Console.WriteLine(TrenutniDatum.ToString());
            try
            {   
                Predavac predavac=Context.Predavaci.Where(q=>q.ID==PredavacID).FirstOrDefault();
                var predavanje=await Context.Predavanja
                .Where(p=>p.Predavac==predavac&&p.Datum<TrenutniDatum)
                .Select(q=>
                new
                {
                   
                            ID=q.ID,
                            Naziv=q.Naziv,    
                            Opis=q.Opis,
                            Sala=q.sala.Ime,
                            Datum=q.Datum.ToLongDateString(),
                            ceoDatum=q.Datum,
                            PocetakPredavanja=q.Datum.ToString("HH:mm"),
                            KrajPredavanja=q.Datum.AddHours(1).ToString("HH:mm"),
                        
                     
                })             
                .OrderByDescending(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();
                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /*[Route("PrikaziPredavanjaPredavacu")]
        [HttpGet]
        public async Task<ActionResult> PrikaziPoPredavacu([FromQuery] int PredavacID)
        {
            try
            {   
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Predavac.ID==PredavacID)
                .Select(p=>
                new
                {
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                }).ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/
        [AllowAnonymous]
        [Route("PrikazPredstojecihPredavanja")]
        [HttpGet]
        public async Task<ActionResult> PrikazPredsojecihPredavanja([FromQuery] int pagenumber)
        {
             const int elPerPage=10;
            DateTime TrenutniDatum=DateTime.Now;            
            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Datum>TrenutniDatum)
                .Select(p=>
                new
                {
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                })
                .OrderBy(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("PrikaziProslaPredavanja")]
        [HttpGet]
        public async Task<ActionResult> PrikazProslihPredavanja([FromQuery] int pagenumber)
        {   
            const int elPerPage=10;
            DateTime TrenutniDatum=DateTime.Now;            
            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Datum<TrenutniDatum)
                .Select(p=>
                new
                {
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                })
                .OrderByDescending(p=>p.ceoDatum)
                .Skip((pagenumber-1)*elPerPage)
                .Take(elPerPage)
                .ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PrikaziTrenutnaPredavanja")]
        [HttpGet]
        public async Task<ActionResult> PrikazTrentutnihPredavanja([FromQuery] DateTime TrenutniDatum)
        {
            //TrenutniDatum=DateTime.Now;     
            
            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Datum<=TrenutniDatum && p.Datum.AddHours(1)>=TrenutniDatum ) //mozda dodje do problema 
                .Select(p=>
                new
                {
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                }).ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PrikaziPredavanjaZaOdredjeniDatum")]
        [HttpGet]
        public async Task<ActionResult> PrikazPredavanjaZaOdredjeniDatum([FromQuery] DateTime OdredjeniDatum)
        {
            //TrenutniDatum=DateTime.Now;     

            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Datum.Date==OdredjeniDatum)
                .Select(p=>
                new
                {
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.ToLongDateString(),
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                }).ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PrikaziPredavanjaIzmedjuDatuma")]
        [HttpGet]
        public async Task<ActionResult> PrikazPredavanjaIzmedjuDatuma([FromQuery] DateTime Pocetni,DateTime Krajnji)
        {
            //TrenutniDatum=DateTime.Now;     

            try
            {
                var predavanja=Context.Predavanja
                    .Include(p=>p.Oblast)
                    .Include(p=>p.sala)
                    .Include(p=>p.Predavac);
                var predavanje=await predavanja
                .Where(p=>p.Datum.Date>=Pocetni && p.Datum.Date<=Krajnji)
                .Select(p=>
                new
                {
                    Naziv=p.Naziv,
                    Opis=p.Opis,
                    Predavac=p.Predavac.Ime + " " + p.Predavac.Prezime,
                    Sala=p.sala.Ime,
                    Datum=p.Datum.Date,
                    PocetakPredavanja=p.Datum.ToString("HH:mm"),
                    KrajPredavanja=p.Datum.AddHours(1).ToString("HH:mm"),
                    Oblast=p.Oblast.Name
                }).ToListAsync();

                return Ok(predavanje);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Organizator")]
        [Route("IzbrisatiPredavanje/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var predavanje = await Context.Predavanja
                    .Include(x=>x.ZahteviPredavac)
                    .Include(x=>x.ZahteviSlusalac)
                    .Where(x=>x.ID==id).FirstOrDefaultAsync();
                var zahtev=await this.Context.ZahteviPredavac
                    .Include(x=>x.Predavac
                    ).Where(x=>x.Predavanje==predavanje).ToListAsync();
                foreach(ZahtevPredavac z in zahtev){
                    Context.ZahteviPredavac.Remove(z);
                }
                 var zahtevslusaoca=await this.Context.ZahteviSlusalac
                    .Include(x=>x.Slusalac
                    ).Where(x=>x.Predavanje==predavanje).ToListAsync();
                foreach(ZahtevSlusalac z in zahtevslusaoca){
                    Context.ZahteviSlusalac.Remove(z);
                }
                Context.Predavanja.Remove(predavanje);
                await Context.SaveChangesAsync();
                return Ok(predavanje);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //promeni predavaca-update
        //dodaj predavaca-add
        //ukloni predavaca-delete
        [Authorize(Roles = "Organizator")]
        [Route("IzmeniPredavanje")]
        [HttpPut]
        public async Task<ActionResult> IzmeniPredavanje(int predavanjeid,int predavacid,string naziv,DateTime datum,string Opis,int OblastID,int SalaID)
        {
            try
            {  
                Predavanje predavanje=await this.Context.Predavanja.Where(x=>x.ID==predavanjeid).FirstOrDefaultAsync();
                Predavac predavac=await this.Context.Predavaci.Where(x=>x.ID==predavacid).FirstOrDefaultAsync();
                Sala sala=await this.Context.Sala.Where(x=>x.ID==SalaID).FirstOrDefaultAsync();
                Oblast oblast=await this.Context.Oblast.Where(x=>x.ID==OblastID).FirstOrDefaultAsync();
                if(naziv!=null && naziv!=predavac.Ime)
                {
                predavanje.Naziv=naziv;
                }
                if(Opis!=null)
                {
                predavanje.Opis=Opis;
                }
                
                if(OblastID!=0)
                {
                predavanje.Oblast=oblast;
                }
                if(datum!=null)
                {
                predavanje.Datum=datum;
                }
                if(predavacid!=0)
                {
                predavanje.Predavac=predavac;
                }
                if(SalaID!=0)
                {
                predavanje.sala=sala;
                }
                await Context.SaveChangesAsync();
                return Ok(predavanje);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Predavac")]
        [Route("PredavacPredavanja/{predavanjeID}")]
        [HttpGet]
        public async Task<ActionResult> PredavacPredavanja(int predavanjeID)
        {
            try
            {
                Predavanje predavanje= await Context.Predavanja.Include(s=>s.Predavac).Where(p=>p.ID==predavanjeID).FirstOrDefaultAsync();
                Predavac predavac=new Predavac{
                    Ime=predavanje.Predavac.Ime,
                    Prezime=predavanje.Predavac.Prezime,
                    Telefon= predavanje.Predavac.Telefon,
                    Zvanje=predavanje.Predavac.Zvanje,
                    Opis=predavanje.Predavac.Opis,
                    Email=predavanje.Predavac.Email,
                    Grad=predavanje.Predavac.Grad,
                };
                return Ok(predavanje.Predavac);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [Route("VratiPrviSlobodanTerminPoSali/{salaID}")]
        // [HttpGet]
        // public async Task<ActionResult> PriviSlobodanTermin(int salaID)
        // {
        //     DateTime ndt = new DateTime();
        // try
        //     {
        //         var predavanja=Context.Predavanja
        //             .Include(p=>p.sala);
        //         var predavanje=await predavanja
        //         .Where(p=>p.sala.ID==salaID)
        //         .Select(p=>
        //         new
        //         {
        //             Datum=p.Datum
        //         })
        //         .OrderByDescending(p=>p.Datum)
        //         .FirstOrDefaultAsync();

        //         var danimeseci=predavanje.Datum.ToString("MM/dd/yyyy HH:mm").Split('-');
        //         var godina=danimeseci[2].Split(' ');
        //         var satinminuti=godina[1].Split(":");
        //         if(Int32.Parse(satinminuti[0])==15)
        //         {
        //             TimeSpan ts = new TimeSpan(8, 15, 0);
        //             ndt=ndt+ts;
        //             return Ok(ndt);   
        //         }

        //         return Ok(satinminuti);   
        //     }
        //     catch(Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        #endregion
    }
}