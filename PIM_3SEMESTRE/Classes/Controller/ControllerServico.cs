using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class ControllerServico
    {
        #region cadastarservico
        public void CadastrarServico(ModelServico servico)
        {
            try
            {
                Banco bd = new Banco();

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(new MySqlParameter(
                    "p_id_tipo_servico",
                    servico.IdTipoServico));

                parametros.Add(new MySqlParameter(
                    "p_nm_titulo_servico",
                    servico.NmTituloServico));

                parametros.Add(new MySqlParameter(
                    "p_ds_servico_resumido",
                    servico.DsServicoResumido));

                parametros.Add(new MySqlParameter(
                    "p_dt_cadastro_servico",
                    servico.DtCadastroServico));

                parametros.Add(new MySqlParameter(
                    "p_dt_prevista_entrega_servico",
                    servico.DtPrevistaEntregaServico));

                parametros.Add(new MySqlParameter(
                    "p_ds_prioridade_servico",
                    servico.DsPrioridadeServico));

                parametros.Add(new MySqlParameter(
                    "p_cd_placa_veiculo_servico",
                    servico.CdPlacaVeiculoServico));

                parametros.Add(new MySqlParameter(
                    "p_nm_modelo_veiculo_servico",
                    servico.NmModeloVeiculoServico));

                parametros.Add(new MySqlParameter(
                    "p_cd_ano_veiculo_servico",
                    servico.CdAnoVeiculoServico));

                parametros.Add(new MySqlParameter(
                    "p_nm_cor_veiculo_servico",
                    servico.NmCorVeiculoServico));

                parametros.Add(new MySqlParameter(
                    "p_qt_quilometragem_veiculo_servico",
                    servico.QtQuilometragemVeiculoServico));

                parametros.Add(new MySqlParameter(
                    "p_id_cliente",
                    servico.IdCliente));

                parametros.Add(new MySqlParameter(
                    "p_vl_servico",
                    servico.VlServico));

                parametros.Add(new MySqlParameter(
                    "p_ds_servico",
                    servico.DsServico));

                parametros.Add(new MySqlParameter(
                    "p_id_mecanico",
                    servico.IdMecanico));

                bd.Executar(
                    "sp_cadastrar_servico",
                    parametros);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao cadastrar serviço. " +
                    ex.Message);
            }
        }
        #endregion


        public MySqlDataReader ListarServicos()
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

                SELECT
                    s.id_servico,
                    s.nm_titulo_servico,
                    s.ds_servico_resumido,
                    s.dt_cadastro_servico,
                    s.dt_prevista_entrega_servico,
                    s.ds_prioridade_servico,
                    s.cd_placa_veiculo_servico,
                    s.nm_modelo_veiculo_servico,
                    s.cd_ano_veiculo_servico,
                    s.nm_cor_veiculo_servico,
                    s.qt_quilometragem_veiculo_servico,
                    s.vl_servico,
                    s.ds_servico,
                    s.st_servico,

                    c.id_cliente,
                    c.cd_telefone_cliente,
                    c.cd_cpf_cliente,

                    u.nm_usuario,
                    u.nm_email_usuario

                FROM servico s

                INNER JOIN cliente c
                    ON s.id_cliente = c.id_cliente

                INNER JOIN usuario u
                    ON c.id_usuario = u.id_usuario

                ORDER BY s.id_servico DESC

                ";

                return bd.ConsultarSQL(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao listar serviços. " +
                    ex.Message);
            }
        }

        public MySqlDataReader BuscarServico(int idServico)
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

SELECT
    s.id_servico,
    s.nm_titulo_servico,
    s.ds_servico_resumido,
    s.dt_cadastro_servico,
    s.dt_prevista_entrega_servico,
    s.ds_prioridade_servico,
    s.cd_placa_veiculo_servico,
    s.nm_modelo_veiculo_servico,
    s.cd_ano_veiculo_servico,
    s.nm_cor_veiculo_servico,
    s.qt_quilometragem_veiculo_servico,
    s.vl_servico,
    s.ds_servico,
    s.st_servico,

    c.id_cliente,
    c.cd_telefone_cliente,
    c.cd_cpf_cliente,

    u.nm_usuario,
    u.nm_email_usuario,

    ts.nm_tipo_servico,

    m.id_mecanico,
    um.nm_usuario AS nm_mecanico

FROM servico s

INNER JOIN cliente c
    ON s.id_cliente = c.id_cliente

INNER JOIN usuario u
    ON c.id_usuario = u.id_usuario

