namespace Projet_Web_01.Classes.Entidades
{
    public class Cursos
    {
        private Dictionary<int, string> cursos = new Dictionary<int, string>()
        {
            {1, "Desenvolvedor Back-end" },
            {2, "Programação em JAVA" },
            {3, "Fundamento do Python" }
        };

        public Dictionary<int,string> Curso
        {
            get { return cursos; }
            set { cursos = value; }
        }


    }
}
