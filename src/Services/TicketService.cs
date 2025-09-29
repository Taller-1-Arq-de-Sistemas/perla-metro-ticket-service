using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_tickets_service.src.DTOs;
using perla_metro_tickets_service.src.Interfaces;
using perla_metro_tickets_service.src.models;
using perla_metro_tickets_service.src.Repositories;

namespace perla_metro_tickets_service.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapperService _mapperService;

        public TicketService(ITicketRepository ticketRepository, IMapperService mapperService)
        {
            _ticketRepository = ticketRepository;
            _mapperService = mapperService;
        }
        public async Task<ViewTicketDto> CreateAsync(CreateTicketDto createTicketDto, string CreatedBy)
        {
            var mappedTicket = _mapperService.CreateDtoToTicket(createTicketDto, CreatedBy);

            var exists = await _ticketRepository.ExistisDuplicateAsync(mappedTicket.PassagerId, mappedTicket.IssuedDate);

            if (exists)
                throw new InvalidOperationException("Ya existe un ticket para este pasajero con esa fecha.");
            await _ticketRepository.CreatedAsync(mappedTicket);

            //Mapeo del ticket para visualizarlo
            var view = _mapperService.TicketToResponse(mappedTicket, "Desconocido" );
            return view;
        }

        public async Task<IEnumerable<ViewTicketDto>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();

            //creacion de lista auxiliar para desplegar
            var results = new List<ViewTicketDto>();

            //Se recorre en los tickets para hacer el mapeo del ticket a visualizar el ticket con el nombre del pasajero
            foreach (var ticket in tickets)
            {
                var viewDto = _mapperService.TicketToResponse(ticket, "Desconocido");
                results.Add(viewDto);
            }

            return results;
        }

        public async Task<ViewTicketDto?> GetByIdAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null) return null;

            //Se retorna el ticket mapeado con el nombre del pasajero
            return _mapperService.TicketToResponse(ticket, "Desconocido");
        }

        public async Task<bool> SoftDeleteAsync(Guid id, string deletedBy)
        {
            return await _ticketRepository.SoftDeleteAsync(id, deletedBy); 
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTicketDto updateTicketDto, string UpdateBy)
        {
            //Se almacena el ticket obtenido desde la id
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null) return false;

            //Se invalida la opción de reactivar el ticket si se ingresara la opción
            if (ticket.State == TicketState.Expired && updateTicketDto.State == TicketState.Active)
                throw new InvalidOperationException("No se puede volver a activar un ticket caducado.");

            //Se mapea la información del ticket editado al ticket almacenado para guardar la información
            _mapperService.UpdateTicketFromDto(ticket, updateTicketDto, UpdateBy);

            //Se guarda la información nueva
            return await _ticketRepository.UpdateAsync(ticket);
        }
    }
}