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
    public partial class Splash : Form
    {

        string connect = Connection.Route("ADMINISCAR");

        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //se o teste de conexão for um sucesso carrega a barra de loading se não da erro
            if (Connection.ConnectionTest(connect))
            {
                if (BarraDeCarregamento.Value < 40)
                {
                    BarraDeCarregamento.Value = BarraDeCarregamento.Value + 2;
                }
                else
                {
                    if (BarraDeCarregamento.Value < 99)
                    {
                        BarraDeCarregamento.Value = BarraDeCarregamento.Value + 2;
                    }
                    else
                    {
                        timer1.Enabled = false;
                        //tenta ver se tem algum email registrado
                        try
                        {



                            string[] campos = new string[] { "EMAIL" };// campo a ser retornado

                            //salva os valores da pesquisa da arrayList "a"
                            ArrayList a = Comand.Select.ArryaListFormat("SELECT * FROM LOGIN_SISTEMA", campos, connect);
                            a[0].ToString();// se testa se existir essa posição o programa continua se não da erro e sai para o catch



                            this.Visible = false;//faz a janela de splash ficar invisivel
                            Login c = new Login();// abre a janela de login do funcionario
                            c.Show();
                        }
                        catch// se der erro significa que não tem nem um funcionario cadastrado ai abre a janela de cadastro
                        {

                            Login b = new Login();// abre a janela de login do funcionario
                            b.Show();

                            FuncionarioCadastrarAlterar a = new FuncionarioCadastrarAlterar();
                            a.Show();

                            this.Visible = false;//faz a janela de splash ficar invisivel
                        }
                    }
                }
            }
            else
            {
                timer1.Enabled = false;
                //informa o erro de conexão ao usuario
                MessageBox.Show("Erro de conecxão com o Banco de dados!");

                //esconde a tela de splash
                this.Visible = false;

                //abre a janela de cadastro de banco de dados
                CadastrarBanco a = new CadastrarBanco();
                a.Show();
            }
        }
    }
}
