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
    id_mecanico,
    st_servico
)
VALUES

(
    1,
    'Troca de Óleo Completa',
    'Troca de óleo e filtro',
    '2026-06-01',
    '2026-06-01',
    'Alta',
    'ABC1D23',
    'Volkswagen Gol',
    '2020',
    'Branco',
    45230.00,
    1,
    180.00,
    'Realizada troca de óleo sintético e substituição do filtro de óleo.',
    1,
    'Concluído'
),

(
    3,
    'Troca de Pastilhas de Freio',
    'Pastilhas dianteiras desgastadas',
    '2026-06-02',
    '2026-06-03',
    'Alta',
    'DEF4G56',
    'Chevrolet Onix',
    '2021',
    'Prata',
    38500.00,
    1,
    350.00,
    'Substituição das pastilhas dianteiras e inspeção dos discos.',
    1,
    'Concluído'
),

(
    4,
    'Reparo na Suspensão',
    'Barulho na suspensão dianteira',
    '2026-06-04',
    '2026-06-06',
    'Média',
    'GHI7J89',
    'Fiat Argo',
    '2022',
    'Vermelho',
    28750.00,
    1,
    890.00,
    'Troca de bieletas e buchas da suspensão dianteira.',
    1,
    'Em andamento'
),

(
    2,
    'Diagnóstico Geral',
    'Veículo apresentando falha ao ligar',
    '2026-06-05',
    '2026-06-05',
    'Alta',
    'JKL1M23',
    'Ford Ka',
    '2018',
    'Preto',
    75200.00,
    1,
    120.00,
    'Realização de testes eletrônicos e mecânicos para identificação da falha.',
    1,
    'Concluído'
),

(
    1,
    'Troca de Óleo Preventiva',
    'Manutenção preventiva',
    '2026-06-06',
    '2026-06-06',
    'Baixa',
    'MNO4P56',
    'Hyundai HB20',
    '2023',
    'Cinza',
    12000.00,
    1,
    170.00,
    'Troca de óleo e conferência dos níveis dos fluidos.',
    1,
    'Concluído'
),

(
    3,
    'Sistema de Freios',
    'Pedal de freio baixo',
    '2026-06-07',
    '2026-06-09',
    'Alta',
    'PQR7S89',
    'Honda Civic',
    '2019',
    'Preto',
    62000.00,
    1,
    750.00,
    'Troca de fluido e revisão completa do sistema de freios.',
    1,
    'Em andamento'
),

(
    4,
    'Alinhamento da Suspensão',
    'Desgaste irregular dos pneus',
    '2026-06-08',
    '2026-06-08',
    'Média',
    'STU1V23',
    'Toyota Corolla',
    '2020',
    'Prata',
    41000.00,
    1,
    250.00,
    'Correção do alinhamento e inspeção da suspensão.',
    1,
    'Concluído'
),

(
    2,
    'Teste de Injeção Eletrônica',
    'Luz da injeção acesa',
    '2026-06-09',
    '2026-06-10',
    'Alta',
    'VWX4Y56',
    'Renault Kwid',
    '2021',
    'Branco',
    32000.00,
    1,
    180.00,
    'Diagnóstico através de scanner automotivo.',
    1,
    'Em andamento'
),

(
    1,
    'Troca de Óleo Premium',
    'Óleo sintético premium',
    '2026-06-10',
    '2026-06-10',
    'Baixa',
    'YZA7B89',
    'Jeep Renegade',
    '2022',
    'Azul',
    25000.00,
    1,
    240.00,
    'Troca de óleo premium e filtro.',
    1,
    'Concluído'
),

(
    3,
    'Revisão dos Freios',
    'Revisão preventiva',
    '2026-06-11',
    '2026-06-12',
    'Média',
    'BCD2E34',
    'Nissan Versa',
    '2021',
    'Branco',
    55000.00,
    1,
    420.00,
    'Verificação das pastilhas, discos e fluido de freio.',
    1,
    'Em andamento'
);