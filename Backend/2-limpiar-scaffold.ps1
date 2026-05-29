$infraPath     = "D:\IC\2026-1\ColegioMaster\Backend\03 infrastructure\ColegioMaster.Infrastructure"
$dbContextPath = "$infraPath\ColegioDbContext.cs"
$configDir     = "$infraPath\Configurations"
$namespace     = "ColegioMaster.Infrastructure"

# ─── Función: extrae bloques Entity<T> con conteo de llaves ─────────────────
function Get-EntityBlocks($content) {
    $results = @()
    $pattern = 'modelBuilder\.Entity<(\w+)>\(entity\s*=>\s*\{'
    $found   = [regex]::Matches($content, $pattern)

    foreach ($m in $found) {
        $entityName = $m.Groups[1].Value
        $startPos   = $m.Index + $m.Length - 1

        $depth = 1
        $pos   = $startPos + 1
        while ($pos -lt $content.Length -and $depth -gt 0) {
            if     ($content[$pos] -eq '{') { $depth++ }
            elseif ($content[$pos] -eq '}') { $depth-- }
            $pos++
        }

        $innerContent = $content.Substring($startPos + 1, $pos - $startPos - 2)
        $results += [PSCustomObject]@{ EntityName = $entityName; Config = $innerContent }
    }
    return $results
}

# ─── Función: genera el archivo de Configuration Class ──────────────────────
function New-ConfigurationClass($entityName, $config) {
    $lines = @(
        "using $namespace.Models;"
        "using Microsoft.EntityFrameworkCore;"
        "using Microsoft.EntityFrameworkCore.Metadata.Builders;"
        ""
        "namespace $namespace.Configurations;"
        ""
        "public class ${entityName}Configuration : IEntityTypeConfiguration<$entityName>"
        "{"
        "    public void Configure(EntityTypeBuilder<$entityName> entity)"
        "    {" + $config
        "    }"
        "}"
        ""
    )
    return $lines -join [System.Environment]::NewLine
}

# ─── PASO 1: Generar Configuration Classes ──────────────────────────────────
Write-Host "[1/2] Generando Configuration Classes..." -ForegroundColor Cyan

$dbContextContent = Get-Content $dbContextPath -Raw
$entityBlocks     = Get-EntityBlocks $dbContextContent

if ($entityBlocks.Count -eq 0) {
    Write-Host "  AVISO: No se encontraron entidades en el DbContext" -ForegroundColor DarkYellow
} else {
    if (-not (Test-Path $configDir)) {
        New-Item -ItemType Directory -Path $configDir | Out-Null
    }
    foreach ($block in $entityBlocks) {
        $configFile    = "$configDir\$($block.EntityName)Configuration.cs"
        $configContent = New-ConfigurationClass $block.EntityName $block.Config
        Set-Content $configFile $configContent -Encoding UTF8
        Write-Host "  OK $($block.EntityName)Configuration.cs" -ForegroundColor Green
    }
}

# ─── PASO 2: Limpiar ColegioDbContext ───────────────────────────────────────
Write-Host "[2/2] Limpiando ColegioDbContext..." -ForegroundColor Cyan

$dbContextContent = Get-Content $dbContextPath -Raw

# Remover OnConfiguring con la connection string hardcodeada
$dbContextContent = $dbContextContent -replace `
    '(?s)\s*protected override void OnConfiguring\(DbContextOptionsBuilder optionsBuilder\)\s*#warning[^\n]*\n[^\n]*\n', `
    "`r`n"

# Reemplazar OnModelCreating completo por la versión limpia
$cleanOnModelCreating = (
    "    protected override void OnModelCreating(ModelBuilder modelBuilder)" + "`r`n" +
    "    {" + "`r`n" +
    "        // Aplica automaticamente todas las IEntityTypeConfiguration<T> del ensamblado" + "`r`n" +
    "        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColegioDbContext).Assembly);" + "`r`n" +
    "`r`n" +
    "        OnModelCreatingPartial(modelBuilder);" + "`r`n" +
    "    }"
)

$dbContextContent = $dbContextContent -replace `
    '(?s)    protected override void OnModelCreating\(ModelBuilder modelBuilder\)\s*\{.*?OnModelCreatingPartial\(modelBuilder\);\s*\}', `
    $cleanOnModelCreating

Set-Content $dbContextPath $dbContextContent -Encoding UTF8
Write-Host "OK ColegioDbContext limpiado" -ForegroundColor Green
