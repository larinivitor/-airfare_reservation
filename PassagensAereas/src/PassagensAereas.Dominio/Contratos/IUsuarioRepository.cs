using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Contratos
{
    public interface IUsuarioRepository
    {
        void Salvar (Usuario usuario);
        void Editar (int id, Usuario usuario);
        Usuario GetUsuario(int id);
        Usuario GetUsuarioPorLoginESenha(string login, string senha);
    }
}