namespace ProjetoAPI01.Classes.DTO
{
    public class LoginResponseDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int Regra { get; set; }

    }
}
