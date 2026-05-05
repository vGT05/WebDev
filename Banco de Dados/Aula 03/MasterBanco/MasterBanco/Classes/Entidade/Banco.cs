using static System.Console;
using Microsoft.Data.SqlClient;
namespace MasterBanco.Classes.Entidade
{
    internal class Banco
    {
        //Campo
        private const decimal TaxaSaque = 5.00m;
        //Propriedades
        public int Id { get; set; }
        public string Titular { get; set; }
        public int NumeroDaConta { get; set; }
        public decimal Saldo { get; set; }

        //Construtores
        public Banco() {}
        public Banco(string titular, int numeroDaConta, decimal saldo) :this(titular, numeroDaConta)
        {
            Saldo = saldo;
        }
        public Banco(string titular, int numeroDaConta)
        {
            Titular = titular;
            NumeroDaConta = numeroDaConta;
            Saldo = 0;
        }

        //Caminho do servidor onde está o Banco de Dados
        private static string conectarCaminho = @"Server = ECFP5121319369\SQLEXPRESS; Database = Clodoaldo; Trusted_Connection = True; TrustServerCertificate = True;";
        
        //Operações CRUD
        //[C]reate
        public static void CadastrarContas(Banco banco)
        {
            //A.K.A "Query"
            string consulta = "INSERT INTO Contas(Titular, NumeroDaConta, Saldo)" +
                "VALUES" +
                "(@Titular, @NumeroDaConta, @Saldo)"; //o "@" serve como uma chamada genérica, que vai ser sobreposto pelo valor a ser chamado
            using (SqlConnection conexao = new SqlConnection(conectarCaminho))
            using (SqlCommand comando = new SqlCommand(consulta, conexao))
            {
                comando.Parameters.AddWithValue("@Titular", banco.Titular); // ^^^^^^ there it is
                comando.Parameters.AddWithValue("@NumeroDaConta", banco.NumeroDaConta);
                comando.Parameters.AddWithValue("@Saldo", banco.Saldo);
                
                conexao.Open();
                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    WriteLine($"Conta cadastrada com sucesso");
                }
            }
        }

        //[R]ead
        public static void LerContas()
        {
            string consulta = "SELECT Id,Titular,NumeroDaConta,Saldo FROM Contas";
            using (SqlConnection conexao = new SqlConnection(conectarCaminho))
            using (SqlCommand comando = new SqlCommand(consulta, conexao))
            {
                conexao.Open();
                using (SqlDataReader leitura = comando.ExecuteReader())
                {
                    if (leitura.HasRows)
                    {
                        while (leitura.Read())
                        {
                            WriteLine($"ID: {leitura["Id"]} |" +
                                $"Conta: {leitura["NumeroDaConta"]} |" +
                                $"Titular: {leitura["Titular"]} |" +
                                $"Saldo: R${leitura["Saldo"]}");
                        }
                    }
                    else
                    {
                        WriteLine("Nenhuma conta encontrada");
                    }
                }
            }
        }

        //[U]pdate
        public static void ModificarContas(int id, string titular, int numeroConta, decimal saldo)
        {
            string consulta = "UPDATE Contas SET Titular = @titular, NumeroDaConta = @numeroConta, Saldo = @saldo WHERE Id = @id";
            using (SqlConnection conexao = new SqlConnection(conectarCaminho))
            using (SqlCommand comando = new SqlCommand(consulta, conexao))
            {
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@titular", titular);
                comando.Parameters.AddWithValue("@numeroConta", numeroConta);
                comando.Parameters.AddWithValue("@saldo",  saldo);

                conexao.Open();
                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    WriteLine("Conta atualizada com sucesso!");
                }
                else
                {
                    WriteLine("Conta não encontrada!");
                }
            }
        }

        //[D]elete
        public static void DeletarContas(int id)
        {
            string consulta = "DELETE FROM Contas WHERE Id = @id";
            using (SqlConnection conexao = new SqlConnection(conectarCaminho))
            using (SqlCommand comando = new SqlCommand(consulta, conexao))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexao.Open();
                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    WriteLine("Conta deletada com sucesso.");
                }
                else
                {
                    WriteLine("Conta não encontrada.");
                }
            }

        }

    }
}
