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
    public partial class ManutencaoConsultarDeleta : Form
    {

        static string connect = Connection.Route("ADMINISCAR");
        string codCar;

        private void atualizaDataGrid()
        {
            //comando de Sql Server para o banco
            dtgManutencao.DataSource = Comand.Select.DataTableFormat(
                "SELECT M.COD_MANUTENCAO, M.NOME_EMP AS NOME_DA_EMPRESA, M.CNPJ, C.PLACA, M.VALOR_MANUTENCAO AS PREÇO, m.DATA_ENTREGA, m.DATA_DEVOLUCAO, T.TELL1 AS TELEFONE_1, T.TELL2 AS TELEFONE_2, e.LOGRADOURO, e.NUMERO, e.CEP, e.CIDADE, e.ESTADO "
                + "FROM MANUTENCAO AS M "
                + "INNER JOIN CARRO AS C ON M.COD_CAR_FK = C.COD_CAR "
                + "INNER JOIN ENDERECO AS E ON M.COD_ENDERECO_FK = E.COD_ENDERECO "
                + "INNER JOIN TELEFONE AS T ON M.COD_TELL_FK = T.COD_TELL",
                connect);
        }

        public ManutencaoConsultarDeleta()
        {
            InitializeComponent();
        }

        private void btConsulta_Click(object sender, EventArgs e)
        {
            //essa variavel tera que guardar os dados que devem ser pesquisados 
            string pesquisa = "";//(#P1)

            pesquisa += mtxtNumeroPedido.Text != "" ? "COD_MANUTENCAO = '" + mtxtNumeroPedido.Text + "';" : "";

            pesquisa += txtNome.Text != "" ? "NOME_EMP = '" + txtNome + "';" : "";

            pesquisa += cbxDevolucao.Checked == true ? "DATA_DEVOLUCAO = '" + dtpDevolucao.Text + "';" : "";

            pesquisa += cbxEntrega.Checked == true ? "DATA_ENTREGA = '" + dtpEntrega.Text + "';" : "";

            pesquisa += mtxtCNPJ.MaskCompleted == true ? "CNPJ = '" +mtxtCNPJ.Text + "';":"";

            pesquisa += txtPlaca.Text != "" ? "COD_CAR_FK = '" + codCar + "';" : "";//ASCADX
    
            pesquisa += mtxtValor.Text != "R$ 0,00" ? "VALOR_MANUTENCAO = '" + mtxtValor.Text.Replace("R$ ", "").Replace(".", "").Replace(",", ".") + "';" : "";

            string[] campos = pesquisa.Split(';');//os dados são separados e convertidos em variavel array

            string comando = "SELECT M.COD_MANUTENCAO, M.NOME_EMP AS NOME_DA_EMPRESA, M.CNPJ, C.PLACA, M.VALOR_MANUTENCAO AS PREÇO, m.DATA_ENTREGA, m.DATA_DEVOLUCAO, T.TELL1 AS TELEFONE_1, T.TELL2 AS TELEFONE_2, e.LOGRADOURO, e.NUMERO, e.CEP, e.CIDADE, e.ESTADO "
                + "FROM MANUTENCAO AS M "
                + "INNER JOIN CARRO AS C ON M.COD_CAR_FK = C.COD_CAR "
                + "INNER JOIN ENDERECO AS E ON M.COD_ENDERECO_FK = E.COD_ENDERECO "
                + "INNER JOIN TELEFONE AS T ON M.COD_TELL_FK = T.COD_TELL";

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

            MessageBox.Show(comando);
            dtgManutencao.DataSource = SqlServer.Comand.Select.DataTableFormat(comando,connect);
        }

        private void ManutencaoConsultarDeleta_Load(object sender, EventArgs e)
        {
            mtxtValor.Text = String.Format("{0:c}", 0.00);
            atualizaDataGrid();
        }

        private void delete()
        {
            if(mtxtNumeroPedido.Text != "")
            {
                string cod_pedido = SqlServer.Comand.Select.StringFormat("SELECT * FROM MANUTENCAO WHERE COD_MANUTENCAO = " + mtxtNumeroPedido.Text, "COD_MANUTENCAO", connect);

                if(cod_pedido != "")
                {
                    DialogResult r;

                    //campos a serem retornado pela busca
                    string[] campos = new string[] { "CNPJ", "NOME_EMP", "DATA_ENTREGA", "DATA_DEVOLUCAO", "NOME_CAR", "PLACA" };

                    //string de comando SqlServer
                    string comando = "select * from MANUTENCAO INNER JOIN CARRO AS C ON COD_CAR = COD_CAR_FK";

                    ArrayList a = Comand.Select.ArryaListFormat(comando, campos, connect);

                    r = MessageBox.Show("Deletar a ficha de manutenção\n"
                        + "Nome da empresa: " + a[1].ToString() 
                        + "\nCNPJ: " + a[0].ToString() 
                        + "\nData de entrega: " + a[2].ToString() 
                        + "\nData de Devolução: " + a[3].ToString() 
                        + "\nCarro :" + a[4].ToString() 
                        + "\nPlaca: " + a[5].ToString(), "ATENÇÃO", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {

                        comando = "DELETE MANUTENCAO WHERE COD_MANUTENCAO = " + mtxtNumeroPedido.Text;
                        Comand.Delete(comando, connect);
                    }
                }
            }
        }

        private void btDeleta_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {

            codCar = Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "COD_CAR", connect);

            if (codCar == "")
            {
                MessageBox.Show("Placa de carro não cadastrada");
                txtPlaca.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mtxtValor_Leave(object sender, EventArgs e)
        {
            double a = double.Parse(mtxtValor.Text.Replace("R$ ", ""));
            mtxtValor.Text = String.Format("{0:c}", a);
        }

        private void mtxtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValor.Text.Contains(","))
                {
                    e.Handled = true; // Caso exista, aborte 
                }
            }

            //aceita apenas números, tecla backspace.
            else if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void cbxEntrega_CheckedChanged(object sender, EventArgs e)
        {
            dtpEntrega.Visible = cbxEntrega.Checked;
        }

        private void cbxDevolução_CheckedChanged(object sender, EventArgs e)
        {
            dtpDevolucao.Visible = cbxDevolucao.Checked;
        }

        private void mtxtValor_Click(object sender, EventArgs e)
        {
            mtxtValor.Text = mtxtValor.Text.Replace("R$ ", "");
        }

        //responsavel por atualiza o horario/data do relogio no canto inferior direito da janela do programa
        private void timer1_Tick(object sender, EventArgs e)
        {
            //formato de apresentação do horario/data
            lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy   HH:mm:ss");
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            mtxtNumeroPedido.Text = "";
            txtNome.Text = "";
            cbxDevolucao.Checked = false;
            cbxEntrega.Checked = false;
            mtxtCNPJ.Text = "";
            txtPlaca.Text = "";

            mtxtValor.Text = String.Format("{0:c}", 0.00);
        }
    }
}
