using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class ClienteController
    {
        public void CadastrarCliente(ClienteModel cliente)
        {
            try
            {
                Banco bd = new Banco();

                List<MySqlParameter> parametros = new List<MySqlParameter>();

                parametros.Add(new MySqlParameter("p_nm_usuario", cliente.NomeUsuario));

                parametros.Add(new MySqlParameter("p_nm_email_usuario", cliente.EmailUsuario));

                parametros.Add(new MySqlParameter("p_nm_senha_usuario", cliente.SenhaUsuario));

                parametros.Add(new MySqlParameter("p_cd_cpf_cliente", cliente.CpfCliente));

                parametros.Add(new MySqlParameter("p_dt_nascimento_cliente", cliente.DataNascimentoCliente));

                parametros.Add(new MySqlParameter("p_cd_telefone_cliente", cliente.TelefoneCliente));

                parametros.Add(new MySqlParameter("p_cd_cep_cliente", cliente.CepCliente));

                parametros.Add(new MySqlParameter("p_nm_rua_cliente", cliente.RuaCliente));

                parametros.Add(new MySqlParameter("p_cd_numero_residencia_cliente", cliente.NumeroResidenciaCliente));

                parametros.Add(new MySqlParameter("p_ds_complemento_residencia_cliente", cliente.ComplementoResidenciaCliente));

                parametros.Add(new MySqlParameter("p_nm_bairro_cliente", cliente.BairroCliente));

                parametros.Add(new MySqlParameter("p_nm_cidade_cliente", cliente.CidadeCliente));

                parametros.Add(new MySqlParameter("p_cd_estado_residencia_cliente", cliente.EstadoResidenciaCliente));

                parametros.Add(new MySqlParameter("p_ds_observacao_cliente", cliente.ObservacaoCliente));

                bd.Executar("sp_cadastrar_cliente", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar cliente. " + ex.Message);
            }
        }

        public MySqlDataReader ListarClientes()
        {
            try
            {
                Banco bd = new Banco();

                return bd.Consultar("sp_listar_clientes");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes. " + ex.Message);
            }
        }
    

    public MySqlDataReader BuscarClientePorId(int idCliente)
        {
            try
            {
                Banco bd = new Banco();

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "p_id_cliente",
                        idCliente
                    )
                );

                return bd.Consultar(
                    "sp_buscar_cliente_por_id",
                    parametros
                );
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao buscar cliente. " + ex.Message
                );
            }
        }
    } 
}
