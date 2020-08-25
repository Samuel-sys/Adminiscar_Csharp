using SqlServer;
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

namespace Prototipo_Sistema_Adminiscar
{
    public partial class VeiculoCadastraAlterar : Form
    {

        string connect = Connection.Route("ADMINISCAR");

        public VeiculoCadastraAlterar()
        {
            InitializeComponent();
        }

        private void Veiculo_Load(object sender, EventArgs e)
        {
            atualizaDataGrid();
            mtxtValDiario.Text = String.Format("{0:c}",0.00);
            mtxtValSemanal.Text = String.Format("{0:c}",0.00);
            mtxtValMensal.Text = String.Format("{0:c}", 0.00);

        }
        void atualizaDataGrid()
        {
            dtgCarro.DataSource = Comand.Select.DataTableFormat("SELECT * FROM CARRO", connect);
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
        }

        private void ConsultaCarro(bool mensagem)
        {
            string[] campos = new string[] { "CATEGORIA", "NOME_CAR", "RENAVAM", "COMBUSTIVEL", "MODELO", "SOM", "SOM_BT", "GPS", "VALOR_DIARIO", "VALOR_SEMANAL", "VALOR_MENSAL" };

            ArrayList a = Comand.Select.ArryaListFormat("SELECT * FROM CARRO where PLACA = '" + txtPlaca.Text + "'", campos, connect);

            //tenta puxar os dados do banco e colocar nos campos
            try
            {
                cbxCategoria.Text = a[0].ToString();     //Categoria
                cbxNomeCar.Text = a[1].ToString();       //Nome
                txtRenavam.Text = a[2].ToString();       //Renavam
                cbxCombustivel.Text = a[3].ToString();   //Combustivel
                cbxModelo.Text = a[4].ToString();        //Modelo do Carro
                lblSOM.Text = a[5].ToString();           //SOM
                lblSOMBT.Text = a[6].ToString();         // SOM BT
                lblGPS.Text = a[7].ToString();           //GPS
                mtxtValDiario.Text = String.Format("{0:c)", a[8].ToString());    //Valor Diario
                mtxtValSemanal.Text = String.Format("{0:c)", a[9].ToString());   //Valor Semanal
                mtxtValMensal.Text = String.Format("{0:c)", a[10].ToString());   //Valor Mensal
            }
            //se não encontrar um parametro o programa uma Mensagem informando ao usuario
            catch (Exception)
            {
                if (mensagem == true)
                    MessageBox.Show("Placa de carro não encontrada", "ATENÇÃO");
            }
        }

        private void btPesquisaCarro_Click(object sender, EventArgs e)
        {
            ConsultaCarro(true);
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            //limpa o campo de preenchimento dos Combo Box
            cbxCategoria.Text = "";
            cbxNomeCar.Text = "";
            cbxCombustivel.Text = "";
            cbxModelo.Text = "";

            //deixa o campo de nome de carro vazio (sem items)
            cbxNomeCar.Items.Clear();

            //limpa o campo de preenchimento
            txtRenavam.Text = "";
            lblSOM.Text = "";
            lblSOMBT.Text = "";
            lblGPS.Text = "";
            mtxtValDiario.Text = "";
            mtxtValSemanal.Text = "";
            mtxtValMensal.Text = "";
        }


