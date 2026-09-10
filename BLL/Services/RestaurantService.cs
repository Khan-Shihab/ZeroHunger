using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class RestaurantService
    {
        IMapper mapper;
        RestaurantRepo repo;
        public RestaurantService(IMapper mapper, RestaurantRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        public List<RestaurantModel> GetAll()
        {
            var data = repo.GetAll();
            var mapped = mapper.Map<List<RestaurantModel>>(data);
            return mapped;
        }
        public RestaurantModel GetbyId(int id)
        {
            var data = repo.GetById(id);
            var mapped = mapper.Map<RestaurantModel>(data);
            return mapped;
        }

        public bool Update(RestaurantUpdateModel d, int id)
        {
            var mapped = mapper.Map<Restaurant>(d);
            var isUpdate = repo.Update(mapped, id);
            return isUpdate;

        }
        public object ResturentWithCollectionReq()
        {
            var data = repo.ResturentWithCollectionReq();
            return data;
        }


    }
}
