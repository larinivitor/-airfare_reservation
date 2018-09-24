using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private PassagensAereasContext context;

        public UsuarioRepository(PassagensAereasContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var usuario = context.Usuarios.Include(p => p.Reservas).FirstOrDefault(p => p.Id == id);
            context.Reservas.RemoveRange(usuario.Reservas);
            context.Usuarios.Remove(usuario);
        }

        public void Editar(int id, Usuario usuarioAlterado)
        {
            var usuario = context.Usuarios.FirstOrDefault(p => p.Id == id);
            usuarioAlterado.AlterarSenha(CriptografarSenha(usuarioAlterado.Senha));
            usuario.Atualizar(usuarioAlterado);
        }

        public Usuario GetUsuario(int id)
        {
            return context.Usuarios.FirstOrDefault(p => p.Id == id);
        }

        public Usuario GetUsuarioPorLoginESenha(string login, string senha)
        {
            var senhaCriptografada = CriptografarSenha(senha);
            return context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Login == login && u.Senha == senhaCriptografada);
        }

        public void Salvar(Usuario usuario)
        {
            usuario.AlterarSenha(CriptografarSenha(usuario.Senha));
            context.Usuarios.Add(usuario);
        }

        private string CriptografarSenha(string senha)
        {
            var inputBytes = Encoding.UTF8.GetBytes(senha);

            var hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}