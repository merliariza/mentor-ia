using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiPortfolio.Helpers.Errors;

namespace ApiPortfolio.Controllers
{

         public class FlashcardController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlashcardController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FlashcardDto>>> Get()
        {
            var flashcards = await _unitOfWork.Flashcard.GetAllAsync();
            return Ok(_mapper.Map<List<FlashcardDto>>(flashcards));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FlashcardDto>> Get(int id)
        {
            var flashcard = await _unitOfWork.Flashcard.GetByIdAsync(id);
            if (flashcard == null)
            {
                return NotFound(new ApiResponse(404, "La experiencia no existe."));
            }

            return Ok(_mapper.Map<FlashcardDto>(flashcard));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Flashcard>> Post(FlashcardDto flashcardDto)
        {
            if (flashcardDto == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            var flashcard = _mapper.Map<Flashcard>(flashcardDto);
            _unitOfWork.Flashcard.Add(flashcard);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Post), new { id = flashcardDto.Id }, flashcardDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] FlashcardDto flashcardDto)
        {
            if (flashcardDto == null)
                return BadRequest(new ApiResponse(400, "Datos inválidos."));

            var existingFlashcard = await _unitOfWork.Flashcard.GetByIdAsync(id);
            if (existingFlashcard == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            var flashcard = _mapper.Map<Flashcard>(flashcardDto);
            _unitOfWork.Flashcard.Update(flashcard);
            await _unitOfWork.SaveAsync();

            return Ok(flashcardDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var flashcard = await _unitOfWork.Flashcard.GetByIdAsync(id);
            if (flashcard == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            _unitOfWork.Flashcard.Remove(flashcard);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
    }