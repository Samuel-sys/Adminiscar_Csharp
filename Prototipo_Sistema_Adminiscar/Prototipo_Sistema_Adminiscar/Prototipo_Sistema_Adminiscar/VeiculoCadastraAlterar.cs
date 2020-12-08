using Prototipo_Sistema_Adminiscar.Controler;
using Prototipo_Sistema_Adminiscar.Models;
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

        public VeiculoCadastraAlterar()
        {
            InitializeComponent();
        }

        string connect = Connection.Route("ADMINISCAR");

        private Carro pooCarro()
        {

            return new Carro()
            {
                NOME_CAR = cbxNomeCar.Text,
                PLACA = txtPlaca.Text,
                RENAVAM = txtRenavam.Text,
                MODELO = cbxModelo.Text,
                CATEGORIA = cbxCategoria.Text,
                COMBUSTIVEL = cbxCombustivel.Text,
                QUILOMETRAGEM = 0,
                SITUACAO = "PATIO",
                VALOR_DIARIO = double.Parse(mtxtValDiario.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "")),
                VALOR_SEMANAL = double.Parse(mtxtValSemanal.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "")),
                VALOR_MENSAL = double.Parse(mtxtValMensal.Text.Replace(".", "").Replace(",", ".").Replace("R$ ", "")),
                SOM = char.Parse(lblSOM.Text),
                SOM_BT = char.Parse(lblSOMBT.Text),
                GPS = char.Parse(lblGPS.Text),
            };
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

        

        private void btPesquisaCarro_Click(object sender, EventArgs e)
        {
            if(txtPlaca.Text == "" || txtPlaca.Text == null)
            {
                MessageBox.Show("Campo de pesquisa vazio");
            }
            else
            {
                var car = CarroADO.ConsultaCarroPlaca(txtPlaca.Text);

                MessageBox.Show(car.ToString());
                if(car == null)
                {
                    MessageBox.Show("Nada encontrado");
                }else
                {

                preencheCarro(car);
                }
            }
        }

        private void preencheCarro(Carro carro)
        {
            cbxNomeCar.Text = carro.NOME_CAR.ToString();
            txtPlaca.Text = carro.PLACA.ToString();
            txtRenavam.Text = carro.RENAVAM.ToString();
            cbxModelo.Text = carro.MODELO.ToString();
            cbxCategoria.Text = carro.CATEGORIA.ToString();
            cbxCombustivel.Text = carro.COMBUSTIVEL.ToString();
            mtxtValDiario.Text = carro.VALOR_DIARIO.ToString();
            mtxtValSemanal.Text = carro.VALOR_SEMANAL.ToString();
            mtxtValMensal.Text = carro.VALOR_MENSAL.ToString();
            lblSOM.Text = carro.SOM.ToString();
            lblSOMBT.Text = carro.SOM_BT.ToString();
            lblGPS.Text = carro.GPS.ToString();

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
            if (tudoPreenchido())
            {
                var car = pooCarro();

                CarroADO.cadastraCarro(car);
            }
            
            atualizaDataGrid();

        }

        private bool tudoPreenchido()
        {
            return cbxCategoria.Text != "" && cbxNomeCar.Text != ""
            && txtPlaca.Text != "" && cbxCombustivel.Text != ""
            && cbxModelo.Text != "" && txtRenavam.Text != ""
            && mtxtValDiario.Text.Replace("R$ ", "") != ""
            && mtxtValSemanal.Text.Replace("R$ ", "") != ""
            && mtxtValMensal.Text.Replace("R$ ", "") != "";
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
            if (tudoPreenchido())
            {

                if (Comand.Select.BoolFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", connect))
                {
                    if (CarroADO.UpdateCarro(pooCarro()))
                    {
                        MessageBox.Show("Carro Atualizado no BD com sucesso");
                        atualizaDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizado o carro");
                    }
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
