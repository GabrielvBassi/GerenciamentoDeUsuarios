using GerenciamentoDeUsuarios.Data;
using GerenciamentoDeUsuarios.Models;
using GerenciamentoDeUsuarios.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoDeUsuarios.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly GerenciamentoDeUsuariosDBContext _dbContext;
        public UsuarioRepositorio(GerenciamentoDeUsuariosDBContext GerenciamentoUsuarioDBContext)
        {
            _dbContext = GerenciamentoUsuarioDBContext;
        }

        public async Task<List<UsuarioModel>> BuscarListagemUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> BuscarListagemUsuarioPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UsuarioModel> BuscarUsuarioCpf(string cpf, int id)
        {
            //Valida se o CPF existe
            var usuario = _dbContext.Usuarios.Where(x => x.Cpf == cpf && x.Id != id);
            return await usuario.FirstOrDefaultAsync();
        }

        public async Task<UsuarioModel> BuscarUsuarioEmail(string email, int id)
        {
            //Valida se o Email existe
            var usuario = _dbContext.Usuarios.Where(x => x.Email == email && x.Id != id);
            return await usuario.FirstOrDefaultAsync();
        }

        public async Task<UsuarioModel> BuscarUsuarioCpf(string cpf)
        {
            //Valida se o CPF existe
            var usuario = _dbContext.Usuarios.Where(x => x.Cpf == cpf);
            return await usuario.FirstOrDefaultAsync();
        }

        public async Task<UsuarioModel> BuscarUsuarioEmail(string email)
        {
            //Valida se o Email existe
            var usuario = _dbContext.Usuarios.Where(x => x.Email == email);
            return await usuario.FirstOrDefaultAsync();
        }


        public async Task<UsuarioModel> AdicionarUsuario(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarListagemUsuarioPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"O usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.Cpf = usuario.Cpf;
            usuarioPorId.Senha = usuario.Senha;
            usuarioPorId.Nascimento = usuario.Nascimento;

             _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> ApagarUsuario(int id)
        {
            UsuarioModel usuarioPorId = await BuscarListagemUsuarioPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"O usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

     
    }
}
