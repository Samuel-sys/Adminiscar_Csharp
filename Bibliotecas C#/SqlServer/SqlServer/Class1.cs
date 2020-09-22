using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;


namespace SqlServer
{

    /// <summary>
    /// Essa classe tera os comando basicos de SqlServer como Insert, Update, Delete e Select
    /// o select entrega os valores pesquisados em mais de uma forma
    /// </summary>
    public class Comand
    {
        /// <summary>
        /// Retona um valor bool (true, false) informando se foi ou não cadastrado os dados no banco de dados
        /// </summary>
        /// 
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer 
        /// <para>"INSERT INTO tabela (coluna1,coluna2,coluna3...) VALUES ('valor1, valor2, valor3...)" </para>
        /// </param>
        /// 
        /// <param name="connect">
        /// <para> Endereço do banco de dados </para>
        /// Caso não passe o usuario e senha
        /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
        /// <para>Caso passe o usuario e senha 
        /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
        /// </param>
        /// <returns></returns>
        public static bool Insert(string comando, string connect)
        {
            if (comando.Substring(0, 6).ToUpper() == "INSERT")
            {
                //ponte de conexão
                SqlConnection conn = new SqlConnection(connect);

                //ponte de comando SqlServer
                SqlCommand com = new SqlCommand(comando, conn);

                //tentando executar o comando SqlServer
                try
                {
                    //abrindo o banco de dados
                    conn.Open();
                    com.ExecuteNonQuery();

                    //fechando o banco de dados
                    conn.Close();

                    //retorna o valor de verdadeiro informando que execução do comando SqlServer foi um sucesso
                    return true;
                }
                catch
                {
                    //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                    return false;
                }
                finally
                {
                    //fechando o banco de dados
                    conn.Close();
                }
            }
            else
            {
                //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                return false;
            }
        }

        /// <summary>
        /// Retona um valor bool (true, false) informando se foi ou não alterado os dados no banco de dados
        /// </summary>
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer 
        /// <para>"UPDATE tabela SET alteração WHERE condição" </para>
        /// </param>
        /// <param name="connect">
        /// <para> Endereço do banco de dados </para>
        /// Caso não passe o usuario e senha
        /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
        /// <para>Caso passe o usuario e senha 
        /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
        /// </param>
        /// <returns></returns>
        public static bool Update(string comando, string connect)
        {

            if (comando.Substring(0, 6).ToUpper() == "UPDATE")
            {

                //ponte de conexão
                SqlConnection conn = new SqlConnection(connect);

                //ponte de comando SqlServer
                SqlCommand com = new SqlCommand(comando, conn);

                //tentando executar o comando SqlServer
                try
                {
                    //abrindo o banco de dados
                    conn.Open();
                    com.ExecuteNonQuery();

                    //fechando o banco de dados
                    conn.Close();

                    //retorna o valor de verdadeiro informando que execução do comando SqlServer foi um sucesso
                    return true;
                }
                catch
                {
                    //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                    return false;
                }
                finally
                {
                    //fechando o banco de dados
                    conn.Close();
                }
            }
            else
            {
                //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                return false;
            }
        }

        /// <summary>
        /// Retorna um valor bool (true, false) informando se foi ou não deletado os dados no banco de dados
        /// </summary>
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer 
        /// <para>"DELETE tabela WHERE condição" </para>
        /// </param>
        /// <param name="connect">
        /// <para> Endereço do banco de dados </para>
        /// Caso não passe o usuario e senha
        /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
        /// <para>Caso passe o usuario e senha 
        /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
        /// </param>
        /// <returns></returns>
        public static bool Delete(string comando, string connect)
        {
            if (comando.Substring(0, 6).ToUpper() == "DELETE")
            {
                //ponte de conexão
                SqlConnection conn = new SqlConnection(connect);

                //ponte de comando SqlServer
                SqlCommand com = new SqlCommand(comando, conn);

                //tentando executar o comando SqlServer
                try
                {
                    //abrindo o banco de dados
                    conn.Open();
                    com.ExecuteNonQuery();

                    //fechando o banco de dados
                    conn.Close();

                    //retorna o valor de verdadeiro informando que execução do comando SqlServer foi um sucesso
                    return true;
                }
                catch
                {
                    //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                    return false;
                }
                finally
                {
                    //fechando o banco de dados
                    conn.Close();
                }
            }
            else
            {
                //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                return false;
            }
        }

