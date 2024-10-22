using IKProject.Application.Interfaces.File;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces;
using IKProject.Persistence.Repositories.UserRepos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using IKProject.Application.Features.Custom;
using IKProject.Application.Interfaces.Repositories;
using IKProject.Persistence.Repositories.CompanyRepos;
using IKProject.Persistence.Services;
using IK.Domain.Entities.Identity;
using IKProject.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Repositories.RequestRepos;

namespace IKProject.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            //var rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), configuration["FileSettings:RootDirectory"]);
            //if (string.IsNullOrEmpty(rootDirectory))
            //    throw new ArgumentNullException(nameof(rootDirectory), "Root directory cannot be null or empty.");


            services.AddTransient<IFileHelper, FileHelper>(provider =>
                new FileHelper(Path.Combine(Directory.GetCurrentDirectory(), configuration["FileSettings:RootDirectory"])));
            services.AddTransient<IDocumentFileHelper, DocumentFileHelper>(provider =>
               new DocumentFileHelper(Path.Combine(Directory.GetCurrentDirectory(), configuration["FileSettings:RootDirectory"])));


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();
            services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
            services.AddScoped<IExpenseRequestWriteRepository, ExpenseRequestWriteRepository>();
            services.AddScoped<IExpenseRequestReadRepository, ExpenseRequestReadRepository>();
            services.AddScoped<IAdvanceRequestWriteRepository, AdvanceRequestWriteRepository>();
            services.AddScoped<IAdvanceRequestReadRepository, AdvanceRequestReadRepository>();
            services.AddScoped<ILeaveRequestWriteRepository, LeaveRequestWriteRepository>();
            services.AddScoped<ILeaveRequestReadRepository, LeaveRequestReadRepository>();

        }
    }
}
