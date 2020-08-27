using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlServer;

namespace Prototipo_Sistema_Adminiscar
{    
    public partial class FuncionarioCadastrarAlterar : Form
    {
        string connect = SqlServer.Connection.Route("ADMINISCAR");//informa o endereço de conexão com o banco de dados
        static string retorno = "";

        //Variaveis estaticas de busca
        static string codTell, codEndereco, codFunc;

        private void consultaLogin(string cod_func)
        {
            //busca os campos do endereco do Login
            try
            {
                //Colunas da tabela Login_Sistema
                string[] campos = new string[] { "EMAIL", "SENHA", "NIVEL_ACESSO" };

                //confirma se existe um 
                ArrayList d = Comand.Select.ArryaListFormat(
                    "SELECT * FROM LOGIN_SISTEMA WHERE COD_FUNC_FK = " + cod_func,
                    campos, connect);

                //colocando os dados no sistema fisico
                txtEmail.Text = d[0].ToString();
                txtSenha.Text = d[1].ToString();
                cbxNivelAcesso.Text = d[2].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("ERRO ao buscar os dados do Login do FUNCIONÁRIO");
            }
        }

        private void consultaEndereco(string cod_endereco)
        {
            //busca os campos do endereco do Funcionario
            try
            {
                //Colunas da tabela Endereco
                string[] camposEdereco = new string[] { "LOGRADOURO", "NUMERO", "BAIRRO", "CEP", "CIDADE", "ESTADO" };

                ArrayList c = Comand.Select.ArryaListFormat(
                    "SELECT * FROM ENDERECO WHERE COD_ENDERECO = " + cod_endereco 
                    , camposEdereco, connect);

                //colocando os dados no sistema fisico
                txtLogradouro.Text = c[0].ToString();
                mtxtNumero.Text = c[1].ToString();
                txtBairro.Text = c[2].ToString();
                mtxtCEP.Text = c[3].ToString();
                txtCidade.Text = c[4].ToString();
                cbxEstado.Text = c[5].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("ERRO ao buscar os dados do endereço do FUNCIONÁRIO");
                    throw;

            }
        }

        private void consultaTelefone(string cod_tell)
        {
            //busca os campos do telefone do Funcionario
            try
            {
                //Colunas da tabela Telefone
                string[] camposTelefone = new string[] { "TELL1", "TELL2" };

                //comando SQL Select Registrando os dados na ArrayList "b" (telefone)
                ArrayList b = Comand.Select.ArryaListFormat(
                    "SELECT * FROM TELEFONE WHERE COD_TELL = " + cod_tell
                    , camposTelefone, connect);

                //ira adapitar conforme a quantidade de caracteres o MaskBox
                if (b[0].ToString().Length == 14)
                    rbtFixo1.Checked = true;
                else
                    rbtCELL1.Checked = true;

                //colocando os dados no sistema fisico
                mtxtTell1.Text = b[0].ToString();
                try
                {

                    //ira adapitar conforme a quantidade de caracteres o MaskBox
                    if (b[1].ToString().Length == 14)
                        rbtFixo2.Checked = true;
                    else
                        rbtCELL2.Checked = true;

                    //colocando os dados no sistema fisico
                    mtxtTell2.Text = b[1].ToString();
                }
                catch { }//telefone 2 e opcional caso o campo esteja vazio da erro 

            }
            catch (Exception)
            {
                //Caso os telefones não possua registro
                MessageBox.Show("ERRO ao buscar os dados do telefone do FUNCIONÁRIO");
            }
        }

        private string cadastraLogin()
        {
            //Colunas da tabela Login_Sistema que teram que retornar os valores para o programa
            string[] campos = new string[] { "EMAIL" };

            try
            {
                ArrayList a = Comand.Select.ArryaListFormat(
                    "SELECT * FROM LOGIN_SISTEMA WHERE EMAIL = '" + txtEmail + "'"
                    , campos, connect);
                a[0].ToString();//se esse espaço da ArrayList retorna erro
                MessageBox.Show("EMAIL DE LOGIN JÁ CADASTRADO");
                return "ERRO";
            }
            catch (Exception)
            {
                for (int i = 0; i < 1; i++)
                {

                    try
                    {
                        ArrayList a = Comand.Select.ArryaListFormat(
                            "SELECT * FROM LOGIN_SISTEMA WHERE EMAIL = '" + txtEmail + "'"
                            , campos, connect);
                        a[0].ToString();
                        retorno = "OK";
                    }
                    catch (Exception)
                    {
                        retorno = ConfirmaSenha();
                    }
                }
            }
            return retorno;
        }

