using JWTLogin.Infrastructure.DependencyInjection;
using JWTLogin.Infrastructure.Mapping;
using JWTLogin.Repository.DB;
using JWTLogin.Seed.Db;
using JWTLogin.Service.Infrastructure.Mapping;
using JWTLoginCommon.Config;
using JWTLoginCommon.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTLogin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string SQLConnection { get; set; }

        private string MySQLConnection { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            //Config
            services.Configure<ConstantsConfig>(Configuration.GetSection(ConstantsConfig.Constant));
            services.Configure<AesCryptoConfig>(Configuration.GetSection(AesCryptoConfig.AesCrypto));
          
            //JWT
            services.AddAuthentication("OAuth")
               .AddJwtBearer("OAuth", config =>
               {
                   var secretBytes = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Constant:Secret"));
                   var key = new SymmetricSecurityKey(secretBytes);

                   config.Events = new JwtBearerEvents()
                   {
                       OnMessageReceived = context =>
                       {
                           if (context.Request.Query.ContainsKey("access_token"))
                           {
                               context.Token = context.Request.Query["access_token"];
                           }

                           return Task.CompletedTask;
                       }
                   };

                   config.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ClockSkew = TimeSpan.Zero,
                       ValidIssuer = Configuration.GetValue<string>("Constant:Issuer"),
                       ValidAudience = Configuration.GetValue<string>("Constant:Audiance"),
                       IssuerSigningKey = key,
                   };
               });


           // AutoMapper
            services.AddAutoMapper
           (
               typeof(ControllerProfile).Assembly,
               typeof(ServiceProfile).Assembly

           );


            // database connection - SQL
            this.SQLConnection = this.Configuration.GetConnectionString("SQL");

            // database connection - Mysql
            this.MySQLConnection = this.Configuration.GetConnectionString("MySQL");

        
            //DB
            services.AddScoped<IDatabaseHelper>(x => new DatabaseHelper(SQLConnection, MySQLConnection));

            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(this.SQLConnection);
            });

            //Dendency Injection
            services.AddDendencyInjection();


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
