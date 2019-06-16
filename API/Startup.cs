using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Email;
using Application.Commands.CategoryCommands;
using Application.Commands.ManufacturerCommands;
using Application.Commands.OrderCommands;
using Application.Commands.ProductCommands;
using Application.Commands.RoleCommands;
using Application.Commands.UserCommands;
using Application.Interfaces;
using DataAccess;
using EfCommands.EfCategoryCommands;
using EfCommands.EfManufacturerCommands;
using EfCommands.EfOrderCommands;
using EfCommands.EfProductCommands;
using EfCommands.EfRoleCommands;
using EfCommands.EfUserCommands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BigEmptyAPI", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<BigEmptyContext>();

            var section = Configuration.GetSection("Email");

            var sender = new SmtpEmailSender(Int32.Parse(section["port"]), section["host"], section["from"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);

            #region Category
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();
            #endregion

            #region Manufacturer
            services.AddTransient<ICreateManufacturerCommand, EfCreateManufacturerCommand>();
            services.AddTransient<IDeleteManufacturerCommand, EfDeleteManufacturerCommand>();
            services.AddTransient<IEditManufacturerCommand, EfEditManufacturerCommand>();
            #endregion

            #region Product
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<IDeleteProductCommand, EfDeleteProductDommand>();
            services.AddTransient<ISearchProductsCommand, EfSearchProductsCommand>();
            services.AddTransient<IEditProductCommand, EfEditProductCommand>();
            services.AddTransient<IGetProductCommand, EfGetProductCommand>();

            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            #endregion

            #region User
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<ISearchUsersCommand, EfSearchUsersCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            #endregion

            #region Role
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            #endregion

            #region Order
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<ISearchOrderCommand, EfSearchOrdersCommand>();

            services.AddTransient<ICreateOrderDetailsCommand, EfCreateOrderDetailsCommand>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
