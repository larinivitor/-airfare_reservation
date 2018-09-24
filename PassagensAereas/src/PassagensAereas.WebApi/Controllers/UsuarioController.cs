using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;
using PassagensAereas.Dominio.Servicos;
using PassagensAereas.Infra;
using PassagensAereas.WebApi.Models.Request;
using PassagensAereas.WebApi.Models.Response;

namespace PassagensAereas.WebApi.Controllers
{
    [Authorize, Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private ITrechoRepository trechoRepository;
        private IReservaRepository reservaRepository;
        private IClasseVooRepository classeVooRepository;
        private IOpcionalRepository opcionalRepository;
        private PassagensAereasContext context;
        private IOptions<SecuritySettings> settings;

        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            ITrechoRepository trechoRepository,
            IReservaRepository reservaRepository,
            IClasseVooRepository classeVooRepository,
            IOpcionalRepository opcionalRepository,
            PassagensAereasContext context,
            IOptions<SecuritySettings> settings)
        {
            this.usuarioRepository = usuarioRepository;
            this.trechoRepository = trechoRepository;
            this.classeVooRepository = classeVooRepository;
            this.opcionalRepository = opcionalRepository;
            this.reservaRepository = reservaRepository;
            this.context = context;
            this.settings = settings;
        }

        // POST api/usuario
        [AllowAnonymous, HttpPost]
        public IActionResult Post([FromBody]UsuarioDto usuarioRequest)
        {
            var usuario = MapearUsuarioDtoParaDominio(usuarioRequest);
            var inconsistencias = UsuarioService.Validar(usuario);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            usuarioRepository.Salvar(usuario);
            context.SaveChanges();

            var usuarioResponse = new UsuarioResponseDto(
                usuario.PrimeiroNome,
                usuario.UltimoNome,
                usuario.CPF,
                usuario.DataNascimento,
                usuario.Login,
                usuario.Admin,
                usuario.Reservas
                );

            return Ok(usuarioResponse);
        }

        // PUT api/usuario/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, UsuarioDto usuarioRequest)
        {
            var usuario = MapearUsuarioDtoParaDominio(usuarioRequest);
            var inconsistencias = UsuarioService.Validar(usuario);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            usuarioRepository.Editar(id, usuario);
            context.SaveChanges();
            return Ok();
        }

        // POST api/usuario/login
        [AllowAnonymous, HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto dadosLogin)
        {
            var usuario = usuarioRepository.GetUsuarioPorLoginESenha(dadosLogin.Login, dadosLogin.Senha);

            if (usuario == null) 
                return BadRequest("Usuario ou senha inválidos");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.SigningKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: new[] {
                        new Claim(ClaimTypes.Name, usuario.Login),
                        new Claim(ClaimTypes.Role, usuario.Admin? "Admin" : "Client"),
                    },
                    expires: DateTime.Now.AddMinutes(45),
                    signingCredentials: signingCredentials
                );

            var id = usuario.Id.ToString();

            return Ok(new
                {
                    id,
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                }
            );
        }

        //GET api/usuario/{idUsuario}
        [HttpGet("{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            var usuario = usuarioRepository.GetUsuario(idUsuario);
            var usuarioResponse = new UsuarioResponseDto(
                usuario.PrimeiroNome,
                usuario.UltimoNome,
                usuario.CPF,
                usuario.DataNascimento,
                usuario.Login,
                usuario.Admin,
                usuario.Reservas
                );
            return Ok(usuarioResponse);
        }

        //PUT api/usuario/{idUsuario}/reservar
        [HttpPost("{idUsuario}/reservar")]
        public IActionResult Reservar(int idUsuario, [FromBody]ReservaDto reservaRequest)
        {
            var usuario = usuarioRepository.GetUsuario(idUsuario);

            if (usuario == null) 
                return NotFound("Usuário não encontrado.");

            var reserva = MapearReservaDtoParaDominio(reservaRequest);

            var inconsistencias = ReservaService.Validar(reserva);

            if (inconsistencias.Any())
                return BadRequest(inconsistencias);

            reservaRepository.Salvar(idUsuario, reserva);

            context.SaveChanges();
            return Ok();
        }

        //GET api/usuario/getValorReserva
        [HttpPost("/getValorReserva")]
        public IActionResult GetValorReserva([FromBody]ReservaDto reservaRequest)
        {
            var reserva = MapearReservaDtoParaDominio(reservaRequest);

            var inconsistencias = ReservaService.Validar(reserva);

            if (inconsistencias.Any())
                return Ok(0);

            return Ok(reserva.ValorTotal);
        }

        //GET api/usuario/{idReserva}/reserva
        [HttpGet("{idReserva}/reserva")]
        public IActionResult GetReserva(int idReserva)
        {
            var reserva = reservaRepository.GetReserva(idReserva);

            if (reserva == null) 
                return NotFound("Reserva não encontrada.");

            return Ok(reserva);
        }

        //GET api/{idUsuario}/reservas
        [HttpGet("{idUsuario}/reservas")]
        public IActionResult GetReservas(int idUsuario)
        {
            var reservas = reservaRepository.GetReservas(idUsuario);

            if (!reservas.Any()) 
                return NotFound("Nenhuma reserva encontrada.");

            return Ok(reservas);
        }

        //DELETE api/usuario/{idReserva}/reserva
        [HttpDelete("{idReserva}/reserva")]
        public IActionResult Delete(int idReserva)
        {
            reservaRepository.Delete(idReserva);
            context.SaveChanges();
            return Ok();
        }

        private Reserva MapearReservaDtoParaDominio(ReservaDto reservaRequest)
        {
            var opcionais = new List<Opcional>();

            foreach(var id in reservaRequest.IdOpcionais)
                opcionais.Add(opcionalRepository.GetOpcional(id));

            return new Reserva(
                trechoRepository.GetTrecho(reservaRequest.IdTrecho),
                classeVooRepository.GetClasseVoo(reservaRequest.IdClasseVoo),
                opcionais
            );
        }

        private Usuario MapearUsuarioDtoParaDominio(UsuarioDto usuarioRequest)
        {
            return new Usuario(
                usuarioRequest.PrimeiroNome,
                usuarioRequest.UltimoNome,
                usuarioRequest.CPF,
                usuarioRequest.DataNascimento,
                usuarioRequest.Login,
                usuarioRequest.Senha,
                usuarioRequest.Admin,
                null
            );
        }
    }
}