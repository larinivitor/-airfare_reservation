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
    [Authorize, Route("api/local")]
    public class LocalController : Controller
    {
        private ILocalRepository localRepository;
        private PassagensAereasContext context;
        private IOptions<SecuritySettings> settings;

        public LocalController(
            ILocalRepository localRepository,
            PassagensAereasContext context,
            IOptions<SecuritySettings> settings)
        {
            this.localRepository = localRepository;
            this.context = context;
            this.settings = settings;
        }

        //GET api/local
        [HttpGet(Name="GetLocais")]
        public IActionResult GetLocais()
        {
            return Ok(localRepository.GetLocais());
        }

        //GET api/local/{id}
        [HttpGet("{id}", Name="GetLocal")]
        public IActionResult GetLocal(int id)
        {
            var classeVoo = localRepository.GetLocal(id);

            if (classeVoo == null) 
                return NotFound("Local não encontrada.");

            return Ok(classeVoo);
        }

        // POST api/local
        [Authorize(Roles="Admin"), HttpPost]
        public IActionResult Post([FromBody]LocalDto localRequest)
        {
            var local = MapearDtoParaDominio(localRequest);
            var inconsistencias = LocalService.Validar(local);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            localRepository.Salvar(local);
            context.SaveChanges();
            return Ok(local);
        }

        // PUT api/local/{id}
        [Authorize(Roles="Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]LocalDto localRequest)
        {
            var local = MapearDtoParaDominio(localRequest);
            var inconsistencias = LocalService.Validar(local);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            localRepository.Editar(id, local);
            context.SaveChanges();
            return Ok();
        }

        //DELETE api/local/{id}
        [Authorize(Roles="Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!localRepository.PodeDeletar(id))
                return BadRequest("Não é possível deletar, pois possui dependências no servidor.");

            localRepository.Delete(id);
            context.SaveChanges();
            return Ok();
        }

        private Local MapearDtoParaDominio(LocalDto localRequest)
        {
            return new Local(
                localRequest.Nome,
                localRequest.LatitudeLocal,
                localRequest.LongitudeLocal
            );
        }
    }
}