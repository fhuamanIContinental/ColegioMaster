USE master
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'master_colegio')
    CREATE DATABASE master_colegio
GO
USE master_colegio
GO

SET NOCOUNT ON;
GO

/* 1. LIMPIEZA DE OBJETOS */
IF OBJECT_ID('dbo.bitacora_master', 'U') IS NOT NULL DROP TABLE dbo.bitacora_master;
IF OBJECT_ID('dbo.comprobante_pago_master', 'U') IS NOT NULL DROP TABLE dbo.comprobante_pago_master;
IF OBJECT_ID('dbo.plan_modulo', 'U') IS NOT NULL DROP TABLE dbo.plan_modulo;
IF OBJECT_ID('dbo.modulo', 'U') IS NOT NULL DROP TABLE dbo.modulo;
IF OBJECT_ID('dbo.cliente_suscripcion', 'U') IS NOT NULL DROP TABLE dbo.cliente_suscripcion;
IF OBJECT_ID('dbo.[plan]', 'U') IS NOT NULL DROP TABLE dbo.[plan];
IF OBJECT_ID('dbo.usuario_plataforma', 'U') IS NOT NULL DROP TABLE dbo.usuario_plataforma;
IF OBJECT_ID('dbo.cliente', 'U') IS NOT NULL DROP TABLE dbo.cliente;
IF OBJECT_ID('dbo.estado_suscripcion', 'U') IS NOT NULL DROP TABLE dbo.estado_suscripcion;
IF OBJECT_ID('dbo.estado_cliente', 'U') IS NOT NULL DROP TABLE dbo.estado_cliente;
GO

/* 2. TABLA CATALOGO: ESTADO_CLIENTE */
CREATE TABLE dbo.estado_cliente (
    id   INT          NOT NULL,
    codigo NVARCHAR(30) NOT NULL,
    descripcion NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_EstadoCliente PRIMARY KEY (id),
    CONSTRAINT UQ_EstadoCliente_Codigo UNIQUE (codigo)
);
GO

/* 3. TABLA CATALOGO: ESTADO_SUSCRIPCION */
CREATE TABLE dbo.estado_suscripcion (
    id   INT          NOT NULL,
    codigo NVARCHAR(30) NOT NULL,
    descripcion NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_EstadoSuscripcion PRIMARY KEY (id),
    CONSTRAINT UQ_EstadoSuscripcion_Codigo UNIQUE (codigo)
);
GO

/* 4. TABLA CLIENTE */
CREATE TABLE dbo.cliente (
    id INT IDENTITY(1,1) NOT NULL,
    ruc CHAR(11) NOT NULL,
    codigo NVARCHAR(30) NOT NULL,
    razon_social NVARCHAR(200) NOT NULL,
    nombre_comercial NVARCHAR(200) NOT NULL,
    direccion NVARCHAR(250) NULL,
    telefono NVARCHAR(30) NULL,
    correo_contacto NVARCHAR(120) NULL,
    servidor_sql NVARCHAR(120) NOT NULL,
    bd_nombre NVARCHAR(120) NOT NULL,
    bd_usuario NVARCHAR(120) NULL,
    bd_password_cifrada NVARCHAR(400) NULL,
    id_estado INT NOT NULL CONSTRAINT DF_Cliente_Estado DEFAULT (1),
    fecha_activacion DATE NULL,
    fecha_creacion DATETIME2(0) NOT NULL CONSTRAINT DF_Cliente_FechaCreacion DEFAULT (SYSUTCDATETIME()),
    fecha_modificacion DATETIME2(0) NULL,
    usuario_creacion NVARCHAR(100) NOT NULL,
    usuario_modificacion NVARCHAR(100) NULL,
    CONSTRAINT PK_Cliente PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_Cliente_Ruc UNIQUE (ruc),
    CONSTRAINT UQ_Cliente_Codigo UNIQUE (codigo),
    CONSTRAINT UQ_Cliente_BdNombre UNIQUE (bd_nombre),
    CONSTRAINT FK_Cliente_EstadoCliente FOREIGN KEY (id_estado) REFERENCES dbo.estado_cliente(id)
);
GO

