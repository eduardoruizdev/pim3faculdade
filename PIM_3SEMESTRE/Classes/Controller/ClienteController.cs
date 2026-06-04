using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Models;
using System;
using System.Collections.Generic;

namespace PIM_3SEMESTRE.Controllers
{
    public class ClienteController
    {

        #region cadastrar cliente
        public void CadastrarCliente(ClienteModel cliente) //METODO DE CADASTRAR CLIENTE
        {
            try
            {
                Banco bd = new Banco(); //CONEXÃO COM BANCO DE DADOS

                List<MySqlParameter> parametros = new List<MySqlParameter>(); //LISTANDO PARAMETROS

                parametros.Add(new MySqlParameter("p_nm_usuario", cliente.NomeUsuario)); //ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_nm_email_usuario", cliente.EmailUsuario)); //ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_nm_senha_usuario", cliente.SenhaUsuario)); //ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_cd_cpf_cliente", cliente.CpfCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_dt_nascimento_cliente", cliente.DataNascimentoCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_cd_telefone_cliente", cliente.TelefoneCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_cd_cep_cliente", cliente.CepCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_nm_rua_cliente", cliente.RuaCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_cd_numero_residencia_cliente", cliente.NumeroResidenciaCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_ds_complemento_residencia_cliente", cliente.ComplementoResidenciaCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_nm_bairro_cliente", cliente.BairroCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_nm_cidade_cliente", cliente.CidadeCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_cd_estado_residencia_cliente", cliente.EstadoResidenciaCliente));//ADICIONANDO PARAMETRO

                parametros.Add(new MySqlParameter("p_ds_observacao_cliente", cliente.ObservacaoCliente));//ADICIONANDO PARAMETRO

                bd.Executar("sp_cadastrar_cliente", parametros);//DECLARANDO QUE O COMANDO É UMA STORE PROCEDURE
            }
            catch (Exception ex) //MENSAGEM DE ERRO
            {
                throw new Exception("Erro ao cadastrar cliente. " + ex.Message);
            }
        }
        #endregion


        #region listar cliente
        public MySqlDataReader ListarClientes() //metodo de listar clientes
        {
            try
            {
                Banco bd = new Banco(); //conexao com banco de dados

                return bd.Consultar("sp_listar_clientes"); //executando comando da store procedure
            }
            catch (Exception ex) //mensagem de erro
            {
                throw new Exception("Erro ao listar clientes. " + ex.Message);
            }
        }
        #endregion


        #region buscar cliente por id
        public MySqlDataReader BuscarClientePorId(int idCliente) //Criação do metodo
        {
            try
            {
                Banco bd = new Banco(); //conexão com o banco

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>(); //listando os parametros

                parametros.Add(
                    new MySqlParameter(
                        "p_id_cliente",
                        idCliente
                    )//adicionando parametro do id
                );

                return bd.Consultar(
                    "sp_buscar_cliente_por_id",
                    parametros
                );//declarando que o comando deve executar a consulta da store procedure
            }
            catch (Exception ex)  //mensagem de erro
            {
                throw new Exception(
                    "Erro ao buscar cliente. " + ex.Message
                );
            }
        }
        #endregion
    }
}
