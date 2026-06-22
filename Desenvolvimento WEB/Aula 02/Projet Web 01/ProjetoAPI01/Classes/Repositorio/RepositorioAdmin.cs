using Microsoft.Data.SqlClient;
using ProjetoAPI01.Classes.DTO;

namespace ProjetoAPI01.Classes.Repositorio
{
    public class RepositorioAdmin
    {
        private readonly string stringConexao;

        public RepositorioAdmin(string conexao)
        {
            stringConexao = conexao;
        }

        public async Task<List<UsuarioAdminDTO>> ListarParaAdminAsync(CancellationToken cancellationToken)
        {
            var resultado = new List<UsuarioAdminDTO>();
            await using var conexao = new SqlConnection(stringConexao);
            await conexao.OpenAsync(cancellationToken);

            const string sql = "SELECT ID, RA, Nome, Regra, StatusWIFI, StatusAction FROM Alunos";
            await using var cmd = new SqlCommand(sql, conexao);
            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                resultado.Add(new UsuarioAdminDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    RA = reader.IsDBNull(reader.GetOrdinal("RA")) ? 0 : reader.GetInt32(reader.GetOrdinal("RA")),
                    Nome = reader.IsDBNull(reader.GetOrdinal("Nome")) ? string.Empty : reader.GetString(reader.GetOrdinal("Nome")),
                    Regra = reader.IsDBNull(reader.GetOrdinal("Regra")) ? 0 : reader.GetInt32(reader.GetOrdinal("Regra")),
                    StatusWIFI = reader.IsDBNull(reader.GetOrdinal("StatusWIFI")) ? string.Empty : reader.GetString(reader.GetOrdinal("StatusWIFI")),
                    StatusAction = reader.IsDBNull(reader.GetOrdinal("StatusAction")) ? string.Empty : reader.GetString(reader.GetOrdinal("StatusAction"))
                });
            }
            return resultado;
        }

        public async Task<bool> AtualizarStatusAsync(int id, string statusWifi, string statusAction, CancellationToken cancellationToken)
        {
            await using var conexao = new SqlConnection(stringConexao);
            await conexao.OpenAsync(cancellationToken);

            const string sql = "UPDATE Alunos SET StatusWIFI = @statusWifi, StatusAction = @statusAction WHERE ID = @id";
            await using var cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@statusWifi", statusWifi);
            cmd.Parameters.AddWithValue("@statusAction", statusAction);
            cmd.Parameters.AddWithValue("@id", id);

            var linhas = await cmd.ExecuteNonQueryAsync(cancellationToken);
            return linhas > 0;
        }
    }
}
