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
using Prototipo_Sistema_Adminiscar.Models;
using Prototipo_Sistema_Adminiscar.Controler;

namespace Prototipo_Sistema_Adminiscar
{
    public partial class ClienteCadastraAltera : Form
    {
        string connect = Connection.Route("ADMINISCAR");

        public ClienteCadastraAltera()
        {
            InitializeComponent();
        }


        /*ENDERECO*/
        private Endereco pooEndereco()
        {
            //preenchendo o Objeto Endereco
            Endereco endereco = new Endereco()
            {
                end_logradouro = txtLogradouro.Text,
                end_numero = int.Parse(mtxtNumero.Text),
                end_bairro = txtBairro.Text,
                end_CEP = mtxtCEP.Text,
                end_cidade = txtCidade.Text,
                end_estado = cbxEstado.Text
            };

            return endereco;
        }
        private void EnderecoPreencheForm(Endereco endereco)
        {
            txtLogradouro.Text = endereco.end_logradouro;
            mtxtNumero.Text = endereco.end_numero.ToString();
            txtBairro.Text = endereco.end_bairro;
            mtxtCEP.Text = endereco.end_CEP;
            txtCidade.Text = endereco.end_cidade;
            cbxEstado.Text = endereco.end_estado;
        }

        /*TELEFONE*/
        private Telefone pooTelefone()
        {
            //objeto a ser retornado
            Telefone retorno = new Telefone();

            //se o usuario preencher os 2 campos ele cadastra os 2 campos
            if (mtxtTell2.MaskCompleted == true)
            {
                retorno.tell1 = mtxtTell1.Text;
                retorno.tell2 = mtxtTell2.Text;
            }
            //se não cadastra apenas 1 telefone
            else
            {
                retorno.tell1 = mtxtTell1.Text;
            }

            return retorno;//retorna o objeto
        }

        private void TelefonePreencheForm(Telefone tell)
        {
            mtxtTell1.Text = tell.tell1.ToString();
            mtxtTell2.Text = tell.tell2.ToString();

        }

        /*Cliente*/
        private Cliente pooCliente()
        {
            Cliente retorno = new Cliente()
            {
                nome_cliente = txtNome.Text,
                cpf_cnpj = mtxtIdentificacao.Text,
                cnh_cliente = mtxtCNH.Text,
                
            };

            return retorno;
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
            consultaCPF();
        }

        private void consultaCPF()
        {
            var cliente = ClienteADO.consultaCpfCnpj(mtxtIdentificacao.Text);
            try
            {
                ClientePreenche(cliente);

                var endereco = EnderecoADO.consultaCodEndereco(cliente.cod_endereco_fk);

                EnderecoPreencheForm(endereco);

                var tell = TelefoneADO.consultaCodTel(cliente.cod_tell_fk);

                TelefonePreencheForm(tell);
            }
            catch
            {
                MessageBox.Show("CPF não cadastrado");
                Clear();
            }
        }

        private void ClientePreenche(Cliente cliente)
        {
            txtNome.Text = cliente.nome_cliente;
            mtxtCNH.Text = cliente.cnh_cliente;
            mtxtIdentificacao.Text = cliente.cpf_cnpj;
        }

        private void atualizaDataGrid()
        {
            var comando = @"SELECT C.NOME_CLIENTE AS 'NOME DO CLIENTE', C.CPF_CNPJ 'CPF \ CNPJ',"
                    + "C.CNH_CLIENTE AS CNH, E.LOGRADOURO, E.NUMERO as 'n°', "
                    + "E.BAIRRO, E.CEP, E.CIDADE, E.ESTADO, T.TELL1 AS '1° TELEFONE', "
                    + "T.TELL2 AS '2° TELEFONE' "
                    + "FROM CLIENTE AS C "
                    + "INNER JOIN ENDERECO AS E on C.COD_ENDERECO_FK = E.COD_ENDERECO "
                    + "INNER JOIN TELEFONE AS T ON C.COD_TELL_FK = T.COD_TELL ";
            //COMANDO SqlServer
            dtgCliente.DataSource = Comand.Select.DataTableFormat(
                comando, connect);
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
            if (tudoPreenchido())
            {
                var cliente = pooCliente();
                var endereco = pooEndereco();
                var tell = pooTelefone();
                ClienteADO.cadastroCliente(cliente, endereco, tell);

                atualizaDataGrid(); 
            }
            else
            {
                MessageBox.Show("Preencha todos os campos");
            }
        }

        private bool tudoPreenchido()
        {
            return txtNome.Text != "" && mtxtIdentificacao.Text != ""
                && mtxtCNH.Text != ""
                && mtxtTell1.MaskCompleted == true
                && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                && txtBairro.Text != "" && txtCidade.Text != ""
                && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true;
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (tudoPreenchido())
            {
                var cliente = pooCliente();
                var endereco = pooEndereco();
                var tell = pooTelefone();

                if(ClienteADO.atualizaCliente(cliente, endereco, tell))
                {
                    MessageBox.Show("Dados Atualizados");
                }
                else
                {
                    MessageBox.Show("Erro ao efetuar a atualização");
                }

                atualizaDataGrid();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos");
            }
        }

        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            if(!mtxtCEP.MaskCompleted == true)
            {
                mtxtCEP.Text = "";
            }
        }

        private void mtxtTell1_Leave(object sender, EventArgs e)
        {
            if(!mtxtTell1.MaskCompleted == true)
            {
                mtxtTell1.Text = "";
            }
        }

        private void mtxtTell2_Leave(object sender, EventArgs e)
        {
            if (!mtxtTell2.MaskCompleted == true)
            {
                mtxtTell2.Text = "";
            }
        }

        private void mtxtCNH_Leave(object sender, EventArgs e)
        {
            if (!mtxtCNH.MaskCompleted == true)
            {
                mtxtCNH.Text = "";
            }
        }

        private void mtxtIdentificacao_Leave(object sender, EventArgs e)
        {
            if (!mtxtIdentificacao.MaskCompleted)
            {
                mtxtIdentificacao.Text = "";
            }else
            {
                consultaCPF();
            }
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
            Clear();
        }

        private void Clear()
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
