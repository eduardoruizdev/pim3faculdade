using MySql.Data.MySqlClient;
using System;

namespace PIM_3SEMESTRE.Controllers
{
    /// <summary>
    /// Controller responsável pela geração dos relatórios
    /// e indicadores do sistema.
    /// </summary>
    public class RelatorioController : Banco
    {
        #region Total de Clientes

        /// <summary>
        /// Retorna a quantidade total de clientes cadastrados.
        /// </summary>
        /// <returns>Total de clientes.</returns>
        public int TotalClientes()
        {
            try
            {
                // Abre conexão com o banco de dados
                Conectar();

                // Consulta SQL para contar todos os clientes
                string sql =
                "SELECT COUNT(*) FROM cliente";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Executa a consulta e retorna o resultado convertido para inteiro
                return Convert.ToInt32(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                // Fecha a conexão com o banco
                Desconectar();
            }
        }

        #endregion

        #region Total de Mecânicos

        /// <summary>
        /// Retorna a quantidade total de mecânicos cadastrados.
        /// </summary>
        /// <returns>Total de mecânicos.</returns>
        public int TotalMecanicos()
        {
            try
            {
                // Abre conexão com o banco de dados
                Conectar();

                // Consulta SQL para contar todos os mecânicos
                string sql =
                "SELECT COUNT(*) FROM mecanico";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Executa a consulta e retorna o resultado convertido para inteiro
                return Convert.ToInt32(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                // Fecha a conexão com o banco
                Desconectar();
            }
        }

        #endregion

        #region Total de Serviços

        /// <summary>
        /// Retorna a quantidade total de serviços cadastrados.
        /// </summary>
        /// <returns>Total de serviços.</returns>
        public int TotalServicos()
        {
            try
            {
                // Abre conexão com o banco
                Conectar();

                // Consulta SQL para contar os serviços
                string sql =
                "SELECT COUNT(*) FROM servico";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Executa a consulta e retorna o resultado
                return Convert.ToInt32(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                // Fecha a conexão
                Desconectar();
            }
        }

        #endregion

        #region Valor Total dos Serviços

        /// <summary>
        /// Retorna a soma do valor de todos os serviços cadastrados.
        /// </summary>
        /// <returns>Valor total dos serviços.</returns>
        public decimal ValorTotalServicos()
        {
            try
            {
                // Abre conexão com o banco
                Conectar();

                // Soma todos os valores dos serviços.
                // IFNULL garante retorno 0 caso não existam registros.
                string sql =
                "SELECT IFNULL(SUM(vl_servico),0) FROM servico";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Executa a consulta e retorna o valor convertido para decimal
                return Convert.ToDecimal(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                // Fecha a conexão
                Desconectar();
            }
        }

        #endregion

        #region Últimos Serviços

        /// <summary>
        /// Lista os serviços cadastrados mais recentemente.
        /// </summary>
        /// <returns>MySqlDataReader contendo os dados dos serviços.</returns>
        public MySqlDataReader ListarUltimosServicos()
        {
            // Consulta que retorna informações do serviço,
            // cliente, mecânico e tipo de serviço.
            string sql = @"

            SELECT
                s.id_servico,
                ts.nm_tipo_servico,
                ucliente.nm_usuario AS cliente,
                umecanico.nm_usuario AS mecanico,
                s.st_servico,
                s.vl_servico

            FROM servico s

            INNER JOIN tipo_servico ts
                ON s.id_tipo_servico =
                ts.id_tipo_servico

            INNER JOIN cliente c
                ON s.id_cliente =
                c.id_cliente

            INNER JOIN usuario ucliente
                ON c.id_usuario =
                ucliente.id_usuario

            INNER JOIN mecanico m
                ON s.id_mecanico =
                m.id_mecanico

            INNER JOIN usuario umecanico
                ON m.id_usuario =
                umecanico.id_usuario

            -- Ordena os registros do mais recente para o mais antigo
            ORDER BY s.id_servico DESC";

            // Executa a consulta e retorna os resultados
            return ConsultarSQL(sql);
        }

        #endregion
    }
}