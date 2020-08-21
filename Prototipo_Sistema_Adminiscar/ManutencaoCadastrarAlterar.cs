using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SqlServer;

namespace Prototipo_Sistema_Adminiscar
{
    public partial class ManutencaoCadastrarAlterar : Form
    {
        string connect = Connection.Route("ADMINISCAR");

        string codEndereco, codTell, codCar, resolvido;

        public ManutencaoCadastrarAlterar()
        {
            InitializeComponent();
        }

        void atualizaDataGrid()
        {
            //comando de Sql Server para o banco
            dtgManutencao.DataSource = Comand.Select.DataTableFormat(
                "SELECT M.COD_MANUTENCAO, M.NOME_EMP AS NOME_DA_EMPRESA, M.CNPJ, C.PLACA, M.VALOR_MANUTENCAO AS PREÇO, M.RESOLVIDO, m.DATA_ENTREGA, m.DATA_DEVOLUCAO, T.TELL1 AS TELEFONE_1, T.TELL2 AS TELEFONE_2, e.LOGRADOURO, e.NUMERO, e.CEP, e.CIDADE, e.ESTADO "
                + "FROM MANUTENCAO AS M "
                + "INNER JOIN CARRO AS C ON M.COD_CAR_FK = C.COD_CAR "
                + "INNER JOIN ENDERECO AS E ON M.COD_ENDERECO_FK = E.COD_ENDERECO "
                + "INNER JOIN TELEFONE AS T ON M.COD_TELL_FK = T.COD_TELL"
                , connect);
        }

        void consultaSituacaoCar() {
            try
            {
                string[] campos = new string[] {"SITUACAO" };
                ArrayList a = Comand.Select.ArryaListFormat(
                    "SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'"
                    , campos, connect);


                switch (a[0].ToString().ToUpper())
                {


                    //caso esteja no patio
                    case "PATIO":
                        MessageBox.Show("Esse carro esta no Patio");
                        break;

                    //caso já esteja alocado
                    case "ALOCADO":
                        MessageBox.Show("Esse carro esta Alocado\nFaça o processo de devolução antes de iniciar um cadastro de manutenção");
                        txtPlaca.Text = "";
                        break;

                    //caso já esteja em manutenção esteja 
                    case "MANUTENÇÃO":

                        DialogResult r;//registra a escolha do cliente

                        r = MessageBox.Show("Esse carro já esta em manutenção!!\nDeseja atualizar os dados dessa manutenção", "ATENÇÃO", MessageBoxButtons.YesNo);
                        if (r == DialogResult.No)// se não tira a placa (o texto) do txtPlaca
                            txtPlaca.Text = "";

                        if (r == DialogResult.Yes)//se sim preenche os campos do sistema para a utilização do funcionario
                        {
                            try
                            {


                                //campos a serem retornados na arrayList #A1
                                string[] campos2 = new string[] { "NOME_EMP", "RESOLVIDO", "DATA_ENTREGA", "DATA_DEVOLUCAO", "CNPJ", "VALOR_MANUTENCAO", "TELL1", "TELL2", "LOGRADOURO", "NUMERO", "BAIRRO", "CIDADE", "ESTADO", "CEP" };

                                /*#(A1)*/
                                ArrayList b = Comand.Select.ArryaListFormat(
                               "SELECT * "
                               + "FROM MANUTENCAO AS M "
                               + " INNER JOIN CARRO AS C ON M.COD_CAR_FK = C.COD_CAR "
                               + " INNER JOIN ENDERECO AS E ON M.COD_ENDERECO_FK = E.COD_ENDERECO"
                               + " INNER JOIN TELEFONE AS T ON M.COD_TELL_FK = T.COD_TELL"
                               + " WHERE C.PLACA = '" + txtPlaca.Text + "' AND RESOLVIDO = 'N'"
                               , campos2 , connect);

                                //preenchimento dos campos #INICIO
                                txtNome.Text = b[0].ToString();

                                if (b[1].ToString() == "S")
                                    rbtResolvidoSIM.Checked = true;
                                else
                                    rbtResolvidoNAO.Checked = true;

                                dateEntrega.Text = b[2].ToString();
                                dateDevolucao.Text = b[3].ToString();
                                mtxtCNPJ.Text = b[4].ToString();
                                mtxtValorManutencao.Text = b[5].ToString();
                                mtxtValorManutencao.Text = String.Format("{0:c}", double.Parse(b[5].ToString()));
                                mtxtTell1.Text = b[6].ToString();
                                mtxtTell2.Text = b[7].ToString();
                                txtLogradouro.Text = b[8].ToString();
                                mtxtNumero.Text = b[9].ToString();
                                txtBairro.Text = b[10].ToString();
                                txtCidade.Text = b[11].ToString();
                                cbxEstado.Text = b[12].ToString();
                                mtxtCEP.Text = b[13].ToString();

                                //preenchimento dos campos #FIM

                                //atualiza o dataGrid
                                dtgManutencao.DataSource = Comand.Select.DataTableFormat
                                    ("SELECT * "
                                    + "FROM MANUTENCAO AS M "
                                    + " INNER JOIN CARRO AS C ON M.COD_CAR_FK = C.COD_CAR "
                                    + " INNER JOIN ENDERECO AS E ON M.COD_ENDERECO_FK = E.COD_ENDERECO"
                                    + " INNER JOIN TELEFONE AS T ON M.COD_TELL_FK = T.COD_TELL"
                                    + " WHERE C.PLACA = '" + txtPlaca.Text + "' AND RESOLVIDO = 'N'"
                                    , connect);

                                MessageBox.Show("Lembrete que só se pode mudar a situação de resolvido");

                            }
                            catch (Exception)
                            {
                                MessageBox.Show("ERRO!, essa ficha de manutenção foi apagada provávelmente");

                            }
                        }
            break;//break do switch case (como esta muito longe pode ficar confuso não custa comentar)
                }
            }
            catch (Exception)
            {
                //caso não encontre esse carro retorna esse valor

                MessageBox.Show("Carro não Cadastrado");
                txtPlaca.Text = "";
            }
            
        }

