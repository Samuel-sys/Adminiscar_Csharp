using Prototipo_Sistema_Adminiscar.Models;
using SqlServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    class ClienteADO
    {
        static string connect = Connection.Route("ADMINISCAR");

        static public bool cadastroCliente(Cliente cliente, Endereco end, Telefone tell)
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

                string comando = "INSERT INTO CLIENTE(NOME_cliente, cpf_cnpj, cnh_cliente, COD_TELL_FK, COD_ENDERECO_FK)" +
                    "VALUES('" + cliente.nome_cliente + "','" + cliente.cpf_cnpj + "','" + cliente.cnh_cliente + "'," + codTel + "," + codEndereco + ")";

                Comand.Insert(comando, connect);

            }

            if (!erros)
                MessageBox.Show("Erro na geração do cadastro do Cliente", "Alerta - Erro no cadastro");
            else
                MessageBox.Show("Cliente cadastrado com sucesso");

            return erros;
        }
        

        internal static bool atualizaCliente(Cliente cliente, Endereco endereco, Telefone tell)
        {


            cliente.cod_endereco_fk = int.Parse(EnderecoADO.cadastrarEndereco(endereco));
            cliente.cod_tell_fk = int.Parse(TelefoneADO.cadastraTelefone(tell));

            //atualiza o funcionario que tiver o mesmo CPF
            return Comand.Update("update CLIENTE set NOME_CLIENTE = '" + cliente.nome_cliente 
                + "', CPF_CNPJ = '" + cliente.cpf_cnpj 
                + "', CNH_CLIENTE = '" + cliente.cnh_cliente
                + "', COD_TELL_FK = " + cliente.cod_tell_fk
                + ", COD_ENDERECO_FK = " + cliente.cod_endereco_fk
                + " where CPF_CNPJ = '" + cliente.cpf_cnpj
                + "'", connect);
        }

        internal static Cliente consultaCpfCnpj(string CpfCnpj)
        {
            string[] campo = new string[] { "COD_ENDERECO_FK", "COD_TELL_FK", "nome_cliente", "cod_cliente", "cnh_cliente" };

            //comando SQL Select Registrando os dados na ArrayList "a" (funcionario)
            ArrayList cliente = Comand.Select.ArryaListFormat(
                "SELECT * FROM cliente WHERE CPF_cnpj = '" + CpfCnpj + "'"
                , campo, connect);

            try
            {
                return new Cliente()
                {
                    cod_endereco_fk = int.Parse(cliente[0].ToString()),
                    cod_tell_fk = int.Parse(cliente[1].ToString()),
                    nome_cliente = cliente[2].ToString(),
                    cod_cliente = int.Parse(cliente[3].ToString()),
                    cnh_cliente = cliente[4].ToString(),
                    cpf_cnpj = CpfCnpj,

                };
            }
            catch
            {

                return null;
            }
        }
    }
}

