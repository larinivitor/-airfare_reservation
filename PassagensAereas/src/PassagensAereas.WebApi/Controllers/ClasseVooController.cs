using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;
using PassagensAereas.Dominio.Servicos;
using PassagensAereas.Infra;
using PassagensAereas.WebApi.Models.Request;

namespace PassagensAereas.WebApi.Controllers
{
    [Authorize, Route("api/classeVoo")]
    public class ClasseVooController : Controller
    {
        private IClasseVooRepository classeVooRepository;
        private PassagensAereasContext context;
        private IOptions<SecuritySettings> settings;

        public ClasseVooController(
            IClasseVooRepository classeVooRepository,
            PassagensAereasContext context,
            IOptions<SecuritySettings> settings)
        {
            this.classeVooRepository = classeVooRepository;
            this.context = context;
            this.settings = settings;
        }

        //GET api/classesVoo
        [HttpGet(Name="GetClasses")]
        public IActionResult GetClasses()
        {
            return Ok(classeVooRepository.GetClasses());
        }

        //GET api/classeVoo/{id}
        [HttpGet("{id}", Name="GetClasse")]
        public IActionResult GetClasseVoo(int id)
        {
            var classeVoo = classeVooRepository.GetClasseVoo(id);

            if (classeVoo == null) 
                return NotFound("Classe não encontrada.");

            return Ok(classeVoo);
        }

        // POST api/classeVoo
        [Authorize(Roles="Admin"), HttpPost]
        public IActionResult Post([FromBody]ClasseVooDto classeVooRequest)
        {
            var classeVoo = MapearDtoParaDominio(classeVooRequest);
            var inconsistencias = ClasseVooService.Validar(classeVoo);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            classeVooRepository.Salvar(classeVoo);
            context.SaveChanges();
            return Ok(classeVoo);
        }

        // PUT api/classeVoo/{id}
        [Authorize(Roles="Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ClasseVooDto classeVooRequest)
        {
            var classeVoo = MapearDtoParaDominio(classeVooRequest);
            var inconsistencias = ClasseVooService.Validar(classeVoo);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            classeVooRepository.Editar(id, classeVoo);
            context.SaveChanges();
            return Ok();
        }

        //DELETE api/classeVoo/{id}
        [Authorize(Roles="Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!classeVooRepository.PodeDeletar(id))
                return BadRequest("Não é possível deletar, pois possui dependências no servidor.");

            classeVooRepository.Delete(id);
            context.SaveChanges();
            return Ok();
        }

        private ClasseVoo MapearDtoParaDominio(ClasseVooDto classeVooRequest)
        {
            return new ClasseVoo(
                classeVooRequest.Nome,
                classeVooRequest.ValorFixo,
                classeVooRequest.ValorMilha
            );
        }
    }
}