        /// <summary>
        /// Retorna um valor string informando o erro na execução
        /// <para>caso não tenha erro na execulção do comando retorna um valor nulo alem de execultar o comando que foi pedido</para>
        /// </summary>
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer
        /// </param>
        /// <param name="connect">
        /// <para> Endereço do banco de dados </para>
        /// Caso não passe o usuario e senha
        /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
        /// <para>Caso passe o usuario e senha 
        /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
        /// </param>
        /// <returns></returns>
        public static string ErroComand(string comando, string connect)
        {
            //ponte de conexão
            SqlConnection conn = new SqlConnection(connect);

            //ponte de comando SqlServer
            SqlCommand com = new SqlCommand(comando, conn);

            //string que irá guardar a menssagem que informa o erro na execulção do comando
            string resposta;

            //tentando executar o comando SqlServer
            try
            {
               //abrindo o banco de dados
                conn.Open();
                com.ExecuteNonQuery();

                //fechando o banco de dados
                conn.Close();

                //informa teve erro na execulção do comando
                resposta = null;

            }
            catch (SqlException erro)
            {
                //registra o erro na string de resposta e informa o erro na execulção do comando
                resposta = erro.Message;
            }
            finally
            {
                //fechando o banco de dados
                conn.Close();
            }


            //retorna a informando qual o erro do comando se não tiver erro retorna nulo
            return resposta;


        }

        /// <summary>
        /// Classe de metodos para pesquisa no banco de dados.
        /// </summary>
        public class Select
        {

            /// <summary>
            /// Retorna o valor da pesquisa em string
            /// </summary>
            /// <param name="comando"> 
            /// O comando tem que seguir o padrão SqlServer 
            /// <para>"SELECT * FROM tabela WHERE condição" </para>
            /// </param>
            /// <param name="campo">
            /// informe o nome do campo (coluna) que deseja ter o retorno
            /// </param>
            /// <param name="connect">
            /// <para> Endereço do banco de dados </para>
            /// Caso não passe o usuario e senha
            /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
            /// <para>Caso passe o usuario e senha 
            /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
            /// </param>
            /// <returns></returns>
            public static string StringFormat(string comando, string campo, string connect)
            {
                //ponte de conexão
                SqlConnection conexao = new SqlConnection(connect);

                //string que vai registra o dado guardado no SQL
                string dado = "";

                //ponte de comando em SQL
                SqlCommand comandos = new SqlCommand(comando, conexao);

                try
                {
                    //abrindo banco
                    conexao.Open();

                    //ponte de leitura de bando SQL
                    SqlDataReader dr = comandos.ExecuteReader();

                    //continua enquanto tiver linha para se ler
                    while (dr.Read())
                    {
                        dado = dr[campo].ToString();
                    }


                }
                catch (Exception)
                {
                    //não faz nada caso não retorne nenhuma informação
                }
                finally
                {
                    //feixando banco
                    conexao.Close();
                }
                return dado;
            }

            /// <summary>
            ///  Retorna a pesquisa em um formato DataTable (ideal para DataGrid) retorna todos os campos (colunas) da tabela 
            /// </summary>
            /// <param name="comando"> 
            /// O comando tem que seguir o padrão SqlServer 
            /// <para>"SELECT * FROM tabela WHERE condição" </para>
            /// </param>
            /// <param name="connect">
            /// <para> Endereço do banco de dados </para>
            /// Caso não passe o usuario e senha
            /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
            /// <para>Caso passe o usuario e senha 
            /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
            /// </param>
            /// <returns></returns>
            public static DataTable DataTableFormat(string comando, string connect)
            {
                //ponte de conexão
                SqlConnection conexao = new SqlConnection(connect);

                //ponte de comando Sql que adapita os dados para uma tabela
                SqlDataAdapter da = new SqlDataAdapter(comando, conexao);

                //objeto do tipo tabela que ira arquivar os valores da tabela lida no SqlServer
                DataTable dt = new DataTable();

                try
                {
                    //arquivando os dados na tabela
                    da.Fill(dt);
                }
                catch (SqlException)
                {
                    //não faz nada caso não retorne nenhuma informação
                }

                return dt;
            }

