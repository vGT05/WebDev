namespace ProjetoAPI01.Classes.DTO
{
    public class UsuarioAdminDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Regra { get; set; }
        public string StatusWIFI { get; set; } = string.Empty;
        public string StatusAction { get; set; } = string.Empty;
    }
}
