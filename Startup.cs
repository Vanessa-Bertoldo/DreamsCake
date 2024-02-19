﻿using lanchonete.Context;
using lanchonete.Repositories;
using lanchonete.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lanchonete
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona o DbContext (banco de dados) ao contêiner de injeção de dependência.
            // Isso permite que o DbContext seja injetado em outras classes, como controladores ou serviços.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Registro dos serviços dos repositorios
            services.AddTransient<ILanchesRepository, LancheRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();

            // Adiciona os serviços MVC ao contêiner de injeção de dependência.
            // Isso inclui serviços para controladores e visualizações.
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
