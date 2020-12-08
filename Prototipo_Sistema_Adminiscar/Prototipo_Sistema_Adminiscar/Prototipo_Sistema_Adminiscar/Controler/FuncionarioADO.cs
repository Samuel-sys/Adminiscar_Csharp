using Prototipo_Sistema_Adminiscar.Models;
using System.Linq;
using SqlServer;
using System.Windows.Forms;
using System.Collections;
using System;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    class FuncionarioADO
    {
        static string connect = Connection.Route("ADMINISCAR");

        static public Funcionario consultaCodFunc(int cod_func)
        {
            string[] campo = new string[] { "COD_ENDERECO_FK", "COD_TELL_FK", "NOME_FUNC", "CPF_FUNC", "CNH_FUNC" };

            //comando SQL Select Registrando os dados na ArrayList "a" (funcionario)
            ArrayList func = Comand.Select.ArryaListFormat(
                "SELECT * FROM FUNCIONARIO WHERE COD_FUNC = " + cod_func
                , campo, connect);

            try
            {
                return new Funcionario()
                {
                    cod_endereco_fk = int.Parse(func[0].ToString()),
                    cod_tell_fk = int.Parse(func[1].ToString()),
                    nome_func = func[2].ToString(),
                    cpf_func = func[3].ToString(),
                    cnh_func = func[4].ToString(),
                    func_cod = cod_func,

                };
            }
            catch 
            {

                return null;
            }
        }

        static internal Funcionario consultaCPF(string cpf)
        {
            string[] campo = new string[] { "COD_ENDERECO_FK", "COD_TELL_FK", "NOME_FUNC", "COD_FUNC", "CNH_FUNC" };

            //comando SQL Select Registrando os dados na ArrayList "a" (funcionario)
            ArrayList func = Comand.Select.ArryaListFormat(
                "SELECT * FROM FUNCIONARIO WHERE CPF_FUNC = '" + cpf + "'"
                , campo, connect);

            try
            {
                return new Funcionario()
                {
                    cod_endereco_fk = int.Parse(func[0].ToString()),
                    cod_tell_fk = int.Parse(func[1].ToString()),
                    nome_func = func[2].ToString(),
                    func_cod = int.Parse(func[3].ToString()),
                    cnh_func = func[4].ToString(),
                    cpf_func = cpf,

                };
            }
            catch
            {

                return null;
            }
        }

        static public bool cadastroFunc(Funcionario func, Endereco end, Telefone tell, Login_sistema login)
        {
            int codEndereco = 0;
            int codTel = 0;
            bool erros = true;

            if (erros)
            {
                try
                {
                    codEndereco = int.Parse(EnderecoADO.cadastrarEndereco(end));
                }
                catch
                {
                    EnderecoADO.erroCadastro(end);
                    erros = false;
                }
            }

            if (erros)
            {
                try
                {
                    codTel = int.Parse(TelefoneADO.cadastraTelefone(tell));
                }
                catch
                {
                    TelefoneADO.erroCadastro(tell);
                    erros = false;
                } 
            }

            if (erros)
            {

                string comando = "INSERT INTO FUNCIONARIO(NOME_FUNC, CPF_FUNC, CNH_FUNC, COD_TELL_FK, COD_ENDERECO_FK)" +
                    "VALUES('" + func.nome_func + "','" + func.cpf_func + "','" + func.cnh_func + "'," + codTel + "," + codEndereco + ")";

                Comand.Insert(comando, connect);

                string cod_func = Comand.Select.StringFormat(
                    "SELECT * FROM FUNCIONARIO WHERE CPF_FUNC ='" + func.cpf_func + "'"
                    , "COD_FUNC", connect);

                try
                {
                    login.cod_func_FK = int.Parse(cod_func);

                    LoginADO.Cadastralogin(login);
                }
                catch
                {
                    erros = false;
                }
            }

            if (!erros)
                MessageBox.Show("Erro na geração do cadastro do funcionario", "Alerta - Erro na cadastro");
            else
                MessageBox.Show("Funcionario cadastrado com sucesso");

            return erros;
        }

        static internal bool atualizaFunc(Funcionario func, Endereco endereco, Telefone tel, Login_sistema login)
        {

            func.cod_endereco_fk = int.Parse(EnderecoADO.cadastrarEndereco(endereco));
            func.cod_tell_fk = int.Parse(TelefoneADO.cadastraTelefone(tel));
            login.cod_func_FK = consultaCPF(func.cpf_func).func_cod;

            //esse comando ira excluir o login anterior e cadastra o novo login com as alterações criadas
            LoginADO.Cadastralogin(login);

            //atualiza o funcionario que tiver o mesmo CPF
            return Comand.Update(
                "update FUNCIONARIO set "
                + "NOME_FUNC = '" + func.nome_func + "',"
                + " CPF_FUNC = '" + func.cpf_func + "',"
                + " CNH_FUNC = '" + func.cnh_func + "',"
                + " COD_ENDERECO_FK = " + func.cod_endereco_fk + ","
                + " COD_TELL_FK = " + func.cod_tell_fk
                + " where CPF_FUNC = '" + func.cpf_func + "'"
                , connect);
        }
    }
}
