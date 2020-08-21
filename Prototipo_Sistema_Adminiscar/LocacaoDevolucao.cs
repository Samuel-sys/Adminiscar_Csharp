using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using SqlServer;
namespace Prototipo_Sistema_Adminiscar
{
    public partial class LocacaoDevolucao : Form
    {
        //string com o endereço do banco de dados
        string connect = Connection.Route("ADMINISCAR");

        string cod_car = "";

        public LocacaoDevolucao()
        {
            InitializeComponent();
        }

        void atualizaDataGrid()
        {
            dataGridView1.DataSource = Comand.Select.DataTableFormat("SELECT * FROM CARRO WHERE SITUACAO <> 'PATIO'", connect);
        }
        

        void ImagemCar()
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

        void ComboBoxCarro()
        {
            switch (cbxCategoria.Text)
            {
                case "Entrada":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Onix");
                    cbxNomeCar.Items.Add("Agile");
                    cbxNomeCar.Items.Add("Fiat mobi");
                    cbxNomeCar.Items.Add("Up");
                    cbxNomeCar.Items.Add("HB20");
                    cbxNomeCar.Items.Add("CELTA");
                    break;
                case "Intermediario":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Golf");
                    cbxNomeCar.Items.Add("renegade");
                    cbxNomeCar.Items.Add("EcoSport");
                    cbxNomeCar.Items.Add("Argo");
                    cbxNomeCar.Items.Add("Cronos");
                    cbxNomeCar.Items.Add("Linea");
                    break;
                case "Esportivo":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Jetta");
                    cbxNomeCar.Items.Add("Golf Tsi");
                    cbxNomeCar.Items.Add("Bmw m3");
                    cbxNomeCar.Items.Add("Ford fusion");
                    cbxNomeCar.Items.Add("Volvo s60");
                    cbxNomeCar.Items.Add("Audi rs4");
                    break;
                case "Luxo":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Land rover evoque");
                    cbxNomeCar.Items.Add("Jeep compass");
                    cbxNomeCar.Items.Add("Camaro zl1");
                    cbxNomeCar.Items.Add("Mercedes c63");
                    break;
            }
            txtPlaca.Text = "";
        }

        string consultaCar()
        {
            //campos a serem retornado pela busca
            string[] campos = new string[] { "CATEGORIA", "NOME_CAR", "COD_CAR", "SITUACAO" };

            //string de comando SqlServer
            string comando = "SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'";

            ArrayList a = Comand.Select.ArryaListFormat(comando, campos, connect);
            try
            {
                if (a[3].ToString().ToUpper() == "ALOCADO")
                {

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
                        case "LUXO":
                            cbxCategoria.SelectedIndex = 3;
                            break;
                    }
                    cbxNomeCar.Text = a[1].ToString();
                    cod_car = a[2].ToString();
                }
                else
                {
                    MessageBox.Show("Nenhum carro " + a[1].ToString() + "esta alocado para se fazer uma devolução" );
                    txtPlaca.Text = "";
                }

            }
            catch
            {
                MessageBox.Show("Carro não Cadastrado", "ATENÇÃO");
                txtPlaca.Text = "";
                throw;
            }

            return cod_car;
        }

        private void btCadastrar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text != "" && mtxtKms.Text != "")
            {
                DialogResult r;

                r = MessageBox.Show("Desejá cadastras o carro como devolvido?\nSe o carro estiver alugado pode causar uma inconsistência de dados no seu sistema", "ATENÇÃO", MessageBoxButtons.YesNo);
                if(r == DialogResult.Yes)
                {
                Comand.Update("UPDATE CARRO SET SITUACAO = 'PATIO', QUILOMETRAGEM = " + mtxtKms.Text + " WHERE PLACA = '" + txtPlaca.Text + "'", connect);
                MessageBox.Show("O carro já está colocado na área de Patio");
                atualizaDataGrid();

                }

            }
            else
            {
                string msg = txtPlaca.Text != "" ? "PREENCHA O CAMPO DA 'PLACA' DO VEICULO" : "" ;

                msg = txtPlaca.Text != "" ? "PREENCHA O CAMPO DA 'QUILOMETRAGEM' DO VEICULO" : msg;

                msg = txtPlaca.Text != "" && mtxtKms.Text != "" ? "PREENCHA O CAMPO DA 'PLACA' E 'QUILOMETRAGEM' DO VEICULO" : msg;
                MessageBox.Show(msg);
            }
        }

        private void mtxtKms_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btPesquisaCliente_Click(object sender, EventArgs e)
        {
            consultaCar();
            ImagemCar();
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            if(txtPlaca.Text != "")
            {

            consultaCar();
            try
            {
            int.Parse(Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "QUILOMETRAGEM", connect));

            }
            catch { }
            }
        }

        private void mtxtKms_Leave(object sender, EventArgs e)
        {
            if (txtPlaca.Text != "")
            {
                //string a = 
                int km = int.Parse(Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "QUILOMETRAGEM", connect));

                int kmtxt = -1;

                if (mtxtKms.Text != "")
                {
                    kmtxt = int.Parse(mtxtKms.Text);
                }
                if (km < kmtxt)
                {
                    mtxtKms.Text = kmtxt.ToString();
                }

                else
                {
                    mtxtKms.Text = km.ToString();
                    MessageBox.Show("Quilometragem incorréta! \núltima quilometragem foi de " + km + "Km");
                }
            }
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxCarro();
            ImagemCar();
        }

        private void cbxNomeCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nome da coluna onde esta arquivado o dado da placa do carro
            string[] campos = new string[] { "PLACA" };
            //comando SQP Server
            ArrayList a =Comand.Select.ArryaListFormat(
                "SELECT * FROM CARRO"
                +" WHERE CATEGORIA = '" + cbxCategoria.Text 
                + "' AND NOME_CAR = '" + cbxNomeCar.Text + "'"
                , campos, connect);

            try
            {
            //colocando o dado da placa do carro disponivel
            txtPlaca.Text = a[0].ToString();
            consultaCar();
            ImagemCar();
            }
            catch
            {
                MessageBox.Show("Não temos nenhum carro " + cbxNomeCar.Text + "cadastrado no sistema");
            }

        }

        private void cbxNomeCar_Leave(object sender, EventArgs e)
        {
            if(cbxNomeCar.Text != "")
            {
            consultaCar();
            ImagemCar();
            }
        }

        private void btCarro_Click(object sender, EventArgs e)
        {
            consultaCar();
            ImagemCar();
        }

        private void LocacaoDevolucao_Load(object sender, EventArgs e)
        {
            atualizaDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
