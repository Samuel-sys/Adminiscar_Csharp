using System;
using System.Collections;
using System.Linq;
using SqlServer;
using System.Text;
using System.Threading.Tasks;
using Prototipo_Sistema_Adminiscar.Models;
using System.Windows.Forms;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    class TelefoneADO
    {
        static string connect = Connection.Route("ADMINISCAR");//informa o endereço de conexão com o banco de dados

        static public string cadastraTelefone(Telefone tell)
        {
            //o campo do telefone 2 e opcional então se ele quiser pode cadastra apenas 1 telefone
            string comandoSelect = tell.tell2 != "" || tell.tell2 != null
                ? "SELECT * FROM TELEFONE WHERE TELL1 = '" + tell.tell1 + "' AND TELL2 = '" + tell.tell2 + "'"
                : "SELECT * FROM TELEFONE WHERE TELL1 = '" + tell.tell1 + "' AND TELL2 is null";

            if (!Comand.Select.BoolFormat(comandoSelect, connect))
            {
                //o campo do telefone 2 e opcional então se ele quiser pode cadastra apenas 1 telefone
                string comandoInsert = tell.tell2 != "" || tell.tell2 != null
                    ? "INSERT INTO TELEFONE (TELL1, TELL2) VALUES ('" + tell.tell1 + "', '" + tell.tell2 + "')"
                    : "INSERT INTO TELEFONE (TELL1) VALUES ('" + tell.tell1 + "')";

                Comand.Insert(comandoInsert, connect);
            }

                return Comand.Select.StringFormat(comandoSelect, "COD_TELL", connect);
        }

        static internal void erroCadastro(Telefone tell)
        {
            string comandoInsert = tell.tell2 != "" || tell.tell2 != null
                    ? "INSERT INTO TELEFONE (TELL1, TELL2) VALUES ('" + tell.tell1 + "', '" + tell.tell2 + "')"
                    : "INSERT INTO TELEFONE (TELL1) VALUES ('" + tell.tell1 + "')";

            string erro = Comand.ErroComand(comandoInsert, connect);

            MessageBox.Show(erro, "Alerta - erro no cadastro do Telefone");
        }

        static public Telefone consultaCodTel(int cod_tell)
        {
            string[] campos = new string[] { "TELL1", "TELL2" };

            ArrayList tell = Comand.Select.ArryaListFormat("SELECT * FROM TELEFONE", campos, connect);

            try
            {
                return new Telefone()
                {
                    tell1 = tell[0].ToString(),
                    tell2 = tell[1].ToString(),
                    codTell = cod_tell,
                };
            }
            catch
            {

                return null;
            }
        }
    }
}