INNER JOIN tipo_servico ts
    ON s.id_tipo_servico = ts.id_tipo_servico

INNER JOIN mecanico m
    ON s.id_mecanico = m.id_mecanico

INNER JOIN usuario um
    ON m.id_usuario = um.id_usuario

WHERE s.id_servico = @idServico

";

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@idServico",
                        idServico));

                return bd.ConsultarSQL(
                    sql,
                    parametros);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao buscar serviço. " +
                    ex.Message);
            }
        }



        public void AtualizarStatus(
            int idServico,
            string status)
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

                UPDATE servico
                SET st_servico = @status
                WHERE id_servico = @idServico

                ";

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@status",
                        status));

                parametros.Add(
                    new MySqlParameter(
                        "@idServico",
                        idServico));

                bd.Conectar();

                MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        new MySqlConnection(
                            DadosConexao.GetConexao()
                        )
                    );

                cmd.Connection.Open();

                cmd.Parameters.Clear();

                foreach (MySqlParameter item in parametros)
                {
                    cmd.Parameters.Add(item);
                }

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao atualizar status. " +
                    ex.Message);
            }
        }


        public void ExcluirServico(int idServico)
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

                DELETE FROM servico
                WHERE id_servico = @idServico

                ";

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@idServico",
                        idServico));

                MySqlConnection conexao =
                    new MySqlConnection(
                        DadosConexao.GetConexao()
                    );

                conexao.Open();

                MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        conexao);

                cmd.Parameters.Clear();

                foreach (MySqlParameter item in parametros)
                {
                    cmd.Parameters.Add(item);
                }

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao excluir serviço. " +
                    ex.Message);
            }
        }


        public MySqlDataReader ListarTiposServico()
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

                SELECT
                    id_tipo_servico,
                    nm_tipo_servico
                FROM tipo_servico
                ORDER BY nm_tipo_servico

                ";

                return bd.ConsultarSQL(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao listar tipos de serviço. " +
                    ex.Message);
            }
        }


        public void EditarServico(ModelServico servico)
        {
            try
            {
                string sql = @"

                UPDATE servico SET

                    id_tipo_servico = @idTipoServico,
                    nm_titulo_servico = @titulo,
                    ds_servico_resumido = @descricaoResumida,
                    dt_prevista_entrega_servico = @dataEntrega,
                    ds_prioridade_servico = @prioridade,
                    cd_placa_veiculo_servico = @placa,
                    nm_modelo_veiculo_servico = @modelo,
                    cd_ano_veiculo_servico = @ano,
                    nm_cor_veiculo_servico = @cor,
                    qt_quilometragem_veiculo_servico = @km,
                    vl_servico = @valor,
                    ds_servico = @descricao

                WHERE id_servico = @idServico

                ";

                MySqlConnection conexao =
                    new MySqlConnection(
                        DadosConexao.GetConexao()
                    );

                conexao.Open();

                MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        conexao);

                cmd.Parameters.AddWithValue(
                    "@idTipoServico",
                    servico.IdTipoServico);

                cmd.Parameters.AddWithValue(
                    "@titulo",
                    servico.NmTituloServico);

                cmd.Parameters.AddWithValue(
                    "@descricaoResumida",
                    servico.DsServicoResumido);

                cmd.Parameters.AddWithValue(
                    "@dataEntrega",
                    servico.DtPrevistaEntregaServico);

                cmd.Parameters.AddWithValue(
                    "@prioridade",
                    servico.DsPrioridadeServico);

                cmd.Parameters.AddWithValue(
                    "@placa",
                    servico.CdPlacaVeiculoServico);

                cmd.Parameters.AddWithValue(
                    "@modelo",
                    servico.NmModeloVeiculoServico);

                cmd.Parameters.AddWithValue(
                    "@ano",
                    servico.CdAnoVeiculoServico);

                cmd.Parameters.AddWithValue(
                    "@cor",
                    servico.NmCorVeiculoServico);

                cmd.Parameters.AddWithValue(
                    "@km",
                    servico.QtQuilometragemVeiculoServico);

                cmd.Parameters.AddWithValue(
                    "@valor",
                    servico.VlServico);

                cmd.Parameters.AddWithValue(
                    "@descricao",
                    servico.DsServico);

                cmd.Parameters.AddWithValue(
                    "@idServico",
                    servico.IdServico);

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao editar serviço. " +
                    ex.Message);
            }
        }




        public MySqlDataReader ListarMecanicos()
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

        SELECT
            m.id_mecanico,
            u.nm_usuario

        FROM mecanico m

        INNER JOIN usuario u
            ON m.id_usuario = u.id_usuario

        ORDER BY u.nm_usuario

        ";

                return bd.ConsultarSQL(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao listar mecânicos. " +
                    ex.Message);
            }
        }



        public MySqlDataReader ListarServicosCliente(
            int idUsuario)
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

