using System;
using System.Windows.Forms;
using SqlServer;
using Prototipo_Sistema_Adminiscar.Controler;
using Prototipo_Sistema_Adminiscar.Models;

namespace Prototipo_Sistema_Adminiscar
{
    public partial class FuncionarioCadastrarAlterar : Form
    {
        string connect = Connection.Route("ADMINISCAR");//informa o endereço de conexão com o banco de dados

        /*ENDERECO*/
        Endereco end;

        private Endereco pooEndereco()
        {
            //preenchendo o Objeto Endereco
            Endereco endereco = new Endereco() {
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
        Telefone tel;

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

        /*LOGIN*/
        Login_sistema login;

        private Login_sistema pooLogin()
        {
            Login_sistema retorno = new Login_sistema()
            {
                email = txtEmail.Text,
                senha = txtSenha.Text,
                nivel_acesso = int.Parse(cbxNivelAcesso.Text)
                // o codigo do funcionario e colocado apois o cadastro do funcionario na class FuncionarioControl
            };

            return retorno;
        }

        private void LoginPreenchimentoForm(Login_sistema login)
        {
            //Preenche os campos do sistema fisico
            txtEmail.Text = login.email;
            txtSenha.Text = login.senha;
            txtConfirmaSenha.Text = login.senha;
            cbxNivelAcesso.Text = login.nivel_acesso.ToString();
        }

        /*FUNCIONARIO*/
        Funcionario func;

        private Funcionario pooFuncionario()
        {
            Funcionario retorno = new Funcionario()
            {
                nome_func = txtNome.Text,
                cpf_func =  mtxtCPF.Text,
                cnh_func =  mtxtCNH.Text,
                // cod_tell, cod_endereco e o cod_func seram preenchidos dentro das class de controle respectivo a seu devido objeto
            };

            return retorno;
        }

        private void FuncionarioPreenchimentoForm(Funcionario func)
        {
            txtNome.Text = func.nome_func;
            mtxtCPF.Text = func.cpf_func.ToString();
            mtxtCNH.Text = func.cnh_func.ToString();
        }

        void Clear()
        {
            //endereco
            txtLogradouro.Text = "";
            mtxtNumero.Text = "";
            txtBairro.Text = "";
            mtxtCEP.Text = "";
            txtCidade.Text = "";
            cbxEstado.Text = "";

            //func
            txtNome.Text = "";
            mtxtCPF.Text = "";
            mtxtCNH.Text = "";

            //tell
            mtxtTell1.Text = "";
            mtxtTell2.Text = "";

            txtEmail.Text = "";
            txtSenha.Text = "";
            txtConfirmaSenha.Text = "";
            cbxNivelAcesso.Text = "";

        }

        //Bt Cadastrar
        private void btCadastrar_Click(object sender, EventArgs e)
        {
            if (tudoPreenchido())
            {
                end = pooEndereco(); //ENDEREÇO
                tel = pooTelefone(); //TELEFONE
                login = pooLogin(); //LOGIN / EMAIL
                func = pooFuncionario(); //FUNCIONARIO

                if(FuncionarioADO.cadastroFunc(func, end, tel, login))
                {
                    atualizaDataGrid();
                    Clear();
                }
            }
            else
                MessageBox.Show("Preencha todos os campos!", "ATENÇÃO");
        }

        //informa se o form esta todo preenchido
        private bool tudoPreenchido()
        {
            return txtNome.Text != "" && mtxtCPF.Text != ""
                && mtxtCNH.Text != "" && cbxNivelAcesso.Text != ""
                && mtxtTell1.MaskCompleted == true
                && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                && txtBairro.Text != "" && txtCidade.Text != ""
                && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true;
        }

        private void btPesquisaEmail_Click(object sender, EventArgs e)
        {
            var login = LoginADO.consultaLogin(txtEmail.Text);

            try
            {
                LoginPreenchimentoForm(login);

                var func = FuncionarioADO.consultaCodFunc(login.cod_func_FK);

                FuncionarioPreenchimentoForm(func);

                var tell = TelefoneADO.consultaCodTel(func.cod_tell_fk);

                TelefonePreencheForm(tell);


                var endereco = EnderecoADO.consultaCodEndereco(func.cod_endereco_fk);

                EnderecoPreencheForm(endereco);

            }
            catch (Exception)
            {
                Clear();
                MessageBox.Show("Login não encontrado");
                
            }
        }



        //alterar ainda não foi feito
        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (tudoPreenchido())
            {
                var func = pooFuncionario();
                var endereco = pooEndereco();
                var tel = pooTelefone();
                var login = pooLogin();

                if (Comand.Select.BoolFormat("select * from FUNCIONARIO WHERE CPF_FUNC = '" + func.cpf_func + "'", connect))
                {
                    if(FuncionarioADO.atualizaFunc(func, endereco, tel, login))
                    {
                        MessageBox.Show("Dados do Funcionario atualizados com sucesso", "Atenção");
                    }
                    else
                    {
                        MessageBox.Show("Erro na atualização", "Atenção");
                    }
                }
                else
                {
                    MessageBox.Show("Não existe nenhum funcionário cadastrado com esse CPF para ser atualizado");
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
            var func = FuncionarioADO.consultaCPF(mtxtCPF.Text);

            try
            {
                FuncionarioPreenchimentoForm(func);

                var endereco = EnderecoADO.consultaCodEndereco(func.cod_endereco_fk);

                EnderecoPreencheForm(endereco);

                var tell = TelefoneADO.consultaCodTel(func.cod_tell_fk);

                TelefonePreencheForm(tell);

                var login = LoginADO.consultaCodFunc(func.func_cod);

                LoginPreenchimentoForm(login);

            }
            catch 
            {
                MessageBox.Show("CPF não cadastrado");
                Clear();
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

    }
}
