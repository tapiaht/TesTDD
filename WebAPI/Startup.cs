using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TesTDD.Domain.Core.Interfaces.Repository;
using TesTDD.Domain.Core.Interfaces.Service;
using TesTDD.Domain.Services;
using TesTDD.Infra.Repository;
using TesTDD.WebAPI.Noticia;
using TesTDD.WebAPI.Filters;
using System;
using Entity = TesTDD.Domain.Core.Entity;

namespace TesTDD.WebAPI
{
    public class Startup
    {
        public Startup() { }


        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesTDD", Version = "v1" });
            });
            #endregion

            #region DbContext
            RegisterDbContext(services);
            #endregion

            #region Dependency Injectionn
            services.AddTransient(typeof(IServiceCrud<Guid, Entity.Noticia>), typeof(NoticiaService));
            services.AddTransient(typeof(INoticiaService), typeof(NoticiaService));
            services.AddTransient(typeof(IRepositoryCrud<,>), typeof(GenericRepositoryCrud<,>));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Filters
            services.AddTransient<ModelStateFilter>();
            #endregion

            services.AddControllers(options => options.Filters.AddService<ModelStateFilter>()).AddFluentValidation();

            services.AddTransient<IValidator<NoticiaModel>, NoticiaValidator>();
        }

        protected virtual void RegisterDbContext(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase(databaseName: "TesTDD_DB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
