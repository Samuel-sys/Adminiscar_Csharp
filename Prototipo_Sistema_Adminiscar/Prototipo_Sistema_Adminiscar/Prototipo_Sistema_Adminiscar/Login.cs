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
    public partial class Login : Form
    {
        string connect;//#C1

        public Login()
        {
            InitializeComponent();

            connect = Connection.Route("ADMINISCAR");//(#C1)

        }
        //armazena o endereço do banco de dados SqlServer

        private void btLogar_Click(object sender, EventArgs e)
        {
            
            // o nome da tabela que os logins são cadastrados no banco de dados
            string tabela = "LOGIN_SISTEMA";

            //esse campos seram retornado (sera informado) no comando de SELECT #A1
            string[] campos = new string[]{ "EMAIL", "SENHA", "NIVEL_ACESSO", "COD_FUNC_FK"};


            //comoando de SQL de consulta
            string comando = "SELECT * FROM " + tabela + " WHERE EMAIL = '" + mtxtLogin.Text + "' AND SENHA = '" + mtxtSenha.Text + "'";

            //se o usuario e a senha não estiverem cadastrados no banco então não volta valor nem um
            ArrayList login = Comand.Select.ArryaListFormat(comando, campos, connect);//(#A1)

            

            try//se o login foi bem sucedido execulta esse codigo se não #A2
            {
                MenuPrincipal m = new MenuPrincipal();//instanciando o form do menu principal
                m.nivel = int.Parse(login[2].ToString());//informa o nivel de acesso do usuario
                m.cod_func = int.Parse(login[3].ToString());//informa o cod_func do usuario
                m.Show();//abre a janela do menu pricipal
                //fecha a janela de login
                this.Close();

            }
            catch//(#A2)
            {
                //informa que o usuario ou senha esta errada
                MessageBox.Show("Usuário ou senha incorreto","ATENÇÃO");
            }
        }

        private void senha_CheckedChanged(object sender, EventArgs e)
        {
            //Mostra a senha
            if(cbxSenha.Checked == true)
            mtxtSenha.UseSystemPasswordChar = false;

            //esconde a senha
            else
            mtxtSenha.UseSystemPasswordChar = true;

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
