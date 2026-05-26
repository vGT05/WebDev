using Projet_Web_01.Classes.Enumeracoes;

namespace Projet_Web_01.Classes.Entidades
{
    abstract public class Usuario
    {
        //ID, Nome, Email, Senha, Roles
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoRegra Regra { get; set; }

    }
}
