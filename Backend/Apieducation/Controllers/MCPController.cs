using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Apieducation.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Apieducation.Controllers
{
    [ApiController]
    [Route("mcp/[controller]")]
    public class ContextController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContextController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("progress/{userId}")]
        public async Task<IActionResult> GetProgress(int userId)
        {
            var progresses = await _unitOfWork.Progress.GetAllAsync();
            var userProgress = progresses
                .Where(p => p.UserMemberId == userId)
                .Select(p => new
                {
                    id = p.Id,
                    topic = p.Topic,
                    score = p.Score,
                    feedback = p.Feedback
                });

            return Ok(new
            {
                name = "progress",
                type = "resource",
                data = userProgress
            });
        }

        [HttpGet("progress/{progressId}/evaluations")]
        public async Task<IActionResult> GetEvaluations(int progressId)
        {
            var sessions = await _unitOfWork.EvaluationSession.GetByProgressIdAsync(progressId);

            return Ok(new
            {
                name = "evaluations",
                type = "resource",
                data = sessions.Select(s => new
                {
                    id = s.Id,
                    score = s.Score,
                    feedback = s.Feedback,
                    progressId = s.ProgressId
                })
            });
        }

        [HttpGet("evaluations/{sessionId}/flashcards")]
        public async Task<IActionResult> GetFlashcards(int sessionId)
        {
            var flashcards = await _unitOfWork.Flashcard.GetByEvaluationSessionIdAsync(sessionId);

            return Ok(new
            {
                name = "flashcards",
                type = "resource",
                data = flashcards.Select(f => new
                {
                    id = f.Id,
                    question = f.Question,
                    answer = f.Answer,
                    evaluationSessionId = f.EvaluationSessionId
                })
            });
        }
    }
}
