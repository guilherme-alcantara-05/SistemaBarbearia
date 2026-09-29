using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SistemaBarbearia
{
    public partial class Form1 : Form
    {
        // Controles da tela
        private TextBox txtNome;
        private TextBox txtTelefone;
        private TextBox txtEmail;
        private Button btnSalvar;
        private Button btnListar;
        private DataGridView dgvClientes;

        public Form1()
        {
            InitializeComponent();
            MontarTela();
        }

        private void MontarTela()
        {
            this.Text = "Sistema Barbearia - Clientes";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ---- Painel de cima ----
            Panel painel = new Panel();
            painel.Dock = DockStyle.Top;
            painel.Height = 160;
            painel.Padding = new Padding(20);

            // Label Nome
            Label lblNome = new Label();
            lblNome.Text = "Nome:";
            lblNome.Location = new System.Drawing.Point(20, 20);
            lblNome.Width = 80;

            txtNome = new TextBox();
            txtNome.Location = new System.Drawing.Point(100, 18);
            txtNome.Width = 300;

            // Label Telefone
            Label lblTel = new Label();
            lblTel.Text = "Telefone:";
            lblTel.Location = new System.Drawing.Point(20, 55);
            lblTel.Width = 80;

            txtTelefone = new TextBox();
            txtTelefone.Location = new System.Drawing.Point(100, 53);
            txtTelefone.Width = 300;

            // Label Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new System.Drawing.Point(20, 90);
            lblEmail.Width = 80;

            txtEmail = new TextBox();
            txtEmail.Location = new System.Drawing.Point(100, 88);
            txtEmail.Width = 300;

            // Botões
            btnSalvar = new Button();
            btnSalvar.Text = "Salvar";
            btnSalvar.Location = new System.Drawing.Point(420, 18);
            btnSalvar.Width = 100;
            btnSalvar.Click += BtnSalvar_Click;

            btnListar = new Button();
            btnListar.Text = "Listar";
            btnListar.Location = new System.Drawing.Point(420, 53);
            btnListar.Width = 100;
            btnListar.Click += BtnListar_Click;

            // Adiciona tudo no painel
            painel.Controls.Add(lblNome);
            painel.Controls.Add(txtNome);
            painel.Controls.Add(lblTel);
            painel.Controls.Add(txtTelefone);
            painel.Controls.Add(lblEmail);
            painel.Controls.Add(txtEmail);
            painel.Controls.Add(btnSalvar);
            painel.Controls.Add(btnListar);

            // ---- DataGridView ----
            dgvClientes = new DataGridView();
            dgvClientes.Dock = DockStyle.Fill;
            dgvClientes.ReadOnly = true;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Adiciona no formulário
            this.Controls.Add(dgvClientes);
            this.Controls.Add(painel);
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite o nome do cliente!");
                return;
            }

            try
            {
                using (var conn = Conexao.Abrir())
                {
                    string sql = "INSERT INTO Clientes (Nome, Telefone, Email) VALUES (@n, @t, @e)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", txtNome.Text);
                        cmd.Parameters.AddWithValue("@t", txtTelefone.Text);
                        cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cliente salvo com sucesso!");
                txtNome.Clear();
                txtTelefone.Clear();
                txtEmail.Clear();
                ListarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void ListarClientes()
        {
            try
            {
                using (var conn = Conexao.Abrir())
                {
                    string sql = "SELECT Id, Nome, Telefone, Email FROM Clientes";
                    using (var adapter = new SqlDataAdapter(sql, conn))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        dgvClientes.DataSource = tabela;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListarClientes();
        }
    }
}