namespace Prototipo_Sistema_Adminiscar.Models
{
    class Funcionario
    {
        public int func_cod { get; set; }

        public string func_nome { get; set; }

        public string func_cpf { get; set; }

        public string func_cnh { get; set; }

        public int func_email { get; set; }

        public int func_senha { get; set; }

        public int func_nivel_acesso { get; set; }

        /*e preciso um cadastro de endereço e de telefone para se obter os codigos deles (cod_tell e cod_endereco)*/

        //esse codigo de endereco tem que vir do cadastro de uma endereço em outra class
        public int cod_endereco_fk { get; set; }

        //esse codigo de telefone tem que vir do cadastro de um telefone em outra class
        public int cod_tell_fk { get; set; }
    }
}
