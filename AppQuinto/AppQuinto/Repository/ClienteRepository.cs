using AppQuinto.Models;
using AppQuinto.Repository.Contract;
using System.Data;
using MySql.Data.MySqlClient;


namespace AppQuinto.Repository
{
	public class ClienteRepository : IClienteRepository
	{
		private readonly string _conexaoMySQL;

		public ClienteRepository(IConfiguration conf)
		{
			_conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
		}
		public void Atualizar(Cliente cliente)
		{
			using (var conexao = new MySqlConnection(_conexaoMySQL))
			{
				conexao.Open();

				MySqlCommand cmd = new MySqlCommand("Update cliente set nome = @nome, endereco=@endereco," +
													"data_nascimento=@data_nascimento, cpf=@cpf, telefone=@telefone Where IdCli=@IdCli", conexao);
                cmd.Parameters.Add("@IdCli", MySqlDbType.VarChar).Value = cliente.idCli;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.nome;
				cmd.Parameters.Add("@endereco", MySqlDbType.VarChar).Value = cliente.endereco;
				cmd.Parameters.Add("@data_nascimento", MySqlDbType.VarChar).Value = cliente.data_nascimento.ToString("yyyy/MM/dd");
				cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cliente.cpf;
				cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.telefone;

				cmd.ExecuteNonQuery();
				conexao.Close();
			}
		}

		public void Cadastrar(Cliente cliente)
		{
			using (var conexao = new MySqlConnection(_conexaoMySQL))
			{
				conexao.Open();

				MySqlCommand cmd = new MySqlCommand("insert into cliente(nome, endereco, data_nascimento, cpf, telefone) " +
													" values (@nome, @endereco, @data_nascimento, @cpf, @telefone)", conexao);
				cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.nome;
				cmd.Parameters.Add("@endereco", MySqlDbType.VarChar).Value = cliente.endereco;
				cmd.Parameters.Add("@data_nascimento", MySqlDbType.VarChar).Value = cliente.data_nascimento.ToString("yyyy/MM/dd");
				cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cliente.cpf;
				cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.telefone;

				cmd.ExecuteNonQuery();
				conexao.Close();

			}
		}

		public void Excluir(int id)
		{
			using (var conexao = new MySqlConnection(_conexaoMySQL))
			{
				conexao.Open();
				MySqlCommand cmd = new MySqlCommand("delete from cliente where IdCli=@IdCli", conexao);
				cmd.Parameters.AddWithValue("@IdCli", id);
				int i = cmd.ExecuteNonQuery();
				conexao.Close();
			}
		}

		public IEnumerable<Cliente> ObterTodosClientes()
		{
			List<Cliente> ClienteList = new List<Cliente>();
			using (var conexao = new MySqlConnection(_conexaoMySQL))
			{
				conexao.Open();
				MySqlCommand cmd = new MySqlCommand("select * from cliente", conexao);

				MySqlDataAdapter da = new MySqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);

				conexao.Clone();

				foreach (DataRow dr in dt.Rows)
				{
					ClienteList.Add(
						new Cliente
						{
							idCli = Convert.ToInt32(dr["IdCli"]),
							nome = Convert.ToString(dr["nome"]),
							endereco = Convert.ToString(dr["endereco"]),
							data_nascimento = Convert.ToDateTime(dr["data_nascimento"]),
							cpf = Convert.ToDecimal(dr["cpf"]),
							telefone = Convert.ToString(dr["telefone"]),
						});
				}
				return ClienteList;
			}
		}

		public Cliente ObterCliente(int id)
		{
			using (var conexao = new MySqlConnection(_conexaoMySQL))
			{
				conexao.Open();
				MySqlCommand cmd = new MySqlCommand("SELECT * from cliente " +
													"where IdCli = @IdCli", conexao);
				cmd.Parameters.AddWithValue("@IdCli", id);

				MySqlDataAdapter da = new MySqlDataAdapter(cmd);
				MySqlDataReader dr;

				Cliente cliente = new Cliente();
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				while (dr.Read())
				{
					cliente.idCli = Convert.ToInt32(dr["IdCli"]);
					cliente.nome = Convert.ToString(dr["nome"]);
					cliente.endereco = Convert.ToString(dr["endereco"]);
					cliente.data_nascimento = Convert.ToDateTime(dr["data_nascimento"]);
					cliente.cpf = Convert.ToDecimal(dr["cpf"]);
					cliente.telefone = Convert.ToString(dr["telefone"]);
				}
				return cliente;
			}
		}
	}
}
