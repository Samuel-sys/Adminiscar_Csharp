using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace BD_ADMINISCAR
{
    class Program
    {
        static string database = "ADMINISCAR";
        static string instancia = "";

        static void Main(string[] args)
        {

            string func = "";
            Console.Write("instancia:");
            instancia = Console.ReadLine();
            if (instancia == "" || instancia == "localhost")
                instancia = @"localhost\SQLEXPRESS";

            do
            {
                string msg = "";

                Console.Clear();

                msg = func == "" ? "Criar (1)\nExcluir (2)\nExcluir e depois Criar(3)\nfunção: " : "\aVALOR INVALIDO\n\nInstancia: " + instancia + " \n\nCriar(1)\nExcluir(2)\nExcluir e depois Criar(3)\nfunção: ";

                Console.Write(msg);
                func = Console.ReadLine();

            } while (func != "1" && func != "2" && func != "3");

            switch (func)
            {
                case "1":
                    cadastraBD();
                    break;
                case "2":
                    deletaBD();
                    break;
                case "3":
                    deletaBD();
                    cadastraBD();
                    break;
            }
            Console.ReadKey();
        }
        static void deletaBD()
        {
            try
            {
                SqlConnection conect = new SqlConnection(@"Data Source = " + instancia + " ; Initial Catalog = master; Integrated Security = True");

                SqlCommand comando = new SqlCommand("DROP DATABASE ADMINISCAR", conect);

                conect.Open();
                comando.ExecuteNonQuery();
                conect.Close();

                Console.WriteLine("DataBase ADMINISCAR excluida");

            }
            catch (SqlException a)
            {

                Console.WriteLine("Erro " + a.Message);
            }
        }
        static void cadastraBD()
        {
            try
            {
                SqlConnection conect = new SqlConnection(@"Data Source = " + instancia + "; Initial Catalog = master; Integrated Security = True");

                SqlCommand comando = new SqlCommand("CREATE DATABASE " + database, conect);

                conect.Open();

                comando.ExecuteNonQuery();

                conect.Close();

                conect = new SqlConnection(@"Data Source = " + instancia + " ; Initial Catalog = " + database + " ; Integrated Security = True");

                comando = new SqlCommand(
                                    " CREATE TABLE TELEFONE( "
                                    + " COD_TELL INT PRIMARY KEY IDENTITY, "
                                    + " TELL1 VARCHAR(15) NOT NULL, "
                                    + " TELL2 VARCHAR(15)) "
                                    + " CREATE TABLE ENDERECO( "
                                    + " COD_ENDERECO INT PRIMARY KEY IDENTITY, "
                                    + " LOGRADOURO VARCHAR(50), "
                                    + " NUMERO INT, "
                                    + " BAIRRO VARCHAR(50), "
                                    + " CEP VARCHAR(10) NOT NULL, "
                                    + " CIDADE VARCHAR(50), "
                                    + " ESTADO VARCHAR(50)) "
                                    + " CREATE TABLE FUNCIONARIO( "
                                    + " COD_FUNC INT PRIMARY KEY IDENTITY, "
                                    + " NOME_FUNC VARCHAR(50), "
                                    + " CPF_FUNC VARCHAR(15) NOT NULL UNIQUE, "
                                    + " CNH_FUNC VARCHAR(15) NOT NULL UNIQUE, "
                                    + " COD_TELL_FK INT, COD_ENDERECO_FK INT, "
                                    + " FOREIGN KEY(COD_TELL_FK) REFERENCES TELEFONE(COD_TELL), "
                                    + " FOREIGN KEY(COD_ENDERECO_FK) REFERENCES ENDERECO(COD_ENDERECO)) "
                                    + " CREATE TABLE LOGIN_SISTEMA( "
                                    + " EMAIL VARCHAR(50) PRIMARY KEY, "
                                    + " SENHA VARCHAR(10) NOT NULL, "
                                    + " NIVEL_ACESSO INT NOT NULL, "
                                    + " COD_FUNC_FK INT, FOREIGN KEY(COD_FUNC_FK) REFERENCES FUNCIONARIO(COD_FUNC)) "
                                    + " CREATE TABLE CLIENTE( "
                                    + " COD_CLIENTE INT PRIMARY KEY IDENTITY, "
                                    + " NOME_CLIENTE VARCHAR(50), "
                                    + " CPF_CNPJ VARCHAR(20) NOT NULL UNIQUE, "
                                    + " CNH_CLIENTE VARCHAR(15) NOT NULL UNIQUE, "
                                    + " COD_TELL_FK INT, COD_ENDERECO_FK INT, "
                                    + " FOREIGN KEY(COD_TELL_FK) REFERENCES TELEFONE(COD_TELL), "
                                    + " FOREIGN KEY(COD_ENDERECO_FK) REFERENCES ENDERECO(COD_ENDERECO)) "
                                    + " CREATE TABLE CARRO( "
                                    + " COD_CAR INT PRIMARY KEY IDENTITY, "
                                    + " NOME_CAR VARCHAR(30) NOT NULL, "
                                    + " PLACA VARCHAR(10) NOT NULL UNIQUE, "
                                    + " RENAVAM VARCHAR(15) NOT NULL UNIQUE, "
                                    + " MODELO VARCHAR(20) NOT NULL, "
                                    + " CATEGORIA VARCHAR(20) NOT NULL, "
                                    + " COMBUSTIVEL VARCHAR(20), "
                                    + " QUILOMETRAGEM INT NOT NULL, "
                                    + " SITUACAO VARCHAR(20) NOT NULL, "
                                    + " VALOR_DIARIO MONEY NOT NULL, "
                                    + " VALOR_SEMANAL MONEY NOT NULL, "
                                    + " VALOR_MENSAL MONEY NOT NULL, "
                                    + " SOM CHAR(1) NOT NULL, "
                                    + " SOM_BT CHAR(1) NOT NULL, "
                                    + " GPS CHAR(1) NOT NULL) "
                                    + " CREATE TABLE MANUTENCAO( "
                                    + " COD_MANUTENCAO INT PRIMARY KEY IDENTITY, "
                                    + " COD_CAR_FK INT NOT NULL, "
                                    + " DATA_ENTREGA DATE NOT NULL, "
                                    + " DATA_DEVOLUCAO DATE, "
                                    + " RESOLVIDO CHAR(1), "
                                    + " VALOR_MANUTENCAO MONEY, "
                                    + " NOME_EMP VARCHAR(50), "
                                    + " CNPJ VARCHAR(20) NOT NULL, "
                                    + " COD_TELL_FK INT NOT NULL, "
                                    + " COD_ENDERECO_FK INT NOT NULL, "
                                    + " FOREIGN KEY(COD_TELL_FK) REFERENCES TELEFONE(COD_TELL), "
                                    + " FOREIGN KEY(COD_ENDERECO_FK) REFERENCES ENDERECO(COD_ENDERECO), "
                                    + " FOREIGN KEY(COD_CAR_FK) REFERENCES CARRO(COD_CAR)) "
                                    + " CREATE TABLE CARTAO_CRED( "
                                    + " COD_CRED INT PRIMARY KEY IDENTITY, "
                                    + " COD_SEGURANCA_CRED INT NOT NULL, "
                                    + " DT_VALIDADE_CRED VARCHAR(10) NOT NULL, "
                                    + " NUM_CARTAO_CRED INT NOT NULL, "
                                    + " COD_CLIENTE_FK INT NOT NULL UNIQUE, "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE)) "
                                    + " CREATE TABLE CARTAO_DEB( "
                                    + " COD_DEB INT PRIMARY KEY IDENTITY, "
                                    + " COD_SEGURANCA_DEB INT NOT NULL, "
                                    + " DT_VALIDADE_DEB VARCHAR(10) NOT NULL, "
                                    + " NUM_CARTAO_DEB INT NOT NULL, "
                                    + " COD_CLIENTE_FK INT NOT NULL UNIQUE, "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE)) "
                                    + " CREATE TABLE PAGAMENTO( "
                                    + " COD_PAG INT PRIMARY KEY IDENTITY, "
                                    + " VALOR MONEY NOT NULL, "
                                    + " COD_CRED_FK INT, "
                                    + " COD_DEB_FK INT, "
                                    + " COD_CLIENTE_FK INT NOT NULL, "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE), "
                                    + " FOREIGN KEY(COD_CRED_FK) REFERENCES CARTAO_CRED(COD_CRED), "
                                    + " FOREIGN KEY(COD_DEB_FK) REFERENCES CARTAO_DEB(COD_DEB)) "
                                    + " CREATE TABLE PEDIDO( "
                                    + " COD_PEDIDO INT PRIMARY KEY IDENTITY, "
                                    + " VALOR MONEY NOT NULL, "
                                    + " TIPO_PEDIDO VARCHAR(10) NOT NULL, "
                                    + " DATA_RETIRADA DATE NOT NULL, "
                                    + " DATA_DEVOLUCAO DATE NOT NULL, "
                                    + " COD_PAG_FK INT NOT NULL UNIQUE, "
                                    + " COD_CAR_FK INT NOT NULL, "
                                    + " COD_CLIENTE_FK INT NOT NULL, "
                                    + " FOREIGN KEY(COD_PAG_FK) REFERENCES PAGAMENTO(COD_PAG), "
                                    + " FOREIGN KEY(COD_CAR_FK) REFERENCES CARRO(COD_CAR), "
                                    + " FOREIGN KEY(COD_CLIENTE_FK) REFERENCES CLIENTE(COD_CLIENTE)) "
                                                        , conect);
                conect.Open();

                comando.ExecuteNonQuery();

                conect.Close();

                Console.WriteLine("DataBase ADMINISCAR criado com sucesso");

            }
            catch (SqlException a)
            {

                Console.WriteLine("Erro na criação do BD \n" + a.Message);
            }


        }
    }
}