        void cadastraEndereco()
        {
            string[] campos = new string[] { "COD_ENDERECO" };

            //continua ate retornar o codigo do Endereço
            for (int i = 0; i < 2; i++)
            {
                //tenta pegar o codigo do endereco
                try
                {
                    ArrayList a = Comand.Select.ArryaListFormat(
                        "SELECT * FROM ENDERECO WHERE CEP = '" + mtxtCEP.Text + "' "
                        + " AND NUMERO = " + mtxtNumero.Text 
                        + " AND LOGRADOURO = '" + txtLogradouro.Text 
                        + "' AND BAIRRO = '" + txtBairro.Text 
                        + "' AND CIDADE = '" + txtCidade.Text 
                        + "' AND ESTADO = '" + cbxEstado.Text + "'"
                        , campos, connect);

                    codEndereco = a[0].ToString();

                }
                catch (Exception)
                {   //pegando os registros
                    string logradouro = txtLogradouro.Text;
                    string numero = mtxtNumero.Text;
                    string bairro = txtBairro.Text;
                    string cidade = txtCidade.Text;
                    string estado = cbxEstado.Text;
                    string CEP = mtxtCEP.Text;
                    //criando a string de comando
                    string comando = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, BAIRRO, CEP, CIDADE, ESTADO) VALUES ("
                    + "'" + logradouro + "'," + numero + ",'" + bairro + "','" + CEP + "','" + cidade + "','" + estado + "')";

                    //inserção dos dados
                    Comand.Insert(comando, connect);

                }
            }
        }

