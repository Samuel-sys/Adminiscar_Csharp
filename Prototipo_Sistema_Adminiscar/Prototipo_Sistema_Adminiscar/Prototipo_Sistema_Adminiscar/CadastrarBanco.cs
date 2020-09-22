using System;
using System.Windows.Forms;
using SqlServer;
using System.Data.SqlClient;

namespace Prototipo_Sistema_Adminiscar
{
    public partial class CadastrarBanco : Form
    {
        static string banco;

        //nome da dataBase
        static string dataBase = "ADMINISCAR";

        static string instancia = "";

        static string usuario = "";

        static string senha = "";

        public CadastrarBanco()
        {
            InitializeComponent();
        }


        private void btCadastarBD_Click(object sender, EventArgs e)
        {
            instancia = txtInstancia.Text;//nome da dataBase

            //se o cliente pasar um valor nulo/vazio ou informa que e localhost informa que o endereço do banco e localhost mesma coisa se checkedBox do LocalHost
            instancia = instancia == "" || instancia == "localhost" || cbxLocalHost.Checked ? @"localhost\SQLEXPRESS" : instancia;

            //se o cliente tiver abilitado o campo de usuario e senha registra o valor se não registra null
            usuario = rdtSim.Checked ? null : txtUsuario.Text;

            //se o cliente tiver abilitado o campo de usuario e senha registra o valor se não registra null
            senha = rdtSim.Checked ? null : txtSenha.Text;


            //insereindo a intancia do no endereço de conexão
            banco = @"Data Source = " + instancia ;

            //se o campo de senha e usuario estiverem nulos ele coloca como uma conexão Integrated Security
            banco += usuario == null && senha == null ? @"; Integrated Security = True" : "; USER ID = " + usuario + "; PASSWORD = " + senha + " ;";

            /*
             NÃO SE ADICIONA A DATABASE NA ROTA DE CONEXÃO DO BANCO DE DADOS PARA TESTAR A CONEXÃO COM AINSTANCIA 
             DEPOIS SE TESTA COM A DATABASE 
             */

            //Se o CheckedBox cbxLocalHost estiver ativo e o campo de Integrated Security estiver com o RadioButton SIM estiver ativo
            if (cbxLocalHost.Checked == true && rdtSim.Checked == true)
                CadastraRouteDataBase();

            //Se o CheckedBox cbxLocalHost estiver ativo e os campos do USER e SENHA estiverem preenchidas
            else if (cbxLocalHost.Checked == true && rdtSim.Checked == false && 
                txtUsuario.Text != "" && txtSenha.Text != "")
                CadastraRouteDataBase();

            //so executa o comando se tiver com todos os campos preenchidos
            else if (txtInstancia.Text != "" && rdtSim.Checked == false
                && txtUsuario.Text != "" && txtSenha.Text != "")
                CadastraRouteDataBase();

            //so executa o comando se tiver com todos os campos preenchidos
            else if (txtInstancia.Text != "" && rdtSim.Checked == true)
                CadastraRouteDataBase();

            else// se faltar algum campo a ser preenchido informa que tem que preencher todos os campos
                MessageBox.Show("Erro preencha todos os campos!");
        }
        

