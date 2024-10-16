using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace AppQuinto.Models
{
	public class Cliente
	{
		[Display(Name = "Código")]
		public int? idCli { get; set; }

		[Display(Name = "Nome Completo")]
		[Required(ErrorMessage = "O campo nome é obrigatório")]
		public string nome { get; set; }

		[Display(Name = "Endereco")]
		[Required(ErrorMessage = "O campo endereco é obrigatório")]
		public string endereco { get; set; }

		[Display(Name = "Telefone")]
		[Required(ErrorMessage = "O campo Telefone é obrigatório")]
		public string telefone { get; set; }

		[Display(Name = "CPF")]
		[Required(ErrorMessage = "O campo CPF é obrigatório")]
		public decimal cpf {  get; set; }

		[Display(Name = "Nascimento")]
		[Required(ErrorMessage = "O campo nascimento é obrigatório")]
		[DataType(DataType.DateTime)]
		public DateTime data_nascimento { get; set; }
	}
}