        void cadastraTelefone()
        {

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    string[] campos = new string[] { "COD_TELL" };

                    //o campo do telefone 2 e opcional então se ele quiser pode cadastra apenas 1 telefone
                    string comandoSelect = mtxtTell2.MaskCompleted == true ? "SELECT * FROM TELEFONE WHERE TELL1 = '" + mtxtTell1.Text + "' AND TELL2 = '" + mtxtTell2.Text + "'" : "SELECT * FROM TELEFONE WHERE TELL1 = '" + mtxtTell1.Text + "' AND TELL2 is null";

                    ArrayList a = Comand.Select.ArryaListFormat(comandoSelect, campos, connect);

                    codTell = a[0].ToString();
                }
                catch (Exception)
                {
                    //o campo do telefone 2 e opcional então se ele quiser pode cadastra apenas 1 telefone
                    string comandoInsert = mtxtTell2.MaskCompleted == true ? "INSERT INTO TELEFONE (TELL1, TELL2) VALUES ('" + mtxtTell1.Text + "', '" + mtxtTell2.Text + "')" : "INSERT INTO TELEFONE (TELL1) VALUES ('" + mtxtTell1.Text + "')";

                    Comand.Insert(comandoInsert, connect);

                }
            }
        }

        void cadastroManutencao()
        {

            string comando = "INSERT INTO MANUTENCAO(NOME_EMP, CNPJ, RESOLVIDO, DATA_ENTREGA, DATA_DEVOLUCAO, VALOR_MANUTENCAO, COD_CAR_FK, COD_TELL_FK, COD_ENDERECO_FK) " +
                             "VALUES('"+ txtNome.Text +"','"+ mtxtCNPJ.Text + "','" + resolvido + "','" + dateEntrega.Text + "','" + dateDevolucao.Text + "'," + mtxtValorManutencao.Text.Replace("R$ ", "").Replace(".", "").Replace(",", ".") + "," + codCar + "," + codTell + "," + codEndereco + ")";
            Comand.Insert(comando, connect);  
        }

        private void btCadastrar_Click(object sender, EventArgs e)
        {
            string situacao = Comand.Select.StringFormat(
                "SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'"
                , "SITUACAO", connect);//informa a situação do carro

            if (codCar != "" && txtNome.Text != "" && mtxtCNPJ.Text != ""
                && mtxtValorManutencao.Text.Replace("R$ ", "") != "" && mtxtTell1.MaskCompleted == true
                && txtLogradouro.Text != "" && mtxtNumero.Text != ""
                && txtBairro.Text != "" && txtCidade.Text != ""
                && cbxEstado.Text != "" && mtxtCEP.MaskCompleted == true)
            {

                if (situacao.ToUpper() == "PATIO")//se ele estive no patio então pode entra em manutenção
                {
                    cadastraEndereco();//responsavel po pegar o cod_endereco
                    cadastraTelefone();//responsavel po pegar o cod_tell

                    codCar = Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "COD_CAR",connect);//responsavel po pegar o cod_car
                    resolvido = rbtResolvidoSIM.Checked == true ? "S" : "N";//se foi resolvido o resultado e "S" se não "N"

                    string situacaoCar = resolvido == "S" ? "PARIO" : "MANUTENÇÃO";//se foi registrado uma manutenção já feita e resolvida continua no patio o carro se não coloca a situação dele como em manutenção

                    Comand.Update("UPDATE CARRO SET SITUACAO = '" + situacaoCar 
                        + "' WHERE PLACA = '" + txtPlaca.Text + "'", connect);//atualiza a situação do carro

                    cadastroManutencao();//metodo que cadastra o funcionario (pega o cod_endereco e o cod_tell pelas variaves globais (staticas)
                    atualizaDataGrid();//atualiza o datagride com a nova linha inserida
                }
                if (situacao.ToUpper() == "ALOCADO")//se o carro estiver alocado não pode se registra uma ficha de manutenção
                    MessageBox.Show("O Carro está Alocado faça a devolução dele para depois entra com uma ficha de Manutenção");

                if (situacao.ToUpper() == "MANUTENÇÃO")//se o carro estiver em manutenção não pode se registra uma ficha de manutenção (mas pode se altera ou atualizar)
                    MessageBox.Show("O Carro já está em manutenção!");

                txtPlaca.Text = "";
            }else
            {
                MessageBox.Show("PREENCHA TODOS OS CAMPOS!","ATENÇÃO");
            }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            if(txtPlaca.Text != "")
            //metodo para mostra a situação do carro
            consultaSituacaoCar();
        }

        private void rbtFixo1_CheckedChanged(object sender, EventArgs e)
        {
            mtxtTell1.Mask = "(00) 0000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtFixo2_CheckedChanged(object sender, EventArgs e)
        {
            mtxtTell2.Mask = "(00) 0000-0000";
            mtxtTell2.Text = "";
        }

        private void rbtCELL1_CheckedChanged(object sender, EventArgs e)
        {
            mtxtTell1.Mask = "(00) 00000-0000";
            mtxtTell1.Text = "";
        }

        private void rbtCELL2_CheckedChanged(object sender, EventArgs e)
        {
            mtxtTell2.Mask = "(00) 00000-0000";
            mtxtTell2.Text = "";
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text != "")
            {
                DialogResult r;

                r = MessageBox.Show("E permitido alterar apenas o estado da manutenção do Carro na area de 'RESOLVIDO'","ATENÇÃO",MessageBoxButtons.YesNo);

                if (r != DialogResult.Yes)
                {

                    if (rbtResolvidoNAO.Checked == true)
                    {
                        codCar = Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'"
                            , "COD_CAR", connect);//responsavel po pegar o cod_car

                        Comand.Update("UPDATE MANUTENCAO SET RESOLVIDO  = 'N' WHERE COD_CAR_FK = " + codCar, connect);

                        Comand.Update("UPDATE CARRO SET SITUACAO = 'MANUTENÇÃO' WHERE PLACA = '" + txtPlaca.Text + "'", connect);
                    }

                    if (rbtResolvidoSIM.Checked == true)
                    {
                        codCar = Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "COD_CAR", connect);//responsavel po pegar o cod_car

                        Comand.Update("UPDATE MANUTENCAO SET RESOLVIDO  = 'S' WHERE COD_CAR_FK = " + codCar, connect);

                        Comand.Update("UPDATE CARRO SET SITUACAO = 'PATIO' WHERE PLACA = '" + txtPlaca.Text + "'", connect);
                    }

                }
            }
            else
                MessageBox.Show("Preencha todos os campos!","ATENÇÃO");
        }

        private void btConsultaDeleta_Click(object sender, EventArgs e)
        {
            ManutencaoConsultarDeleta a = new ManutencaoConsultarDeleta();
            a.Show();
        }

        private void mtxtValorManutencao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValorManutencao.Text.Contains(","))
                {
                    e.Handled = true; // Caso exista, aborte 
                }
            }

            //aceita apenas números, tecla backspace.
            else if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void mtxtNumero_KeyPress(object sender, KeyPressEventArgs e)
        { 

            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void mtxtValorManutencao_Leave(object sender, EventArgs e)
        {
            double a = double.Parse(mtxtValorManutencao.Text.Replace("R$ ", ""));
            mtxtValorManutencao.Text = String.Format("{0:c}", a);
        }

        private void mtxtValorManutencao_Click(object sender, EventArgs e)
        {
            mtxtValorManutencao.Text = mtxtValorManutencao.Text.Replace("R$ ", "");
        }

        //responsavel por atualiza o horario/data do relogio no canto inferior direito da janela do programa
        private void timer1_Tick(object sender, EventArgs e)
        {
            //formato de apresentação do horario/data
            lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy   HH:mm:ss");
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            mtxtCNPJ.Text = "";
            txtPlaca.Text = "";
            mtxtValorManutencao.Text = String.Format("{0:c}", 0.00);

            txtLogradouro.Text = "";
            mtxtNumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            mtxtCEP.Text = "";

            mtxtTell1.Text = "";
            mtxtTell2.Text = "";
        }

        private void dateDevolucao_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btPesquisaCliente_Click(object sender, EventArgs e)
        {
            //metodo para mostra a situação do carro
            consultaSituacaoCar();
        }

        private void ManutencaoCadastrar_Load(object sender, EventArgs e)
        {

            dateDevolucao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dateEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");
            atualizaDataGrid();
            
            mtxtValorManutencao.Text = String.Format("{0:c}", 0.00);
        }

    }
}