        private void CadastraRouteDataBase()
        {

            //teste de conexão com o Banco sem o parametro da DataBase
            if (Connection.ConnectionTest(banco))
            {

                //Se a conexão com a DataBase não for possivel execulta esse trecho do codigo, se for possivel cria o ficheiro 
                //(o comando dentro do if cria o ficheiro se for um sucesso volta um valor true se não conseguir envia um valor false)
                if (!Connection.RegisterRouteDataBase(instancia, "ADMINISCAR", usuario, senha))
                {
                    //responsavel por armazenar a respota do cliete
                    DialogResult r;

                    //cria um MessageBox com a opção YES (SIM) NO (NÃO) e CANCEL (CANCELAR) registra a opcão escolhida pelo cliente
                    r = MessageBox.Show("Não foi encontrada a DataBase do sistema Deseja cadastra a DataBase ? \n"
                        + "\nSe você já tinha registros na sua DataBase tem o risco de perder todos os dados"
                        + "\nDeseja cadastrar a DataBase \"ADMINICAR\"?", "ATENÇÃO", MessageBoxButtons.YesNoCancel);


                    if (r == DialogResult.Yes)//se o cliente clicar em YES (SIM) inicia o cadastro da DataBase
                        cadastraDataBase();//cadastra a DataBase

                    else//qualquer outra opção não cria nem uma modificação na DataBase e fecha o programa.
                    {
                        //informa o cliente que não avera nem uma modificação no banco de dados
                        MessageBox.Show("Entre em contato com o desenvolvedor do Sistema.\nFinalizando o Programa...","ATENÇÃO");

                        //se o usuario não quiser instalar a DataBase fecha o programa para evitar erros futuros.
                        Application.Exit();
                    }
                }

                //informa o usuario do cadastro do sistema
                MessageBox.Show("Conexão com o Banco de Dados com sucesso\nO programa sera Fechado para a alteração no sistema.","ATENÇÃO");
                
                //fecha a janela logo apois terminar o cadastro
                Application.Exit();
            }

            else//se a conexão derre erro informa o usuario
                MessageBox.Show("FALHA DE CONEXÃO COM O BANCO DE DADOS!", "ATENÇÃO");

        }


        private void rdtSim_CheckedChanged(object sender, EventArgs e)// deixa os campos de senha e usuario visíveis ao usuário
        {
            //Deixa a area de preenchimento de user e senha invisivel
            txtUsuario.Visible = false;
            txtSenha.Visible =   false;
            lblUsuario.Visible = false;
            lblSenha.Visible = false;
        }

        private void rdtNao_CheckedChanged(object sender, EventArgs e)// deixa os campos de senha e usuario invisíveis ao usuário
        {
            //Deixa a area de preenchimento de user e senha visivel
            txtUsuario.Visible = true;
            txtSenha.Visible = true;
            lblUsuario.Visible = true;
            lblSenha.Visible = true;
        }
        
