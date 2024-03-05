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
    public class ReportFeedbackController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public ReportFeedbackController(KonferencijaContext context)
        {
            Context = context;
        }
        [Authorize(Roles = "Predavac")]
        [Route("ReportFeedback/{feedbackId}/{text}")]
        [HttpPost]
        public async Task<ActionResult> ReportFeedback(int feedbackId,string text)
        {
           try
            {
                var f= await Context.Feedbacks.Where(q=>q.ID==feedbackId).Include(q=>q.KometarisaniPredavac).FirstOrDefaultAsync();
                ReportFeedback rf=new ReportFeedback();
                rf.Datum=DateTime.Now;
                rf.Feedback=f;
                rf.Status="Neobradjen";
                rf.Opis=text;
                rf.Komentar=f.KometarisaniPredavac;
                f.Tip=1;
                Context.ReportFeedbacks.Add(rf);
                await Context.SaveChangesAsync();
                

                return Ok(rf);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Authorize(Roles = "Organizator")]
        [Route("ObrisiFeedback/{reportid}/{br}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiFeedback(int reportid,int br)
        {
           try
            {

               
                var rf=await Context.ReportFeedbacks.Where(q=>q.ID==reportid).Include(q=>q.Feedback).FirstOrDefaultAsync();
                var f= await Context.Feedbacks.Where(q=>q==rf.Feedback).FirstOrDefaultAsync();
                
                
               
                Context.ReportFeedbacks.Remove(rf);
                await Context.SaveChangesAsync();
                f.Tip=0;
                if(br==1){
                Context.Feedbacks.Remove(f);
                await Context.SaveChangesAsync();
                }
                

                return Ok(rf);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Organizator")]
        [Route("vratiReports")]
        [HttpGet]
        public async Task<ActionResult> vratiReports()
        {
           try
            {
                
                
                var r=Context.ReportFeedbacks.Include(w=>w.Feedback).ThenInclude(r=>r.SlusalacKomentarise).Include(w=>w.Feedback).ThenInclude(r=>r.KometarisaniPredavac);
                // var aa="";
                // var s= await r.Skip(brstr).Take(brstr+1).ToListAsync();
                // if(!s.Any()){
                //     aa="end";
                // }
                var report=await r.Select(q=>new{
                    reportid=q.ID,
                    datumC=q.Datum,
                    datum=q.Datum.ToLongDateString(),
                    opis=q.Opis,
                    predavac=q.Feedback.KometarisaniPredavac.Ime+" "+q.Feedback.KometarisaniPredavac.Prezime,
                    slusalac=q.Feedback.SlusalacKomentarise.Ime+" "+q.Feedback.SlusalacKomentarise.Prezime,
                    feedback=q.Feedback.ID,
                    //kraj=aa,
            }).OrderBy(p=>p.datumC).ToListAsync();
                
                

                return Ok(report);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}