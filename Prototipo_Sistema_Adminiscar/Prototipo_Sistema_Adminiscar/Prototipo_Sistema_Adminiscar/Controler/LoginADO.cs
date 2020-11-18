using Prototipo_Sistema_Adminiscar.Models;
using SqlServer;
using System.Collections;
using System;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    internal class LoginADO
    {
        static string connect = Connection.Route("ADMINISCAR");

        //cadastro
        static public bool Cadastralogin(Login_sistema login)
        {
            //deleta qualquer login que tenha cadastrado com o mesmo cod_fun já cadastrado
            Comand.Delete("DELETE FROM LOGIN_SISTEMA WHERE COD_FUNC_FK = " + login.cod_func_FK ,connect);
            //esse metodo so retorna um valor bool em nada interfere na ação do sistema

            string comando = "insert into LOGIN_SISTEMA (EMAIL, SENHA, NIVEL_ACESSO, COD_FUNC_FK)"
                + " values ('" + login.email + "','" + login.senha + "','" + login.nivel_acesso + "','" + login.cod_func_FK + "')";

            return Comand.Insert(comando, connect);

        }

        //alteração
        static public bool Updatelogin(Login_sistema login)
        {
            string comando = "insert into LOGIN_SISTEMA (EMAIL, SENHA, NIVEL_ACESSO, COD_FUNC_FK)"
                + " values ('" + login.email + "','" + login.senha + "','" + login.nivel_acesso + "','" + login.cod_func_FK + "')";

            return Comand.Insert(comando, connect);

        }

        //consultas
        static public Login_sistema consultaLogin(string login)
        {
            string[] campos = new string[] { "SENHA", "NIVEL_ACESSO", "COD_FUNC_FK" };

            ArrayList loginSistema = Comand.Select.ArryaListFormat(
                "SELECT * FROM LOGIN_SISTEMA WHERE EMAIL = '" + login + "'"
                , campos, connect);

            try
            {
                return new Login_sistema()
                {
                    email = login,
                    senha = loginSistema[0].ToString(),
                    nivel_acesso = int.Parse(loginSistema[1].ToString()),
                    cod_func_FK = int.Parse(loginSistema[2].ToString())
                };
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        static internal Login_sistema consultaCodFunc(int func_cod)
        {
            string[] campos = new string[] { "SENHA", "NIVEL_ACESSO", "email" };

            ArrayList loginSistema = Comand.Select.ArryaListFormat(
                "SELECT * FROM LOGIN_SISTEMA WHERE cod_func_fk = " + func_cod 
                , campos, connect);

            try
            {
                return new Login_sistema()
                {
                    senha = loginSistema[0].ToString(),
                    nivel_acesso = int.Parse(loginSistema[1].ToString()),
                    email = loginSistema[2].ToString(),
                    cod_func_FK = func_cod
                };
            }
            catch
            {

                return null;
            }
        }
    }
}