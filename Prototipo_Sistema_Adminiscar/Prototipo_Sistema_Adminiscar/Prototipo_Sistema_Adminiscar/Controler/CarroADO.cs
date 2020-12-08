using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototipo_Sistema_Adminiscar.Models;
using SqlServer;
using System.Windows.Forms;
using System.Collections;

namespace Prototipo_Sistema_Adminiscar.Controler
{
    class CarroADO
    {
        static string connect = Connection.Route("ADMINISCAR");

        internal static void cadastraCarro(Carro car)
        {
            var comando = "INSERT INTO CARRO (NOME_CAR, PLACA, "
                            + "RENAVAM, MODELO, CATEGORIA, COMBUSTIVEL, QUILOMETRAGEM, "
                            + "SITUACAO, VALOR_DIARIO, VALOR_SEMANAL, VALOR_MENSAL, SOM, SOM_BT, GPS) values('"
                            + car.NOME_CAR + "','" 
                            + car.PLACA + "','"
                            + car.RENAVAM + "','"
                            + car.MODELO + "','"
                            + car.CATEGORIA + "','"
                            + car.COMBUSTIVEL
                            + "', 0, 'PATIO','"
                            + car.VALOR_DIARIO
                            + "','" + car.VALOR_SEMANAL + "','"
                            + car.VALOR_MENSAL
                            + "','" + car.SOM
                            + "','" + car.SOM_BT
                            + "','" + car.GPS
                            + "' )";

            try
            {
                if (Comand.Insert(comando, connect))
                {
                    MessageBox.Show("Cadastrado com sucesso");
                }else
                {
                    Comand.ErroComand(comando, connect);
                }
            }
            catch
            {

                throw;
            }
        }

        internal static Carro ConsultaCarroPlaca(String placa)
        {
            string[] campos = new string[] {
                "CATEGORIA",
                "NOME_CAR",
                "RENAVAM",
                "COMBUSTIVEL",
                "MODELO",
                "SOM",
                "SOM_BT",
                "GPS",
                "VALOR_DIARIO",
                "VALOR_SEMANAL",
                "VALOR_MENSAL",
                "COD_CAR",
                "SITUACAO",
                "QUILOMETRAGEM"
            };

            string comando = "SELECT * FROM CARRO where PLACA = '" + placa + "'";

            ArrayList car = Comand.Select.ArryaListFormat(comando, campos, connect);

            //tenta puxar os dados do banco e colocar nos campos
            try
            {
                var newCar = new Carro()
                {
                    PLACA = placa,                                              //Placa
                    CATEGORIA = car[0].ToString(),                              //Categoria
                    NOME_CAR = car[1].ToString(),                               //Nome
                    RENAVAM = car[2].ToString(),                                //Renavam  
                    COMBUSTIVEL = car[3].ToString(),                            //Combustivel
                    MODELO = car[4].ToString(),                                 //Modelo do Carro
                    SOM = char.Parse(car[5].ToString()),                        //SOM
                    SOM_BT = char.Parse(car[6].ToString()),                     //SOM BT
                    GPS = char.Parse(car[7].ToString()),                        //GPS
                    VALOR_DIARIO =  double.Parse(car[8] .ToString()),           //Valor Diario
                    VALOR_SEMANAL = double.Parse(car[9] .ToString()),           //Valor Semanal
                    VALOR_MENSAL =  double.Parse(car[10].ToString()),           //Valor Mensal
                    COD_CAR = int.Parse(car[11].ToString()),                    //Cod Car
                    SITUACAO = car[12].ToString(),                              //Sitiação do carro
                    QUILOMETRAGEM = int.Parse(car[13].ToString())               //Quilometragem
                };
                return newCar;
            }
            //se não encontrar um parametro o programa envia uma Mensagem informando ao usuario
            catch (Exception)
            {
                    return null;
            }
        }

        public static bool UpdateCarro(Carro car)
        {
            string comando = "UPDATE CARRO SET NOME_CAR ='" + car.NOME_CAR 
                + "', RENAVAM ='" + car.RENAVAM
                + "', MODELO ='" + car.MODELO
                + "', COMBUSTIVEL ='" + car.COMBUSTIVEL
                + "', VALOR_DIARIO ='" + car.VALOR_DIARIO
                + "', VALOR_SEMANAL ='" + car.VALOR_SEMANAL
                + "', VALOR_MENSAL ='" + car.VALOR_MENSAL
                + "' WHERE PLACA ='" + car.PLACA + "'";

            return Comand.Update(comando, connect);

        }
    }
}
