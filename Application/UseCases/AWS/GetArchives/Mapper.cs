using System;
using AutoMapper;
using Domain.Entities;
using Domain.Records;

namespace Application.UseCases.AWS.GetArchives;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Archive, BaseResponse>()
            .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path)); 
    }
}
