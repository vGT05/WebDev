using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.IO;
using System.Text.Json;
using ProjetoAPI01.Classes.DTO;


namespace AplicativoDesktop01
{
    public partial class TelaAdmin : Form
    {
        // HttpClient para consumir a API (BaseAddress lido de appsettings.json)
        private HttpClient http;

        private List<UsuarioAdminDTO> usuarios = new();
        private UsuarioAdminDTO? selecionado;

        public TelaAdmin()
        {
            InitializeComponent();
            // Default base URL (matches ProjetoAPI01 launchSettings)
            var baseUrl = "http://localhost:5143/";

            try
            {
                // Procura pelo arquivo ProjetoAPI01/ProjetoAPI01.http subindo diretórios a partir do bin
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                for (int depth = 0; depth < 10; depth++)
                {
                    var candidate = Path.Combine(dir, "ProjetoAPI01", "ProjetoAPI01.http");
                    if (!File.Exists(candidate)) candidate = Path.Combine(dir, "ProjetoAPI01.http");
                    if (File.Exists(candidate))
                    {
                        var lines = File.ReadAllLines(candidate);
                        foreach (var line in lines)
                        {
                            var t = line.Trim();
                            if (t.StartsWith("@ProjetoAPI01_HostAddress", StringComparison.OrdinalIgnoreCase))
                            {
                                var parts = t.Split('=', 2);
                                if (parts.Length == 2)
                                {
                                    var val = parts[1].Trim();
                                    // remove aspas se existirem
                                    if ((val.StartsWith("\"") && val.EndsWith("\"")) || (val.StartsWith("'") && val.EndsWith("'")))
                                        val = val.Substring(1, val.Length - 2);
                                    if (!val.EndsWith("/")) val += "/";
                                    baseUrl = val;
                                }
                                break;
                            }
                        }
                        break;
                    }
                    var parent = Directory.GetParent(dir);
                    if (parent == null) break;
                    dir = parent.FullName;
                }
            }
            catch
            {
                // fallback para padrão
            }

            http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        private async void TelaAdmin_Load(object sender, EventArgs e)
        {
            await CarregarUsuariosAsync();
            MontarTabela();
        }

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            button1 = new Button();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.GradientActiveCaption;
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.None;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            // Ajusta número de colunas para os campos exibidos: ID, RA, Nome, WIFI, Ação, Botão
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F)); // ID
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F)); // RA
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Nome
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F)); // WIFI
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F)); // Ação
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F)); // Botão
            tableLayoutPanel1.Location = new Point(164, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(681, 264);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.TabStop = true;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(3, 230);
            button1.Name = "button1";
            button1.Size = new Size(155, 46);
            button1.TabIndex = 0;
            button1.Text = "Aprovar";
            button1.Click += button1_Click;
            // 
            // TelaAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(857, 290);
            Controls.Add(button1);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "TelaAdmin";
            Load += TelaAdmin_Load;
            ResumeLayout(false);

        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private async Task CarregarUsuariosAsync()
        {
            usuarios.Clear();
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var lista = await http.GetFromJsonAsync<List<UsuarioAdminDTO>>("api/usuarios/admin", cts.Token);
                if (lista != null)
                {
                    usuarios.AddRange(lista);
                }
            }
            catch (Exception ex)
            {
                // Simplified error handling: no fallbacks, report the error.
                MessageBox.Show("Erro ao carregar usuários da API: " + ex.Message);
            }
        }

        private void MontarTabela()
        {
            tableLayoutPanel1.Controls.Clear();
            // cabeçalho
            tableLayoutPanel1.RowCount = usuarios.Count + 1;
            tableLayoutPanel1.Controls.Add(new Label { Text = "ID", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "RA", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 1, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "Nome", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 2, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "WIFI", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 3, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "Ação", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 4, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "", AutoSize = true }, 5, 0);

            for (int i = 0; i < usuarios.Count; i++)
            {
                var u = usuarios[i];
                int row = i + 1;
                tableLayoutPanel1.Controls.Add(new Label { Text = u.Id.ToString(), AutoSize = true }, 0, row);
                tableLayoutPanel1.Controls.Add(new Label { Text = u.RA.ToString(), AutoSize = true }, 1, row);
                tableLayoutPanel1.Controls.Add(new Label { Text = u.Nome, AutoSize = true }, 2, row);
                tableLayoutPanel1.Controls.Add(new Label { Text = u.StatusWIFI, AutoSize = true }, 3, row);
                tableLayoutPanel1.Controls.Add(new Label { Text = u.StatusAction, AutoSize = true }, 4, row);

                var btn = new Button { Text = "Selecionar", Tag = u };
                btn.Click += (s, e) =>
                {
                    selecionado = (UsuarioAdminDTO)((Button)s).Tag;
                    MessageBox.Show($"Selecionado: {selecionado.Nome}");
                };
                tableLayoutPanel1.Controls.Add(btn, 5, row);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um usuário antes.");
                return;
            }

            try
            {
                var dto = new AdminUpdateDTO { StatusWIFI = "Ativado", StatusAction = "aprovado" };
                // Mantém PUT via API simples; sem alterações funcionais.
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var resp = await http.PutAsJsonAsync($"api/usuarios/admin/{selecionado.Id}", dto, cts.Token);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Acesso aprovado com sucesso.");
                    await CarregarUsuariosAsync();
                    MontarTabela();
                }
                else
                {
                    MessageBox.Show($"Falha ao aprovar: {resp.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao aprovar via API: " + ex.Message);
            }
        }
        private IContainer components;
        public TableLayoutPanel tableLayoutPanel1;
        private Button button1;

        // Usando DTOs do projeto API (ProjetoAPI01.Classes.DTO)







    }
}