        private void btCadastrar_Click(object sender, EventArgs e)
        {
            if(cbxCategoria.Text != "" && cbxNomeCar.Text != "" 
            && txtPlaca.Text != "" && cbxCombustivel.Text != ""
            && cbxModelo.Text != "" && txtRenavam.Text != "" 
            && mtxtValDiario.Text.Replace("R$ ","") != ""
            && mtxtValSemanal.Text.Replace("R$ ","") != ""
            && mtxtValMensal.Text.Replace("R$ ","") != "")
            Comand.Insert("INSERT INTO CARRO (NOME_CAR, PLACA, "
                + "RENAVAM, MODELO, CATEGORIA, COMBUSTIVEL, QUILOMETRAGEM, "
                +"SITUACAO, VALOR_DIARIO, VALOR_SEMANAL, VALOR_MENSAL, SOM, SOM_BT, GPS) values('" 
                + cbxNomeCar.Text + "','" + txtPlaca.Text + "','" + txtRenavam.Text + "','" 
                + cbxModelo.Text + "','" + cbxCategoria.Text + "','" + cbxCombustivel.Text 
                + "', 0, 'PATIO','" 
                + mtxtValDiario.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "") 
                + "','" + mtxtValSemanal.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "") + "','" 
                + mtxtValMensal.Text.Replace(".", "").Replace(",", ".").Replace("R$ ","") 
                + "','" + lblSOM.Text + "','" + lblSOMBT.Text + "','" + lblGPS.Text + "' )", connect);
            atualizaDataGrid();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btConsultaDeleta_Click(object sender, EventArgs e)
        {
            VeiculoConsultaDeleta a = new VeiculoConsultaDeleta();
            a.Show();
        }


        private void btCadastra_Click(object sender, EventArgs e)
        {
            if (cbxCategoria.Text != "" && cbxNomeCar.Text != ""
            && txtPlaca.Text != "" && cbxCombustivel.Text != ""
            && cbxModelo.Text != "" && txtRenavam.Text != ""
            && mtxtValDiario.Text.Replace("R$ ", "") != ""
            && mtxtValSemanal.Text.Replace("R$ ", "") != ""
            && mtxtValMensal.Text.Replace("R$ ", "") != "")
            {
                string a = Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "COD_CAR", connect);

                if (a != "")
                {
                    string comando = "UPDATE CARRO SET NOME_CAR ='" + cbxNomeCar.Text + "', RENAVAM ='" + txtRenavam.Text + "', MODELO ='" + cbxModelo.Text + "', COMBUSTIVEL ='" + cbxCombustivel.Text + "', VALOR_DIARIO ='" + mtxtValDiario.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "") + "', VALOR_SEMANAL ='" + mtxtValSemanal.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "") + "', VALOR_MENSAL ='" + mtxtValMensal.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "") + "' WHERE PLACA ='" + txtPlaca.Text + "'";
                    Comand.Update(comando, connect);
                }
                else
                {
                    MessageBox.Show("Nenhum dado Carro cadastrado com a placa: " + txtPlaca.Text,"ATENÇÃO");
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!");
            }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            ConsultaCarro(false);
        }
        private void mtxtValDiario_Leave(object sender, EventArgs e)
        {
            if(mtxtValDiario.Text != "")
            {
            double a = double.Parse(mtxtValDiario.Text.Replace("R$ ",""));
            mtxtValDiario.Text = String.Format("{0:c}",a);
            }
        }

        private void mtxtValSemanal_Leave(object sender, EventArgs e)
        {
            if(mtxtValSemanal.Text != "")
            {
            double a = double.Parse(mtxtValSemanal.Text.Replace("R$ ", ""));
            mtxtValSemanal.Text = String.Format("{0:c}", a);
            }
        }

        private void mtxtValMensal_Leave(object sender, EventArgs e)
        {
            double a = double.Parse(mtxtValMensal.Text.Replace("R$ ", ""));
            mtxtValMensal.Text = String.Format("{0:c}", a);
        }

        private void mtxtValDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValDiario.Text.Contains(","))
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


        private void mtxtValSemanal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValSemanal.Text.Contains(","))
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

        private void mtxtValMensal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValMensal.Text.Contains(","))
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

        private void mtxtValDiario_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtValDiario.Text = mtxtValDiario.Text.Replace("R$ ", "");
        }

        private void mtxtValSemanal_Click(object sender, EventArgs e)
        {
            mtxtValSemanal.Text = mtxtValSemanal.Text.Replace("R$ ", "");
        }

        private void mtxtValMensal_Click(object sender, EventArgs e)
        {
            mtxtValMensal.Text = mtxtValMensal.Text.Replace("R$ ", "");
        }
    }
}