/* 6. TABLA USUARIO_PLATAFORMA */
CREATE TABLE dbo.usuario_plataforma (
    id                    BIGINT        IDENTITY(1,1) NOT NULL,
    nombres               NVARCHAR(120) NOT NULL,
    apellidos             NVARCHAR(120) NOT NULL,
    correo                NVARCHAR(150) NOT NULL,
    clave_cifrada         NVARCHAR(500) NOT NULL,  -- AES cifrado desde el backend
    intentos_fallidos     INT           NOT NULL CONSTRAINT DF_UsuarioPlataforma_IntentosFallidos DEFAULT (0),
    bloqueado_hasta       DATETIME2(0)  NULL,       -- NULL = no bloqueado
    ultimo_acceso         DATETIME2(0)  NULL,
    estado                BIT           NOT NULL CONSTRAINT DF_UsuarioPlataforma_Estado DEFAULT (1),
    fecha_creacion        DATETIME2(0)  NOT NULL CONSTRAINT DF_UsuarioPlataforma_FechaCreacion DEFAULT (SYSUTCDATETIME()),
    fecha_modificacion    DATETIME2(0)  NULL,
    usuario_creacion      NVARCHAR(100) NOT NULL,
    usuario_modificacion  NVARCHAR(100) NULL,
    CONSTRAINT PK_UsuarioPlataforma PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_UsuarioPlataforma_Correo UNIQUE (correo)
);
GO

/* 7. TABLA PLAN */
CREATE TABLE dbo.[plan] (
    id INT IDENTITY(1,1) NOT NULL,
    codigo NVARCHAR(30) NOT NULL,
    nombre NVARCHAR(120) NOT NULL,
    precio_mensual DECIMAL(12,2) NOT NULL,
    precio_anual DECIMAL(12,2) NOT NULL,
    max_estudiante INT NULL,
    max_usuario INT NULL,
    estado BIT NOT NULL CONSTRAINT DF_Plan_Estado DEFAULT (1),
    fecha_creacion DATETIME2(0) NOT NULL CONSTRAINT DF_Plan_FechaCreacion DEFAULT (SYSUTCDATETIME()),
    fecha_modificacion DATETIME2(0) NULL,
    usuario_creacion NVARCHAR(100) NOT NULL,
    usuario_modificacion NVARCHAR(100) NULL,
    CONSTRAINT PK_Plan PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_Plan_Codigo UNIQUE (codigo),
    CONSTRAINT CK_Plan_Precios CHECK (precio_mensual >= 0 AND precio_anual >= 0)
);
GO

/* 8. TABLA CLIENTESUSCRIPCION */
CREATE TABLE dbo.cliente_suscripcion (
    id BIGINT IDENTITY(1,1) NOT NULL,
    id_cliente INT NOT NULL,
    id_plan INT NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NULL,
    modalidad NVARCHAR(20) NOT NULL,
    monto_pactado DECIMAL(12,2) NOT NULL,
    id_estado INT NOT NULL CONSTRAINT DF_ClienteSuscripcion_Estado DEFAULT (1),
    fecha_creacion DATETIME2(0) NOT NULL CONSTRAINT DF_ClienteSuscripcion_FechaCreacion DEFAULT (SYSUTCDATETIME()),
    fecha_modificacion DATETIME2(0) NULL,
    usuario_creacion NVARCHAR(100) NOT NULL,
    usuario_modificacion NVARCHAR(100) NULL,
    CONSTRAINT PK_ClienteSuscripcion PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_ClienteSuscripcion_Cliente FOREIGN KEY (id_cliente) REFERENCES dbo.cliente(id),
    CONSTRAINT FK_ClienteSuscripcion_Plan FOREIGN KEY (id_plan) REFERENCES dbo.[plan](id),
    CONSTRAINT FK_ClienteSuscripcion_EstadoSuscripcion FOREIGN KEY (id_estado) REFERENCES dbo.estado_suscripcion(id),
    CONSTRAINT CK_ClienteSuscripcion_Modalidad CHECK (modalidad IN ('MENSUAL', 'ANUAL')),
    CONSTRAINT CK_ClienteSuscripcion_Monto CHECK (monto_pactado >= 0)
);
GO