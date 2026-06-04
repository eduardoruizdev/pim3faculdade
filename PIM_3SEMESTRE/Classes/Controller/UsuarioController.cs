using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento dos usuários do sistema.
    /// Realiza operações de cadastro e validação de usuários.
    /// </summary>
    public class UsuarioController : Banco
    {
        #region Cadastrar Usuário

        /// <summary>
        /// Cadastra um novo usuário no banco de dados.
        /// </summary>
        /// <param name="usuario">
        /// Objeto contendo os dados do usuário.
        /// </param>
        /// <returns>
        /// Retorna o ID do usuário cadastrado.
        /// </returns>
        public int CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                // Abre conexão com o banco de dados
                Conectar();

                // Comando SQL responsável por inserir um novo usuário
                string sql = @"
                INSERT INTO usuario
                (
                    nm_usuario,
                    nm_email_usuario,
                    nm_senha_usuario,
                    id_tipo_usuario
                )
                VALUES
                (
                    @nome,
                    @email,
                    @senha,
                    @tipo
                );";

                // Cria o comando SQL
                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                // Adiciona o nome do usuário como parâmetro
                cmd.Parameters.AddWithValue(
                    "@nome",
                    usuario.NomeUsuario);

                // Adiciona o e-mail do usuário como parâmetro
                cmd.Parameters.AddWithValue(
                    "@email",
                    usuario.EmailUsuario);

                // Adiciona a senha do usuário como parâmetro
                cmd.Parameters.AddWithValue(
                    "@senha",
                    usuario.SenhaUsuario);

                // Adiciona o tipo de usuário como parâmetro
                cmd.Parameters.AddWithValue(
                    "@tipo",
                    usuario.IdTipoUsuario);

                // Executa o comando INSERT
                cmd.ExecuteNonQuery();

                // Obtém o ID gerado automaticamente pelo banco
                int idUsuario =
                Convert.ToInt32(
                    cmd.LastInsertedId);

                // Retorna o ID do usuário cadastrado
                return idUsuario;
            }
            catch (Exception ex)
            {
                // Lança uma exceção personalizada
                throw new Exception(
                "Erro ao cadastrar usuário. " +
                ex.Message);
            }
            finally
            {
                // Fecha a conexão com o banco de dados
                Desconectar();
            }
        }

        #endregion

        #region Verificar E-mail

        /// <summary>
        /// Verifica se já existe um usuário cadastrado
        /// com o e-mail informado.
        /// </summary>
        /// <param name="email">
        /// E-mail a ser pesquisado.
        /// </param>
        /// <returns>
        /// True se o e-mail existir;
        /// False caso contrário.
        /// </returns>
        public bool VerificarEmailExiste(string email)
        {
            try
            {
                // Consulta SQL para verificar a existência do e-mail
                string sql = @"
                SELECT *
                FROM usuario
                WHERE nm_email_usuario = @email";

                // Lista de parâmetros da consulta
                List<MySqlParameter> parametros =
                new List<MySqlParameter>();

                // Adiciona o parâmetro e-mail
                parametros.Add(
                    new MySqlParameter(
                        "@email",
                        email)
                );

                // Executa a consulta
                MySqlDataReader dados =
                ConsultarSQL(
                    sql,
                    parametros);

                // Retorna verdadeiro caso exista registro
                return dados.HasRows;
            }
            catch (Exception ex)
            {
                // Lança uma exceção personalizada
                throw new Exception(
                "Erro ao verificar email. " +
                ex.Message);
            }
        }

        #endregion
    }
}