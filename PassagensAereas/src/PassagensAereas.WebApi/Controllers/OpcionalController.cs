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
    [Authorize, Route("api/opcional")]
    public class OpcionalController : Controller
    {
        private IOpcionalRepository opcionalRepository;
        private PassagensAereasContext context;
        private IOptions<SecuritySettings> settings;

        public OpcionalController(
            IOpcionalRepository opcionalRepository,
            PassagensAereasContext context,
            IOptions<SecuritySettings> settings)
        {
            this.opcionalRepository = opcionalRepository;
            this.context = context;
            this.settings = settings;
        }

        //GET api/opcional
        [HttpGet(Name="GetOpcionais")]
        public IActionResult GetOpcionais()
        {
            return Ok(opcionalRepository.GetOpcionais());
        }

        //GET api/opcional/{id}
        [HttpGet("{id}", Name="GetOpcional")]
        public IActionResult GetOpcional(int id)
        {
            var opcional = opcionalRepository.GetOpcional(id);

            if (opcional == null) 
                return NotFound("Opcional não encontrado.");

            return Ok(opcional);
        }

        // POST api/opcional
        [Authorize(Roles="Admin"), HttpPost]
        public IActionResult Post([FromBody]OpcionalDto opcionalRequest)
        {
            var opcional = MapearDtoParaDominio(opcionalRequest);
            var inconsistencias = OpcionalService.Validar(opcional);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            opcionalRepository.Salvar(opcional);
            context.SaveChanges();
            return Ok(opcional);
        }

        // PUT api/opcional/{id}
        [Authorize(Roles="Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OpcionalDto opcionalRequest)
        {
            var opcional = MapearDtoParaDominio(opcionalRequest);
            var inconsistencias = OpcionalService.Validar(opcional);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            opcionalRepository.Editar(id, opcional);
            context.SaveChanges();
            return Ok();
        }

        //DELETE api/opcional/{id}
        [Authorize(Roles="Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!opcionalRepository.PodeDeletar(id))
                return BadRequest("Não é possível deletar, pois possui dependências no servidor.");

            opcionalRepository.Delete(id);
            context.SaveChanges();
            return Ok();
        }

        private Opcional MapearDtoParaDominio(OpcionalDto opcionalRequest)
        {
            return new Opcional(
                opcionalRequest.Nome,
                opcionalRequest.Descricao,
                opcionalRequest.Valor
            );
        }
    }
}