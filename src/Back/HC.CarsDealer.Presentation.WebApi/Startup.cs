using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.CarsDealer.Application.Abstractions;
using HC.CarsDealer.Application.Business;
using HC.CarsDealer.Domain.Entities;
using HC.CarsDealer.Domain.Interfaces.PersistenceSupport;
using HC.CarsDealer.Persistence.LocalDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HC.CarsDealer.Presentation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SeedDb();
        }
        private void SeedDb()
        {
            string uri = Configuration.GetSection("LocalDb").GetSection("Uri").Value;
            var sb = new StringBuilder()
                .Append("[")
                .Append("{\"Model\":\"Sonic LT Sedan\", \"Description\":\"KBB Rating: 4\", \"Year\":2015,\"Brand\":\"Chevrolet\", \"Kilometers\":91446,\"Price\":7262,\"Id\":1},")
                .Append("{\"Model\":\"300 Limited AWD\", \"Description\":\"\", \"Year\":2019,\"Brand\":\"Chrysler\", \"Kilometers\":37165,\"Price\":21880,\"Id\":2},")
                .Append("{\"Model\":\"Civic LX Sedan\", \"Description\":\"KBB Rating: 4\", \"Year\":2016,\"Brand\":\"Honda\", \"Kilometers\":21249,\"Price\":16747,\"Id\":3},")
                .Append("{\"Model\":\"X5 sDrive35i\", \"Description\":\"KBB Rating: 4.2\", \"Year\":2014,\"Brand\":\"BMW\", \"Kilometers\":44253,\"Price\":22411,\"Id\":4},")
                .Append("{\"Model\":\"M240i Coupe\", \"Description\":\"Certified - KBB Rating: 4.5\", \"Year\":2017,\"Brand\":\"BMW\", \"Kilometers\":27538,\"Price\":33574,\"Id\":5}")
                .Append("]");
            File.WriteAllText(uri, sb.ToString());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string uri = Configuration.GetSection("LocalDb").GetSection("Uri").Value;
            services.AddCors(options => {
                options.AddPolicy("DevPolicy", builder => {
                    builder.WithOrigins("http://localhost:4200");
                    builder.WithMethods("GET, POST, PUT, DELETE");
                });
            });
            services.AddControllers();
            services.AddSingleton(typeof(IJsonSourceManager<Product>), s => new JsonSourceManager<Product>(uri));
            services.AddSingleton(typeof(IDbContext<>), typeof(LocalDbContext<>));
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
