using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostgreTest.Middleware;
using PostgreTest.Models;
using PostgreTest.Repositories.Abstract;
using PostgreTest.Repositories.Concrete;
using PostgreTest.Services.Abstract;
using PostgreTest.Services.Concrete;
using Swashbuckle.AspNetCore.Swagger;

namespace PostgreTest
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
            #region Swagger Configuration 
            var description = Configuration["ApiDescription"];
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["ApiVersion"], new Microsoft.OpenApi.Models.OpenApiInfo { Title = Configuration["ApiName"], Version = Configuration["ApiVersion"], Description = description });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllEnumsAsStrings();
            });
            #endregion
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddControllers();
            services.AddMvc();
            services.AddEntityFrameworkNpgsql().AddDbContext<MyWebApiContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/docs.json";
            }).UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["ApiVersion"] + "/docs.json", Configuration["ApiName"] + " v" + Configuration["ApiVersion"]);
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MigrateDatabase<MyWebApiContext>();

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello mw delegate.");
                var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {
                    var culture = new CultureInfo(cultureQuery);

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }

                // Call the next delegate/middleware in the pipeline
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