            /// <summary>
            /// Retorna a pesquisa em um formato ArryaList
            /// </summary>
            /// <param name="comando">
            /// O comando tem que seguir o padrão SqlServer 
            /// <para>"SELECT * FROM tabela WHERE condição" </para>
            /// </param>
            /// <param name="campos">
            /// informe o nome do campo (coluna) que deseja ter o retorno
            /// </param>
            /// <param name="connect">
            /// <para> Endereço do banco de dados </para>
            /// Caso não passe o usuario e senha
            /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
            /// <para>Caso passe o usuario e senha 
            /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
            /// </param>
            /// <returns></returns>
            public static ArrayList ArryaListFormat(string comando, string[] campos, string connect)
            {

                //ponte de conexão
                SqlConnection conexao = new SqlConnection(connect);

                //responsavel por armazenar os dados que forem puxados do banco
                ArrayList texto = new ArrayList();


                try
                {

                    //ponte de comando SQL
                    SqlCommand comandos = new SqlCommand(comando, conexao);

                    //Abrindo o Banco
                    conexao.Open();

                    SqlDataReader dr = comandos.ExecuteReader();
                    //ira continuar ate toda a table ser lida
                    while (dr.Read())
                    {
                        //ira continuar ate todos os campos da linha serem lidos
                        for (int i = 0; i != campos.Length; i++)
                        {
                            //adicionando o item a ArrayList
                            texto.Add(dr[campos[i]].ToString());
                        }
                    }

                }
                catch (Exception)
                {
                    //não faz nada caso não retorne nenhuma informação
                }
                finally
                {
                    //fechando o banco
                    conexao.Close();
                }
                return texto;
            }

            /// <summary>
            /// Retorna um valor bool informando se existe ou não algum retorno para o comando Sql informado
            /// </summary>
            /// <param name="comando"> 
            /// O comando tem que seguir o padrão SqlServer 
            /// <para>"SELECT * FROM tabela WHERE condição" </para>
            /// </param>
            /// <param name="connect">
            /// <para> Endereço do banco de dados </para>
            /// Caso não passe o usuario e senha
            /// - Data Source = instancia; Initial Catalog = database; Integrated Security = True;
            /// <para>Caso passe o usuario e senha 
            /// - Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha; </para>
            /// </param>
            /// <returns></returns>
            public static bool BoolFormat(string comando, string connect)
            {
                SqlConnection conexao = new SqlConnection(connect);

                bool resposta = true;

                try//apenas para segurança evitar possíveis falhas no programa
                {
                    SqlCommand comand = new SqlCommand(comando, conexao);//ponte com o comando SqlServer

                    conexao.Open();

                    SqlDataReader dr = comand.ExecuteReader();

                    resposta = dr.Read();//se tiver algum dado que possa ser lido ele ira retornar true se não false

                }
                catch
                {
                    resposta = false;// se der algum erro na execulção do comando retorna false
                }
                finally//se tiver algum erro fecha o banco antes de finalizar a operação
                {
                    conexao.Close();
                }

                return resposta;

            }

        }
    }

    /// <summary>
    /// Essa classe tem o objetivo de organizar e gerenciar seus endereços de conexão com o banco de dados do sistema
    /// </summary>
    public class Connection
    {

        //ponte de leitura e escrita
        static StreamReader sr;
        static StreamWriter sw;

        /// <summary>
        /// responsavel por criar as pastas onde seram guardados os do ficheiro com a(s) rota(s) do(s) banco de dados
        /// </summary>
        private static void PastaDataBase()
        {
            //pasta data e dentro dela tera a pasta DataBase
            DirectoryInfo di = new DirectoryInfo(@"Data");
            di.CreateSubdirectory(@"DataBase");
        }

