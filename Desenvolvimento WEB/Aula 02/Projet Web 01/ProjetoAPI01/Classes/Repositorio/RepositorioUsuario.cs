using Microsoft.Data.SqlClient;
using ProjetoAPI01.Classes.DTO;

namespace ProjetoAPI01.Classes.Repositorio
{
    public class RepositorioUsuario
    {
        private readonly string stringConexao;

        public RepositorioUsuario(string conexao)
        {
            this.stringConexao = conexao;
        }

        //Consulta dó usuário por email e senha, retorna somente dados necessário para o login
        public async Task<UsuarioDTO?> BuscarPorEmailESenha (string email, string senha, CancellationToken cancellationToken)
        {
            await using var conexao = new SqlConnection(stringConexao);
            await conexao.OpenAsync(cancellationToken);

            const string comandoSQL = """
                SELECT TOP 1 Id, Nome, Regra
                FROM Alunos
                WHERE Email = @email AND Senha = @senha
                """;
            await using var comando = new SqlCommand(comandoSQL, conexao);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);

            await using var leitor = await comando.ExecuteReaderAsync(cancellationToken);
            if (await leitor.ReadAsync(cancellationToken))
            {
                return null;
            }
            return new UsuarioDTO
            {
                Id = leitor.GetInt32(leitor.GetOrdinal("Id")),
                Nome = leitor.GetString(leitor.GetOrdinal("Nome")),
                Regra = leitor.GetInt32(leitor.GetOrdinal("Regra"))
            };
        }
    }
}
