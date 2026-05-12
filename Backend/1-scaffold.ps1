$connectionString = 'Data Source=localhost;Initial Catalog=master_colegio;Persist Security Info=True;User ID=sa;Password=P@ssw0rd123!;Trust Server Certificate=True'
$apiPath   = "d:\GHB\Curso\master\Backend\ColegioMaster\01 api\ColegioMaster.Api"
$infraPath = "d:\GHB\Curso\master\Backend\ColegioMaster\03 infrastructure\ColegioMaster.Infrastructure"

Write-Host "[1/1] Generando modelos desde la base de datos..." -ForegroundColor Cyan
Set-Location $apiPath

dotnet ef dbcontext scaffold $connectionString Microsoft.EntityFrameworkCore.SqlServer `
    -o "$infraPath\Models" `
    --context-dir "$infraPath" `
    --context ColegioDbContext `
    --project "$infraPath\ColegioMaster.Infrastructure.csproj" `
    --force

if ($LASTEXITCODE -eq 0) {
    Write-Host "OK Scaffold completado" -ForegroundColor Green
} else {
    Write-Host "ERROR en el scaffold" -ForegroundColor Red
    exit 1
}
