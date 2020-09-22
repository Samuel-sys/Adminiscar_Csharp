using SqlServer;
using System.Collections;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    class cadastroEndereco
    {
        string connect = Connection.Route("ADMINISCAR");//informa o endereço de conexão com o banco de dados

        /// <summary>
        /// </summary>
        /// <param name="end">
        /// objeto com os dados do endereço a ser pesquisado
        /// </param>
        /// <returns></returns>
        public bool cadastrarEndereco(Models.Endereco end)
        {

            //comando para a verificação se existe algum cadastro com o endereço desejado 
            //(evita redundancia no banco de dados cadastrando mais de um endereço igual no Banco de Dados)
            string comandoConsulta = "SELECT * FROM ENDERECO WHERE CEP = '" + end.end_CEP
                    + "' AND NUMERO = " + end.end_numero
                    + " AND LOGRADOURO = '" + end.end_logradouro
                    + "' AND BAIRRO = '" + end.end_bairro
                    + "' AND CIDADE = '" + end.end_cidade
                    + "' AND ESTADO = '" + end.end_estado + "'";
            /*
             * não e recomendado cadastra mais de um endereço 
             * com os mesmos dados por isso se faz a consulta 
             * primeiro para evitar a redundancia
             */

            if (!Comand.Select.BoolFormat(comandoConsulta, connect))
            {

                //comando para inserir dados na tabela endereço
                string comandoCadastro = "INSERT INTO ENDERECO "
                    + " (LOGRADOURO,"
                    + " NUMERO,"
                    + " BAIRRO,"
                    + " CEP,"
                    + " CIDADE,"
                    + " ESTADO)"
                    + " VALUES('"
                    + end.end_logradouro + "',"
                    + end.end_numero + ",'"
                    + end.end_bairro + "','"
                    + end.end_CEP + "','"
                    + end.end_cidade + "','"
                    + end.end_estado + "')";

                //insere a linha no banco (caso tenha algum erro na inserção no banco retorna false e sera perciso execultar um comando que apresente o erro)
                return Comand.Insert(comandoCadastro, connect); //esse comando retorna true se a inserção for um sucesso se não retorna um valor false

            }
            else
                return false;//se tiver um endereço com esse dados cadastrados ele retorna false para não inserir 2 linha no comando com o mesmo valor

        }

        public void erroCadastro()
        {

        }


    }
}