        /// <summary>
        /// Lê o ficheiro cadastrado pelo comando RegisterBD() com a DataBase Registrada e retorna a rota de conexão
        /// </summary>
        /// <param name="DataBase">
        /// nome da DataBase que esta cadastrada no sistema (ficheiro)
        /// </param>
        /// <returns></returns>
        public static string Route(string DataBase)//(#BD)
        {
            //variavel que atribuindo endereço do banco de dados
            string local = "";

            //verifica a existencia do ficheiro se não existir acusa o erro
            if (File.Exists(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
            {
                using (sr = File.OpenText(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
                {
                    //caso já tenha cadastro no arquivo txt ele ira ler o endereço
                    local = sr.ReadLine();
                }
            }

            //retorna o endereço do banco de dados
            return local;
        }

        /// <summary>
        /// Respondavel por registar em um ficheiro a rota de conexão para a DataBase desejada
        /// </summary>
        /// <param name="instancia">
        /// Instancia do Banco de Dados desejada
        /// </param>
        /// <param name="DataBase">
        /// Nome da DataBase do Banco de Dados
        /// </param>
        /// <param name="user">
        /// Nome do Usuario do Banco de Dados
        /// </param>
        /// <param name="password">
        /// Senha do Usuario do Banco de Dados
        /// </param>
        /// <returns></returns>
        public static bool RegisterRouteDataBase(string instancia, string DataBase, string user, string password)
        {

            //se o usuario passa o valor vazio converte para nulo
            user =
                user == "" ?//Condição

                null        //Se
                : user;     //Se não

            //se o usuario passa o valor vazio converte para nulo
            password =
                password == "" ?//Condição 

                null            //Se
                : password;     //Se não


            //se o cliente pasar um valor nulo/vazio ou informa que e local informa que o endereço do banco e localhost
            instancia = instancia == "" || instancia == "localhost" || instancia == null ? @"localhost\SQLEXPRESS" : instancia;

            //endereço do Banco (instancia) e da DataBase
            string banco = @"Data Source = " + instancia + "; Initial Catalog = " + DataBase;

            //Caso o não se de senha ou usuarioa vai se entender que e um caso de Integrated Securyt caso o contrario sera cadastrado
            banco += user == null && password == null ? @"; Integrated Security = True" : "; USER ID = " + user + "; PASSWORD = " + password + " ;";

            //Caso não passe o usuario e senha
            //Data Source = "INSTANCIA"; Initial Catalog = "DATABASE"; Integrated Security = True;

            //Caso passe o usuario e senha
            //Data Source = "INSTANCIA"; Initial Catalog = "DATABASE"; USER ID = USUARIO; PASSWORD = SENHA;


            //testa primeiro a conecxão se ela for realizada com sucesso ela salva no ficheiro se não não faz auteração
            bool resposta = ConnectionTest(banco);

            if (resposta == true)
            {
                try
                {

                    PastaDataBase();//cria a pasta onde sera gravado os arquivos

                    //se existir com esse nome ele sera sobreescrevido
                    using (sw = File.CreateText(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
                        //escrevendo as informações do banco de dados no ficheiro
                        sw.Write(banco);

                    resposta = true;

                }
                catch (System.Exception)
                {
                    //se o ficheiro estiver aberto em segundo plano ou algo do genero retorna falso
                    resposta = false;
                }
            }

            return resposta;
        }

        /// <summary>
        /// Rota de conexão com o Banco de Dados
        /// </summary>
        /// <param name="Route">
        /// Rota de conexão com o banco de dados "DATA SOURCE = instancia; INITIAL CATALOG = dataBase; USER ID = usuario; PASSWORD = senha;"
        /// </param>
        /// <returns></returns>
        public static bool ConnectionTest(string Route)
        {

            bool resposta;

            //CONECTANDO AO BANCO DE DADOS
            SqlConnection connect = new SqlConnection(Route);
            try
            {
                //abrindo o banco de dados para testar a conecção
                connect.Open();
                //quando o banco abri e tiver sucesso registra a resposta como verdadeira a conexão com o banco
                resposta = true;

            }
            catch
            {
                //quando o banco abri e tiver erro registra a resposta como falsa a conexão com o banco
                resposta = false;
            }
            finally
            {
                //fecha o banco apois o sucesso ou erro da execução do codigo
                connect.Close();
            }
            //retorna o valor da resposta
            return resposta;
        }

    }
}