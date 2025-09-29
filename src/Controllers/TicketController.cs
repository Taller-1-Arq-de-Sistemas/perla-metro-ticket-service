using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using perla_metro_tickets_service.src.DTOs;
using perla_metro_tickets_service.src.Interfaces;

namespace perla_metro_tickets_service.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto createTicketDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdBy = User.Identity?.Name ?? "system";
                var ticket = await _ticketService.CreateAsync(createTicketDto, createdBy);

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(id);
                if (ticket is null) return NotFound();

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var UpdateBy = User.Identity?.Name ?? "system";

                var success = await _ticketService.UpdateAsync(id, updateTicketDto, UpdateBy);

                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            try
            {
                var deletedBy = User.Identity?.Name ?? "system";
                var success = await _ticketService.SoftDeleteAsync(id, deletedBy);

                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}