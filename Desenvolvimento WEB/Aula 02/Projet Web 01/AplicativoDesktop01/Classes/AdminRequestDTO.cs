using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoDesktop01.Classes
{
    internal class AdminRequestDTO
    {
        public int ID { get; set; }
        public int RA { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string CursoID { get; set; } = string.Empty;
        public int Regra { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }   
}
