using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_tickets_service.src.DTOs;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.Interfaces
{
    public interface IMapperService
    {
        ViewTicketDto TicketToResponse(Ticket ticket, string passagerName);
        Ticket CreateDtoToTicket(CreateTicketDto createTicketDto, string createdBy);
        void UpdateTicketFromDto(Ticket ticket, UpdateTicketDto updateTicketDto, string updateBy);
    }
}