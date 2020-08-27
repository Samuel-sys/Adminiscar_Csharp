using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlServer;

namespace Prototipo_Sistema_Adminiscar
{
    public partial class ClienteCadastraAltera : Form
    {

        string codEndereco, codTell;

        string connect = Connection.Route("ADMINISCAR");

        public ClienteCadastraAltera()
        {
            InitializeComponent();
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
                    ArrayList a =
                        Comand.Select.ArryaListFormat("SELECT * FROM ENDERECO WHERE CEP = '"
                        + mtxtCEP.Text + "' AND NUMERO = " + mtxtNumero.Text + " AND LOGRADOURO = '"
                        + txtLogradouro.Text + "' AND BAIRRO = '" + txtBairro.Text + "' AND CIDADE = '"
                        + txtCidade.Text + "' AND ESTADO = '" + cbxEstado.Text + "'", campos, connect);

                    //se não retornar nem um valor esse espaço sera inexiste e então da erro e vai para o espaço catch
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
                    Comand.Insert(comando, connect);

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
                    string comandoInsert = mtxtTell2.MaskCompleted == true ? "INSERT INTO TELEFONE (TELL1, TELL2) VALUES ('" + mtxtTell1.Text + "', '" + mtxtTell2.Text + "')" : "INSERT INTO TELEFONE (TELL1) VALUES ('" + mtxtTell1.Text + "')" ;

                    Comand.Insert(comandoInsert,connect);

                }
            }
            
            
        }
        private void consultaEndereco(string cod_endereco)
        {
            //busca os campos do endereco do Funcionario
            try
            {
                //Colunas da tabela Endereco
                string[] camposEdereco = new string[] { "LOGRADOURO", "NUMERO", "BAIRRO", "CEP", "CIDADE", "ESTADO" };

                ArrayList c = Comand.Select.ArryaListFormat("SELECT * FROM ENDERECO WHERE COD_ENDERECO = " + cod_endereco, camposEdereco, connect);

                //colocando os dados no sistema fisico
                txtLogradouro.Text = c[0].ToString();
                mtxtNumero.Text = c[1].ToString();
                txtBairro.Text = c[2].ToString();
                mtxtCEP.Text = c[3].ToString();
                txtCidade.Text = c[4].ToString();
                cbxEstado.Text = c[5].ToString();

            }
            catch
            {
                MessageBox.Show("ERRO ao buscar os dados do endereço do FUNCIONÁRIO");

            }
        }
        private void ConsultaCliente(string func)
        {
            string[] campos = new string[] { "NOME_CLIENTE", "CPF_CNPJ", "CNH_CLIENTE", "COD_ENDERECO_FK", "COD_TELL_FK" };

            ArrayList a = Comand.Select.ArryaListFormat("SELECT * FROM CLIENTE WHERE CPF_CNPJ = '" + mtxtIdentificacao.Text + "'", campos,connect);
            try
            {

                txtNome.Text = a[0].ToString();

                if (a[1].ToString().Length >= 18)
                    rbtPJ.Checked = true;
                else
                    rbtPF.Checked = true;

                mtxtIdentificacao.Text = a[1].ToString();
                mtxtCNH.Text = a[2].ToString();

                consultaEndereco(a[3].ToString());
                consultaTelefone(a[4].ToString());
            }
            catch (Exception)
            {
                if(func != "")
                {
                MessageBox.Show("Cliente não encontrado", "ATENÇÃO");
                }
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
                ArrayList b = Comand.Select.ArryaListFormat("SELECT * FROM TELEFONE WHERE COD_TELL = " + cod_tell, camposTelefone,connect);

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



        private void btDesloga_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdtPF_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CPF: ";
            mtxtIdentificacao.Mask = "000,000,000-00";
        }

        private void rbtPJ_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CNPJ: ";
            mtxtIdentificacao.Mask = "00,000,000/0000-00";
        }

        private void btConsultaDeleta_Click(object sender, EventArgs e)
        {
            ClienteConsultaDeleta cli = new ClienteConsultaDeleta();
            cli.Show();
        }

        private void rbtFixo1_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell1.Mask = "(00) 0000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtCELL1_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell1.Mask = "(00) 00000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtFixo2_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell2.Mask = "(00) 0000-0000";
            mtxtTell2.Text = "";
        }

        private void rbtCELL2_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Funcionario
            mtxtTell2.Mask = "(00) 00000-0000";
            mtxtTell2.Text = "";
        }

        private void btPesquisaCliente_Click(object sender, EventArgs e)
        {
            ConsultaCliente("Mostrar MessagerBox");
        }

        private void atualizaDataGrid()
        {
            //COMANDO SqlServer
            dtgCliente.DataSource = Comand.Select.DataTableFormat(
                "SELECT C.NOME_CLIENTE AS NOME, C.CPF_CNPJ , C.CNH_CLIENTE AS CNH, E.LOGRADOURO, E.NUMERO, E.BAIRRO, E.CEP, E.CIDADE, E.ESTADO, T.TELL1 AS 1_TELEFONE, T.TELL2 AS 2_TELEFONE "
                + "FROM CLIENTE AS C "
                + "INNER JOIN ENDERECO AS E on C.COD_ENDERECO_FK = E.COD_ENDERECO "
                + "INNER JOIN TELEFONE AS T ON C.COD_TELL_FK = T.COD_TELL ",connect);
        }

        private void ClienteCadastraAltera_Load(object sender, EventArgs e)
        {
            atualizaDataGrid();
        }

        private void btLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && mtxtIdentificacao.Text != ""
                && mtxtCNH.Text != ""
                && mtxtTell1.MaskCompleted == true
                && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                && txtBairro.Text != "" && txtCidade.Text != ""
                && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true)
            {
                cadastraEndereco();
                cadastraTelefone();
                cadastraCliente();
                atualizaDataGrid();
            }
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (mtxtIdentificacao.Text != "")
            {
                string a = Comand.Select.StringFormat("SELECT * FROM CLIENTE","COD_CLIENTE", connect);

                if (a != "")
                {
                    if (txtNome.Text != "" && mtxtIdentificacao.Text != ""
                    && mtxtCNH.Text != ""
                    && mtxtTell1.MaskCompleted == true
                    && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                    && txtBairro.Text != "" && txtCidade.Text != ""
                    && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true)
                    {
                        cadastraEndereco();
                        cadastraTelefone();
                        string comando = "UPDATE CLIENTE SET NOME_CLIENTE ='" + txtNome.Text + "', CNH_CLIENTE = '" + mtxtCNH.Text + "', COD_TELL_FK =" + codTell + ", COD_ENDERECO_FK =" + codEndereco + " WHERE CPF_CNPJ='" + mtxtIdentificacao.Text + "'";
                        Comand.Update(comando, connect);
                    }
                    else
                        MessageBox.Show("Preencha todos os campos!!");

                }
                else
                {
                    MessageBox.Show("Esse CPF/CNPJ ainda não foi cadastrado", "ATENÇÃO");
                }
            }
            else
                MessageBox.Show("Preencha todos os campos!!");
        }

        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            if(!mtxtCEP.MaskCompleted == true)
            {
                mtxtCEP.Text = "";
                MessageBox.Show("CEP inválido", "ATENÇÃO");
            }
        }

        private void mtxtTell1_Leave(object sender, EventArgs e)
        {
            if(!mtxtTell1.MaskCompleted == true)
            {
                MessageBox.Show("Numero de telefone inválido", "ATENÇÃO");
                mtxtTell1.Text = "";
            }
        }

        private void mtxtTell2_Leave(object sender, EventArgs e)
        {
            if (!mtxtTell2.MaskCompleted == true)
            {
                MessageBox.Show("Número de telefone inválido", "ATENÇÃO");
                mtxtTell2.Text = "";
            }
        }

        private void mtxtCNH_Leave(object sender, EventArgs e)
        {
            if (!mtxtCNH.MaskCompleted == true)
            {
                MessageBox.Show("Número da CNH inválido", "ATENÇÃO");
                mtxtCNH.Text = "";
            }
        }

        private void mtxtIdentificacao_Leave(object sender, EventArgs e)
        {
            ConsultaCliente("");
        }

        private void mtxtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        //responsavel por atualiza o horario/data do relogio no canto inferior direito da janela do programa
        private void timer1_Tick(object sender, EventArgs e)
        {
            //formato de apresentação do horario/data
            lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy   HH:mm:ss");
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            mtxtIdentificacao.Text = "";
            mtxtCNH.Text = "";

            txtLogradouro.Text = "";
            mtxtNumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            mtxtCEP.Text = "";

            mtxtTell1.Text = "";
            mtxtTell2.Text = "";
        }

        private void cadastraCliente()
        {
            string comando = "INSERT INTO CLIENTE(NOME_CLIENTE, CPF_CNPJ, CNH_CLIENTE, COD_TELL_FK, COD_ENDERECO_FK) " +
                "VALUES('" + txtNome.Text + "','" + mtxtIdentificacao.Text + "','" + mtxtCNH.Text + "'," + codTell + "," + codEndereco + ")";
            Comand.Insert(comando, connect);
        }
    }
}
