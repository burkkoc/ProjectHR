using AutoMapper;
using IK.Domain.Entities.Concrete;
using IKProject.Application.Features.Querries.GetSingleCompany;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
