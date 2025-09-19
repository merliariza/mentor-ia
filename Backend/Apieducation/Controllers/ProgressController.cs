using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiPortfolio.Helpers.Errors;

namespace ApiPortfolio.Controllers
{

         public class ProgressController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProgressController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProgressDto>>> Get()
        {
            var progresses = await _unitOfWork.Progress.GetAllAsync();
            return Ok(_mapper.Map<List<ProgressDto>>(progresses));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProgressDto>> Get(int id)
        {
            var progress = await _unitOfWork.Progress.GetByIdAsync(id);
            if (progress == null)
            {
                return NotFound(new ApiResponse(404, "La experiencia no existe."));
            }

            return Ok(_mapper.Map<ProgressDto>(progress));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Progress>> Post(ProgressDto progressDto)
        {
            if (progressDto == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            var progress = _mapper.Map<Progress>(progressDto);
            _unitOfWork.Progress.Add(progress);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Post), new { id = progressDto.Id }, progressDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] ProgressDto progressDto)
        {
            if (progressDto == null)
                return BadRequest(new ApiResponse(400, "Datos inválidos."));

            var existingProgress = await _unitOfWork.Progress.GetByIdAsync(id);
            if (existingProgress == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            var progress = _mapper.Map<Progress>(progressDto);
            _unitOfWork.Progress.Update(progress);
            await _unitOfWork.SaveAsync();

            return Ok(progressDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var progress = await _unitOfWork.Progress.GetByIdAsync(id);
            if (progress == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            _unitOfWork.Progress.Remove(progress);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
    }