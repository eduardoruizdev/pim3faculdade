using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento dos tipos de serviços.
    /// Realiza operações de cadastro, consulta, verificação e exclusão.
    /// </summary>
    public class TipoServicoController : Banco
    {
        #region Cadastrar Tipo de Serviço

        /// <summary>
        /// Cadastra um novo tipo de serviço no banco de dados.
        /// </summary>
        /// <param name="tipoServico">
        /// Objeto contendo os dados do tipo de serviço.
        /// </param>
        public void CadastrarTipoServico(
            TipoServicoModel tipoServico)
        {
            try
            {
                // Abre conexão com o banco de dados
                Conectar();

                // Comando SQL para inserir um novo tipo de serviço
                string sql = @"
                INSERT INTO tipo_servico
                (
                    nm_tipo_servico
                )
                VALUES
                (
                    @nome
                );";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Adiciona o parâmetro com o nome do serviço
                cmd.Parameters.AddWithValue(
                    "@nome",
                    tipoServico.NomeTipoServico
                );

                // Executa o comando INSERT
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Lança exceção personalizada
                throw new Exception(
                "Erro ao cadastrar tipo de serviço. "
                + ex.Message);
            }
            finally
            {
                // Fecha a conexão com o banco
                Desconectar();
            }
        }

        #endregion

        #region Verificar Existência do Tipo de Serviço

        /// <summary>
        /// Verifica se um tipo de serviço já está cadastrado.
        /// </summary>
        /// <param name="nomeServico">
        /// Nome do tipo de serviço a ser pesquisado.
        /// </param>
        /// <returns>
        /// True se existir; False caso contrário.
        /// </returns>
        public bool VerificarTipoServicoExiste(
            string nomeServico)
        {
            try
            {
                // Consulta SQL para verificar existência do registro
                string sql = @"
                SELECT *
                FROM tipo_servico
                WHERE nm_tipo_servico = @nome";

                // Lista de parâmetros da consulta
                List<MySqlParameter> parametros =
                new List<MySqlParameter>();

                // Adiciona o parâmetro nome
                parametros.Add(
                    new MySqlParameter(
                        "@nome",
                        nomeServico
                    )
                );

                // Executa a consulta
                MySqlDataReader dados =
                ConsultarSQL(sql, parametros);

                // Retorna verdadeiro se houver registros encontrados
                return dados.HasRows;
            }
            catch (Exception ex)
            {
                // Lança exceção personalizada
                throw new Exception(
                "Erro ao verificar serviço. "
                + ex.Message);
            }
        }

        #endregion

        #region Listar Tipos de Serviço

        /// <summary>
        /// Retorna todos os tipos de serviços cadastrados.
        /// </summary>
        /// <returns>
        /// MySqlDataReader contendo os registros encontrados.
        /// </returns>
        public MySqlDataReader ListarTiposServico()
        {
            try
            {
                // Consulta SQL para listar todos os tipos de serviço
                string sql = @"
                SELECT *
                FROM tipo_servico
                ORDER BY id_tipo_servico ASC";

                // Executa a consulta e retorna os dados
                return ConsultarSQL(sql);
            }
            catch (Exception ex)
            {
                // Lança exceção personalizada
                throw new Exception(
                "Erro ao listar serviços. "
                + ex.Message);
            }
        }

        #endregion

        #region Excluir Tipo de Serviço

        /// <summary>
        /// Remove um tipo de serviço do banco de dados.
        /// </summary>
        /// <param name="id">
        /// Identificador do tipo de serviço.
        /// </param>
        public void ExcluirTipoServico(int id)
        {
            try
            {
                // Abre conexão com o banco
                Conectar();

                // Comando SQL para exclusão do registro
                string sql = @"
                DELETE FROM tipo_servico
                WHERE id_tipo_servico = @id";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Adiciona o parâmetro ID
                cmd.Parameters.AddWithValue("@id", id);

                // Executa a exclusão
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Lança exceção personalizada
                throw new Exception(
                "Erro ao excluir serviço. "
                + ex.Message);
            }
            finally
            {
                // Fecha a conexão com o banco
                Desconectar();
            }
        }

        #endregion
    }
}