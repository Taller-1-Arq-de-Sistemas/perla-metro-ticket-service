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

        public async Task<IEnumerable<ViewTicketDto>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();

            //creacion de lista auxiliar para desplegar
            var results = new List<ViewTicketDto>();

            //Se recorre en los tickets para hacer el mapeo del ticket a visualizar el ticket con el nombre del pasajero
            foreach (var ticket in tickets)
            {
                var passager = await getPassengerFromApiAsync(ticket.PassagerId);
                var viewDto = _mapperService.TicketToResponse(ticket, passager?.Name ?? "Desconocido");
                results.Add(viewDto);
            }

            return results;
        }

        public async Task<ViewTicketDto?> GetByIdAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null) return null;

            //Se obtiene el pasajero
            var passenger = await getPassengerFromApiAsync(ticket.PassagerId);

            //Se retorna el ticket mapeado con el nombre del pasajero
            return _mapperService.TicketToResponse(ticket, passenger?.Name ?? "Desconocido");
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