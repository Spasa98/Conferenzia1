using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SOFTVRSKO_PROJ
{
    public class Startup
    {   
        private string _contentRootPath="";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this._contentRootPath=env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            string conn = Configuration.GetConnectionString("KonferencijaCS");
            if(conn.Contains("%CONTENTROOTPATH%"))
            {
                    conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }
            services.AddDbContext<KonferencijaContext>(options=>
            {
                options.UseSqlServer(conn);
            
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SOFTVRSKO_PROJ", Version = "v1" });
            });

            services.AddCors(options=>{options.AddPolicy("Cors", builder=>{
                builder.WithOrigins(new string[]{
                "http://localhost:8080",
                "https://localohost:8080",
                "http://127.0.0.1:8080",
                "https://127.0.0.1:8080",
                "https://localhost:5001",
                "https://127.0.0.1:5001"
                }).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SOFTVRSKO_PROJ v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Cors");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
