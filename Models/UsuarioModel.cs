﻿namespace GerenciamentoDeUsuarios.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set;}
        //senha em Hash
        public string Cpf { get; set;}

    }
}
