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
    public partial class ClienteConsultaDeleta : Form
    {

        static string connect = Connection.Route("ADMINISCAR");

        private void consultaEndereco(string cod_endereco)
        {
            //busca os campos do endereco do Cliente
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
            catch (Exception)
            {
                MessageBox.Show("ERRO ao buscar os dados do endereço do CLIENTE");
                throw;

            }
        }

        private void consultaTelefone(string cod_tell)
        {
            //busca os campos do telefone do Cliente
            try
            {
                //Colunas da tabela Telefone
                string[] camposTelefone = new string[] { "TELL1", "TELL2" };

                //comando SQL Select Registrando os dados na ArrayList "b" (telefone)
                ArrayList b = Comand.Select.ArryaListFormat("SELECT * FROM TELEFONE WHERE COD_TELL = " + cod_tell, camposTelefone, connect);

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
                MessageBox.Show("ERRO ao buscar os dados do telefone do CLIENTE");
            }
        }

        public ClienteConsultaDeleta()
        {
            InitializeComponent();
        }

        private void ClienteConsultaDeleta_Load(object sender, EventArgs e)
        {

            dtgCliente.DataSource = Comand.Select.DataTableFormat(
                "SELECT C.NOME_CLIENTE AS NOME, C.CPF_CNPJ , C.CNH_CLIENTE AS CNH, E.LOGRADOURO,"
                + " E.NUMERO, E.CEP, E.CIDADE, E.ESTADO, E.BAIRRO, T.TELL1 AS TELEFONE, "
                + " T.TELL2 AS TELEFONE FROM CLIENTE AS C "
                + "INNER JOIN ENDERECO AS E on C.COD_ENDERECO_FK = E.COD_ENDERECO "
                + "INNER JOIN TELEFONE AS T ON C.COD_TELL_FK = T.COD_TELL ",connect);
        }

        private void btConsultaDeleta_Click(object sender, EventArgs e)
        {
            ClienteConsultaDeleta cli = new ClienteConsultaDeleta();
            cli.Show();
        }

        private void BtVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btPesquisaCliente_Click(object sender, EventArgs e)
        {
            //Colunas da tabela cliente
            string[] campos = new string[] { "NOME_CLIENTE", "CPF_CNPJ", "CNH_CLIENTE", "COD_ENDERECO_FK", "COD_TELL_FK" };

            ArrayList a = SqlServer.Comand.Select.ArryaListFormat("SELECT * FROM CLIENTE WHERE CPF_CNPJ = '" + mtxtIdentificacao.Text + "'", campos, connect);
            try
            {

            //inserindo as informações no sistema fisico
            txtNome.Text = a[0].ToString();

            //conforme o cliente o maskBox Identetificação (cpf/cnpj) e alterado 
            if (a[1].ToString().Length >= 18)
                rbtPJ.Checked = true;
            else
                rbtPF.Checked = true;

            //inserindo as informações no sistema fisico
            mtxtIdentificacao.Text = a[1].ToString();
            mtxtCNH.Text = a[2].ToString();

            //metodos que inserem os dados (telefone e entedereço) ao sistema fisico 
            consultaEndereco(a[3].ToString());
            consultaTelefone(a[4].ToString());
            }
            catch (Exception)
            {
                //informa ao messageBox se e um CPF que foi pesquisado ou um CNPJ
                string msg = rbtPF.Checked == true ? "CPF" : "CNPJ";

                MessageBox.Show( msg + " não encontrado", "ATENÇÃO");
            }
        }

        private void rbtPF_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CPF: ";
            mtxtIdentificacao.Mask = "000,000,000-00";
        }

        private void rbtPJ_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CNPJ: ";
            mtxtIdentificacao.Mask = "00.000.000/0000-00";
        }

        private void rbtFixo1_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Cliente
            mtxtTell1.Mask = "(00) 0000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtCELL1_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Cliente
            mtxtTell1.Mask = "(00) 00000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtFixo2_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Cliente
            mtxtTell2.Mask = "(00) 0000-0000";
            mtxtTell2.Text = "";
        }

        private void rbtCELL2_CheckedChanged(object sender, EventArgs e)
        {
            //adaptando a mascara conforme o tipo de numero do Cliente
            mtxtTell2.Mask = "(00) 00000-0000";
            mtxtTell2.Text = "";
        }

        private void btConsulta_Click(object sender, EventArgs e)
        {
            //essa variavel tera que guardar os dados que devem ser pesquisados 
            string pesquisa = "";//(#P1)

            pesquisa += txtNome.Text != "" ? " C.NOME_CLIENTE like '%" + txtNome.Text + "%';" : "" ;//campo do nome do cliente

            pesquisa += mtxtIdentificacao.MaskCompleted ? " C.CPF_CNPJ = '" + mtxtIdentificacao.Text + "';" : "" ;//campo do CPF/CNPJ (intendificação)

            pesquisa += mtxtCNH.MaskCompleted ? " C.CNH_CLIENTE = '" + mtxtCNH.Text + "';" : "";//campo do CNH

            pesquisa += txtLogradouro.Text != "" ? " E.LOGRADOURO LIKE '%" + txtLogradouro.Text + "%';" : "";//campo do Logradouro

            pesquisa += mtxtNumero.Text != "" ? " E.NUMERO = " + mtxtNumero.Text + ";": "";//campo do Numero DA CASA

            pesquisa += txtBairro.Text != "" ? " E.BAIRRO LIKE '%" + txtBairro.Text + "%';" : "";//campo do Bairro

            pesquisa += txtCidade.Text != "" ? " E.CIDADE LIKE '%" + txtCidade.Text + "%';" : "";//campo da Cidade

            pesquisa += cbxEstado.Text != "" ? " E.ESTADO = '" + cbxEstado.Text + "';" : "";//campo do UF (ESTADO)

            pesquisa += mtxtCEP.MaskCompleted == true ? " E.CEP = '" + mtxtCEP.Text + "';" : "";//campo do CEP

            pesquisa += mtxtTell1.MaskCompleted == true ? " T.TELL1 = '" + mtxtTell1.Text + "';" : "";//campo do telegone (1°)

            pesquisa += mtxtTell2.MaskCompleted == true ? " T.TELL2 = '" + mtxtTell2.Text + "';" : "";//campo do telegone (1°)

            string[] campos = pesquisa.Split(';');//os dados são separados e convertidos em variavel array

            //string comando = "SELECT * FROM CARRO";
            string comando = "SELECT C.NOME_CLIENTE AS NOME, C.CPF_CNPJ , C.CNH_CLIENTE AS CNH, E.LOGRADOURO, E.NUMERO, E.CEP, E.CIDADE, E.ESTADO, E.BAIRRO, T.TELL1 AS TELEFONE, T.TELL2 AS TELEFONE FROM CLIENTE AS C INNER JOIN ENDERECO AS E on C.COD_ENDERECO_FK = E.COD_ENDERECO INNER JOIN TELEFONE AS T ON C.COD_TELL_FK = T.COD_TELL "; // variavel de comando em SQLServer


            //esse loop serve para encrementar o comando em SQL enquanto tiver parametos para serem adicionado ele adicona
            for (int i = 0; i < campos.Length; i++)
            {
                //caso não tenha parametros na variavel pesquisa #P1 não se coloca where assim mostrando toda a tabela sem filtro
                if (i == 0 && campos.Length > 1)
                    comando += " WHERE ";

                //se tiver mais de 1 parametro se coloca AND entre os parametros
                if (i > 0 && i != campos.Length - 1)
                    comando += " AND ";

                //adicionado parametro ao comando SQLServer
                comando += campos[i];
            }



            //se poem os dados da tabela no DataGrid
            dtgCliente.DataSource = SqlServer.Comand.Select.DataTableFormat(comando,connect);
            

            MessageBox.Show("Pesquisa realizada com sucesso");
        }

        private void mtxtIdentificacao_Leave(object sender, EventArgs e)
        {
            if (!mtxtIdentificacao.MaskCompleted)
            {
                MessageBox.Show("Preencha todo o campo para fazer a pesquisa");
                mtxtIdentificacao.Text = "";
            }
        }

        private void mtxtCNH_Leave(object sender, EventArgs e)
        {
            if (!mtxtCNH.MaskCompleted)
            {
                MessageBox.Show("Preencha todo o campo para fazer a pesquisa");
                mtxtCNH.Text = "";
            }
        }

        private void mtxtTell1_Leave(object sender, EventArgs e)
        {
            if (!mtxtTell1.MaskCompleted)
            {
                MessageBox.Show("Preencha todo o campo para fazer a pesquisa");
                mtxtTell1.Text = "";
            }
        }

        private void mtxtTell2_Leave(object sender, EventArgs e)
        {
            if (!mtxtTell2.MaskCompleted)
            {
                MessageBox.Show("Preencha todo o campo para fazer a pesquisa");
                mtxtTell2.Text = "";
            }
        }

        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            if (!mtxtCEP.MaskCompleted)
            {
                MessageBox.Show("Preencha todo o campo para fazer a pesquisa");
                mtxtCEP.Text = "";
            }
        }

        private void deletar()
        {
            string comando ="DELETE CLIENTE WHERE CPF_CNPJ = '" + mtxtIdentificacao.Text + "'";

            if (mtxtIdentificacao.MaskCompleted == true)
                SqlServer.Comand.Delete(comando, connect);

            else
                MessageBox.Show("PREENCHA O CAMPO DE IDENTIFICAÇÃO CPF/CNPJ PARA EFETUAR A FUNÇÃO DELETAR");

            dtgCliente.DataSource = SqlServer.Comand.Select.DataTableFormat("SELECT C.NOME_CLIENTE AS NOME, C.CPF_CNPJ , C.CNH_CLIENTE AS CNH, E.LOGRADOURO, E.NUMERO, E.CEP, E.CIDADE, E.ESTADO, E.BAIRRO, T.TELL1 AS TELEFONE, T.TELL2 AS TELEFONE FROM CLIENTE AS C INNER JOIN ENDERECO AS E on C.COD_ENDERECO_FK = E.COD_ENDERECO INNER JOIN TELEFONE AS T ON C.COD_TELL_FK = T.COD_TELL ",connect);
        }

        private void btDeletar_Click(object sender, EventArgs e)
        {
            deletar();
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
    }
}
