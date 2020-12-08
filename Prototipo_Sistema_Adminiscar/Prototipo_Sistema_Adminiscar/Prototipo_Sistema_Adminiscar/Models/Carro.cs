using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototipo_Sistema_Adminiscar.Models
{
    class Carro
    {
        public int COD_CAR { get; set; }
        public string NOME_CAR { get; set; }
        public string PLACA { get; set; }
        public string RENAVAM { get; set; }
        public string MODELO { get; set; }
        public string CATEGORIA { get; set; }
        public string COMBUSTIVEL { get; set; }
        public int QUILOMETRAGEM { get; set; }
        public string SITUACAO { get; set; }
        public double VALOR_DIARIO { get; set; }
        public double VALOR_SEMANAL { get; set; }
        public double VALOR_MENSAL { get; set; }
        public char SOM { get; set; }
        public char SOM_BT { get; set; }
        public char GPS { get; set; }
    }
}
