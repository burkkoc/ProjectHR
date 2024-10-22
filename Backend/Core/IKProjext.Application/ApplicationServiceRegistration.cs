using FluentValidation.AspNetCore;
using IKProject.Application.AutoMapper;
using IKProject.Application.Methods.Get;
using IKProject.Application.Methods.PasswordReset;
using IKProject.Application.Methods.Update;
using IKProject.Application.Validators;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IKProject.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
#pragma warning disable CS0618 // Type or member is obsolete
            services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<UserRegisterValidator>()
                 .RegisterValidatorsFromAssemblyContaining<UserUpdateValidator>());
#pragma warning restore CS0618 // Type or member is obsolete

            services.AddTransient<UpdateHelper>();
            services.AddTransient<PasswordHelper>();
            services.AddTransient<GetHelper>();
            services.AddHttpContextAccessor();

            AutoMapperConfig.RegisterMappings(services);
        }
    }
}




