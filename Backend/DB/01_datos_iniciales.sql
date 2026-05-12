USE master_colegio
GO

SET NOCOUNT ON;
GO

/* ============================================================
   DATOS INICIALES - master_colegio
   Generado: 2026-05-10
   Nota: clave_cifrada generada con AES-256-CBC desde el backend
         Clave/IV configurados en appsettings.json > CifradoAes
   ============================================================ */

/* 1. ESTADO_CLIENTE */
IF NOT EXISTS (SELECT 1 FROM dbo.estado_cliente WHERE id = 1)
INSERT INTO dbo.estado_cliente (id, codigo, descripcion) VALUES
    (1, 'ACTIVO',     'Cliente activo y operativo'),
    (2, 'INACTIVO',   'Cliente desactivado temporalmente'),
    (3, 'SUSPENDIDO', 'Cliente suspendido por incumplimiento');
GO

/* 2. ESTADO_SUSCRIPCION */
IF NOT EXISTS (SELECT 1 FROM dbo.estado_suscripcion WHERE id = 1)
INSERT INTO dbo.estado_suscripcion (id, codigo, descripcion) VALUES
    (1, 'ACTIVA',    'Suscripcion vigente'),
    (2, 'VENCIDA',   'Suscripcion con fecha fin superada'),
    (3, 'CANCELADA', 'Suscripcion cancelada antes de su vencimiento');
GO

/* 3. PLAN */
IF NOT EXISTS (SELECT 1 FROM dbo.[plan] WHERE codigo = 'BASICO')
INSERT INTO dbo.[plan] (codigo, nombre, precio_mensual, precio_anual, max_estudiante, max_usuario, estado, usuario_creacion) VALUES
    ('BASICO',      'Plan Basico',      49.90,   499.90,  100,  5,    1, 'SISTEMA'),
    ('ESTANDAR',    'Plan Estandar',   99.90,   999.90,  500,  15,   1, 'SISTEMA'),
    ('PROFESIONAL', 'Plan Profesional', 199.90, 1999.90, NULL, NULL, 1, 'SISTEMA');
GO

/* 4. USUARIO_PLATAFORMA
      correo        : fhuaman
      clave original: @Flehuar21
      clave_cifrada : AES-256-CBC con clave/IV de appsettings.json */
IF NOT EXISTS (SELECT 1 FROM dbo.usuario_plataforma WHERE correo = 'fhuaman')
INSERT INTO dbo.usuario_plataforma
    (nombres, apellidos, correo, clave_cifrada, estado, usuario_creacion)
VALUES
    ('Franklin', 'Huaman', 'fhuaman', 'mGErFoUo22DlRlk0qEqG8g==', 1, 'SISTEMA');
GO
