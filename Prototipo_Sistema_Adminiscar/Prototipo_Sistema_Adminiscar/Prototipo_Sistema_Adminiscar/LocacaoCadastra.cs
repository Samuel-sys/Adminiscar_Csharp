using SqlServer;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Prototipo_Sistema_Adminiscar
{
    public partial class LocacaoCadastra : Form
    {
        string connect = Connection.Route("ADMINISCAR");
        string cod_pag = "";//(#V1)
        string cod_car = "";//(#V2)
        string cod_cred = "";//(#V3)
        string cod_deb = "";//(#V4)
        string cod_cliente = "";//(#V5)
        float pag_total = 0;


        public LocacaoCadastra()
        {
            InitializeComponent();
        }

        void imgCarro()
        {
            switch (cbxNomeCar.Text)
            {
                case "Onix":
                    pbxCarro.Load(@"Carro\Onix.jpg");
                    break;

                case "Agile":
                    pbxCarro.Load(@"Carro\Agile.jpg");
                    break;

                case "Fiat mobi":
                    pbxCarro.Load(@"Carro\Fiat_mobi.jpg");
                    break;

                case "Up":
                    pbxCarro.Load(@"Carro\Up.jpg");
                    break;

                case "HB20":
                    pbxCarro.Load(@"Carro\HB20.jpg");
                    break;

                case "CELTA":
                    pbxCarro.Load(@"Carro\CELTA.jpg");
                    break;
                //quebra
                case "Golf":
                    pbxCarro.Load(@"Carro\Golf.jpg");
                    break;

                case "renegade":
                    pbxCarro.Load(@"Carro\renegade.jpg");
                    break;

                case "EcoSport":
                    pbxCarro.Load(@"Carro\EcoSport.jpg");
                    break;

                case "Argo":
                    pbxCarro.Load(@"Carro\Argo.jpg");
                    break;

                case "Cronos":
                    pbxCarro.Load(@"Carro\Cronos.jpg");
                    break;

                case "Linea":
                    pbxCarro.Load(@"Carro\Linea.jpg");
                    break;
                //quebra
                case "Jetta":
                    pbxCarro.Load(@"Carro\Jetta.jpg");
                    break;

                case "Golf Tsi":
                    pbxCarro.Load(@"Carro\Golf_Tsi.jpg");
                    break;

                case "Bmw m3":
                    pbxCarro.Load(@"Carro\Bmw_m3.jpg");
                    break;

                case "Ford fusion":
                    pbxCarro.Load(@"Carro\Ford_fusion.jpg");
                    break;

                case "Volvo s60":
                    pbxCarro.Load(@"Carro\Volvo_s60.jpg");
                    break;

                case "Land rover evoque":
                    pbxCarro.Load(@"Carro\Land_rover_evoque.jpg");
                    break;
                //quebra
                case "Camaro zl1":
                    pbxCarro.Load(@"Carro\Camaro_zl1.jpg");
                    break;

                case "Jeep compass":
                    pbxCarro.Load(@"Carro\Jeep_compass.jpg");
                    break;

                case "Mercedes c63":
                    pbxCarro.Load(@"Carro\Mercedes_c63.jpg");
                    break;

                case "Audi rs4":
                    pbxCarro.Load(@"Carro\Audi_rs4.jpg");
                    break;

                default:
                    pbxCarro.Load(@"Carro\default.png");
                    break;
            }
        }

        void atualizarDataGrid()//atualiza o DataGrid com todas as linha
        {
            //comando SqlServer de consulta
            string comando =
                "SELECT CLI.NOME_CLIENTE, CLI.CPF_CNPJ, C.NOME_CAR AS NOME_CARRO, C.PLACA, P.VALOR, P.DATA_RETIRADA, P.DATA_DEVOLUCAO "
                    + " FROM PEDIDO AS P"
                    + " INNER JOIN CARRO AS C ON COD_CAR = COD_CAR_FK"
                    + " INNER JOIN CLIENTE AS CLI ON COD_CLIENTE = COD_CLIENTE_FK";

            //atualiza o dataGrid
            dtgPagamento.DataSource = Comand.Select.DataTableFormat(comando, connect);

        }

        void organizaPagamento()//organiza os valores (#P1)
        {
            //se o cliente tiver com o cartão de credito e de debido ativado
            if (cbxCRED.Checked == true && cbxDEB.Checked == true)
            {
                //coloca metado do valor do aluguel no credito e a outra metade no debito
                mtxtValPagoCRED.Text = String.Format("{0:c}", pag_total / 2);
                mtxtValPagoDEB.Text = String.Format("{0:c}", pag_total / 2);
            }
            else if (cbxCRED.Checked == true)//se o cliente tiver apenas o cartão de credito ativado
            {
                //coloca o valor do aluguel no cartão de credito
                mtxtValPagoCRED.Text = String.Format("{0:c}", pag_total);
            }
            else//se o cliente tiver apenas o cartão de debito ativado
            {
                //coloca o valor do aluguel no cartão de debito
                mtxtValPagoDEB.Text = String.Format("{0:c}", pag_total);
            }
        }

        void dataEntrega()//informa os valores do plano de aluguel escolhido pelo cliente
        {
            DateTime time = this.dtpDataEntrega.Value;//Data de referencia para informar a data de devolução

            //se caso o usuario informar quantidade de tempo
            if (cbxValor.Text != "")
            {
                //informa o valor a ser pago conforma a escolha do aluguel (diario, semanal, mensal)
                switch (cbxValor.Text)
                {

                    case "Diário"://escolha de aluguel diario

                        if (mtxtTempo.Text != "")//se o funcionario não tiver preenchido o campo de pesquisa inpede a um erro de converção no if seguinte
                        {

                            //se o cliente quiser aluguar durante mais de 6 dias informa que seria melhor escolherer um plano semanal ou mensal
                            if (int.Parse(mtxtTempo.Text) > 6)
                            {
                                //informa o usuario
                                MessageBox.Show("O Plano Diario suporta até 6 dias, para mais tempo consulte os outros planos");

                                //coloca o maximo de tempo de aluguel possivel
                                mtxtTempo.Text = "6";
                            }
                            //torna o campo de devolução visivel ao cliente
                            this.dtpDataDevolucao.Visible = true;
                            this.lblDataDevolucao.Visible = true;
                            this.dtpDataDevolucao.Value = time.AddDays(int.Parse(mtxtTempo.Text));//adicionando Dias a data de devolução

                            //tenta pegar os valores do carro do carro e registra no sistema
                            try
                            {
                                //multiplica a quantidade de tempo pelo valor do aluguel
                                pag_total = int.Parse(mtxtTempo.Text) * float.Parse(Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "VALOR_DIARIO", connect));

                                //informa o valor e converte para moeda
                                lblTotal.Text = String.Format("{0:c}", pag_total);

                                //organiza o pagamento nos cartões de credito e debito #P1
                                organizaPagamento();

                            }
                            catch { }// seder erro não faz nada
                        }
                        break;


                    case "Semanal"://escolha de aluguel Semanal
                        if (mtxtTempo.Text != "")//se o funcionario não tiver preenchido o campo de pesquisa inpede a um erro de converção no if seguinte
                        {

                            //se o cliente quiser aluguar durante mais de semanas informa que seria melhor escolherer um plano mensal
                            if (int.Parse(mtxtTempo.Text) > 4)
                            {
                                //informa o usuario
                                MessageBox.Show("O Plano Semanal suporta até 4 Semanas, para mais tempo consulte os outros planos");

                                //coloca o maximo de tempo de aluguel possivel
                                mtxtTempo.Text = "4";
                            }

                            //torna o campo de devolução visivel ao cliente
                            this.dtpDataDevolucao.Visible = true;
                            this.lblDataDevolucao.Visible = true;
                            this.dtpDataDevolucao.Value = time.AddDays(int.Parse(mtxtTempo.Text) * 7);//adicionando Semanas a data de devolução

                            //tenta pegar os valores do carro do carro e registra no sistema
                            try
                            {
                                //multiplica a quantidade de tempo pelo valor do aluguel
                                pag_total = int.Parse(mtxtTempo.Text) * float.Parse(Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "VALOR_SEMANAL", connect));

                                //informa o valor e converte para moeda
                                lblTotal.Text = String.Format("{0:c}", pag_total);

                                //organiza o pagamento nos cartões de credito e debito #P1
                                organizaPagamento();
                            }
                            catch { }// seder erro não faz nada

                        }
                        break;


                    case "Mensal"://escolha de aluguel Semanal
                        if (mtxtTempo.Text != "")//se o funcionario não tiver preenchido o campo de pesquisa inpede a um erro de converção no if seguinte
                        {
                            //se o cliente quiser aluguar durante mais de 2 meses informa que não e possivel
                            if (int.Parse(mtxtTempo.Text) > 2)
                            {
                                //informa o usuario
                                MessageBox.Show("O Plano Mensal suporta até 2 meses, para mais tempo após o uso renove o aluguel");

                                //coloca o maximo de tempo de aluguel possivel
                                mtxtTempo.Text = "2";
                            }

                            //torna o campo de devolução visivel ao cliente
                            this.dtpDataDevolucao.Visible = true;
                            this.lblDataDevolucao.Visible = true;
                            this.dtpDataDevolucao.Value = time.AddMonths(int.Parse(mtxtTempo.Text));//adicionando meses a data de devolução

                            //tenta pegar os valores do carro do carro e registra no sistema
                            try
                            {
                                //multiplica a quantidade de tempo pelo valor do aluguel
                                pag_total = int.Parse(mtxtTempo.Text) * float.Parse(Comand.Select.StringFormat("SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", "VALOR_MENSAL", connect));

                                //informa o valor e converte para moeda
                                lblTotal.Text = String.Format("{0:c}", pag_total);

                                //organiza o pagamento nos cartões de credito e debito #P1
                                organizaPagamento();

                            }
                            catch { }// seder erro não faz nada

                        }
                        break;

                    case ""://caso o cliente não tenha colocado o tipo de plano de alugue não faz nada
                        break;
                }
            }
        }

        void ConsultaCodCliente(bool mensagem)// retorna o numero do codigo do cliente para a varivel #V5
        {
            //consulta no banco de dados e retorna o valor do codigo do cliente
            cod_cliente = Comand.Select.StringFormat("SELECT * FROM CLIENTE WHERE CPF_CNPJ = '" + mtxtIdentificacao.Text + "'", "COD_CLIENTE", connect);

            if (cod_cliente == "")//se não retornar nem um valor ou seja não encontrar o codigo informa o usuario e limpa o campo de indentidade
            {
                //informa ao messageBox #Mbx1 se esta pesquisando um CPF ou um CNPJ
                string msg = rbtPF.Checked == true ? "CPF" : "CNPJ";

                if (mensagem == true)
                    MessageBox.Show(msg + " não cadastrado");//(#Mbx1)

                //limpa o mtxtIdentificacao
                mtxtIdentificacao.Text = "";
            }
        }

        void consultaCardCred(bool mensagem)// retorna o numero do codigo do cartão de credito para a varivel #V3
        {
            //se tiver cadastrado o cliente já preenche os campos de Cartão de Credito
            try
            {

                ConsultaCodCliente(false);

                //campos da tabela com os dados do cartão de credito
                string[] campos = new string[] { "COD_CRED", "COD_SEGURANCA_CRED", "DT_VALIDADE_CRED", "NUM_CARTAO_CRED" };

                //consulta no banco a existencia de um cartão de credito e traz os dados do cartão de credito do cliente 
                ArrayList a = Comand.Select.ArryaListFormat("SELECT * FROM CARTAO_CRED WHERE COD_CLIENTE_FK = " + cod_cliente, campos, connect);

                cod_cred = a[0].ToString();//arquiva o numero do cod_deb da tabela para usos futuros

                txtCodSegurancaCRED.Text = a[1].ToString(); //inserindo o codigo de segurança no campo do sistema 
                dtpValidadeCRED.Text = a[2].ToString();     //inserindo o Validade do cartão no campo do sistema 
                txtNumeroCartCRED.Text = a[3].ToString();   //inserindo o numero do cartão no campo do sistema 

            }
            catch//caso de erro não faz nada
            { }

        }

        void consultaCardDeb(bool mensagem)// retorna o numero do codigo do cartão de credito para a varivel #V4
        {
            //se tiver cadastrado o cliente já preenche os campos de Cartão de Debito
            try
            {
                //evitar erros caso não tenha o codigo do cliente
                ConsultaCodCliente(false);

                //campos da tabela com os dados do cartão de debito
                string[] campos = new string[] { "COD_DEB", "COD_SEGURANCA_DEB", "DT_VALIDADE_DEB", "NUM_CARTAO_DEB" };

                //consulta no banco a existencia de um cartão de debito e traz os dados do cartão de debito do cliente 
                ArrayList a = Comand.Select.ArryaListFormat("SELECT * FROM CARTAO_DEB WHERE COD_CLIENTE_FK = " + cod_cliente, campos, connect);

                cod_deb = a[0].ToString();//arquiva o numero do cod_deb da tabela para usos futuros

                txtCodSegurancaDEB.Text = a[1].ToString();//inserindo o codigo de segurança no campo do sistema
                dtpValidadeDEB.Text = a[2].ToString();    //inserindo o Validade do cartão no campo do sistema 
                txtNumeroCartDEB.Text = a[3].ToString();  //inserindo o numero do cartão no campo do sistema 

            }
            catch//caso de erro não faz nada
            { }

        }

        void ComboBoxCarro()//responsavel por entregar o nome dos carros conforme a categoria do carro
        {
            switch (cbxCategoria.Text)//usa o parametro do campo escolhido no ComboBox 
            {
                case "Entrada":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Onix");
                    cbxNomeCar.Items.Add("Agile");
                    cbxNomeCar.Items.Add("Fiat mobi");
                    cbxNomeCar.Items.Add("Up");
                    cbxNomeCar.Items.Add("HB20");
                    cbxNomeCar.Items.Add("CELTA");
                    lblSOM.Text = "N";
                    lblSOMBT.Text = "N";
                    lblGPS.Text = "N";
                    break;
                case "Intermediario":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Golf");
                    cbxNomeCar.Items.Add("renegade");
                    cbxNomeCar.Items.Add("EcoSport");
                    cbxNomeCar.Items.Add("Argo");
                    cbxNomeCar.Items.Add("Cronos");
                    cbxNomeCar.Items.Add("Linea");
                    lblSOM.Text = "S";
                    lblSOMBT.Text = "N";
                    lblGPS.Text = "N";
                    break;
                case "Esportivo":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Jetta");
                    cbxNomeCar.Items.Add("Golf Tsi");
                    cbxNomeCar.Items.Add("Bmw m3");
                    cbxNomeCar.Items.Add("Ford fusion");
                    cbxNomeCar.Items.Add("Volvo s60");
                    cbxNomeCar.Items.Add("Audi rs4");
                    lblSOM.Text = "S";
                    lblSOMBT.Text = "N";
                    lblGPS.Text = "S";
                    break;
                case "Luxo":
                    cbxNomeCar.Text = "";
                    cbxNomeCar.Items.Clear();
                    cbxNomeCar.Items.Add("Land rover evoque");
                    cbxNomeCar.Items.Add("Jeep compass");
                    cbxNomeCar.Items.Add("Camaro zl1");
                    cbxNomeCar.Items.Add("Mercedes c63");
                    lblSOM.Text = "S";
                    lblSOMBT.Text = "S";
                    lblGPS.Text = "S";
                    break;
            }
            txtPlaca.Text = "";//limpa o campo da placa do carro 
        }

        string consultaCar()//responsavel por pesquisar 
        {
            string[] campos = new string[] { "CATEGORIA", "NOME_CAR", "COD_CAR", "SITUACAO" };
            ArrayList a = Comand.Select.ArryaListFormat(
                "SELECT * FROM CARRO WHERE PLACA = '" + txtPlaca.Text + "'", campos, connect);
            try
            {
                if (a[3].ToString().ToUpper() == "PATIO")
                {

                    switch (a[0].ToString().ToUpper())
                    {
                        case "ENTRADA":
                            cbxCategoria.SelectedIndex = 0;
                            break;

                        case "INTERMEDIARIO":
                            cbxCategoria.SelectedIndex = 1;
                            break;
                        case "ESPORTIVO":
                            cbxCategoria.SelectedIndex = 2;
                            break;
                        case "LUXO":
                            cbxCategoria.SelectedIndex = 3;
                            break;
                    }
                    cbxNomeCar.Text = a[1].ToString();
                    cod_car = a[2].ToString();
                }
                else
                    MessageBox.Show("O Carro " + a[1].ToString() + " está em estado de " + a[3].ToString());

            }
            catch
            {
                MessageBox.Show("Carro não Cadastrado", "Atenção");
                txtPlaca.Text = "";
            }

            return cod_car;
        }

        void CadastraCardCred()
        {
            cod_cred = "";
            for (int i = 0; i < 1; i++)
            {
                consultaCardCred(false);
                if (cod_cred == "")
                {
                    Comand.Insert("INSERT INTO CARTAO_CRED(COD_SEGURANCA_CRED, DT_VALIDADE_CRED, NUM_CARTAO_CRED, COD_CLIENTE_FK) VALUES('" + txtCodSegurancaCRED.Text + "', '" + dtpValidadeCRED.Text + "', '" + txtNumeroCartCRED.Text + "', " + cod_cliente + ")", connect);
                }
            }
        }

        void CadastraCardDeb()
        {
            cod_deb = "";
            for (int i = 0; i < 1; i++)
            {
                consultaCardDeb(false);
                if (cod_deb == "")
                {
                    Comand.Insert("INSERT INTO CARTAO_DEB(COD_SEGURANCA_DEB, DT_VALIDADE_DEB, NUM_CARTAO_DEB, COD_CLIENTE_FK) "
                        + " VALUES ('" + txtCodSegurancaDEB.Text + "', '" + dtpValidadeDEB.Text + "', '" + txtNumeroCartDEB.Text + "', " + cod_cliente + ")"
                        , connect);
                }
            }
        }

        void Pagamento()
        {
            string comando;

            if (cbxCRED.Checked == true && cbxDEB.Checked == true)
            {
                CadastraCardCred();//se o cartão já estive cadastrado ele faz uma consulta e salva o calor do Cod_cred
                CadastraCardDeb();//se o cartão já estive cadastrado ele faz uma consulta e salva o calor do Cod_deb
                comando = "INSERT INTO PAGAMENTO(VALOR, COD_CRED_FK, COD_DEB_FK, COD_CLIENTE_FK) VALUES(" + pag_total + ", '" + cod_cred + "', '" + cod_deb + "', '" + cod_cliente + "')";
                Comand.Insert(comando, connect);
                cod_pag = Comand.Select.StringFormat("select IDENT_CURRENT( 'PAGAMENTO' ) AS COLUNA", "COLUNA", connect);//DESCOBRE QUAL NUMERO DO ULTIMO COD_PAGAMENTO INSERIDO (PELO METODO IDENTITY)
            }
            else if (cbxCRED.Checked == true)
            {
                CadastraCardCred();//se o cartão já estive cadastrado ele faz uma consulta e salva o calor do Cod_cred

                //comando de insert SqlServer
                comando = "INSERT INTO PAGAMENTO(VALOR, COD_CRED_FK, COD_CLIENTE_FK) VALUES('" + pag_total + "', '" + cod_cred + "', '" + cod_cliente + "')";

                //cadastra o pagamento com cartão de credito
                Comand.Insert(comando, connect);

                //informa qual foi a ultima linha cadastrada ou seja essa assim pegando o codigo do pagamento e salvando na variavel
                cod_pag = Comand.Select.StringFormat("select IDENT_CURRENT( 'PAGAMENTO' ) AS COLUNA", "COLUNA", connect);//DESCOBRE QUAL NUMERO DO ULTIMO COD_PAGAMENTO INSERIDO (PELO METODO IDENTITY)
            }
            else
            {
                CadastraCardDeb();//se o cartão já estive cadastrado ele faz uma consulta e salva o calor do Cod_deb

                //comando de insert SqlServer
                comando = "INSERT INTO PAGAMENTO(VALOR, COD_DEB_FK, COD_CLIENTE_FK) VALUES('" + pag_total + "', '" + cod_deb + "', '" + cod_cliente + "')";
                Comand.Insert(comando, connect);
                cod_pag = Comand.Select.StringFormat("select IDENT_CURRENT( 'PAGAMENTO' ) AS COLUNA", "COLUNA", connect);
            }
        }

        void Pedido()
        {/*FALTA COMPLETAR*/
            string comando;
            cod_car = consultaCar();


            if (cbxCRED.Checked == true && cbxDEB.Checked == true)
            {
                CadastraCardCred();//se o cartão já estive cadastrado ele faz uma consulta e salva o calor do Cod_cred
                CadastraCardDeb();//se o cartão já estive cadastrado ele faz uma consulta e salva o calor do Cod_deb
            }

            else if (cbxCRED.Checked == true)
                CadastraCardCred();//se o cartão já estive cadastrado ele faz uma consulta e salva o numero do Cod_cred na variavel #V3

            else if (cbxDEB.Checked == true)
                CadastraCardDeb();//se o cartão já estive cadastrado ele faz uma consulta e salva o o numero do Cod_deb na variavel #V4

            //comando de insert de SqlServer
            comando = "INSERT INTO PEDIDO (VALOR, TIPO_PEDIDO, DATA_RETIRADA, DATA_DEVOLUCAO, COD_PAG_FK, COD_CAR_FK, COD_CLIENTE_FK) VALUES "
            + "('" + pag_total + "','" + cbxValor.Text.ToUpper() + "','" + dtpDataEntrega.Text + "','" + dtpDataDevolucao.Text + "','" + cod_pag + "','" + cod_car + "','" + cod_cliente + "')";

            //Salva a nova linha com os dados do pedido efetuado
            Comand.Insert(comando, connect);
        }

        private void btCadastra_Click(object sender, EventArgs e)
        {
            //confere se todos os campos foram preenchidos
            if (cod_cliente != "" && txtPlaca.Text != "" && mtxtTempo.Text != "" && cbxValor.Text != "" && pag_total != 0)
            {
                //se tiver ativado o cartão o de credito cadastra o cartão
                if (cbxCRED.Checked == true)
                    CadastraCardCred();

                //se tiver ativado o cartão o de debito cadastra o cartão
                if (cbxDEB.Checked == true)
                    CadastraCardDeb();

                //cadastra a ficha de pagamento no banco de dados e registra o codigo do pagamento na variavel #V1
                Pagamento();

                //cadastra a ficha do pedido de locação no banco de dados e registra o codigo do pedido na variavel #V2
                Pedido();

                //atualiza os dados do DataGrid com a nova linha cadastrada
                atualizarDataGrid();

                //comando SqlServer
                string comando = "UPDATE CARRO SET SITUACAO = 'ALOCADO' WHERE PLACA ='" + txtPlaca.Text + "'";

                //faz a alteração no Banco de Dados no carro informando que ele já esta locado
                Comand.Update(comando, connect);

                //limpa o campo da placa do carro para evitar erros futuros (cadastra 2 alocações com o mesmo carro)
                txtPlaca.Text = "";
            }
            else
            {
                MessageBox.Show("Preencha todos os campos", "ATENÇÃO");
            }
        }

        private void rbtPF_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CPF: ";
            mtxtIdentificacao.Mask = "000,000,000-00";
        }

        private void rbtPJ_CheckedChanged(object sender, EventArgs e)
        {
            lblIdentificacao.Text = "CNPJ: ";
            mtxtIdentificacao.Mask = "00,000,000/0000-00";
        }

        private void btCarro_Click(object sender, EventArgs e)
        {
            consultaCar();
            dataEntrega();
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxCarro();
        }

        private void cbxNomeCar_Leave(object sender, EventArgs e)
        {
            if (cbxNomeCar.Text != "")
            {

                //nome da coluna onde esta arquivado o dado da placa do carro
                string[] campos = new string[] { "PLACA" };
                //comando SQP Server
                ArrayList a = Comand.Select.ArryaListFormat("SELECT * FROM CARRO"
                    + " WHERE SITUACAO = 'PATIO' AND CATEGORIA = '" + cbxCategoria.Text
                    + "' AND NOME_CAR = '" + cbxNomeCar.Text + "'"
                    , campos, connect);

                //tenta ver se tem um carro com nome e categoria vago se tiver apresenta ao txt
                try
                {
                    //colocando o dado da placa do carro disponivel
                    txtPlaca.Text = a[0].ToString();
                    dataEntrega();
                }
                catch (Exception)
                {
                    //informá que não a um carro com essas especificações livre
                    MessageBox.Show("Não temos mais esse carro " + cbxNomeCar.Text, "ATENÇÃO");
                    ComboBoxCarro();//metodo para limpar a area de texto do nome do carro carro
                }
            }
        }

        private void cbxNomeCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataEntrega();
            imgCarro();
            //nome da coluna onde esta arquivado o dado da placa do carro
            string[] campos = new string[] { "PLACA" };
            //comando SQP Server
            ArrayList a = Comand.Select.ArryaListFormat(
                "SELECT * FROM CARRO WHERE SITUACAO = 'PATIO' AND CATEGORIA = '" + cbxCategoria.Text + "' AND NOME_CAR = '" + cbxNomeCar.Text + "'"
                , campos, connect);

            //tenta ver se tem um carro com nome e categoria vago se tiver apresenta ao txt
            try
            {
                //colocando o dado da placa do carro disponivel
                txtPlaca.Text = a[0].ToString();
            }
            catch (Exception)
            {
                //informá que não a um carro com essas especificações livre
                MessageBox.Show("Não temos mais esse carro disponivel " + cbxNomeCar.Text, "ATENÇÃO");
                ComboBoxCarro();//metodo para limpar a area de texto do nome do carro carro
            }

        }

        private void mtxtIdentificacao_Leave(object sender, EventArgs e)
        {
            //se o usuario não digitar nada não apresenta a mensagem de Identificação valida ou não valida
            if (mtxtIdentificacao.Text != "")
            {
                ConsultaCodCliente(true);//atualiza o cod_cliente e informa se o CPF/CNPJ for invalido 

                txtCodSegurancaCRED.Text = "";
                txtCodSegurancaDEB.Text = "";
                txtNumeroCartCRED.Text = "";
                txtNumeroCartDEB.Text = "";

                //se quiser que mostra o messagerBox coloca true se não false

                consultaCardCred(false);//atualizando os dados

                consultaCardDeb(false);//atualizando os dados

            }
        }

        private void btCred_Click(object sender, EventArgs e)
        {
            consultaCardCred(true);
        }

        private void LocacaoCadastraAltera_Load(object sender, EventArgs e)
        {
            atualizarDataGrid();
            pbxCarro.Load(@"Carro\default.png");
            mtxtValPagoCRED.Text = String.Format("{0:c}", 0.00);
            mtxtValPagoDEB.Text = String.Format("{0:c}", 0.00);

        }

        private void mtxtTempo_Leave(object sender, EventArgs e)
        {

            if (cbxValor.Text != "")
                dataEntrega();
        }

        private void dtpDataDevolucao_ValueChanged(object sender, EventArgs e)
        {
            dataEntrega();
        }

        private void cbxValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataEntrega();
        }

        private void txtNumeroCartCRED_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;

        }

        private void txtNumeroCartDEB_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;
        }

        private void txtCodSegurancaDEB_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;
        }

        private void txtCodSegurancaCRED_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números, tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;
        }

        private void mtxtValPagoCRED_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValPagoCRED.Text.Contains(","))
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

        private void mtxtValPagoDEB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (mtxtValPagoDEB.Text.Contains(","))
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

        private void mtxtValPagoDEB_Leave(object sender, EventArgs e)
        {
            if (cbxCRED.Checked == true && cbxDEB.Checked == true)
            {
                float a = float.Parse(mtxtValPagoDEB.Text.Replace("R$ ", "").Replace(".", ""));

                if (a < pag_total)
                    a = pag_total - a;

                else if (a > pag_total)
                {
                    a = pag_total / 2;
                    mtxtValPagoDEB.Text = String.Format("{0:c}", a);
                }
                mtxtValPagoCRED.Text = String.Format("{0:c}", a);
            }
            else
                mtxtValPagoDEB.Text = String.Format("{0:c}", pag_total);

        }

        private void mtxtValPagoCRED_Leave(object sender, EventArgs e)
        {
            try
            {

                if (cbxCRED.Checked == true && cbxDEB.Checked == true)
                {
                    float a = float.Parse(mtxtValPagoCRED.Text.Replace("R$ ", "").Replace(".", ""));

                    if (a < pag_total)
                    {
                        a = pag_total - a;
                    }
                    else if (a > pag_total)
                    {
                        MessageBox.Show("Valor Invalido");
                        a = pag_total / 2;
                        mtxtValPagoCRED.Text = String.Format("{0:c}", a);
                    }

                    mtxtValPagoDEB.Text = String.Format("{0:c}", a);
                }
                else
                    mtxtValPagoCRED.Text = String.Format("{0:c}", pag_total);
            }
            catch (Exception)
            {
            }

        }

        private void btAtualizarCardCRED_Click(object sender, EventArgs e)
        {
            if (cod_cred != "")
            {
                string comando = "UPDATE CARTAO_CRED SET COD_SEGURANCA_CRED = '" + txtCodSegurancaCRED.Text + "',"
                    + " NUM_CARTAO_CRED = '" + txtNumeroCartCRED.Text + "',"
                    + " DT_VALIDADE_CRED = '" + dtpValidadeCRED.Text + "'"
                    + " WHERE COD_CRED = " + cod_cred;

                Comand.Update(comando, connect);

            }
            else
            {
                MessageBox.Show("Esse cartão ainda não foi cadastrado para sofrer alterações no banco !", "ATENÇÃO");
            }
        }

        private void btAtualizarCardDEB_Click(object sender, EventArgs e)
        {
            if (cod_deb != "")
            {
                string comando = "UPDATE CARTAO_DEB SET COD_SEGURANCA_DEB = '" + txtCodSegurancaDEB.Text + "',"
                + " NUM_CARTAO_DEB = '" + txtNumeroCartDEB.Text + "',"
                + " DT_VALIDADE_DEB = '" + dtpValidadeDEB.Text + "' "
                + " WHERE COD_DRED = " + cod_deb;

                Comand.Update(comando, connect);

            }
            else
            {
                MessageBox.Show("Esse cartão ainda não foi cadastrado para sofrer alterações no banco !", "ATENÇÃO");
            }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            if (txtPlaca.Text != "")
            {
                consultaCar();
                dataEntrega();
            }
        }

        private void cbxCRED_CheckedChanged(object sender, EventArgs e)
        {
            cbxCRED.Checked = cbxCRED.Checked;
            dataEntrega();
            switch (cbxCRED.Checked)
            {
                case true:
                    lblCRED.Text = "Ativo";
                    mtxtValPagoCRED.Visible = true;
                    break;
                case false:
                    lblCRED.Text = "Desativo";
                    mtxtValPagoCRED.Visible = false;
                    break;
            }
            if (cbxCRED.Checked == false && cbxDEB.Checked == false)
            {
                MessageBox.Show("Ao menos uma das formas de pagamentos tem que estar selecionada");
                cbxCRED.Checked = true;
            }
        }

        private void cbxDEB_CheckedChanged(object sender, EventArgs e)
        {
            dataEntrega();
            switch (cbxDEB.Checked)
            {
                case true:
                    lblDEB.Text = "Ativo";
                    mtxtValPagoDEB.Visible = true;
                    break;
                case false:
                    lblDEB.Text = "Desativo";
                    mtxtValPagoDEB.Visible = false;
                    break;
            }
            if (cbxDEB.Checked == false && cbxCRED.Checked == false)
            {
                MessageBox.Show("Ao menos uma das formas de pagamentos tem que estar selecionada");
                cbxDEB.Checked = true;
            }
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            pbxCarro.Load(@"Carro\default.png");
            mtxtIdentificacao.Text = "";
            lblDataDevolucao.Visible = false;
            dtpDataDevolucao.Visible = false;
            cbxNomeCar.Text = "";
            txtPlaca.Text = "";
            mtxtTempo.Text = "";
            lblTotal.Text = "";

            lblSOM.Text = "";
            lblSOMBT.Text = "";
            lblGPS.Text = "";

            cbxCRED.Checked = true;
            cbxDEB.Checked = true;

            txtNumeroCartCRED.Text = "";
            txtNumeroCartDEB.Text = "";
            txtCodSegurancaCRED.Text = "";
            txtCodSegurancaDEB.Text = "";
            mtxtValPagoCRED.Text = "";
            mtxtValPagoDEB.Text = "";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btConsultaDeleta_Click(object sender, EventArgs e)
        {
            LocacaoConsultaDeleta a = new LocacaoConsultaDeleta();
            a.Show();
        }

        private void mtxtTempo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita apenas números e tecla backspace.
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita até 10 caracteres e a tecla backspace
            if (txtPlaca.Text.Length == 10 && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;
        }

        private void cbxNomeCar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aceita até 30 caracteres e a tecla backspace
            if (txtPlaca.Text.Length == 30 && !(e.KeyChar == (char)Keys.Back))
                e.Handled = true;
        }

        private void pbxCarro_Click(object sender, EventArgs e)
        {

        }
    }
}
