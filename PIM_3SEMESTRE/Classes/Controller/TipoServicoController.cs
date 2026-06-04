using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class TipoServicoController : Banco
    {

        public void CadastrarTipoServico(
            TipoServicoModel tipoServico)
        {
            try
            {
                Conectar();

                string sql = @"
                INSERT INTO tipo_servico
                (
                    nm_tipo_servico
                )
                VALUES
                (
                    @nome
                );";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue(
                    "@nome",
                    tipoServico.NomeTipoServico
                );

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao cadastrar tipo de serviço. "
                + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public bool VerificarTipoServicoExiste(
            string nomeServico)
        {
            try
            {
                string sql = @"
                SELECT *
                FROM tipo_servico
                WHERE nm_tipo_servico = @nome";

                List<MySqlParameter> parametros =
                new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@nome",
                        nomeServico
                    )
                );

                MySqlDataReader dados =
                ConsultarSQL(sql, parametros);

                return dados.HasRows;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao verificar serviço. "
                + ex.Message);
            }
        }
    

public MySqlDataReader ListarTiposServico()
        {
            try
            {
                string sql = @"
        SELECT *
        FROM tipo_servico
        ORDER BY id_tipo_servico ASC";

                return ConsultarSQL(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao listar serviços. "
                + ex.Message);
            }
        }

        public void ExcluirTipoServico(int id)
        {
            try
            {
                Conectar();

                string sql = @"
        DELETE FROM tipo_servico
        WHERE id_tipo_servico = @id";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao excluir serviço. "
                + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
    }
}