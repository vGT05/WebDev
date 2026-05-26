namespace Projet_Web_01.Classes.Entidades
{
    public class Cursos
    {
        private Dictionary<int, string> cursos = new Dictionary<int, string>()
        {
            {1, "Desenvolvedor Back-end" },
            {2, "Técnico em eletroeletronica" },
            {3, "Mecânico de manutenção" }
        };

        public Dictionary<int,string> Curso
        {
            get { return cursos; }
            set { cursos = value; }
        }


    }
}
