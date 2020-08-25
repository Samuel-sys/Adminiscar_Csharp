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
        /// Retona um valor bool (true, false) informando se foi ou não cadastrado os dados no banco
        /// </summary>
        /// 
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer 
        /// "INSERT INTO tabela (coluna1,coluna2,coluna3...) VALUES ('valor1, valor2, valor3...)" 
        /// </param>
        /// 
        /// <param name="connect">
        /// Endereço do banco de dados 
        /// Caso não passe o usuario e senha
        /// Data Source = "INSTANCIA"; Initial Catalog = "DATABASE"; Integrated Security = True;
        /// Caso passe o usuario e senha
        /// Data Source = "INSTANCIA"; Initial Catalog = "DATABASE"; USER ID = USUARIO; PASSWORD = SENHA;
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

        public class Select
        {

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

            public static ArrayList ArryaListFormat(string comando, string[] campos, string connect)
            {

                //ponte de conexão
                SqlConnection conexao = new SqlConnection(connect);

                //responsavel por armazenar os dados que forem puxados do banco
                ArrayList texto = new ArrayList();


                try
                {

                    //ponte de comando SQP
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

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Connection
    {

        //ponte de leitura e escrita
        static StreamReader sr;
        static StreamWriter sw;

        //responsavel por criar as pastas onde seram guardados os do ficheiro (com a rota do banco de dados)
        private void PastaDataBase()
        {
            //pasta data e dentro dela tera a pasta DataBase
            DirectoryInfo di = new DirectoryInfo(@"Data");
            di.CreateSubdirectory(@"DataBase");
        }

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

        public static bool RegisterBD(string instancia, string DataBase, string user, string password)
        {
            //se o usuario passa o valor vazio converte para nulo
            user = user == "" ? null : user;
            //se o usuario passa o valor vazio converte para nulo
            password = password == "" ? null : password;

            //se o cliente pasar um valor nulo/vazio ou informa que e local informa que o endereço do banco e localhost
            if (instancia == "" || instancia == "localhost" || instancia == null)
                instancia = @"localhost\SQLEXPRESS";

            //endereço do banco e da DataBase
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

                    //se existir com esse nome ele sera sobreescrevido
                    using (sw = File.CreateText(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
                        //escrevendo as informações do banco de dados no ficheiro
                        sw.Write(banco);

                }
                catch (System.Exception)
                {
                    //se o ficheiro estiver aberto em segundo plano ou algo do genero retorna falso
                    return false;
                }
            }

            return resposta;

        }

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