namespace AplicativoDesktop01
{
    public partial class TelaLogin : Form
    {
        public TelaLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = "Ronaldo";
            string senha = "123456";

            bool comparacao1 = textBox1.Text == usuario;
            bool comparacao2 = textBox2.Text == senha;

            if (comparacao1 & comparacao2)
            {
                MessageBox.Show("Usuário e senha corretos!");
                this.Hide();
                using(var telaAdm = new TelaAdmin())
                {
                    telaAdm.ShowDialog();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.");
            }
        }

        private void TelaLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
