namespace Prototipo_Sistema_Adminiscar.Models
{
    class Cliente
    {
        public int cod_cliente { get; set; }

        public string nome_cliente { get; set; }

        public string cpf_cnpj { get; set; }

        public string cnh_cliente { get; set; }

        public int cod_tell_fk { get; set; }

        public int cod_endereco_fk { get; set; }

    }
}

