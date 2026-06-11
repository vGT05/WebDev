using Microsoft.EntityFrameworkCore;
using Projet_Web_01.Classes.Entidades;
using Projet_Web_01.Dados;

namespace Projet_Web_01.Classes.Serv
{
    public class AuthService
    {
        private readonly AlunoContext dbContext;
        public AuthService(AlunoContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Aluno?> ValidarLogAsync(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                return null;
            }

            var aluno = await dbContext.Alunos.FirstOrDefaultAsync(a => a.Email == email && a.Senha == senha);
            return aluno;
        }
    }
}
