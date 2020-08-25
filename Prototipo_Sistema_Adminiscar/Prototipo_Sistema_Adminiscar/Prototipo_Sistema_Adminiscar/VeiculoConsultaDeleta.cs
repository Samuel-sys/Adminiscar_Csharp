using System;
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
    public partial class VeiculoConsultaDeleta : Form
    {

        string connect = Connection.Route("ADMINISCAR");

        public VeiculoConsultaDeleta()
        {
            InitializeComponent();
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
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
                    cbxNomeCar.Items.Add("Ford ka");
                    break;
                case "Intermediario":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Golf");
                    cbxNomeCar.Items.Add("renegade");
                    cbxNomeCar.Items.Add("EcoSport");
                    cbxNomeCar.Items.Add("Argo");
                    cbxNomeCar.Items.Add("Cronos");
                    cbxNomeCar.Items.Add("Linea");
                    break;
                case "Esportivo":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Jetta");
                    cbxNomeCar.Items.Add("Golf Tsi");
                    cbxNomeCar.Items.Add("Bmw m3");
                    cbxNomeCar.Items.Add("Ford fusion");
                    cbxNomeCar.Items.Add("Volvo s60");
                    cbxNomeCar.Items.Add("Audi rs4");
                    break;
                case "Luxo":
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Land rover evoque");
                    cbxNomeCar.Items.Add("Jeep compass");
                    cbxNomeCar.Items.Add("Camaro zl1");
                    cbxNomeCar.Items.Add("Mercedes c63");
                    break;
            }
        }

        private void btConsulta_Click(object sender, EventArgs e)
        {
            //essa variavel tera que guardar os dados que devem ser pesquisados 
            string pesquisa = "";//(#P1)

            pesquisa += cbxCategoria.Text != "" ? " CATEGORIA = '" + cbxCategoria.Text +"';" : "";//caso o campo "Categoria" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            pesquisa += cbxNomeCar.Text != "" ? " NOME_CAR ='" + cbxNomeCar.Text + "';" : "";//caso o campo "Nome do Carro" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            pesquisa += cbxModelo.Text != "" ? " MODELO ='" + cbxModelo.Text + "';": "";//caso o campo "Modelo do Carro" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            pesquisa += txtPlaca.Text != "" ? " PLACA ='" + txtPlaca.Text + "';" : "";//caso o campo "Placa" seja preenchido e acionado o dado de comparação se não nada e adicionado ao codigo

            string[] campos = pesquisa.Split(';');//os dados são separados e convertidos em variavel array

            string comando = "SELECT * FROM CARRO "; // variavel de comando em SQLServer

            //esse loop serve para encrementar o comando em SQL enquanto tiver parametos para serem adicionado ele adicona
            for (int i = 0; i < campos.Length; i++)
            {
                //caso não tenha parametros na variavel pesquisa #P1 não se coloca where assim mostrando toda a tabela sem filtro
                if (i == 0 && campos.Length > 1)
                    comando += "WHERE ";

                //se tiver mais de 1 parametro se coloca AND entre os parametros
                if (i > 0 && i != campos.Length - 1)
                    comando += " AND ";

                //adicionado parametro ao comando SQLServer
                comando += campos[i];
            }

            //se poem os dados da tabela no DataGrid
            dtgCarro.DataSource = Comand.Select.DataTableFormat(comando, connect);
        }

        void AtualizaDataGrid()
        {
            dtgCarro.DataSource = Comand.Select.DataTableFormat("SELECT * FROM CARRO", connect);

        }

        private void VeiculoConsultaDeleta_Load(object sender, EventArgs e)
        {
            AtualizaDataGrid();
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            //limpando o campo de preenchimento
            cbxCategoria.Text = "";
            cbxNomeCar.Text = "";
            cbxModelo.Text = "";
            txtPlaca.Text = "";

            //tirando os items do ComboBox com o nome dos carros
            cbxNomeCar.Items.Clear();
        }

        private void deletar()
        {
            string nome_car = Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "NOME_CAR", connect);

            if (nome_car != "")
            {

                DialogResult r;
                r = MessageBox.Show("Deletar o Carro: \nNOME: " + nome_car + "\nPLACA: " + txtPlaca.Text, "ATENÇÃO", MessageBoxButtons.YesNo);

                string comando = "DELETE CARRO WHERE PLACA ='" + txtPlaca.Text + "'";
                if (r == DialogResult.Yes)//se o cliente precionar o butão sim ele deleta 
                    Comand.Delete(comando, connect);
            }
            else
                MessageBox.Show("Placa invalida, carro inesistente no sistema");
        }

        private void cbxNomeCar_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btDeletar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text == "")
                MessageBox.Show("Para deletar um carro, preencha o campo 'PLACA' com a placa do veículo que deseja excluir");
            else
                deletar();

            AtualizaDataGrid();
        }
    }
}
