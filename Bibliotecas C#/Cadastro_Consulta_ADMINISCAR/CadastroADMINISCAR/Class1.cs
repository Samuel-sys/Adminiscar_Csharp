using System;
using System.Collections;
using System.Linq;
using SqlServer;
namespace ADMINISCAR
{
    class Cadastra
    {
        public bool Endereco(string CEP, string numero, string logradouro, string bairro, string cidade, string estado, string RouthConnect)
        {
            string[] campos = new string[] { "COD_ENDERECO" };

            //continua ate retornar o codigo do Endereço
            for (int i = 0; i < 2; i++)
            {
                //tenta pegar o codigo do endereco
                try
                {
                    ArrayList a =
                        Comand.Select.ArryaListFormat("SELECT * FROM ENDERECO WHERE CEP = '"
                        + CEP + "' AND NUMERO = " + numero + " AND LOGRADOURO = '"
                        + logradouro + "' AND BAIRRO = '" + bairro + "' AND CIDADE = '"
                        + cidade + "' AND ESTADO = '" + estado + "'"
                        , campos
                        , RouthConnect);

                    //se não retornar nem um valor esse espaço sera inexiste e então da erro e vai para o espaço catch
                    a[0].ToString();

                    return false;

                }
                catch
                {   
                    //criando a string de comando
                    string comando = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, BAIRRO, CEP, CIDADE, ESTADO) VALUES ("
                    + "'" + logradouro + "'," + numero + ",'" + bairro + "','" + CEP + "','" + cidade + "','" + estado + "')";

                    //inserção dos dados
                    Comand.Insert(comando, RouthConnect);

                    return true;

                }
            }
            return false;
        }

        public void Telefone()
        {

        }
    }
}
