using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiPortfolio.Helpers.Errors;

namespace ApiPortfolio.Controllers
{

         public class EvaluationSessionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EvaluationSessionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EvaluationSessionDto>>> Get()
        {
            var evaluationSessions = await _unitOfWork.EvaluationSession.GetAllAsync();
            return Ok(_mapper.Map<List<EvaluationSessionDto>>(evaluationSessions));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EvaluationSessionDto>> Get(int id)
        {
            var evaluationSession = await _unitOfWork.EvaluationSession.GetByIdAsync(id);
            if (evaluationSession == null)
            {
                return NotFound(new ApiResponse(404, "La experiencia no existe."));
            }

            return Ok(_mapper.Map<EvaluationSessionDto>(evaluationSession));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EvaluationSession>> Post(EvaluationSessionDto evaluationSessionDto)
        {
            if (evaluationSessionDto == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            var evaluationSession = _mapper.Map<EvaluationSession>(evaluationSessionDto);
            _unitOfWork.EvaluationSession.Add(evaluationSession);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Post), new { id = evaluationSessionDto.Id }, evaluationSessionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] EvaluationSessionDto evaluationSessionDto)
        {
            if (evaluationSessionDto == null)
                return BadRequest(new ApiResponse(400, "Datos inválidos."));

            var existingEvaluationSession = await _unitOfWork.EvaluationSession.GetByIdAsync(id);
            if (existingEvaluationSession == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            var evaluationSession = _mapper.Map<EvaluationSession>(evaluationSessionDto);
            _unitOfWork.EvaluationSession.Update(evaluationSession);
            await _unitOfWork.SaveAsync();

            return Ok(evaluationSessionDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var evaluationSession = await _unitOfWork.EvaluationSession.GetByIdAsync(id);
            if (evaluationSession == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            _unitOfWork.EvaluationSession.Remove(evaluationSession);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
    }