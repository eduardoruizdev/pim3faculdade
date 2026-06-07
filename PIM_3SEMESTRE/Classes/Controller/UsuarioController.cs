using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PIM_3SEMESTRE.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento dos usuários do sistema.
    /// Realiza operações de cadastro e validação de usuários.
    /// </summary>
    public class UsuarioController : Banco
    {
        #region Cadastrar Usuário

        public int CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                Conectar();

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

                MySqlCommand cmd =
                new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue(
                    "@nome",
                    usuario.NomeUsuario);

                cmd.Parameters.AddWithValue(
                    "@email",
                    usuario.EmailUsuario);

                cmd.Parameters.AddWithValue(
                    "@senha",
                    usuario.SenhaUsuario);

                cmd.Parameters.AddWithValue(
                    "@tipo",
                    usuario.IdTipoUsuario);

                cmd.ExecuteNonQuery();

                int idUsuario =
                Convert.ToInt32(
                    cmd.LastInsertedId);

                return idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao cadastrar usuário. " +
                ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        #endregion

        #region Verificar E-mail

        public bool VerificarEmailExiste(string email)
        {
            try
            {
                string sql = @"
                SELECT *
                FROM usuario
                WHERE nm_email_usuario = @email";

                List<MySqlParameter> parametros =
                new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter(
                        "@email",
                        email)
                );

                MySqlDataReader dados =
                ConsultarSQL(
                    sql,
                    parametros);

                return dados.HasRows;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao verificar email. " +
                ex.Message);
            }
        }

        #endregion

        #region Listar Funcionários
    public DataTable ListarFuncionarios()
        {
            try
            {
                Conectar();

                string sql = @"
        SELECT
            u.id_usuario,
            u.nm_usuario,
            u.nm_email_usuario,
            t.nm_tipo_usuario
        FROM usuario u

        INNER JOIN tipo_usuario t
            ON u.id_tipo_usuario =
               t.id_tipo_usuario

        WHERE u.id_tipo_usuario IN (2,3)

        ORDER BY u.id_usuario DESC";

                MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        conexao
                    );

                MySqlDataAdapter da =
                    new MySqlDataAdapter(cmd);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao listar funcionários. "
                    + ex.Message
                );
            }
            finally
            {
                Desconectar();
            }
        }


        #endregion

        #region Excluir Usuário
public void ExcluirUsuario(int idUsuario)
        {
            try
            {
                Conectar();

                // =====================================
                // EXCLUI SERVIÇOS DO MECÂNICO
                // =====================================

                string sqlServico = @"
        DELETE FROM servico
        WHERE id_mecanico IN
        (
            SELECT id_mecanico
            FROM mecanico
            WHERE id_usuario = @id
        )";

                MySqlCommand cmdServico =
                    new MySqlCommand(
                        sqlServico,
                        conexao
                    );

                cmdServico.Parameters.AddWithValue(
                    "@id",
                    idUsuario
                );

                cmdServico.ExecuteNonQuery();

                // =====================================
                // EXCLUI MECÂNICO
                // =====================================

                string sqlMecanico = @"
        DELETE FROM mecanico
        WHERE id_usuario = @id";

                MySqlCommand cmdMecanico =
                    new MySqlCommand(
                        sqlMecanico,
                        conexao
                    );

                cmdMecanico.Parameters.AddWithValue(
                    "@id",
                    idUsuario
                );

                cmdMecanico.ExecuteNonQuery();

                // =====================================
                // EXCLUI USUÁRIO
                // =====================================

                string sqlUsuario = @"
        DELETE FROM usuario
        WHERE id_usuario = @id";

                MySqlCommand cmdUsuario =
                    new MySqlCommand(
                        sqlUsuario,
                        conexao
                    );

                cmdUsuario.Parameters.AddWithValue(
                    "@id",
                    idUsuario
                );

                cmdUsuario.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao excluir usuário: "
                    + ex.Message
                );
            }
            finally
            {
                Desconectar();
            }
        }


        #endregion
    }
}