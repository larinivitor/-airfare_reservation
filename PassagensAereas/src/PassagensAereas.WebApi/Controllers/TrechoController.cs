using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;
using PassagensAereas.Dominio.Servicos;
using PassagensAereas.Infra;
using PassagensAereas.WebApi.Models.Request;

namespace PassagensAereas.WebApi.Controllers
{
    [Authorize, Route("api/trecho")]
    public class TrechoController : Controller
    {
        private ITrechoRepository trechoRepository;
        private ILocalRepository localRepository;
        private PassagensAereasContext context;
        private IOptions<SecuritySettings> settings;

        public TrechoController(
            ITrechoRepository trechoRepository,
            ILocalRepository localRepository,
            PassagensAereasContext context,
            IOptions<SecuritySettings> settings)
        {
            this.trechoRepository = trechoRepository;
            this.localRepository = localRepository;
            this.context = context;
            this.settings = settings;
        }

        //GET api/trecho
        [HttpGet(Name="GetTrechos")]
        public IActionResult GetTrechos()
        {
            return Ok(trechoRepository.GetTrechos());
        }

        //GET api/trecho/{id}
        [HttpGet("{id}", Name="GetTrecho")]
        public IActionResult GetTrecho(int id)
        {
            var trecho = trechoRepository.GetTrecho(id);

            if (trecho == null) 
                return NotFound("Trecho não encontrado.");

            return Ok(trecho);
        }

        // POST api/trecho
        [Authorize(Roles="Admin"), HttpPost]
        public IActionResult Post([FromBody]TrechoDto trechoRequest)
        {
            var trecho = MapearDtoParaDominio(trechoRequest);
            var inconsistencias = TrechoService.Validar(trecho);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            trechoRepository.Salvar(trecho);
            context.SaveChanges();
            return Ok(trecho);
        }

        // PUT api/trecho/{id}
        [Authorize(Roles="Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TrechoDto trechoRequest)
        {
            var trecho = MapearDtoParaDominio(trechoRequest);
            var inconsistencias = TrechoService.Validar(trecho);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            trechoRepository.Editar(id, trecho);
            context.SaveChanges();
            return Ok();
        }

        //DELETE api/trecho/{id}
        [Authorize(Roles="Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!trechoRepository.PodeDeletar(id))
                return BadRequest("Não é possível deletar, pois possui dependências no servidor.");

            trechoRepository.Delete(id);
            context.SaveChanges();
            return Ok();
        }

        private Trecho MapearDtoParaDominio(TrechoDto trechoRequest)
        {
            return new Trecho(
                trechoRequest.Nome,
                localRepository.GetLocal(trechoRequest.IdLocalA),
                localRepository.GetLocal(trechoRequest.IdLocalB)
            );
        }
    }
}