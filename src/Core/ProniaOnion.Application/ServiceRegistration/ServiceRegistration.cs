using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ProniaOnion.Application.Validators;
using System.Reflection;

namespace ProniaOnion.Application.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(ProductCreateDtoValidator)));
           
            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
