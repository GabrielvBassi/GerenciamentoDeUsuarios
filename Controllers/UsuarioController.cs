using GerenciamentoDeUsuarios.Data;
using GerenciamentoDeUsuarios.Models;
using GerenciamentoDeUsuarios.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace GerenciamentoDeUsuarios.Controllers
{
    // <summary>
    /// API Gerenciamento de Usuários.
    /// </summary> 

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        /// <summary>
        /// Listagem de usuários.
        /// </summary>        
        /// <response code="200">Retorna uma listagem de usuários</response>
        /// <response code="500">Exception Error</response>

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarListagemUsuario()
        {
            try
            {
                List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarListagemUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString()); // Codigo 500 quando da algum erro inesperado
            }
        }

        /// <summary>
        /// Listagem de usuários por ID.
        /// </summary>        
        /// <response code="200">Retorna uma listagem de usuários com base no ID</response>
        /// <response code="400">Erro de Validação do ID</response>
        /// <response code="500">Exception Error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarListagemUsuarioPorId(int id)
        {

            //Validando se existe um usuario com o ID informado

            try
            {
                if (id == null || id <= 0)
                {
                    throw new ValidationException("O Id é inválido.");
                }

                UsuarioModel usuarios = await _usuarioRepositorio.BuscarListagemUsuarioPorId(id);
                return Ok(usuarios);
            }
            catch (ValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message.ToString()); // Erros e validação de campos usando o ValidationException
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString()); // Codigo 500 quando da algum erro inesperado
            }
        }



        /// <summary>
        /// Cadastro de um novo usuário.
        /// </summary>        
        /// <response code="200">Cadastro de usário efetuado</response>
        /// <response code="201">Cadastro de usário efetuado</response>
        /// <response code="400">Erro de Validação dos dados informados</response>
        /// <response code="500">Exception Error</response>
        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario([FromBody] UsuarioModel usuarioModel)
        {
            try
            {
                ValidarCampos(usuarioModel);

                //Aqui tem que ser um metodo de pesquisa e nele você ja faz a pesquisa por CPF 
                var usuarioCpf = await _usuarioRepositorio.BuscarUsuarioCpf(usuarioModel.Cpf);


                if (usuarioCpf != null)
                {
                    throw new ValidationException("O CPF já está cadastrado no sistema.");
                }

                //Aqui tem que ser um metodo de pesquisa e nele você ja faz a pesquisa por Email 
                var emailExistente = await _usuarioRepositorio.BuscarUsuarioEmail(usuarioModel.Email);

                if (emailExistente != null)
                {
                    throw new ValidationException("O E-mail já está cadastrado no sistema.");
                }

                //usuarioModel.Senha = Convert.ToBase64String(usuarioModel.Senha);

                await _usuarioRepositorio.AdicionarUsuario(usuarioModel);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message.ToString()); // Erros e validação de campos usando o ValidationException
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString()); // Codigo 500 quando da algum erro inesperado
            }
        }

        /// <summary>
        /// Atualização de usuário.
        /// </summary>        
        /// <response code="200">Alteração efetuada</response>
        /// <response code="400">Erro de Validação do dados informados</response>
        /// <response code="500">Exception Error</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> AtualizarUsuario([FromBody] UsuarioModel usuarioModel, int id)
        {

            try
            {
                ValidarCampos(usuarioModel);

                //Aqui tem que ser um metodo de pesquisa e nele você ja faz a pesquisa por CPF 
                if (id == null || id <= 0)
                {
                    throw new ValidationException("O Id é inválido.");
                }

                var usuarioCpf = await _usuarioRepositorio.BuscarUsuarioCpf(usuarioModel.Cpf, id);


                if (usuarioCpf != null)
                {
                    throw new ValidationException("O CPF já está cadastrado no sistema.");
                }

                //Aqui tem que ser um metodo de pesquisa e nele você ja faz a pesquisa por Email 
                var emailExistente = await _usuarioRepositorio.BuscarUsuarioEmail(usuarioModel.Email, id);

                if (emailExistente != null)
                {
                    throw new ValidationException("O E-mail já está cadastrado no sistema.");
                }

                await _usuarioRepositorio.AtualizarUsuario(usuarioModel, id);
                var result = await _usuarioRepositorio.BuscarListagemUsuarioPorId(id);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message.ToString()); // Erros e validação de campos usando o ValidationException
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString()); // Codigo 500 quando da algum erro inesperado
            }
        }


        /// <summary>
        /// Exclusão de usuário.
        /// </summary>        
        /// <response code="200">Exclusão efetuada</response>
        /// <response code="500">Exception Error</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> ApagarUsuario(int id)
        {
            bool apagado = await _usuarioRepositorio.ApagarUsuario(id);
            return Ok(apagado);
        }

        private void ValidarCampos(UsuarioModel usuario)
        {
            if (usuario.Nome == null)
            {
                throw new ValidationException("É obrigatório o preenchiemnto do Nome.");
            }
            if (usuario.Senha == null)
            {
                throw new ValidationException("É obrigatório o preenchiemnto da Senha.");
            }
            if (usuario.Cpf == null)
            {
                throw new ValidationException("É obrigatório o preenchiemnto do CPF.");
            }
            if (usuario.Email == null)
            {
                throw new ValidationException("É obrigatório o preenchiemnto do E-mail.");
            }
            if (usuario.Nascimento == null)
            {
                throw new ValidationException("É obrigatório o preenchiemnto da Data de nascimento.");
            }
        }

    }
}
