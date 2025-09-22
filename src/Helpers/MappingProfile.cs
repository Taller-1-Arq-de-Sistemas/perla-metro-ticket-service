using AutoMapper;
using perla_metro_tickets_service.src.DTOs;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.Helpers
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Ticket, ViewTicketDto>();
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<UpdateTicketDto, Ticket>();
        }
    }
}