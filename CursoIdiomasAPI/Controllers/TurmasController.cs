using AutoMapper;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Models.DTOs;
using CursoIdiomasAPI.Pagination;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        [HttpGet("pagination")]
        public ActionResult<IEnumerable<TurmaDTO>> Get([FromQuery] TurmasParameters turmasParameters)
        {
            var turmas = _unitOfWork.TurmaRepository.GetTurmasPages(turmasParameters);

           return ObterTurmas(turmas);
        }

        [HttpGet("pagination/filter/quantidadeAlunos")]
        public ActionResult<IEnumerable<TurmaDTO>> GetTurmasFiltradas([FromQuery] TurmasFiltroNumeroAlunos turmasParams) 
        {
            var turmas = _unitOfWork.TurmaRepository.GetTurmasFiltroNumeroAlunos(turmasParams);
            
            return ObterTurmas(turmas);
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

        private ActionResult<IEnumerable<TurmaDTO>> ObterTurmas(PagedList<Turma> turmas)
        {
            var metadata = new
            {
                turmas.TotalCount,
                turmas.PageSize,
                turmas.CurrentPage,
                turmas.TotalPages,
                turmas.HasNext,
                turmas.HasPrevious
            };

            //adicionar metadados ao cabeçalho da resposta
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var turmasDto = _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
            return Ok(turmasDto);
        }
    }
}
