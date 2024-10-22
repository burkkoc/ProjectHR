using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace IKProject.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        public string TC { get; set; }
        public string FirstName { get; set; }
        public IFormFile PhotoFile { get; set; }
        public string LastName { get; set; }
        public string? SecondName { get; set; }
        public string? SecondLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
    }
}