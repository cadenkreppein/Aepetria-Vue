using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aepetria_vue_api.DAL;
using aepetria_vue_api.Services.Implementations;
using aepetria_vue_api.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace aepetria_vue_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<AepetriaDbContext>(options =>
                options.UseMySql("server=aepetria.cirfe6c99t3o.us-east-2.rds.amazonaws.com;database=aepetria;user=AepetriaAPI;password=WfGBxfsHIH2VS5SB"));
            services.AddScoped<IRemoteImageService, RemoteImageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(options => options.WithOrigins("http://localhost:80", "http://localhost:8080", "https://aepetria.com", "http://aepetria.com").AllowAnyMethod());
            app.UseWebSockets();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
