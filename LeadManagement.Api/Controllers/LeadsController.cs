using AutoMapper;
using LeadManagement.Application.Commands;
using LeadManagement.Application.DTOs;
using LeadManagement.Domain.Enums;
using LeadManagement.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeadManagement.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar leads.
    /// </summary>
    [ApiController]
    [Route("api/leads")]
    [Produces("application/json")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LeadsController(ILeadRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista paginada de leads, com filtro por nome e status.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 5)</param>
        /// <param name="search">Texto para busca por nome</param>
        /// <param name="status">Filtro por status (Invited, Accepted, Declined)</param>
        /// <returns>Lista paginada de leads</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLeads(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? search = null,
            [FromQuery] string? status = null)
        {
            var result = await _repository.GetFilteredAsync(search, status, pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Busca um lead pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lead = await _repository.GetByIdAsync(id);
            if (lead == null) return NotFound();

            var dto = _mapper.Map<LeadDto>(lead);
            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo lead.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateLeadCommand command)
        {
            var lead = await _mediator.Send(command);
            var leadDto = _mapper.Map<LeadDto>(lead);

            return CreatedAtAction(nameof(GetById), new { id = lead.Id }, leadDto);
        }

        /// <summary>
        /// Aceita um lead (aplica desconto se necessário).
        /// </summary>
        [HttpPost("accept/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Accept(Guid id)
        {
            var result = await _mediator.Send(new AcceptLeadCommand(id));
            return Ok(result);
        }

        /// <summary>
        /// Recusa um lead.
        /// </summary>
        [HttpPost("decline/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Decline(Guid id)
        {
            var result = await _mediator.Send(new DeclineLeadCommand(id));
            return Ok(result);
        }

        [HttpGet("invited")]
        public async Task<IActionResult> GetInvited()
        {
            var leads = await _repository.GetByStatusAsync(LeadStatus.Invited);
            return Ok(leads);
        }

        [HttpGet("accepted")]
        public async Task<IActionResult> GetAccepted()
        {
            var leads = await _repository.GetByStatusAsync(LeadStatus.Accepted);
            return Ok(leads);
        }
    }
}
