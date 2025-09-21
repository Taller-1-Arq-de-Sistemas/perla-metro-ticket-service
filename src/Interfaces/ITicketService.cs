using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_tickets_service.src.DTOs;

namespace perla_metro_tickets_service.src.Interfaces
{
    public interface ITicketService
    {
        Task<ViewTicketDto> CreateAsync(CreateTicketDto createTicketDto, string CreatedBy);
        Task<IEnumerable<ViewTicketDto>> GetAllAsync();
        Task<ViewTicketDto?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateTicketDto updateTicketDto, string UpdateBy);
        Task<bool> SoftDeleteAsync(Guid id, string deletedBy);
    }
}