SELECT
    s.id_servico,
    s.nm_titulo_servico,
    s.ds_servico_resumido,
    s.dt_cadastro_servico,
    s.dt_prevista_entrega_servico,
    s.ds_prioridade_servico,
    s.cd_placa_veiculo_servico,
    s.nm_modelo_veiculo_servico,
    s.cd_ano_veiculo_servico,
    s.nm_cor_veiculo_servico,
    s.qt_quilometragem_veiculo_servico,
    s.vl_servico,
    s.ds_servico,
    s.st_servico,

    ts.nm_tipo_servico,

    um.nm_usuario AS nm_mecanico

FROM servico s

INNER JOIN cliente c
    ON s.id_cliente = c.id_cliente

INNER JOIN usuario u
    ON c.id_usuario = u.id_usuario

INNER JOIN tipo_servico ts
    ON s.id_tipo_servico = ts.id_tipo_servico

INNER JOIN mecanico m
    ON s.id_mecanico = m.id_mecanico

INNER JOIN usuario um
    ON m.id_usuario = um.id_usuario

WHERE u.id_usuario = @idUsuario

ORDER BY s.id_servico DESC

";

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@idUsuario",
                        idUsuario));

                return bd.ConsultarSQL(
                    sql,
                    parametros);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao listar histórico do cliente. " +
                    ex.Message);
            }
        }

        public MySqlDataReader ListarServicosMecanico(int idUsuario)
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

SELECT
    s.id_servico,
    s.nm_titulo_servico,
    s.ds_servico_resumido,
    s.dt_cadastro_servico,
    s.dt_prevista_entrega_servico,
    s.ds_prioridade_servico,
    s.cd_placa_veiculo_servico,
    s.nm_modelo_veiculo_servico,
    s.cd_ano_veiculo_servico,
    s.nm_cor_veiculo_servico,
    s.qt_quilometragem_veiculo_servico,
    s.vl_servico,
    s.ds_servico,
    s.st_servico,

    c.id_cliente,

    uc.nm_usuario AS nm_cliente,

    um.nm_usuario AS nm_mecanico

FROM servico s

INNER JOIN cliente c
    ON s.id_cliente = c.id_cliente

INNER JOIN usuario uc
    ON c.id_usuario = uc.id_usuario

INNER JOIN mecanico m
    ON s.id_mecanico = m.id_mecanico

INNER JOIN usuario um
    ON m.id_usuario = um.id_usuario

WHERE um.id_usuario = @idUsuario

ORDER BY s.id_servico DESC

";

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@idUsuario",
                        idUsuario));

                return bd.ConsultarSQL(
                    sql,
                    parametros);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao listar serviços do mecânico. " +
                    ex.Message);
            }
        }
    

    public MySqlDataReader BuscarServicoPorCpf(string cpf)
        {
            try
            {
                Banco bd = new Banco();

                string sql = @"

        SELECT
            s.id_servico,
            s.nm_titulo_servico,
            s.ds_servico_resumido,
            s.dt_cadastro_servico,
            s.dt_prevista_entrega_servico,
            s.ds_prioridade_servico,
            s.cd_placa_veiculo_servico,
            s.nm_modelo_veiculo_servico,
            s.cd_ano_veiculo_servico,
            s.nm_cor_veiculo_servico,
            s.qt_quilometragem_veiculo_servico,
            s.vl_servico,
            s.ds_servico,
            s.st_servico,

            c.id_cliente,
            c.cd_telefone_cliente,
            c.cd_cpf_cliente,

            u.nm_usuario,
            u.nm_email_usuario

        FROM servico s

        INNER JOIN cliente c
            ON s.id_cliente = c.id_cliente

        INNER JOIN usuario u
            ON c.id_usuario = u.id_usuario

        WHERE REPLACE(REPLACE(c.cd_cpf_cliente, '.', ''), '-', '')
              LIKE CONCAT('%', @cpf, '%')

        ORDER BY s.id_servico DESC

        ";

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@cpf",
                        cpf
                    )
                );

                return bd.ConsultarSQL(
                    sql,
                    parametros
                );
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao buscar serviço por CPF. " +
                    ex.Message
                );
            }
        }
    }
}