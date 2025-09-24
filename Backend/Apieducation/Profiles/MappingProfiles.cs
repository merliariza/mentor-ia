using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using AutoMapper;
namespace Apieducation.Profiles
{

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Progress, ProgressDto>().ReverseMap();
            CreateMap<UserMember, UserMemberDto>().ReverseMap();
            CreateMap<EvaluationSession, EvaluationSessionDto>().ReverseMap();
            CreateMap<Flashcard, FlashcardDto>().ReverseMap();
        }
    }
}