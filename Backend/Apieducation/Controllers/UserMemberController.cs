using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiPortfolio.Helpers.Errors;
using Application.DTOs;

namespace ApiPortfolio.Controllers
{

         public class UserMemberController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserMemberController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserMemberDto>>> Get()
        {
            var userMembers = await _unitOfWork.UserMember.GetAllAsync();
            return Ok(_mapper.Map<List<UserMemberDto>>(userMembers));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserMemberDto>> Get(int id)
        {
            var userMember = await _unitOfWork.UserMember.GetByIdAsync(id);
            if (userMember == null)
            {
                return NotFound(new ApiResponse(404, "La experiencia no existe."));
            }

            return Ok(_mapper.Map<UserMemberDto>(userMember));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserMember>> Post(UserMemberDto userMemberDto)
        {
            if (userMemberDto == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            var userMember = _mapper.Map<UserMember>(userMemberDto);
            _unitOfWork.UserMember.Add(userMember);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Post), new { id = userMemberDto.Id }, userMemberDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UserMemberDto userMemberDto)
        {
            if (userMemberDto == null)
                return BadRequest(new ApiResponse(400, "Datos inválidos."));

            var existingUserMember = await _unitOfWork.UserMember.GetByIdAsync(id);
            if (existingUserMember == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            var userMember = _mapper.Map<UserMember>(userMemberDto);
            _unitOfWork.UserMember.Update(userMember);
            await _unitOfWork.SaveAsync();

            return Ok(userMemberDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userMember = await _unitOfWork.UserMember.GetByIdAsync(id);
            if (userMember == null)
                return NotFound(new ApiResponse(404, "La experiencia solicitada no existe."));

            _unitOfWork.UserMember.Remove(userMember);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
    }