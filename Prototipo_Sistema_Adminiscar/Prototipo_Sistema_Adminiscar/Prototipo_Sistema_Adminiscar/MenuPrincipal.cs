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
    public partial class MenuPrincipal : Form
    {

        static string connect = Connection.Route("ADMINISCAR");

        public int nivel;
        public int cod_func;
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void sairDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //responsavel por atualiza o horario/data do relogio no canto inferior direito da janela do programa
        private void timer1_Tick(object sender, EventArgs e)
        {
            //formato de apresentação do horario/data
            lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy   HH:mm:ss");
        }

        //oque se deve fazer quando a janela for iniciada
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

            if(nivel <= 2)
            {
                //caso o nivel do usuario (funcionario) for menos que nivel 2
                tlpBanco.Visible = false;//não presenta a opção de banco
                tlpFuncionario.Visible = false;//não presenta a opção de funcionario
            }

            string[] campos = new string[] { "NOME_FUNC" };//nome dos campos que iram retonar os dados #A1

            string tabela = "FUNCIONARIO";//nome da tabela

            string comando = "SELECT * FROM "+ tabela +" WHERE COD_FUNC = " + cod_func;//comando de SQL onde que so ira apresentar o nome do funcionario que tiver o cod_func informado

            ArrayList a = Comand.Select.ArryaListFormat(comando, campos, connect);//(#A1)

            lblNomeFunc.Text = a[0].ToString();//nome do funcionario e informado para o sistema e apresentado na Label "lblNomeFunc"
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClienteCadastraAltera cliente = new ClienteCadastraAltera();
            cliente.Show();
        }

        private void btManutencao_Click(object sender, EventArgs e)
        {
            ManutencaoCadastrarAlterar a = new ManutencaoCadastrarAlterar();
            a.Show();
        }

        private void btVeiculo_Click(object sender, EventArgs e)
        {
            VeiculoCadastraAlterar a = new VeiculoCadastraAlterar();
            a.Show();
        }

        private void btDevolucao_Click(object sender, EventArgs e)
        {
            LocacaoDevolucao a = new LocacaoDevolucao();
            a.Show();
        }

        private void btLocacao_Click(object sender, EventArgs e)
        {
            LocacaoCadastra a = new LocacaoCadastra();
            a.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FuncionarioCadastrarAlterar a = new FuncionarioCadastrarAlterar();
            a.Show();
        }

        private void btBanco_Click(object sender, EventArgs e)
        {
            CadastrarBanco a = new CadastrarBanco();
            a.Show();
        }
    }
}
