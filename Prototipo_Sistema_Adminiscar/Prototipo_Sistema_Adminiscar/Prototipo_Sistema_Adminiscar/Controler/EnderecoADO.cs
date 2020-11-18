using SqlServer;
using System.Windows.Forms;
using System;
using Prototipo_Sistema_Adminiscar.Models;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    class EnderecoADO
    {
        static private string connect = Connection.Route("ADMINISCAR");//informa o endereço de conexão com o banco de dados

        static public string cadastrarEndereco(Models.Endereco end)
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

            if (!Comand.Select.BoolFormat(comandoConsulta, connect)) //se não tiver em um cadastrado ele cadastra
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
                Comand.Insert(comandoCadastro, connect); //esse comando retorna true se a inserção for um sucesso se não retorna um valor false

            }
            
            //segundo teste de condição caso o cadastro tenha sido um sucesso
            if(Comand.Select.BoolFormat(comandoConsulta, connect))
            {
                return Comand.Select.StringFormat(comandoConsulta, "COD_ENDERECO", connect);
            }

            return null;//caso não encontre o endereco ele retorna nulo

        }

        static public void erroCadastro(Models.Endereco end)
        {

            //comando para inserir dados na tabela endereço
            string comando = "INSERT INTO ENDERECO "
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

            string erro = Comand.ErroComand(comando, connect);

            if(erro != null)
            {
                MessageBox.Show(erro, "Alerta - erro no cadastro do Endereço");
            }
        }

        // Esse metodo tem a função de entregar apenas o valor do codigo do endereço
        static public string CodEnd(Models.Endereco end)
        {
            //string responsavel por trazer o valor do cod_endereco
            string a =
                Comand.Select.StringFormat(//comando da biblioteca "SqlServer"
                "SELECT * FROM ENDERECO WHERE CEP = '" + end.end_CEP
                + "' AND NUMERO = " + end.end_numero
                + " AND LOGRADOURO = '" + end.end_logradouro
                + "' AND BAIRRO = '" + end.end_bairro
                + "' AND CIDADE = '" + end.end_cidade
                + "' AND ESTADO = '" + end.end_estado + "'"
                , "COD_ENDERECO", connect);

            return a;//retorna o valor da consulta
        }

        static public Endereco consultaCodEndereco(int cod_endereco)
        {
            var campos = new string[] {"logradouro", "numero", "bairro", "CEP", "cidade", "estado"};

            var end = Comand.Select.ArryaListFormat("SELECT * FROM ENDERECO", campos, connect);

            try
            {
                return new Endereco()
                {
                    end_cod = cod_endereco,
                    end_logradouro = end[0].ToString(),
                    end_numero = int.Parse(end[1].ToString()),
                    end_bairro = end[2].ToString(),
                    end_CEP = end[3].ToString(),
                    end_cidade = end[4].ToString(),
                    end_estado = end[5].ToString()
                };
            }
            catch 
            {
                return null;
            }
        
        }
    }
}