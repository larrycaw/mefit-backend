using MeFit.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeFit
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsProduction())
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                });

                //services.AddApplicationInsightsTelemetry();

                services.AddDbContext<MeFitDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                services.AddDbContext<MeFitDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("LocalDevelopment")));

            }

            //services.AddDefaultIdentity<MeFitDbContext>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<MeFitDbContext>();

            //if (Environment.IsProduction())
            //{
            //    // Read credential from configuration
            //    var bytes = Convert.FromBase64String(Configuration["SigningKeyBase64"]);

            //    // https://github.com/dotnet/runtime/issues/24406#issuecomment-351087167
            //    var collection = new X509Certificate2Collection();
            //    collection.Import(bytes);

            //    services.AddIdentityServer()
            //        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
            //        .AddSigningCredential(collection[0]);

            //}
            //else
            //{
            //    services.AddIdentityServer()
            //        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            //}

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();


            services.AddControllers();
            //services.AddDbContext<MeFitDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeFit", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeFit v1"));
            }
            else
            {
                app.UseForwardedHeaders();
                //app.UseExceptionHandler("/Error");
                //app.UseHsts();

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