        static void cadastraDataBase()
        {

            try
            {

                //temos que ter acesso pelo menos a dataBase Master
                SqlConnection conect = new SqlConnection(@"Data Source = " + instancia + "; Initial Catalog = master; Integrated Security = True");

                //comando para criar a DataBase do sistema
                SqlCommand comando = new SqlCommand("CREATE DATABASE " + dataBase, conect);//(#BD1)

                //tenta cadastrar a DataBase no Banco de Dados
                try
                {
                    conect.Open();

                    comando.ExecuteNonQuery();//execulta o comando SqlServer #BD1

                }
                catch (SqlException e )
                {
                    //informa um erro na criaxão da dataBase
                    MessageBox.Show("Erro na conexão com o banco\nE preciso a conexão com a DataBase \" master \" \n\n" + e.Message,"ATENÇÃO");
                }
                finally
                {
                    conect.Close();
                }

                Connection.RegisterRouteDataBase(instancia, "ADMINISCAR", usuario, senha);

                conect.ConnectionString = Connection.Route("ADMINISCAR") ;

                comando = new SqlCommand(/* (#BD2) */
                                    " CREATE TABLE TELEFONE( "
                                    + " COD_TELL INT PRIMARY KEY IDENTITY, "
                                    + " TELL1 VARCHAR(15) NOT NULL, "
                                    + " TELL2 VARCHAR(15)) "
                                    + " CREATE TABLE ENDERECO( "
                                    + " COD_ENDERECO INT PRIMARY KEY IDENTITY, "
                                    + " LOGRADOURO VARCHAR(50), "
                                    + " NUMERO INT, "
                                    + " BAIRRO VARCHAR(50), "
                                    + " CEP VARCHAR(10) NOT NULL, "
                                    + " CIDADE VARCHAR(50), "
                                    + " ESTADO VARCHAR(50)) "
                                    + " CREATE TABLE FUNCIONARIO( "
                                    + " COD_FUNC INT PRIMARY KEY IDENTITY, "
                                    + " NOME_FUNC VARCHAR(50), "
                                    + " CPF_FUNC VARCHAR(15) NOT NULL UNIQUE, "
                                    + " CNH_FUNC VARCHAR(15) NOT NULL UNIQUE, "
                                    + " COD_TELL_FK INT, COD_ENDERECO_FK INT, "
                                    + " FOREIGN KEY(COD_TELL_FK) REFERENCES TELEFONE(COD_TELL), "
                                    + " FOREIGN KEY(COD_ENDERECO_FK) REFERENCES ENDERECO(COD_ENDERECO)) "
                                    + " CREATE TABLE LOGIN_SISTEMA( "
                                    + " EMAIL VARCHAR(50) PRIMARY KEY, "
                                    + " SENHA VARCHAR(10) NOT NULL, "
                                    + " NIVEL_ACESSO INT NOT NULL, "
                                    + " COD_FUNC_FK INT, FOREIGN KEY(COD_FUNC_FK) REFERENCES FUNCIONARIO(COD_FUNC)) "
                                    + " CREATE TABLE CLIENTE( "
                                    + " COD_CLIENTE INT PRIMARY KEY IDENTITY, "
                                    + " NOME_CLIENTE VARCHAR(50), "
                                    + " CPF_CNPJ VARCHAR(20) NOT NULL UNIQUE, "
                                    + " CNH_CLIENTE VARCHAR(15) NOT NULL UNIQUE, "
                                    + " COD_TELL_FK INT, COD_ENDERECO_FK INT, "
                                    + " FOREIGN KEY(COD_TELL_FK) REFERENCES TELEFONE(COD_TELL), "
                                    + " FOREIGN KEY(COD_ENDERECO_FK) REFERENCES ENDERECO(COD_ENDERECO)) "
                                    + " CREATE TABLE CARRO( "
                                    + " COD_CAR INT PRIMARY KEY IDENTITY, "
                                    + " NOME_CAR VARCHAR(30) NOT NULL, "
                                    + " PLACA VARCHAR(10) NOT NULL UNIQUE, "
                                    + " RENAVAM VARCHAR(15) NOT NULL UNIQUE, "
                                    + " MODELO VARCHAR(20) NOT NULL, "
                                    + " CATEGORIA VARCHAR(20) NOT NULL, "
                                    + " COMBUSTIVEL VARCHAR(20), "
                                    + " QUILOMETRAGEM INT NOT NULL, "
                                    + " SITUACAO VARCHAR(20) NOT NULL, "
                                    + " VALOR_DIARIO DECIMAL(8,2) NOT NULL, "
                                    + " VALOR_SEMANAL DECIMAL(8,2) NOT NULL, "
                                    + " VALOR_MENSAL DECIMAL(8,2) NOT NULL, "
                                    + " SOM CHAR(1) NOT NULL, "
                                    + " SOM_BT CHAR(1) NOT NULL, "
                                    + " GPS CHAR(1) NOT NULL) "
                                    + " CREATE TABLE MANUTENCAO( "
                                    + " COD_MANUTENCAO INT PRIMARY KEY IDENTITY, "
                                    + " COD_CAR_FK INT NOT NULL, "
                                    + " DATA_ENTREGA DATE NOT NULL, "
                                    + " DATA_DEVOLUCAO DATE, "
                                    + " RESOLVIDO CHAR(1), "
                                    + " VALOR_MANUTENCAO DECIMAL(8,2), "
                                    + " NOME_EMP VARCHAR(50), "
                                    + " CNPJ VARCHAR(20) NOT NULL, "
                                    + " COD_TELL_FK INT NOT NULL, "
                                    + " COD_ENDERECO_FK INT NOT NULL, "
                                    + " FOREIGN KEY(COD_TELL_FK) REFERENCES TELEFONE(COD_TELL), "
                                    + " FOREIGN KEY(COD_ENDERECO_FK) REFERENCES ENDERECO(COD_ENDERECO), "
                                    + " FOREIGN KEY(COD_CAR_FK) REFERENCES CARRO(COD_CAR)) "
                                    + " CREATE TABLE CARTAO_CRED( "
                                    + " COD_CRED INT PRIMARY KEY IDENTITY, "
                                    + " COD_SEGURANCA_CRED INT NOT NULL, "
                                    + " DT_VALIDADE_CRED VARCHAR(10) NOT NULL, "
                                    + " NUM_CARTAO_CRED INT NOT NULL, "
                                    + " COD_CLIENTE_FK INT NOT NULL UNIQUE, "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE)) "
                                    + " CREATE TABLE CARTAO_DEB( "
                                    + " COD_DEB INT PRIMARY KEY IDENTITY, "
                                    + " COD_SEGURANCA_DEB INT NOT NULL, "
                                    + " DT_VALIDADE_DEB VARCHAR(10) NOT NULL, "
                                    + " NUM_CARTAO_DEB INT NOT NULL, "
                                    + " COD_CLIENTE_FK INT NOT NULL UNIQUE, "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE)) "
                                    + " CREATE TABLE PAGAMENTO( "
                                    + " COD_PAG INT PRIMARY KEY IDENTITY, "
                                    + " VALOR DECIMAL(8,2) NOT NULL, "
                                    + " COD_CRED_FK INT, "
                                    + " COD_DEB_FK INT, "
                                    + " COD_CLIENTE_FK INT NOT NULL, "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE), "
                                    + " FOREIGN KEY(COD_CRED_FK) REFERENCES CARTAO_CRED(COD_CRED), "
                                    + " FOREIGN KEY(COD_DEB_FK) REFERENCES CARTAO_DEB(COD_DEB)) "
                                    + " CREATE TABLE PEDIDO( "
                                    + " COD_PEDIDO INT PRIMARY KEY IDENTITY, "
                                    + " VALOR DECIMAL(8,2) NOT NULL, "
                                    + " TIPO_PEDIDO VARCHAR(10) NOT NULL, "
                                    + " DATA_RETIRADA DATE NOT NULL, "
                                    + " DATA_DEVOLUCAO DATE NOT NULL, "
                                    + " COD_PAG_FK INT NOT NULL UNIQUE, "
                                    + " COD_CAR_FK INT NOT NULL, "
                                    + " COD_CLIENTE_FK INT NOT NULL, "
                                    + " FOREIGN KEY(COD_PAG_FK) REFERENCES PAGAMENTO(COD_PAG), "
                                    + " FOREIGN KEY(COD_CAR_FK) REFERENCES CARRO(COD_CAR), "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE)) "
                                                        , conect);
                conect.Open();

                comando.ExecuteNonQuery();//execulta o comando SqlServer #BD2

                conect.Close();

                //informa o usuario que o cadastro foi bem sucedida
                MessageBox.Show("DataBase ADMINISCAR criado com sucesso","ATENÇÃO");

            }
            catch (SqlException a)
            {
                //informa o usuario que o cadastro teve um erro
                MessageBox.Show("Erro na criação do BD \n" + a.Message + "\n" + banco,"ATENÇÃO");

            }
        }

        private void cbxLocalHost_CheckedChanged(object sender, EventArgs e)
        {
            //deixa a area de preenchimento de instancia invisivel se for celecionado
            lblInstancia.Visible = !cbxLocalHost.Checked;
            txtInstancia.Visible = !cbxLocalHost.Checked;

            //faz automaticamente o sistema se colocar como um Integrated Security
            rdtSim.Checked = cbxLocalHost.Checked;
            rdtNao.Checked = !cbxLocalHost.Checked;
        }
    }
}
