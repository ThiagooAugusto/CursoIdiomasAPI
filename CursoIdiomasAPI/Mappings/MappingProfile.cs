using AutoMapper;
using CursoIdiomasAPI.Models.DTOs;
using CursoIdiomasAPI.Models;

namespace CursoIdiomasAPI.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<AlunoCreateDTO,Aluno>().ReverseMap();
            CreateMap<AlunoUpdateDTO,Aluno>().ReverseMap();
            CreateMap<AlunoDTO,Aluno>().ReverseMap();
            CreateMap<TurmaDTO,Turma>().ReverseMap();
            CreateMap<TurmaCreateDTO,Turma>().ReverseMap();
        }
    }
}
