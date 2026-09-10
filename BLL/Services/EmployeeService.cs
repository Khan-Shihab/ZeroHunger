using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class EmployeeService
    {
        IMapper mapper;
        EmployeeRepo repo;
        public EmployeeService(IMapper mapper, EmployeeRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        public List<EmployeeModel> GetAll()
        {
            var data = repo.GetAll();
            var mapped = mapper.Map<List<EmployeeModel>>(data);
            return mapped;
        }
        public EmployeeModel GetbyId(int id)
        {
            var data = repo.GetById(id);
            var mapped = mapper.Map<EmployeeModel>(data);
            return mapped;
        }
        public bool Update(EmployeeUpdateModel d, int id)
        {
            var mapped = mapper.Map<Employee>(d);
            var isUpdate = repo.Update(mapped);
            return isUpdate;

        }
        public List<EmployeeModel> GetAvailableEmployees()
        {
            var data = repo.GetAvailableEmployees();
            var mapped = mapper.Map<List<EmployeeModel>>(data);
            return mapped;
        }


    }
}
