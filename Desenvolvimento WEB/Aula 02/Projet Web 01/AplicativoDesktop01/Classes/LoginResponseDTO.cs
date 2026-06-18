using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoDesktop01.Classes
{
    internal class LoginResponseDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int  Regra { get; set; }

    }
}
