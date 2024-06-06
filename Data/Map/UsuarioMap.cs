using GerenciamentoDeUsuarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeUsuarios.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        //Mapeamento dos campos na tabela Usuario do banco de dados
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(90);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(90);
            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(11);
            builder.Property(x => x.Nascimento).IsRequired().HasMaxLength(20);

            //Atribuindo ao CPF um campo unico, evitando duplicidade
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();




        }
    }
}
