using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AuthServices
    {
        UserRepo userrepo;
        RestaurantRepo resrepo;
        EmployeeRepo emprepo;
        IMapper mapper;
        public AuthServices(UserRepo userrepo, RestaurantRepo resrepo, EmployeeRepo emprepo, IMapper mapper)
        {
            this.userrepo = userrepo;
            this.resrepo = resrepo;
            this.emprepo = emprepo;
            this.mapper = mapper;
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        public bool Register(AuthModel u)
        {
            var taken = userrepo.IsEmailTaken(u.Email);
            if (taken) return false;

            if (u.Role == "RESTAURANT" && (string.IsNullOrEmpty(u.Address) || string.IsNullOrEmpty(u.ContactPerson)))
                return false;

            if (u.Role == "EMPLOYEE" && string.IsNullOrEmpty(u.Phone))
                return false;

            var user = new User
            {
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                PasswordHash = HashPassword(u.PasswordHash),
                Role = u.Role,
                IsActive = true
            };
            userrepo.Create(user);

            if (user.Role == "RESTAURANT")
            {
                var restaurant = new Restaurant
                {
                    UserId = user.Id,
                    Address = u.Address!,
                    ContactPerson = u.ContactPerson!,
                    IsVerified = false
                };
                resrepo.Create(restaurant);
            }
            else if (user.Role == "EMPLOYEE")
            {
                var employee = new Employee
                {
                    UserId = user.Id,
                    Phone = u.Phone!,
                    CoverageArea = u.CoverageArea,
                    Availability = "AVAILABLE"
                };
                emprepo.Create(employee);
            }

            return true;
        }
        public LoginResponseModel? Login(LoginModel l)
        {
            var user = userrepo.GetByEmail(l.Email);
            if (user == null) return null;
            if (!user.IsActive) return null;
            if (!VerifyPassword(l.Password, user.PasswordHash)) return null;

            return new LoginResponseModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

    }

}
