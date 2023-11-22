using ClassLibrary1;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SchoolApp.API.Middlewares;
using SchoolApp.Data.UserManagement;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.DataAccess.Implementations;
using SchoolApp.UserManagement;
using SchoolApp.UserManagement.Commands;
using SchoolApp.UserManagement.Models;
using SchoolApp.UserManagement.Queries;
using System.Reflection;

namespace SchoolApp
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
            services.AddLogging();
            services.AddDbContext<SchoolAppContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            services.AddTransient(typeof(IDataAccess<Teacher>), typeof(DataAccess<Teacher>));
            services.AddTransient(typeof(IDataAccess<Student>), typeof(DataAccess<Student>));
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            //
           // services.AddScoped(typeof(IRequestHandler<GetTeachersQuery, GetTeacherResponse>), typeof(GetTeachersHandler));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolApp", Version = "v1" });
            });            
            services.AddMediatR(typeof(Class1).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolApp v1"));
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
