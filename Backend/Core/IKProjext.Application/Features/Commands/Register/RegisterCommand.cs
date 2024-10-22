using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IKProject.Application.Features.Commands.Register
{
    public class RegisterCommand : IRequest<Unit>
    {
        public string? TC { get; set; }
        public string? FirstName { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public string? SecondLastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mail { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PlaceOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Profession { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public int? Salary { get; set; }
    }
}
