using System;
using System.IO;
using NLog;
using BookStoreApp.Extensions;
using BookStoreApp.Models;
using BookStoreApp.Models.DataManager;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Call CORS configuration
            services.ConfigureCors();
            // Call IIS configuration
            services.ConfigureIISIntegration();
            // Call custom external logger service
            services.ConfigureLoggerService();
            services.AddDbContext<BookStoreContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:BooksDB"]));
            services.AddScoped<IDataRepository<Author, AuthorDTO>, AuthorDataManager>();
            services.AddScoped<IDataRepository<Book, BookDTO>, BookDataManager>();
            services.AddScoped<IDataRepository<Publisher, PublisherDTO>, PublisherDataManager>();

            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });
                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
