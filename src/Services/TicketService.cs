using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_tickets_service.src.DTOs;
using perla_metro_tickets_service.src.Interfaces;
using perla_metro_tickets_service.src.Repositories;

namespace perla_metro_tickets_service.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapperService _mapperService;
        private readonly HttpClient _httpClient;

        public TicketService(ITicketRepository ticketRepository, IMapperService mapperService, HttpClient httpClient)
        {
            _ticketRepository = ticketRepository;
            _mapperService = mapperService;
            _httpClient = httpClient;
        }
        public async Task<ViewTicketDto> CreateAsync(CreateTicketDto createTicketDto, string CreatedBy)
        {
            //mapeo del ticket creado, se ingresa como creado por un fucionario por defecto para agregar usuario en futuras versiones.
            var mappedTicket = _mapperService.CreateDtoToTicket(createTicketDto, "funcionario");

            if (await _ticketRepository.ExistisDuplicateAsync(mappedTicket.PassagerId, mappedTicket.IssuedDate))
                throw new InvalidOperationException("Ya existe un ticket para este con esa fecha.");

            //Obtiene los datos del pasajero desde la API MAIN
            var passager = await getPassengerFromApiAsync(mappedTicket.PassagerId);

            if (passager == null)
                throw new KeyNotFoundException("No se encontró el pasajero en el sistema.");

            await _ticketRepository.CreatedAsync(mappedTicket);

            //Mapeo del ticket para visualizarlo
            var view = _mapperService.TicketToResponse(mappedTicket, passager.Name);
            return view;
        }

        public Task<IEnumerable<ViewTicketDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ViewTicketDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(Guid id, string deletedBy)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid id, UpdateTicketDto updateTicketDto, string UpdateBy)
        {
            throw new NotImplementedException();
        }

        //llamado a la API MAIN para obtener al pasajero
        private async Task<PassagerDto?> getPassengerFromApiAsync(Guid passengerId)
        {
            //Se debe definir el url que tendra la api main para obtener al pasajero.
            var response = await _httpClient.GetAsync($"/api/passengers/{passengerId}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<PassagerDto>();
        }
    }
}