        string ConfirmaSenha()
        {
            string a;
            if (txtSenha.Text == txtConfirmaSenha.Text)
            {
                string email = txtEmail.Text;
                string senha = txtConfirmaSenha.Text;
                string nivelAcesso = cbxNivelAcesso.Text;

                string comando = "INSERT INTO LOGIN_SISTEMA(EMAIL, SENHA, NIVEL_ACESSO, COD_FUNC_FK) " +
                    "VALUES('" + email + "','" + senha + "'," + nivelAcesso + "," + codFunc + ")";
                Comand.Insert(comando, connect);
                MessageBox.Show("Cadastro de Login Realizado com sucesso");
                a = "OK";
            }
            else
            {
                MessageBox.Show("SENHAS INCOMPATÍVEIS");
                txtSenha.Text = "";
                txtConfirmaSenha.Text = "";
                a = "ERRO";
            }
            return a;
        }

        private void cadastraEndereco()
        {
            string[] campos = new string[] { "COD_ENDERECO" };

            //continua ate retornar o codigo do Endereço
            for (int i = 0; i < 2; i++)
            {
                //tenta pegar o codigo do endereco
                try
                {
                    ArrayList a = Comand.Select.ArryaListFormat(
                        "SELECT * FROM ENDERECO WHERE CEP = '" + mtxtCEP.Text 
                        + "' AND NUMERO = " + mtxtNumero.Text 
                        + " AND LOGRADOURO = '" + txtLogradouro.Text 
                        + "' AND BAIRRO = '" + txtBairro.Text 
                        + "' AND CIDADE = '" + txtCidade.Text 
                        + "' AND ESTADO = '" + cbxEstado.Text + "'"
                        , campos, connect);

                    codEndereco = a[0].ToString();

                }
                catch (Exception)
                {   //pegando os registros
                    string logradouro = txtLogradouro.Text;
                    string numero = mtxtNumero.Text;
                    string bairro = txtBairro.Text;
                    string cidade = txtCidade.Text;
                    string estado = cbxEstado.Text;
                    string CEP = mtxtCEP.Text;
                    //criando a string de comando
                    string comando = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, BAIRRO, CEP, CIDADE, ESTADO) VALUES ("
                    + "'" + logradouro + "'," + numero + ",'" + bairro + "','" + CEP + "','" + cidade + "','" + estado + "')";

                    //inserção dos dados
                    Comand.Insert(comando,connect);

                }
            }
        }

