using MySql.Data.MySqlClient;
using System;

namespace PIM_3SEMESTRE.Controllers
{
    public class RelatorioController : Banco
    {

        public int TotalClientes()
        {
            try
            {
                Conectar();

                string sql =
                "SELECT COUNT(*) FROM cliente";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                return Convert.ToInt32(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                Desconectar();
            }
        }

        public int TotalMecanicos()
        {
            try
            {
                Conectar();

                string sql =
                "SELECT COUNT(*) FROM mecanico";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                return Convert.ToInt32(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                Desconectar();
            }
        }

        public int TotalServicos()
        {
            try
            {
                Conectar();

                string sql =
                "SELECT COUNT(*) FROM servico";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                return Convert.ToInt32(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                Desconectar();
            }
        }

        public decimal ValorTotalServicos()
        {
            try
            {
                Conectar();

                string sql =
                "SELECT IFNULL(SUM(vl_servico),0) FROM servico";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                return Convert.ToDecimal(
                    cmd.ExecuteScalar()
                );
            }
            finally
            {
                Desconectar();
            }
        }

        public MySqlDataReader ListarUltimosServicos()
        {
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

            ORDER BY s.id_servico DESC";

            return ConsultarSQL(sql);
        }

    }
}
