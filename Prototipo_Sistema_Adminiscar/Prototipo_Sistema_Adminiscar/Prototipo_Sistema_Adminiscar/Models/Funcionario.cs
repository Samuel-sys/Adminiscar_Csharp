namespace Prototipo_Sistema_Adminiscar.Models
{
    class Funcionario
    {
        public int func_cod { get; set; }

        public string nome_func { get; set; }

        public string cpf_func { get; set; }

        public string cnh_func { get; set; }

        /*e preciso um cadastro de endereço e de telefone para se obter os codigos deles (cod_tell e cod_endereco)*/

        //esse codigo de endereco tem que vir do cadastro de uma endereço em outra class
        public int cod_endereco_fk { get; set; }

        //esse codigo de telefone tem que vir do cadastro de um telefone em outra class
        public int cod_tell_fk { get; set; }


    }
}
