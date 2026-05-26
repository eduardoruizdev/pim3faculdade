DROP SCHEMA IF EXISTS auto_tech_pim;
CREATE SCHEMA auto_tech_pim;
USE auto_tech_pim;

CREATE TABLE tipo_usuario
(
    id_tipo_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nm_tipo_usuario VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE usuario
(
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nm_usuario VARCHAR(220) NOT NULL,
    nm_email_usuario VARCHAR(220) NOT NULL UNIQUE,
    nm_senha_usuario VARCHAR(220) NOT NULL,
    id_tipo_usuario INT NOT NULL,

    CONSTRAINT fk_tipo_usuario_usuario
        FOREIGN KEY (id_tipo_usuario)
        REFERENCES tipo_usuario(id_tipo_usuario)
);

CREATE TABLE cliente
(
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL UNIQUE,
    cd_cpf_cliente VARCHAR(14) UNIQUE NOT NULL,
    dt_nascimento_cliente DATE,
    cd_telefone_cliente VARCHAR(15),
    cd_cep_cliente VARCHAR(10),
    nm_rua_cliente VARCHAR(220),
    cd_numero_residencia_cliente INT,
    ds_complemento_residencia_cliente VARCHAR(50),
    nm_bairro_cliente VARCHAR(220),
    nm_cidade_cliente VARCHAR(220),
    cd_estado_residencia_cliente CHAR(2),
    ds_observacao_cliente TEXT,

    CONSTRAINT fk_usuario_cliente
        FOREIGN KEY (id_usuario)
        REFERENCES usuario(id_usuario)
);

CREATE TABLE mecanico
(
    id_mecanico INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL UNIQUE,
    ds_especialidade_mecanico VARCHAR(220),
    ds_observacao_mecanico TEXT,

    CONSTRAINT fk_usuario_mecanico
        FOREIGN KEY (id_usuario)
        REFERENCES usuario(id_usuario)
);

CREATE TABLE tipo_servico
(
    id_tipo_servico INT AUTO_INCREMENT PRIMARY KEY,
    nm_tipo_servico VARCHAR(220) NOT NULL
);

CREATE TABLE servico
(
    id_servico INT AUTO_INCREMENT PRIMARY KEY,
    id_tipo_servico INT NOT NULL,
    nm_titulo_servico VARCHAR(220) NOT NULL,
    ds_servico_resumido VARCHAR(220),
    dt_cadastro_servico DATE,
    dt_prevista_entrega_servico DATE,
    ds_prioridade_servico VARCHAR(100),
    cd_placa_veiculo_servico VARCHAR(10),
    nm_modelo_veiculo_servico VARCHAR(220),
    cd_ano_veiculo_servico YEAR,
    nm_cor_veiculo_servico VARCHAR(100),
    qt_quilometragem_veiculo_servico DECIMAL(10,2),
    id_cliente INT NOT NULL,
    vl_servico DECIMAL(10,2),
    ds_servico TEXT,
    id_mecanico INT NOT NULL,
    st_servico VARCHAR(100) DEFAULT 'Em andamento',

    CONSTRAINT fk_tipo_servico_servico
        FOREIGN KEY (id_tipo_servico)
        REFERENCES tipo_servico(id_tipo_servico),

    CONSTRAINT fk_cliente_servico
        FOREIGN KEY (id_cliente)
        REFERENCES cliente(id_cliente),

    CONSTRAINT fk_mecanico_servico
        FOREIGN KEY (id_mecanico)
        REFERENCES mecanico(id_mecanico)
);



/*--------------MASSA DE TESTE---------------*/
INSERT INTO tipo_usuario
(
    nm_tipo_usuario
)
VALUES
('Administrador'),
('Funcionario'),
('Mecanico'),
('Cliente');

INSERT INTO usuario
(
    nm_usuario,
    nm_email_usuario,
    nm_senha_usuario,
    id_tipo_usuario
)
VALUES
(
    'Administrador Master',
    'admin@autotech.com',
    '123456',
    1
),
(
    'Carlos Funcionario',
    'funcionario@autotech.com',
    '123456',
    2
),
(
    'Joao Mecanico',
    'mecanico@autotech.com',
    '123456',
    3
),
(
    'Eduardo Cliente',
    'cliente@autotech.com',
    '123456',
    4
);

INSERT INTO mecanico
(
    id_usuario,
    ds_especialidade_mecanico,
    ds_observacao_mecanico
)
VALUES
(
    3,
    'Motor e Suspensão',
    'Especialista em veículos nacionais'
);

INSERT INTO cliente
(
    id_usuario,
    cd_cpf_cliente,
    dt_nascimento_cliente,
    cd_telefone_cliente,
    cd_cep_cliente,
    nm_rua_cliente,
    cd_numero_residencia_cliente,
    ds_complemento_residencia_cliente,
    nm_bairro_cliente,
    nm_cidade_cliente,
    cd_estado_residencia_cliente,
    ds_observacao_cliente
)
VALUES
(
    4,
    '111.111.111-11',
    '2004-08-15',
    '(13)99999-9999',
    '11380-000',
    'Rua das Oficinas',
    120,
    'Casa',
    'Centro',
    'São Vicente',
    'SP',
    'Cliente VIP'
);
INSERT INTO tipo_servico (nm_tipo_servico)
VALUES
('Troca de óleo'),
('Teste'),
('Freios'),
('Suspensão');
DELIMITER $$

CREATE PROCEDURE sp_cadastrar_servico
(
    IN p_id_tipo_servico INT,
    IN p_nm_titulo_servico VARCHAR(220),
    IN p_ds_servico_resumido VARCHAR(220),
    IN p_dt_cadastro_servico DATE,
    IN p_dt_prevista_entrega_servico DATE,
    IN p_ds_prioridade_servico VARCHAR(100),
    IN p_cd_placa_veiculo_servico VARCHAR(10),
    IN p_nm_modelo_veiculo_servico VARCHAR(220),
    IN p_cd_ano_veiculo_servico YEAR,
    IN p_nm_cor_veiculo_servico VARCHAR(100),
    IN p_qt_quilometragem_veiculo_servico DECIMAL(10,2),
    IN p_id_cliente INT,
    IN p_vl_servico DECIMAL(10,2),
    IN p_ds_servico TEXT,
    IN p_id_mecanico INT
)
BEGIN

    INSERT INTO servico
    (
        id_tipo_servico,
        nm_titulo_servico,
        ds_servico_resumido,
        dt_cadastro_servico,
        dt_prevista_entrega_servico,
        ds_prioridade_servico,
        cd_placa_veiculo_servico,
        nm_modelo_veiculo_servico,
        cd_ano_veiculo_servico,
        nm_cor_veiculo_servico,
        qt_quilometragem_veiculo_servico,
        id_cliente,
        vl_servico,
        ds_servico,
        id_mecanico
    )
    VALUES
    (
        p_id_tipo_servico,
        p_nm_titulo_servico,
        p_ds_servico_resumido,
        p_dt_cadastro_servico,
        p_dt_prevista_entrega_servico,
        p_ds_prioridade_servico,
        p_cd_placa_veiculo_servico,
        p_nm_modelo_veiculo_servico,
        p_cd_ano_veiculo_servico,
        p_nm_cor_veiculo_servico,
        p_qt_quilometragem_veiculo_servico,
        p_id_cliente,
        p_vl_servico,
        p_ds_servico,
        p_id_mecanico
    );

END$$

DELIMITER ;



DELIMITER $$

CREATE PROCEDURE sp_listar_clientes()
BEGIN

    SELECT
        c.id_cliente,
        u.nm_usuario
    FROM cliente c
    INNER JOIN usuario u
        ON c.id_usuario = u.id_usuario
    ORDER BY u.nm_usuario;

END$$

DELIMITER ;

USE auto_tech_pim;

/*-----PROCEDURE DE VALIDAR LOGIN*/
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_validar_login $$

CREATE PROCEDURE sp_validar_login
(
    IN p_email VARCHAR(220),
    IN p_senha VARCHAR(220)
)
BEGIN

    SELECT
        u.id_usuario,
        u.nm_usuario,
        u.nm_email_usuario,
        tu.nm_tipo_usuario
    FROM usuario u

    INNER JOIN tipo_usuario tu
        ON u.id_tipo_usuario = tu.id_tipo_usuario

    WHERE u.nm_email_usuario = p_email
    AND u.nm_senha_usuario = p_senha;

END $$

DELIMITER ;


/*-----CADASTRAR CLIENTE------*/
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_cadastrar_cliente $$

CREATE PROCEDURE sp_cadastrar_cliente
(
    p_nm_usuario VARCHAR(220),
    p_nm_email_usuario VARCHAR(220),
    p_nm_senha_usuario VARCHAR(220),

    p_cd_cpf_cliente VARCHAR(14),
    p_dt_nascimento_cliente DATE,
    p_cd_telefone_cliente VARCHAR(15),
    p_cd_cep_cliente VARCHAR(10),
    p_nm_rua_cliente VARCHAR(220),
    p_cd_numero_residencia_cliente INT,
    p_ds_complemento_residencia_cliente VARCHAR(50),
    p_nm_bairro_cliente VARCHAR(220),
    p_nm_cidade_cliente VARCHAR(220),
    p_cd_estado_residencia_cliente CHAR(2),
    p_ds_observacao_cliente TEXT
)
BEGIN

    DECLARE v_id_usuario INT;

    INSERT INTO usuario
    (
        nm_usuario,
        nm_email_usuario,
        nm_senha_usuario,
        id_tipo_usuario
    )
    VALUES
    (
        p_nm_usuario,
        p_nm_email_usuario,
        p_nm_senha_usuario,
        4
    );

    SET v_id_usuario = LAST_INSERT_ID();

    INSERT INTO cliente
    (
        id_usuario,
        cd_cpf_cliente,
        dt_nascimento_cliente,
        cd_telefone_cliente,
        cd_cep_cliente,
        nm_rua_cliente,
        cd_numero_residencia_cliente,
        ds_complemento_residencia_cliente,
        nm_bairro_cliente,
        nm_cidade_cliente,
        cd_estado_residencia_cliente,
        ds_observacao_cliente
    )
    VALUES
    (
        v_id_usuario,
        p_cd_cpf_cliente,
        p_dt_nascimento_cliente,
        p_cd_telefone_cliente,
        p_cd_cep_cliente,
        p_nm_rua_cliente,
        p_cd_numero_residencia_cliente,
        p_ds_complemento_residencia_cliente,
        p_nm_bairro_cliente,
        p_nm_cidade_cliente,
        p_cd_estado_residencia_cliente,
        p_ds_observacao_cliente
    );

END $$

DELIMITER ;



DELIMITER $$

CREATE PROCEDURE sp_listar_tipos_servico()
BEGIN

    SELECT
        id_tipo_servico,
        nm_tipo_servico
    FROM tipo_servico
    ORDER BY nm_tipo_servico;

END$$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE sp_buscar_cliente_por_id
(
    IN p_id_cliente INT
)
BEGIN

    SELECT
        c.id_cliente,
        u.nm_usuario,
        u.nm_email_usuario,
        c.cd_cpf_cliente,
        c.cd_telefone_cliente
    FROM cliente c
    INNER JOIN usuario u
        ON c.id_usuario = u.id_usuario
    WHERE c.id_cliente = p_id_cliente;

END$$

DELIMITER ;