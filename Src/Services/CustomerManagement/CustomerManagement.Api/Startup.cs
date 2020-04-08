using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CustomerManagement.Api.Configure;
using Infrastructure.Api.Configure;
using Infrastructure.Api.Hosting;
using System.Collections.Generic;
using CustomerManagement.Domain.CustomerManagmentDbContext;
using CustomerManagement.Domain.Models.CustomerAggregate.Events.DomainEventHandlers;
using CustomerManagement.Commands.Customers;

namespace CustomerManagement.Api
{
    public class Startup : RootStartup
    {
        private readonly Assembly[] _mediatRAssemblies = { typeof(CustomerCreateEventHandler).Assembly };
        private readonly IList<Assembly> _registerFluentValidation = new List<Assembly>() { typeof(CreateCustomer).Assembly };

        public override IEnumerable<Assembly> RegisterFluentValidation => _registerFluentValidation;
        public override Assembly[] MediatRAssemblies => _mediatRAssemblies;

        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSqlContext<CustomerManagementDbContext>(Configuration.GetConnectionString("AppDbContextConnection"));

            services.AddRepositories();
            services.AddCommandsQueries();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}
