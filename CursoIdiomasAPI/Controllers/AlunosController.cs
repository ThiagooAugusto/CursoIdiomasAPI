using AutoMapper;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Models.DTOs;
using CursoIdiomasAPI.Pagination;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CursoIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlunosController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AlunoDTO>> Get()
        {
            var alunos = _unitOfWork.AlunoRepository.GetAll();

            if (alunos == null)
                return NotFound("Não há alunos cadastrados!");

            var alunosDTO = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

            return Ok(alunosDTO);
        }
        [HttpGet("{id:int}",Name ="ObterAlunoPorId")]
        public ActionResult<AlunoDTO> Get(int id) 
        { 
            var aluno = _unitOfWork.AlunoRepository.Get(a=>a.AlunoId == id);

            if(aluno == null)
                return NotFound();

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);
            return Ok(alunoDTO);
        }
        [HttpGet("matricula/{matricula}", Name = "ObterAlunoPorMatricula")]
        public ActionResult<AlunoDTO> Get(string matricula)
        {
            var aluno = _unitOfWork.AlunoRepository.Get(a => a.Matricula == matricula);

            if (aluno == null)
                return NotFound();

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);
            return Ok(alunoDTO);
        }

        //[HttpGet("pagination")]
        //public ActionResult<IEnumerable<AlunoDTO>> Get([FromQuery] AlunosParameters alunosParams)
        //{
        //    var alunos = _unitOfWork.AlunoRepository.GetAlunosPages(alunosParams);

        //    var alunosDTO = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

        //    return Ok(alunosDTO);
        //}


        [HttpGet("pagination")]
        public ActionResult<IEnumerable<AlunoDTO>> Get([FromQuery] AlunosParameters alunosParameters)
        {
            var alunos = _unitOfWork.AlunoRepository.GetAlunosPages(alunosParameters);

            return ObterAlunos(alunos);
        }

        [HttpGet("pagination/nome/filter")]
        public ActionResult<IEnumerable<AlunoDTO>> GetAlunosFiltrados([FromQuery] AlunosFiltroNome alunosFiltro)
        {
            var alunos = _unitOfWork.AlunoRepository.GetAlunosNomeFiltro(alunosFiltro);
            return ObterAlunos(alunos);
        }

        [HttpPost("turmas/{id}")]
        public ActionResult<AlunoDTO> Post(int id, AlunoCreateDTO alunoDTO)
        {
            //Faz validações de entrada
            if (alunoDTO == null)
                return BadRequest(); 

            //Busca no banco
            var alunoComCPF = _unitOfWork.AlunoRepository.Get(a=>a.CPF == alunoDTO.CPF);
            var turma = _unitOfWork.TurmaRepository.Get(a=>a.TurmaId == id);

            //Faz verificações dos requisitos para cadastro
            if (alunoComCPF != null)
                return BadRequest("Aluno com Cpf já cadastrado!");

            if (turma == null)
                return NotFound("Turma não encontrada!");

            if (turma.Alunos.Any(t => t.Matricula == alunoDTO.Matricula))
                return BadRequest("Aluno com essa matrícula já está cadastrado na turma!");

            if (turma.Alunos.Count >= 5)
                return BadRequest("Turma já está cheia!");

            //Formatar cpf para salvar no banco
            var cpfFormatado = alunoDTO.CPF.Replace(".", "").Replace("-", "");
            alunoDTO.CPF = cpfFormatado;

            var alunoEntity = _mapper.Map<Aluno>(alunoDTO);
            alunoEntity.Turmas.Add(turma);

            //Cadastra aluno se verificações passaram
            var alunoCadastrado = _unitOfWork.AlunoRepository.Add(alunoEntity);
            _unitOfWork.Commit();

            var alunoCadastradoDTO = _mapper.Map<AlunoDTO>(alunoCadastrado);
            return new CreatedAtRouteResult("ObterAlunoPorId", new { id = alunoCadastrado.AlunoId }, alunoCadastradoDTO);

        }

        [HttpPut("{id}")]
        public ActionResult<AlunoDTO> Put(int id, AlunoUpdateDTO alunoDTO)
        {
            if (alunoDTO == null)
                return BadRequest();

            var aluno = _mapper.Map<Aluno>(alunoDTO);

            var alunoAtualizado = _unitOfWork.AlunoRepository.Update(aluno);
            _unitOfWork.Commit();

            var alunoAtualizadoDTO = _mapper.Map<AlunoDTO>(alunoAtualizado);

            return Ok(alunoAtualizadoDTO);
        }

        [HttpDelete("{id}")]
        public ActionResult<AlunoDTO> Delete(int id) 
        {
            var aluno = _unitOfWork.AlunoRepository.Get(a=>a.AlunoId == id);

            if (aluno == null)
                return NotFound("Aluno não encontrado!");

            var alunoDeletado = _unitOfWork.AlunoRepository.Delete(aluno);
            _unitOfWork.Commit();

            var alunoDeletadoDTO = _mapper.Map<AlunoDTO>(alunoDeletado);

            return Ok(alunoDeletadoDTO);
        }

        //Adiciona metadados da paginação ao cabeçalho da resposta
        private ActionResult<IEnumerable<AlunoDTO>> ObterAlunos(PagedList<Aluno> alunos)
        {
            var metadata = new
            {
                alunos.TotalCount,
                alunos.PageSize,
                alunos.CurrentPage,
                alunos.TotalPages,
                alunos.HasNext,
                alunos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var alunosDto = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
            return Ok(alunosDto);
        }
    }
}
