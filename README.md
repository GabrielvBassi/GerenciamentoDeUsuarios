# GerenciamentoDeUsuarios

Foi utilizado neste projeto o .NET FRAMEWORK 6.0

Foi utilizado neste projeto as seguintes bibliotecas:

- Microsoft.EntityFrameworkCore (Vers�o6.0.0)
- Microsoft.EntityFrameworkCore.Design (Vers�o6.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (Vers�o6.0.0)
- Microsoft.EntityFrameworkCore.Tools (Vers�o6.0.0)
- Swashbuckle.AspNetCore (Vers�o6.2.3)

Para que possa iniciar o projeto com sucesso, � necess�rio realizar os seguintes passos:

1- Definir conex�o com o banco de dados:
Acessar o arquivo "appsettings.json" e alterar o campo "ConnectionStrings": {"DataBase":" e inserir a string de conex�o com o banco de dados SQLSERVER,
informando o diret�rio e os dados de acesso para que sejam liberadas as permiss�es do banco de dados.

2 - Executar a Migration:
Acessar o "Console de gerenciador de pacotes"
Executar os seguintes comandos: 
	Add-Migration initialDB -Context GerenciamentoDeUsuariosDBContext
	Update-Database


Ap�s esses passos, ser� poss�vel a utiliza��o da API.




A API foi criada utilizando o EntityFramework para facilitar a cria��o e integra��o com o banco a partir do c�digo fonte desenvolvido,
o que facilita a manuten��o do c�digo se necess�rio.

Utilizei da defini��oi do controller para implementar os m�todos criados no reposit�rio e definir m�todos adicionais para a valida��o.
Devido ao tempo curto de desenvolvimento do projeto, n�o foi poss�vel criar uma classe para a classifica��o das exce��es geradas,
ent�o, opetei por realizar o tratamento delas individualmente no pr�prio 'UsuarioController'.

Defini algumas informa��es complementares no Swagger para facilitar o entendimento dos usu�rios da API.

Precisei definir uma sobrecarga de m�todos para a busca por CPF e Email, devido a necessidade de validar os campos para evitar a duplicidade no cadastro.
Como encontrei uma situa��o onde ocorria um erro ao alterar o usu�rio, mantendo as informa��es, optei por realizar a sobrecarga para evitar a situa��o,
n�o foi a melhor maneira, mas foi a mais r�pida que consegui identificar para corre��o da n�o conformidade.

