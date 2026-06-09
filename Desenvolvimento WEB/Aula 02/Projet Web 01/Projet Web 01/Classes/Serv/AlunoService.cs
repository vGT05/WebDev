using Projet_Web_01.Classes.Entidades;
using Projet_Web_01.Dados;

namespace Projet_Web_01.Classes.Serv
{
    public class AlunoService
    {
        //Campo
        private readonly AlunoContext dbContext;
        //Construtor
        public AlunoService(AlunoContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Tarefa
        public async Task<ResultadoCadastro> CadastrarAluno(Aluno aluno)
        {
            try
            {
                //validação básica
                if (string.IsNullOrWhiteSpace(aluno.Nome))
                {
                    return new ResultadoCadastro
                    {
                        Sucesso = false, 
                        Mensagem = "Por favor informe o nome válido de aluno."
                    };
                }
                if (aluno.RA <= 0)
                {
                    return new ResultadoCadastro 
                    {
                        Sucesso = false,
                        Mensagem = "Por favor informe um RA válido."
                    };
                }
                if (aluno.CursoID <= 0) 
                {
                    return new ResultadoCadastro
                    {
                        Sucesso = false,
                        Mensagem = "Por favor selecione um curso"
                    };
                }
                //Definir os status padrão para novos cadastros
                aluno.StatusWIFI = "Inativo";
                aluno.StatusAction = "Aguardando aprovação.";
                
                if (string.IsNullOrWhiteSpace(aluno.Email))
                {
                    aluno.Email = $"ra{aluno.RA}@aluno.local";
                }
                
                if (string.IsNullOrEmpty(aluno.Senha))
                {
                    aluno.Senha = aluno.RA.ToString();   
                }   
                                
                //Adicionar o aluno no banco de dados
                dbContext.Alunos.Add(aluno);
                await dbContext.SaveChangesAsync();

                return new ResultadoCadastro
                {
                    Sucesso = true,
                    Mensagem = "Aluno Cadastrado com sucesso"
                };
            }

            catch (Exception ex)
            {
                return new ResultadoCadastro
                {
                    Sucesso = false,
                    Mensagem = $"Erro ao cadastrar o aluno: {ex.Message}"
                };
            }
        }

    }
}
