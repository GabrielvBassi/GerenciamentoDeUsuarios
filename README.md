# GerenciamentoDeUsuarios

Foi utilizado neste projeto o .NET 6.0

Foi utilizado neste projeto as seguintes bibliotecas:

- Microsoft.EntityFrameworkCore (Versão6.0.0)
- Microsoft.EntityFrameworkCore.Design (Versão6.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (Versão6.0.0)
- Microsoft.EntityFrameworkCore.Tools (Versão6.0.0)
- Swashbuckle.AspNetCore (Versão6.2.3)

Para que possa iniciar o projeto com sucesso, é necessário realizar os seguintes passos:

1- Definir conexão com o banco de dados:
Acessar o arquivo "appsettings.json" e alterar o campo "ConnectionStrings": {"DataBase":" e inserir a string de conexão com o banco de dados SQLSERVER,
informando o diretório e os dados de acesso para que sejam liberadas as permissões do banco de dados.

2 - Executar a Migration:
Acessar o "Console de gerenciador de pacotes"
Executar os seguintes comandos: 
	Add-Migration initialDB -Context GerenciamentoDeUsuariosDBContext
	Update-Database


Após esses passos, será possível a utilização da API.




A API foi criada utilizando o EntityFramework para facilitar a criação e integração com o banco a partir do código fonte desenvolvido,
o que facilita a manutenção do código se necessário.

Utilizei da definição do controller para implementar os métodos criados no repositório e definir métodos adicionais para a validação.
Devido ao tempo curto de desenvolvimento do projeto, não foi possível criar uma classe para a classificação das exceções geradas,
então, optei por realizar o tratamento delas individualmente no próprio 'UsuarioController'.

Defini algumas informações complementares no Swagger para facilitar o entendimento dos usuários da API.

Precisei definir uma sobrecarga de métodos para a busca por CPF e Email, devido a necessidade de validar os campos para evitar a duplicidade no cadastro.
Como encontrei uma situação onde ocorria um erro ao alterar o usuário, mantendo as informações, optei por realizar a sobrecarga para evitar essa situação,
não foi a melhor maneira, mas foi a mais rápida que consegui identificar para correção da não conformidade.

