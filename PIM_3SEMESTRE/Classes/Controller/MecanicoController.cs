using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class MecanicoController : Banco
    {
        #region Cadastrar Mecanico
        public void CadastrarMecanico(MecanicoModel mecanico) //criando metodo de cadastrar mecanico
        {
            try
            {
                Conectar(); //conectando a classe banco

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
                );"; //executando comando diretamente no banco de dados

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao); //declarando comando mysql

                cmd.Parameters.AddWithValue(
                    "@idUsuario",
                    mecanico.IdUsuario
                ); //adicionando parametro

                cmd.Parameters.AddWithValue(
                    "@especialidade",
                    mecanico.EspecialidadeMecanico
                ); //adicionando parameto

                cmd.Parameters.AddWithValue(
                    "@observacao",
                    mecanico.ObservacaoMecanico
                );//adicionando parametro

                cmd.ExecuteNonQuery(); //mandando executar o comando
            }
            catch (Exception ex) //exibindo mensagem de erro
            {
                throw new Exception(
                "Erro ao cadastrar mecânico. " + ex.Message);
            }
            finally
            {
                Desconectar(); //desconcetando do banco
            }
        }
        #endregion
    }
}
