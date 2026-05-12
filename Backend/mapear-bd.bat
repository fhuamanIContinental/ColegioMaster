@echo off
echo === MAPEO DE BASE DE DATOS ===
echo.

echo [1/2] Ejecutando scaffold...
powershell -ExecutionPolicy Bypass -File "%~dp01-scaffold.ps1"
if %ERRORLEVEL% neq 0 (
    echo ERROR en el scaffold. Proceso detenido.
    pause
    exit /b 1
)

echo.
echo [2/2] Limpiando scaffold y generando Configurations...
powershell -ExecutionPolicy Bypass -File "%~dp02-limpiar-scaffold.ps1"
if %ERRORLEVEL% neq 0 (
    echo ERROR en la limpieza.
    pause
    exit /b 1
)

echo.
echo === PROCESO COMPLETADO ===
pause