        private void cadastraTelefone()
        {

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    string[] campos = new string[] { "COD_TELL" };

                    //o campo do telefone 2 e opcional então se ele quiser pode cadastra apenas 1 telefone
                    string comandoSelect = mtxtTell2.MaskCompleted == true ? "SELECT * FROM TELEFONE WHERE TELL1 = '" + mtxtTell1.Text + "' AND TELL2 = '" + mtxtTell2.Text + "'" : "SELECT * FROM TELEFONE WHERE TELL1 = '" + mtxtTell1.Text + "' AND TELL2 is null";

                    ArrayList a = Comand.Select.ArryaListFormat(comandoSelect, campos, connect);

                    codTell = a[0].ToString();
                }
                catch (Exception)
                {
                    //o campo do telefone 2 e opcional então se ele quiser pode cadastra apenas 1 telefone
                    string comandoInsert = mtxtTell2.MaskCompleted == true ? "INSERT INTO TELEFONE (TELL1, TELL2) VALUES ('" + mtxtTell1.Text + "', '" + mtxtTell2.Text + "')" : "INSERT INTO TELEFONE (TELL1) VALUES ('" + mtxtTell1.Text + "')";

                    Comand.Insert(comandoInsert, connect);

                }
            }
        }

        public FuncionarioCadastrarAlterar()
        {
            InitializeComponent();
        }

        void atualizaDataGrid()
        {
            dtgConsultaFuncionario.DataSource = Comand.Select.DataTableFormat(
                    "SELECT F.NOME_FUNC AS NOME, F.CPF_FUNC AS CPF, F.CNH_FUNC AS CNH, LOGRADOURO, E.NUMERO, E.CEP, E.BAIRRO, E.CIDADE, E.ESTADO, T.TELL1 AS TELEFONE, T.TELL2 AS TELEFONE "
                    + " FROM FUNCIONARIO AS F"
                    + " INNER JOIN ENDERECO AS E ON F.COD_ENDERECO_FK = E.COD_ENDERECO"
                    + " INNER JOIN TELEFONE AS T ON F.COD_TELL_FK = T.COD_TELL"
                    , connect);
        }

        private void BtVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormFuncionario_Load(object sender, EventArgs e)
        {
            //mostrando os funcionarios cadastrados
            atualizaDataGrid();

        }

        private void btPesquisaCPF_Click(object sender, EventArgs e)
        {

            //busca os dados do Funcionario
            try
            {
                //Colunas do funcionario
                string[] campos = new string[] { "NOME_FUNC", "CPF_FUNC", "CNH_FUNC", "COD_TELL_FK", "COD_ENDERECO_FK", "COD_FUNC" };

                //comando SQL Select Registrando os dados na ArrayList "a" (funcionario)
                ArrayList a = Comand.Select.ArryaListFormat(
                    "SELECT * FROM FUNCIONARIO WHERE CPF_FUNC = '" + mtxtCPF.Text + "'"
                    , campos, connect);

                txtNome.Text = a[0].ToString();
                mtxtCPF.Text = a[1].ToString();
                mtxtCNH.Text = a[2].ToString();

                //metodos para preencher os campos (Telefone, Endereco, Login)
                consultaTelefone(a[3].ToString());
                consultaEndereco(a[4].ToString());
                consultaLogin(a[5].ToString());


            }
            catch (Exception)
            {
                //informa que não a retorno dos dados do funcionario
                MessageBox.Show("CPF não cadastrado", "ATEÇÃO");
            }
        }

        private void rbtFixoTELL1_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell1.Mask = "(00) 0000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtFixoTELL2_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell2.Mask = "(00) 0000-0000";
            mtxtTell2.Text = "";
        }

        private void rbtFixoCELL1_CheckedChanged(object sender, EventArgs e)
        {
        //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell1.Mask = "(00) 00000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtFixoCELL2_CheckedChanged(object sender, EventArgs e)
        {
        //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell2.Mask = "(00) 00000-0000";
            mtxtTell2.Text = "";
        }

        private void btPesquisaEmail_Click(object sender, EventArgs e)
        {

            string[] campos = new string[] { "SENHA", "NIVEL_ACESSO", "COD_FUNC_FK"};

            ArrayList a = Comand.Select.ArryaListFormat(
                "SELECT * FROM LOGIN_SISTEMA WHERE EMAIL = '" + txtEmail.Text + "'"
                , campos, connect);
            try
            {
                //Preenche os campos do sistema fisico
                txtSenha.Text = a[0].ToString();
                cbxNivelAcesso.Text = a[1].ToString();

                //Colunas do funcionario
                string[] campo_funcionario = new string[] { "COD_ENDERECO_FK", "COD_TELL_FK", "NOME_FUNC", "CPF_FUNC", "CNH_FUNC" };

                //comando SQL Select Registrando os dados na ArrayList "a" (funcionario)
                ArrayList b = Comand.Select.ArryaListFormat(
                    "SELECT * FROM FUNCIONARIO WHERE COD_FUNC = " + a[2]
                    , campo_funcionario, connect);


                //metodos para preencher os campos (Telefone, Endereco)
                consultaEndereco(b[0].ToString());
                consultaTelefone(b[1].ToString());

                //Preenche os campos do sistema fisico
                txtNome.Text = b[2].ToString();
                mtxtCPF.Text = b[3].ToString();
                mtxtCNH.Text = b[4].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Login não encontrado");
            }
        }

        private void btCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && mtxtCPF.Text != ""
                && mtxtCNH.Text != ""
                && mtxtTell1.MaskCompleted == true
                && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                && txtBairro.Text != "" && txtCidade.Text != ""
                && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true)
            {
                string a = Comand.Select.StringFormat(
                    "SELECT * FROM FUNCIONARIO"
                    , "COD_FUNC",
                    connect);

                cadastraEndereco();
                cadastraTelefone();
                cadastrarFunc();
                cadastraLogin();
                atualizaDataGrid();
            }
            else
                MessageBox.Show("Preencha todos os campos!", "ATENÇÃO");
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {

            if (txtNome.Text != "" && mtxtCPF.Text != ""
                && mtxtCNH.Text != ""
                && mtxtTell1.MaskCompleted == true
                && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                && txtBairro.Text != "" && txtCidade.Text != ""
                && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true)
            {
                cadastraEndereco();
                cadastraTelefone();
                cadastrarFunc();
                cadastraLogin();
                atualizaDataGrid();
            }
            else
                MessageBox.Show("Preencha todos os campos!", "ATENÇÃO");

        }

        private void mtxtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //formato de apresentação do horario/data
            lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy   HH:mm:ss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            MenuPrincipal b = new MenuPrincipal();
            b.Close();
            this.Close();
        }

        private void cadastrarFunc()
        {
            string comando = "INSERT INTO FUNCIONARIO(NOME_FUNC, CPF_FUNC, CNH_FUNC, COD_TELL_FK, COD_ENDERECO_FK)" +
                "VALUES('" + txtNome.Text + "','" + mtxtCPF.Text + "','" + mtxtCNH.Text + "'," + codTell + "," + codEndereco +")";

            Comand.Insert(comando, connect);

            string cod_func = Comand.Select.StringFormat(
                "SELECT * FROM FUNCIONARIO WHERE CPF_FUNC ='" + mtxtCPF.Text + "'"
                , "COD_FUNC", connect);
            codFunc = cod_func;
        }

    }
}
