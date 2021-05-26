using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky.DTO;
using Parky.Entity;

namespace Parky.ParkyMapper
{
    public class ParkyMapings : Profile
    {
        public ParkyMapings()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
            CreateMap<Trails, TrailDTO>().ReverseMap();
            CreateMap<Trails, TrailCreatetDTO>().ReverseMap();
            CreateMap<Trails, TrailUpdateDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}