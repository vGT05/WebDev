using Projet_Web_01.Classes.Enumeracoes;

namespace Projet_Web_01.Classes.Entidades
{
    public class Admin : Usuario
    {
        public TipoRegra Regra { get; set; } = TipoRegra.Admin;

    }
}
