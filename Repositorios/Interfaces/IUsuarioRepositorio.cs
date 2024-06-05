using GerenciamentoDeUsuarios.Models;

namespace GerenciamentoDeUsuarios.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> BuscarListagemUsuarios();
        Task<UsuarioModel> BuscarListagemUsuarioPorId(int id);
        Task<UsuarioModel> AdicionarUsuario(UsuarioModel usuario);
        Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario, int id);
        Task<bool> ApagarUsuario(int id);
    }
}
