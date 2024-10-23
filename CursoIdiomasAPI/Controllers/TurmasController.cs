using AutoMapper;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Models.DTOs;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CursoIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TurmasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TurmaDTO>> Get()
        {
            var turmas = _unitOfWork.TurmaRepository.GetAll();
            var turmasDTO = _mapper.Map<IEnumerable<TurmaDTO>>(turmas);

            return Ok(turmasDTO);
        }

        [HttpGet("{id}",Name ="ObterTurmaPorId")]
        public ActionResult<Turma> Get(int id) 
        {
            var turma = _unitOfWork.TurmaRepository.Get(t=>t.TurmaId ==  id);

            if(turma == null)
                return NotFound("Turma não encontrada!");

            var turmaDTO = _mapper.Map<TurmaDTO>(turma);

            return Ok(turmaDTO);
        }
        [HttpGet("codigo/{codigo}", Name = "ObterTurmaPorCodigo")]
        public ActionResult<Turma> Get(string codigo)
        {
            var turma = _unitOfWork.TurmaRepository.Get(t => t.Codigo == codigo);

            if (turma == null)
                return NotFound("Turma não encontrada!");

            var turmaDTO = _mapper.Map<TurmaDTO>(turma);

            return Ok(turmaDTO);
        }

        [HttpPost]
        public ActionResult<TurmaDTO> Post(TurmaCreateDTO turmaDTO)
        {
            if (turmaDTO == null)
                return BadRequest();

            var turma = _mapper.Map<Turma>(turmaDTO);

            var turmaCriada = _unitOfWork.TurmaRepository.Add(turma);
            _unitOfWork.Commit();

            var turmaCriadaDTO = _mapper.Map<TurmaDTO>(turmaCriada);

            return new CreatedAtRouteResult("ObterTurmaPorId", new { id = turmaCriada.TurmaId }, turmaCriadaDTO);
        }

        [HttpDelete]
        public ActionResult<TurmaDTO> Delete(int id)
        {
            var turma = _unitOfWork.TurmaRepository.Get(t=>t.TurmaId == id);

            if (turma == null)
                return NotFound("Turma não encontrada!");

            if(turma.Alunos.Count > 0)
                return BadRequest("Turma não pode ser deletada pois possui alunos!");

            var turmaDeletada = _unitOfWork.TurmaRepository.Delete(turma);
            _unitOfWork.Commit();

            var turmaDTO = _mapper.Map<TurmaDTO>(turmaDeletada);
            return Ok(turmaDTO);
            
        }
    }
}
