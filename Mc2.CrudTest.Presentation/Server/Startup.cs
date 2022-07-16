using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Application.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.Application.Customer.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Customer.Commands.UpdateCustomer;
using Mc2.CrudTest.Application.Customer.Queries.GetAllCustomer;
using Mc2.CrudTest.Application.Customer.Queries.GetCustomerById;
using Mc2.CrudTest.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mc2.CrudTest.Presentation.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                    ServiceLifetime.Transient);

            //services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(GetAllCustomersQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetCustomerByIdQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateCustomerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteCustomerCommand).GetTypeInfo().Assembly);
            //services.AddMediatR(this.GetType().Assembly);


            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mc2 API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                        },
                         new string[] {}

                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mc2.Api v1"));

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
