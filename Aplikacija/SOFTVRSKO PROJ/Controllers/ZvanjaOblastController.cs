using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ZvanjeOblastSala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZvanjeOblastController : ControllerBase
    {
        public KonferencijaContext Context { get; set; }

        public ZvanjeOblastController(KonferencijaContext context)
        {
            Context = context;
        }
        
        [Authorize(Roles = "Organizator")]
        [Route("DodajZvanje")]
        [HttpPost]
        public async Task<ActionResult> DodatiZvanje(string zvanje)
        {
            var z = new Zvanje();
            try
            {
                z.Name = zvanje;
                Context.Zvanje.Add(z);
                await Context.SaveChangesAsync();
                return Ok(z);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("DodajOblast")]
        [HttpPost]
        public async Task<ActionResult> dodajOblast(string name)
        {
           try
            {  
                
                Oblast o=new Oblast{
                    Name=name,
                };
                Context.Oblast.Add(o);
                await Context.SaveChangesAsync();

                return Ok(o);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiJednogOrganizatora")]
        [HttpGet]
        public async Task<ActionResult> VratiJednogOrganizatora([FromQuery] int idnumber)
        {   
            try
            {
                var organizatori = Context.Organizator;
                var organizator = await organizatori.Where(p=>p.ID==idnumber)
                .Select(p=>
                new
                {   
                    ID=p.ID,
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    Email=p.Email,
                    Lozinka=p.Lozinka,
                })
                .ToListAsync();

                return Ok(organizator);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiSale")]
        [HttpGet]
        public async Task<ActionResult> VratiSale([FromQuery] int idnumber)
        {   
            try
            {
                var  sale = Context.Organizator;
                var organizator = await Context.Sala.Where(p=>p.ID==idnumber)
                .Select(p=>
                new
                {   
                    sala=p.Ime,
                    kapacitet=p.Kapacitet
                })
                .ToListAsync();

                return Ok(organizator);   
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("IzmenitiOrganizatora/{organizatorID}")]
        [HttpPut]
        public async Task<ActionResult> IzmenitiOrganizatora(int organizatorID, [FromBody] Organizator organizator)
        {
           try
            {  
                Organizator o = await Context.Organizator.Where(x=>x.ID==organizatorID).FirstOrDefaultAsync();
                
                if(string.IsNullOrWhiteSpace(organizator.Ime)){
                    organizator.Ime=o.Ime;
                }else if(string.IsNullOrWhiteSpace(organizator.Prezime)){
                    organizator.Prezime=o.Prezime;
                }else if(string.IsNullOrWhiteSpace(organizator.Email)){
                    organizator.Email=o.Email;
                }else if(string.IsNullOrWhiteSpace(organizator.Lozinka)){
                    organizator.Lozinka=o.Lozinka;
                }
                    o.Ime = organizator.Ime;
                    o.Prezime = organizator.Prezime;
                    o.Email = organizator.Email;
                    o.Lozinka=organizator.Lozinka;

                await Context.SaveChangesAsync();

                return Ok(organizator);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Organizator")]
        [Route("DodajOrganizatora")]
        [HttpPost]
        public async Task<ActionResult> DodajOrganizator([FromBody] Organizator organizator)
        {
           try
            { 
                Context.Organizator.Add(organizator);
                await Context.SaveChangesAsync();
                return Ok(organizator);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }  
}