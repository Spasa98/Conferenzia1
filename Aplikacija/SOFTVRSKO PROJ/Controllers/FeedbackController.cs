using System;
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
    public class FeedbackController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public FeedbackController(KonferencijaContext context)
        {
            Context = context;
        }
        [Authorize(Roles ="Slusalac")]
        [Route("NovFeedback/{slusalacID}/{predavacID}")]
        [HttpPost]
        public async Task<ActionResult> NovFeedback(int slusalacID,int predavacID,[FromBody] Feedback feedback)
        {
           try
            {  
                
                var predavac = Context.Predavaci.Where(p => p.ID == predavacID).FirstOrDefault();
                var slusalac = Context.Slusaoci.Where(p => p.ID == slusalacID).FirstOrDefault();
                
                    feedback.SlusalacKomentarise=slusalac;
                    feedback.Tip=0;
                    feedback.Datum = DateTime.Now;
                    feedback.KometarisaniPredavac = predavac;
                

               Context.Feedbacks.Add(feedback);
                await Context.SaveChangesAsync();

                return Ok(feedback);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /*
        [Route("NovFeedback")]
        [HttpPost]
        public async Task<ActionResult> NovFeedback([FromBody] Feedback feedback)
        {
           try
            {  
  
                if(feedback.Ocena<1||feedback.Ocena>5){return BadRequest("Ocena mora biti izmedju 1 i 5");}
                var predavac = Context.Predavaci.Where(p => p.ID == feedback.predavacID).FirstOrDefault();
                var slusalac = Context.Slusaoci.Where(p => p.ID == feedback.slusalacID).FirstOrDefault();
                feedback.KometarisaniPredavac=predavac;
                feedback.SlusalacKomentarise=slusalac;
                Context.Feedbacks.Add(feedback);
                await Context.SaveChangesAsync();

                return Ok("Uspesno dodat feedback");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("NovFeedback")]
        [HttpGet]
        public async Task<ActionResult> VratiPredavaceZaKojeJeMogucFeedback([FromBody] Feedback feedback)
        {
           try
            {  
  
                if(feedback.Ocena<1||feedback.Ocena>5){return BadRequest("Ocena mora biti izmedju 1 i 5");}
                var predavac = Context.Predavaci.Where(p => p.ID == feedback.predavacID).FirstOrDefault();
                var slusalac = Context.Slusaoci.Where(p => p.ID == feedback.slusalacID).FirstOrDefault();
                feedback.KometarisaniPredavac=predavac;
                feedback.SlusalacKomentarise=slusalac;
                Context.Feedbacks.Add(feedback);
                await Context.SaveChangesAsync();

                return Ok("Uspesno dodat feedback");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        */
        
        // [Route("ObrisiFeedback/{feedbackID}")]
        // [HttpDelete]
        // public async Task<ActionResult> ObrisiFeedback(int feedbackID)
        // {
        //     if(feedbackID<=0)
        //     {
        //         return BadRequest("Pogresan id");
        //     }
        //     try
        //     {
        //         var feedback = await Context.Feedbacks.FindAsync(feedbackID);
        //         Context.Feedbacks.Remove(feedback);
        //         await Context.SaveChangesAsync();
        //         return Ok($"Uspešno izbrisan feedback ");
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        [Route("PromeniFeedback/{feedbackID}/{ocena}/{opis}")]
        [HttpPut]
        public async Task<ActionResult> PromeniZahtev(int feedbackID,int ocena,string opis)
        {
            if(feedbackID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                Feedback feedback=await Context.Feedbacks.Where(x=>x.ID==feedbackID).FirstOrDefaultAsync();
                
                feedback.Datum = DateTime.Now;
                feedback.Ocena = ocena;
                feedback.Opis=opis;
                await Context.SaveChangesAsync();

                return Ok($"Uspešno promenjen zahtev");
                
                          
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [Route("ProsecnaOcenaPredavaca/{predavacID}")]
        [HttpGet]
        public async Task<ActionResult> ProsecnaOcenaPredavaca(int predavacID)
        {
            
            try
            {
                var i=0;
                var s=0;
                Predavac predavac=await Context.Predavaci.Where(x=>x.ID==predavacID).FirstOrDefaultAsync();
                var feedbacks=await Context.Feedbacks.Select(p=>new{p.Ocena,p.KometarisaniPredavac}).Where(x=>x.KometarisaniPredavac==predavac).ToListAsync();
                foreach(var f in feedbacks){
                    i++;
                    s+=f.Ocena;
                }
                double prosek=s/(double)i;
                

                return Ok(prosek);
                
                          
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [AllowAnonymous]
        [Route("VratiFeedbacksPredavaca/{predavacID}/{pagenumber}")]
        [HttpGet]
        public async Task<ActionResult> VratiFeedbacksPredavaca(int predavacID, int pagenumber)
        {
            const int elPerPage=2;
            try
            {   
                var aa="";
                var feedbacks = Context.Feedbacks
                    .Include(p=>p.KometarisaniPredavac)
                    .Include(p=>p.SlusalacKomentarise);
                    var s= await Context.Feedbacks.Where(x=>x.KometarisaniPredavac.ID==predavacID).Skip((elPerPage)*pagenumber).Take((elPerPage+1)*pagenumber).ToListAsync();
                if(!s.Any()){
                    aa="end";
                }
                var feed = await feedbacks
                .Where(x=>x.KometarisaniPredavac.ID==predavacID)
                .Select(p=>
                new
                {   
                    id=p.ID,
                    opis=p.Opis,
                    ocena=p.Ocena,
                    tip=p.Tip,
                    ceoDatum=p.Datum,
                    Datum=p.Datum.ToLongDateString(),
                    Vreme=p.Datum.ToShortTimeString(),
                    ime=p.SlusalacKomentarise.Ime,
                    prezime=p.SlusalacKomentarise.Prezime,
                    slusalacid=p.SlusalacKomentarise.ID,
                    kraj=aa,
                    reported=p.Tip,
                })
                .OrderByDescending(p=>p.ceoDatum)
                .Take(elPerPage*pagenumber)
                .ToListAsync();

                return Ok(feed); 
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}