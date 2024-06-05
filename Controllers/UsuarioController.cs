using GerenciamentoDeUsuarios.Models;
using GerenciamentoDeUsuarios.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace GerenciamentoDeUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        //Listagem de Usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarListagemUsuario()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarListagemUsuarios();
            
            if (usuarios.Any())
            {
                return Ok(usuarios);
            }
            else
            {
                return BadRequest("Não existe nenhum usuário cadastrado.");
            }

        }
        //Listagem de Usuarios por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarListagemUsuarioPorId(int id)
        {
            UsuarioModel usuarios = await _usuarioRepositorio.BuscarListagemUsuarioPorId(id);

            //Validando se existe um usuario com o ID informado
            if (usuarios != null)
            {
                return Ok(usuarios);
            }
            else
            {
                return BadRequest("Não existe nenhum usuário cadastrado com esse ID.");
            }
        }

        //Cadastro de novo usuário
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CadastrarUsuario([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _usuarioRepositorio.AdicionarUsuario(usuarioModel);
            return Ok(usuario);
        }
        //Verificando se o CPF já está cadastrado no sistema


        //Atualização de usuario
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> AtualizarUsuario([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await _usuarioRepositorio.AtualizarUsuario(usuarioModel, id);
            return Ok(usuario);
        }


        //Exclusão de usuario
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> ApagarUsuario(int id)
        {
            bool apagado = await _usuarioRepositorio.ApagarUsuario(id);
            return Ok(apagado);
        }
    }
}
