using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class MecanicoController : Banco
    {

        public void CadastrarMecanico(MecanicoModel mecanico)
        {
            try
            {
                Conectar();

                string sql = @"
                INSERT INTO mecanico
                (
                    id_usuario,
                    ds_especialidade_mecanico,
                    ds_observacao_mecanico
                )
                VALUES
                (
                    @idUsuario,
                    @especialidade,
                    @observacao
                );";

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue(
                    "@idUsuario",
                    mecanico.IdUsuario
                );

                cmd.Parameters.AddWithValue(
                    "@especialidade",
                    mecanico.EspecialidadeMecanico
                );

                cmd.Parameters.AddWithValue(
                    "@observacao",
                    mecanico.ObservacaoMecanico
                );

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao cadastrar mecânico. " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
