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
    public partial class CadastrarBanco : Form
    {
        public CadastrarBanco()
        {
            InitializeComponent();
        }

        private void btCadastarBD_Click(object sender, EventArgs e)
        {
            //so executa o comando se tiver com todos os campos preenchidos
            if (txtInstancia.Text != "" && rdtSim.Checked == true
                && txtUsuario.Text != "" && txtSenha.Text != "")
                CadastraRouteDataBase();

            //so executa o comando se tiver com todos os campos preenchidos
            else if (txtInstancia.Text != "" && rdtSim.Checked == false)
                CadastraRouteDataBase();

            else// se faltar algum campo a ser preenchido informa que tem que preencher todos os campos
                MessageBox.Show("Erro preencha todos os campos!");
        }

        private void CadastraRouteDataBase()
        {
            //nome da dataBase
            string dataBase = "ADMINISCAR";

            string instancia = txtInstancia.Text;//nome da dataBase

            //se o cliente pasar um valor nulo/vazio ou informa que e local informa que o endereço do banco e localhost
                instancia = instancia == "localhost" ? @"localhost\SQLEXPRESS": instancia;

            //se o cliente tiver abilitado o campo de usuario e senha registra o valor se não registra null
            string usuario = rdtSim.Checked ? txtUsuario.Text : null;

            //se o cliente tiver abilitado o campo de usuario e senha registra o valor se não registra null
            string senha = rdtSim.Checked ? txtSenha.Text : null;

            //endereço do banco e da DataBase
            string banco = @"Data Source = " + instancia + "; Initial Catalog = " + dataBase;

            //Caso o não se de senha ou usuarioa vai se entender que e um caso de Integrated Securyt caso o contrario sera cadastrado
            banco += usuario == null && senha == null ? @"; Integrated Security = True" : "; USER ID = " + usuario + "; PASSWORD = " + senha + " ;";

            //teste de conexão
            if (Connection.ConnectionTest(banco))
            {
                Connection.RegisterBD(instancia, "ADMINISCAR", usuario, senha);//se a conexão for um sucesso cadastra o endereço no banco
                MessageBox.Show("Cadastrado com sucesso\nO programa sera Fechado para a alteração no sistema.","ATENÇÃO");
                //fecha a janela logo apois terminar o cadastro
                Application.Exit();
            }

            else//se a conexão derre erro informa o usuario
                MessageBox.Show("FALHA DE CONEXÃO COM O BANCO DE DADOS!", "ATENÇÃO");

        }


        private void rdtSim_CheckedChanged(object sender, EventArgs e)// deixa os campos de senha e usuario visíveis ao usuário
        {
            txtUsuario.Visible = true;
            txtSenha.Visible = true;
            lblInstancia.Visible = true;
            lblSenha.Visible = true;
        }

        private void rdtNao_CheckedChanged(object sender, EventArgs e)// deixa os campos de senha e usuario invisíveis ao usuário
        {
            txtUsuario.Visible = false;
            txtSenha.Visible =   false;
            lblInstancia.Visible = false;
            lblSenha.Visible = false;
        }
    }
}
