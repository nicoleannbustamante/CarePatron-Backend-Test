using Application.Models.Responses;
using AutoMapper;
using Domain.Aggregates.Clients;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientResponse>();
        }
    }
}
