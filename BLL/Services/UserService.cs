using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserService
    {
        IMapper mapper;
        UserRepo repo;
        public UserService(IMapper mapper, UserRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        public List<UserModel> GetAll()
        {
            var data = repo.GetAll();
            var mapped = mapper.Map <List<UserModel>> (data);
            return mapped;
        }
        public UserModel GetbyId(int id)
        {
            var data = repo.GetById (id);
            var mapped = mapper.Map<UserModel>(data);
            return mapped;
        }
        public bool Update(UserUpdateModel d,int id)
        {
            var mapped = mapper.Map<User>(d);
            var isUpdate = repo.Update(mapped, id);
            return isUpdate;
        }
        public bool Delete(int id)
        {
            var user = repo.GetById(id);
            if (user == null) return false;

            user.IsActive = false;
            return repo.Update(user, id);
        }
    }
}
