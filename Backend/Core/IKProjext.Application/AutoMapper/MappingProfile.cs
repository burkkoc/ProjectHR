using AutoMapper;
using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IK.Domain.Enums;
using IKProject.Application.DTOs.RequestDTOs;
using IKProject.Application.Features.Commands.Register;
using IKProject.Application.Features.Commands.RegisterCompany;
using IKProject.Application.Features.Commands.Requests.Advance;
using IKProject.Application.Features.Commands.Requests.Leave;
using IKProject.Application.Features.Commands.Requests.Advance.Create;
using IKProject.Application.Features.Commands.Requests.Expense.Create;
using IKProject.Application.Features.Commands.Requests.Leave.Create;
using IKProject.Application.Features.Querries.GetAdvance;
using IKProject.Application.Features.Querries.GetAllEmployees;
using IKProject.Application.Features.Querries.GetAllManagers;
using IKProject.Application.Features.Querries.GetAllRequests;
using IKProject.Application.Features.Querries.GetCompany;
using IKProject.Application.Features.Querries.GetSingleCompany;
using IKProject.Application.Features.Querries.GetUser;
using IKProject.Application.Features.Querries.Login;
using IKProject.Application.Methods.Register;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKProject.Application.Features.Commands.RegisterUser;

namespace IKProject.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CultureInfo currentCulture = new CultureInfo("tr-TR");

            CreateMap<RegisterCommand, UserInformation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<RegisterCommand, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore());

            CreateMap<RegisterCompanyCommand, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Logo, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Added, opt => opt.Ignore());

            CreateMap<RegisterCommand, AppUser>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Mail))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserInformation, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshTokenEndDate, opt => opt.Ignore())
            .ForMember(dest => dest.VerificationCode, opt => opt.Ignore())
            .ForMember(dest => dest.VerificationCodeExpiration, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Mail));
            

            CreateMap<RegisterCommand, UserInformation>() 
                .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.TC))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.SecondLastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.PlaceOfBirth))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.Profession))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => RegisterHelpers.ProcessPhotoFile(src.PhotoFile)))
                .ForMember(dest => dest.AppUserId, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore()) 
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Added, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.Ignore());



            CreateMap<Company, GetCompaniesQueryResponse>()
                .ForMember(dest => dest.appUsers, opt => opt.Ignore());

            CreateMap<Company, Company>()
               .ForMember(dest => dest.AppUser, opt => opt.Ignore());



            CreateMap<AppUser, GetAllManagersQueryResponse>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserInformation.Address))
                .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.UserInformation.TC))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserInformation.BirthDate))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.UserInformation.Department))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.UserInformation.EndDate))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.UserInformation.PlaceOfBirth))
                .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.UserInformation.Profession))
                .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.UserInformation.SecondLastName))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.UserInformation.SecondName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.UserInformation.StartDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.UserInformation.Salary))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UserInformation.Photo))
                .ForMember(dest => dest.CompanyEmail, opt => opt.Ignore());

            //CreateMap<AppUser, GetAllEmployeesQueryResponse>()
            //   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserInformation.Address))
            //   .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.UserInformation.TC))
            //   .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserInformation.BirthDate))
            //   .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.UserInformation.Department))
            //   .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.UserInformation.EndDate))
            //   .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.UserInformation.PlaceOfBirth))
            //   .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.UserInformation.Profession))
            //   .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.UserInformation.SecondLastName))
            //   .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.UserInformation.SecondName))
            //   .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.UserInformation.StartDate))
            //   .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.UserInformation.Salary))
            //   .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UserInformation.Photo));

            CreateMap<Company, GetSingleCompanyQueryResponse>();

            CreateMap<AppUser , GetUserByTokenResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.UserInformation.TC))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.UserInformation.SecondName))
            .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.UserInformation.SecondLastName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserInformation.BirthDate))
            .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.UserInformation.PlaceOfBirth))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.UserInformation.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.UserInformation.EndDate))
            .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.UserInformation.Profession))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.UserInformation.Department))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserInformation.Address))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UserInformation.Photo))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.UserInformation.Salary));

            CreateMap<AppUser, GetAllEmployeesQueryResponse>()
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserInformation.Address))
               .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.UserInformation.TC))
               .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserInformation.BirthDate))
               .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.UserInformation.Department))
               .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.UserInformation.EndDate))
               .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.UserInformation.PlaceOfBirth))
               .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.UserInformation.Profession))
               .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.UserInformation.SecondLastName))
               .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.UserInformation.SecondName))
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.UserInformation.StartDate))
               .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.UserInformation.Salary))
               .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UserInformation.Photo));

            CreateMap<AppUser, LoginQueryResponse>()
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<CreateAdvanceRequestCommand, AdvanceRequest>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.AdvanceType, opt => opt.MapFrom(src => src.AdvanceType))
              .ForMember(dest => dest.AdvanceCurrency, opt => opt.MapFrom(src => src.AdvanceCurrency))
              .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => RequestStatus.Beklemede));

            CreateMap<CreateExpenseRequestCommand, ExpenseRequest>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.Document, opt => opt.Ignore())
              .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => RequestStatus.Beklemede))
              .ForMember(dest => dest.ExpenseType, opt => opt.MapFrom(src => Enum.Parse(typeof(ExpenseType), src.ExpenseType.ToString())));

            CreateMap<CreateLeaveRequestCommand, LeaveRequest>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => Enum.Parse(typeof(LeaveType), src.LeaveType.ToString())))
             .ForMember(dest => dest.LeaveStartDate, opt => opt.MapFrom(src => src.LeaveStartDate))
             .ForMember(dest => dest.LeaveEndDate, opt => opt.MapFrom(src => src.LeaveEndDate))
             .ForMember(dest => dest.DaysCount, opt => opt.MapFrom(src => src.DaysCount))
             .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => RequestStatus.Beklemede));


            CreateMap<ExpenseRequest, ExpenseRequestDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<AdvanceRequest, AdvanceRequestDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<LeaveRequest, LeaveRequestDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<AppUser, AppUser>()
                .ForMember(dest => dest.UserInformation, opt => opt.Ignore())
                .ForMember(dest => dest.AppUserAdvanceRequests, opt => opt.Ignore())
                .ForMember(dest => dest.AppUserExpenseRequests, opt => opt.Ignore())
                .ForMember(dest => dest.AppUserLeaveRequests, opt => opt.Ignore());

            //CreateMap<AdvanceRequest, AdvanceRequest>()
            //    .ForMember(dest => dest.AppUser, opt => opt.Ignore())
            //    .ForMember(dest => dest.AppUserAdvanceRequests, opt => opt.Ignore());

            //CreateMap<ExpenseRequest, ExpenseRequest>()
            //    .ForMember(dest => dest.AppUser, opt => opt.Ignore())
            //    .ForMember(dest => dest.AppUserExpenseRequests, opt => opt.Ignore());

            //CreateMap<LeaveRequest, LeaveRequest>()
            //    .ForMember(dest => dest.AppUser, opt => opt.Ignore())
            //    .ForMember(dest => dest.AppUserLeaveRequests, opt => opt.Ignore());
            CreateMap<RegisterUserCommand, UserInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Added, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<RegisterUserCommand, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Mail.ToLower().Trim()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Mail))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Mail.ToUpper()))
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Trim()));
        }

    }
}

