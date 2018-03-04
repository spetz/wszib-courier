using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Courier.Api.Framework;
using Courier.Core.Commands;
using Courier.Core.Commands.Parcels;
using Courier.Core.Domain;
using Courier.Core.Options;
using Courier.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Courier.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<AppOptions>(Configuration.GetSection("app"));
            services.Configure<JwtOptions>(Configuration.GetSection("jwt"));

            var builder = new ContainerBuilder();
            var apiAssembly = typeof(Startup).Assembly;
            var coreAssembly = typeof(IParcelService).Assembly;

            builder.RegisterAssemblyTypes(apiAssembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(coreAssembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(coreAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<PasswordHasher<User>>()
                .As<IPasswordHasher<User>>();
                
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var dataSeeder = app.ApplicationServices.GetService<IDataSeeder>();
            dataSeeder.SeedAsync();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
