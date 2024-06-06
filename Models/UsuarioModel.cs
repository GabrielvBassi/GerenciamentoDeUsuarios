using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeUsuarios.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set;}
        public string Cpf { get; set;}
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Nascimento { get; set; }

    }
}
