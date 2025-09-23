using AutoMapper;
using perla_metro_tickets_service.src.DTOs;
using perla_metro_tickets_service.src.Interfaces;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.Services
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Ticket CreateDtoToTicket(CreateTicketDto createTicketDto, string createdBy)
        {
            var ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.CreatedBy = createdBy;
            ticket.IssuedDate = new DateTime(ticket.IssuedAt.Year, ticket.IssuedAt.Month, ticket.IssuedAt.Day, ticket.IssuedAt.Hour, ticket.IssuedAt.Minute, 0);
            return ticket;
        }

        public ViewTicketDto TicketToResponse(Ticket ticket, string passagerName)
        {
            var response = _mapper.Map<ViewTicketDto>(ticket);
            response.PassengerName = passagerName;
            return response;
        }

        public void UpdateTicketFromDto(Ticket ticket, UpdateTicketDto updateTicketDto, string updateBy)
        {
            _mapper.Map(updateTicketDto, ticket);
            ticket.UpdateBy = updateBy;
            ticket.UpdateAt = DateTime.UtcNow;
        }
    }
}