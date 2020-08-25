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
    public partial class LocacaoConsultaDeleta : Form
    {
        string connect = Connection.Route("ADMINISCAR");

        public LocacaoConsultaDeleta()
        {
            InitializeComponent();
        }

        void imgCarro()
        {
            switch (cbxNomeCar.Text)
            {
                case "Onix":
                    pbxCarro.Load(@"Carro\Onix.jpg");
                    break;

                case "Agile":
                    pbxCarro.Load(@"Carro\Agile.jpg");
                    break;

                case "Fiat mobi":
                    pbxCarro.Load(@"Carro\Fiat_mobi.jpg");
                    break;

                case "Up":
                    pbxCarro.Load(@"Carro\Up.jpg");
                    break;

                case "HB20":
                    pbxCarro.Load(@"Carro\HB20.jpg");
                    break;

                case "CELTA":
                    pbxCarro.Load(@"Carro\CELTA.jpg");
                    break;
                //quebra
                case "Golf":
                    pbxCarro.Load(@"Carro\Golf.jpg");
                    break;

                case "renegade":
                    pbxCarro.Load(@"Carro\renegade.jpg");
                    break;

                case "EcoSport":
                    pbxCarro.Load(@"Carro\EcoSport.jpg");
                    break;

                case "Argo":
                    pbxCarro.Load(@"Carro\Argo.jpg");
                    break;

                case "Cronos":
                    pbxCarro.Load(@"Carro\Cronos.jpg");
                    break;

                case "Linea":
                    pbxCarro.Load(@"Carro\Linea.jpg");
                    break;
                //quebra
                case "Jetta":
                    pbxCarro.Load(@"Carro\Jetta.jpg");
                    break;

                case "Golf Tsi":
                    pbxCarro.Load(@"Carro\Golf_Tsi.jpg");
                    break;

                case "Bmw m3":
                    pbxCarro.Load(@"Carro\Bmw_m3.jpg");
                    break;

                case "Ford fusion":
                    pbxCarro.Load(@"Carro\Ford_fusion.jpg");
                    break;

                case "Volvo s60":
                    pbxCarro.Load(@"Carro\Volvo_s60.jpg");
                    break;

                case "Land rover evoque":
                    pbxCarro.Load(@"Carro\Land_rover_evoque.jpg");
                    break;
                //quebra
                case "Camaro zl1":
                    pbxCarro.Load(@"Carro\Camaro_zl1.jpg");
                    break;

                case "Jeep compass":
                    pbxCarro.Load(@"Carro\Jeep_compass.jpg");
                    break;

                case "Mercedes c63":
                    pbxCarro.Load(@"Carro\Mercedes_c63.jpg");
                    break;

                case "Audi rs4":
                    pbxCarro.Load(@"Carro\Audi_rs4.jpg");
                    break;

                default:
                    pbxCarro.Load(@"Carro\default.png");
                    break;
            }
        }

        private void ComboBoxCarro()
        {
            switch (cbxCategoria.Text)
            {
                case "Entrada":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Onix");
                    cbxNomeCar.Items.Add("Agile");
                    cbxNomeCar.Items.Add("Fiat mobi");
                    cbxNomeCar.Items.Add("Up");
                    cbxNomeCar.Items.Add("HB20");
                    cbxNomeCar.Items.Add("CELTA");
                    lblSOM.Text = "N";
                    lblSOMBT.Text = "N";
                    lblGPS.Text = "N";
                    break;
                case "Intermediario":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Golf");
                    cbxNomeCar.Items.Add("renegade");
                    cbxNomeCar.Items.Add("EcoSport");
                    cbxNomeCar.Items.Add("Argo");
                    cbxNomeCar.Items.Add("Cronos");
                    cbxNomeCar.Items.Add("Linea");
                    lblSOM.Text = "S";
                    lblSOMBT.Text = "N";
                    lblGPS.Text = "N";
                    break;
                case "Esportivo":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Jetta");
                    cbxNomeCar.Items.Add("Golf Tsi");
                    cbxNomeCar.Items.Add("Bmw m3");
                    cbxNomeCar.Items.Add("Ford fusion");
                    cbxNomeCar.Items.Add("Volvo s60");
                    cbxNomeCar.Items.Add("Audi rs4");
                    lblSOM.Text = "S";
                    lblSOMBT.Text = "N";
                    lblGPS.Text = "S";
                    break;
                case "Luxo":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Land rover evoque");
                    cbxNomeCar.Items.Add("Jeep compass");
                    cbxNomeCar.Items.Add("Camaro zl1");
                    cbxNomeCar.Items.Add("Mercedes c63");
                    lblSOM.Text = "S";
                    lblSOMBT.Text = "S";
                    lblGPS.Text = "S";
                    break;
            }
            txtPlaca.Text = "";
        }

        private void LocacaoConsultaDeleta_Load(object sender, EventArgs e)
        {
            //Fazendo com que as combobox sejam não preenchiveis
            cbxCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            string comando = "SELECT P.COD_PEDIDO, CLI.NOME_CLIENTE, CLI.CPF_CNPJ, C.NOME_CAR AS NOME_CARRO, C.PLACA, P.VALOR, P.DATA_RETIRADA, P.DATA_DEVOLUCAO, P.TIPO_PEDIDO "
            + " FROM PEDIDO AS P"
            + " INNER JOIN CARRO AS C ON COD_CAR = COD_CAR_FK"
            + " INNER JOIN CLIENTE AS CLI ON COD_CLIENTE = COD_CLIENTE_FK";
            dtgLocacao.DataSource = Comand.Select.DataTableFormat(comando,connect);
            imgCarro();
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxCarro();
        }

        private void rbtPJ_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CNPJ: ";
            mtxtIdentificacao.Mask = "00,000,000/0000-00";
        }

        private void rbtPF_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CPF: ";
            mtxtIdentificacao.Mask = "000,000,000-00";
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            mtxtIdentificacao.Clear();
            
        }

        private void cbxNomeCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgCarro();
            if (cbxNomeCar.Text != "")
            {
                //nome da coluna onde esta arquivado o dado da placa do carro
                string[] campos = new string[] { "PLACA" };

                //string de comando SqlServer
                string comando = "SELECT * FROM CARRO WHERE AND CATEGORIA = '" + cbxCategoria.Text + "' AND NOME_CAR = '" + cbxNomeCar.Text + "'";

                //comando SQP Server
                ArrayList a = Comand.Select.ArryaListFormat(comando,campos,connect);

                //tenta ver se tem um carro com nome e categoria vago se tiver apresenta ao txt
                try
                {
                    //colocando o dado da placa do carro disponivel
                    txtPlaca.Text = a[0].ToString();
                }
                catch (Exception)
                {
                    //informá que não a um carro com essas especificações livre
                    MessageBox.Show("Não temos mais o carro " + cbxNomeCar.Text, "ATENÇÃO");
                    ComboBoxCarro();//metodo para limpar a area de texto do nome do carro carro
                }
            }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            string[] campos = new string[] { "CATEGORIA", "NOME_CAR" };

            string comando = "SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'";

            ArrayList a = Comand.Select.ArryaListFormat(comando, campos, connect);

            try
            {

                cbxCategoria.Text = a[0].ToString();
                cbxNomeCar.Text = a[0].ToString();
            }
            catch
            {
                MessageBox.Show("Carro Cadastrado", "Atenção");
            }
        }

        private void btConsultar_Click(object sender, EventArgs e)
        {
            //essa variavel tera que guardar os dados que devem ser pesquisados 
            string pesquisa = "";//(#P1)

            pesquisa += cbxCategoria.Text != "" ? " c.CATEGORIA = '" + cbxCategoria.Text + "';" : "";                         //caso o campo "Categoria" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            pesquisa += cbxNomeCar.Text != "" ? " c.NOME_CAR ='" + cbxNomeCar.Text + "';" : "";                               //caso o campo "Nome do Carro" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            pesquisa += txtPlaca.Text != "" ? " c.PLACA ='" + txtPlaca.Text + "';" : "";                                      //caso o campo "Placa" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            pesquisa += mtxtIdentificacao.MaskCompleted == true ? " cli.CPF_CNPJ = '" + mtxtIdentificacao.Text + "';" : "";

            pesquisa += cbxEntrega.Checked == true ? " p.DATA_RETIRADA = '" + dtpDataEntrega.Text + "';" : "" ;

            pesquisa += cbxDevolucao.Checked == true ? " p.DATA_DEVOLUCAO = '" + dtpDataDevolucao.Text + "';" : "" ;

            pesquisa += cbxValorBase.Text != "" ? " p.TIPO_PEDIDO = '" + cbxValorBase.Text + "';" : "";

            string[] campos = pesquisa.Split(';');//os dados são separados e convertidos em variavel array

            string comando = "SELECT P.COD_PEDIDO, CLI.NOME_CLIENTE, CLI.CPF_CNPJ, C.NOME_CAR AS NOME_CARRO, C.PLACA, P.VALOR, P.DATA_RETIRADA, P.DATA_DEVOLUCAO, P.TIPO_PEDIDO "
            + " FROM PEDIDO AS P"
            + " INNER JOIN CARRO AS C ON COD_CAR = COD_CAR_FK"
            + " INNER JOIN CLIENTE AS CLI ON COD_CLIENTE = COD_CLIENTE_FK"; // variavel de comando em SQLServer

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

            txtPlaca.Text = comando;
            dtgLocacao.DataSource = Comand.Select.DataTableFormat(comando, connect);
        }

        private void btPesquisaCodPedido_Click(object sender, EventArgs e)
        {
            string[] campos = new string[] { "CATEGORIA", "NOME_CAR", "PLACA", "CPF_CNPJ", "DATA_RETIRADA", "DATA_DEVOLUCAO", "TIPO_PEDIDO" };
            try
            {
                //string de comando SqlServer
                string comando = "SELECT * FROM PEDIDO AS P"
                    + " INNER JOIN CARRO AS C ON COD_CAR = COD_CAR_FK "
                    + " INNER JOIN CLIENTE AS CLI ON COD_CLIENTE = COD_CLIENTE_FK "
                    + " WHERE P.COD_PEDIDO = " + txtCodigoPedido.Text;

                ArrayList a = Comand.Select.ArryaListFormat(comando, campos, connect);

                switch (a[0].ToString().ToUpper())
                {
                    case "ENTRADA":
                        cbxCategoria.SelectedIndex = 0;
                        break;
                    case "INTERMEDIARIO":
                        cbxCategoria.SelectedIndex = 1;
                        break;
                    case "ESPORTIVO":
                        cbxCategoria.SelectedIndex = 2;
                        break;
                    case "Luxo":
                        cbxCategoria.SelectedIndex = 3;
                        break;
                }


                cbxNomeCar.Text = a[1].ToString();
                txtPlaca.Text = a[2].ToString();
                mtxtIdentificacao.Text = a[3].ToString();
                dtpDataEntrega.Text = a[4].ToString();
                dtpDataDevolucao.Text = a[5].ToString();
                cbxValorBase.Text = a[6].ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Número de pedido não cadastrado\n ", "ATENÇÃO");
            }
        }

        private void txtCodigoPedido_Leave(object sender, EventArgs e)
        {
            if (txtCodigoPedido.Text != "")
            {
                try
                {
                    //campos a serem retornado pela busca
                    string[] campos = new string[] { "CATEGORIA", "NOME_CAR", "PLACA", "CPF_CNPJ", "DATA_RETIRADA", "DATA_DEVOLUCAO", "TIPO_PEDIDO" };
                    
                    //string de comando SqlServer
                    string comando = "SELECT * FROM PEDIDO AS P"
                        + " INNER JOIN CARRO AS C ON COD_CAR = COD_CAR_FK"
                        + " INNER JOIN CLIENTE AS CLI ON COD_CLIENTE = COD_CLIENTE_FK"
                        + " WHERE P.COD_PEDIDO = " + txtCodigoPedido.Text;

                    ArrayList a = Comand.Select.ArryaListFormat(comando, campos, connect);

                    switch (a[0].ToString().ToUpper())
                    {
                        case "ENTRADA":
                            cbxCategoria.SelectedIndex = 0;
                            break;
                        case "INTERMEDIARIO":
                            cbxCategoria.SelectedIndex = 1;
                            break;
                        case "ESPORTIVO":
                            cbxCategoria.SelectedIndex = 2;
                            break;
                        case "Luxo":
                            cbxCategoria.SelectedIndex = 3;
                            break;
                    }


                    cbxNomeCar.Text = a[1].ToString();
                    txtPlaca.Text = a[2].ToString();
                    mtxtIdentificacao.Text = a[3].ToString();
                    dtpDataEntrega.Text = a[4].ToString();
                    dtpDataDevolucao.Text = a[5].ToString();
                    cbxValorBase.Text = a[6].ToString();
                }
                catch (Exception)
                {

                    MessageBox.Show("Número de pedido não cadastrado\n ", "ATENÇÃO");
                }
            }
        }

        private void btDeletar_Click(object sender, EventArgs e)
        {
            if (txtCodigoPedido.Text == "")
            {
                MessageBox.Show("PREENCHA O CAMPO DO CODIGO DO PEDIDO", "ATENÇÃO");
            }
            else
            {
                DialogResult r;

                r = MessageBox.Show("Desejá apagar o pedido? ?"
                    + "\nSe você apagar um pedido que não foi concluído (devolução do carro) "
                    + "o carro irá ficar com a situação \"PÁTIO\" e não podera mais ser mudado pelo sistema!"
                    , "ATENÇÃO", MessageBoxButtons.YesNo);

                if (r == DialogResult.Yes)
                    Comand.Delete("DELETE PEDIDO WHERE COD_PEDIDO = " + txtCodigoPedido.Text, connect);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxEntrega_CheckedChanged(object sender, EventArgs e)
        {
            dtpDataEntrega.Visible = cbxEntrega.Checked;
        }

        private void cbxDevolucao_CheckedChanged(object sender, EventArgs e)
        {
            dtpDataDevolucao.Visible = cbxDevolucao.Checked;
        }
    }
}
