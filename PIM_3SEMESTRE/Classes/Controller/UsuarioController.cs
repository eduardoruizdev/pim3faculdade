using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class UsuarioController : Banco
    {

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

                cmd.Parameters.AddWithValue("@nome",
                usuario.NomeUsuario);

                cmd.Parameters.AddWithValue("@email",
                usuario.EmailUsuario);

                cmd.Parameters.AddWithValue("@senha",
                usuario.SenhaUsuario);

                cmd.Parameters.AddWithValue("@tipo",
                usuario.IdTipoUsuario);

                cmd.ExecuteNonQuery();

                int idUsuario =
                Convert.ToInt32(cmd.LastInsertedId);

                return idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao cadastrar usuário. " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

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
                    new MySqlParameter("@email", email)
                );

                MySqlDataReader dados =
                ConsultarSQL(sql, parametros);

                return dados.HasRows;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "Erro ao verificar email. " + ex.Message);
            }
        }
    }
}
