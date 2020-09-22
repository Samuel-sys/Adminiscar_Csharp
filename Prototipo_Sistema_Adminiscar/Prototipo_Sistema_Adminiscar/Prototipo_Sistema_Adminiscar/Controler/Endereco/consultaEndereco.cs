using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using SqlServer;

namespace Prototipo_Sistema_Adminiscar.Controler.Endereco
{
    class consultaEndereco
    {
        string connect = Connection.Route("ADMINISCAR");//informa o endereço de conexão com o banco de dados

        /// <summary>
        /// Esse metodo tem a função de entregar apenas o valor do codigo do endereço
        /// <para>se retornar um valor vazio significa que não existe um cadastro com os dados fornecidos</para>
        /// </summary>
        /// <param name="end">
        /// objeto da classe endereco tem que ter os dados de 
        /// <para>
        /// end_numero, end_logradouro, end_bairro, end_cidade, end_estado
        /// </para>
        /// </param>
        /// <returns></returns>
        public string CodEnd(Models.Endereco end)
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
    }
}