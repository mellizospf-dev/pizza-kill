$ErrorActionPreference = "Stop"

if (-not $env:ULTRAKILL_DIR) {
    throw "Set ULTRAKILL_DIR to your ULTRAKILL installation folder first."
}

$game = (Resolve-Path $env:ULTRAKILL_DIR).Path
$required = @(
    "BepInEx\core\BepInEx.dll",
    "ULTRAKILL_Data\Managed\UnityEngine.dll",
    "ULTRAKILL_Data\Managed\UnityEngine.CoreModule.dll",
    "ULTRAKILL_Data\Managed\UnityEngine.InputLegacyModule.dll",
    "ULTRAKILL_Data\Managed\Assembly-CSharp.dll"
)

foreach ($file in $required) {
    if (-not (Test-Path (Join-Path $game $file))) {
        throw "Missing required file: $file. Install BepInEx and run ULTRAKILL once."
    }
}

dotnet build ".\src\PizzaMayhem\PizzaMayhem.csproj" -c Release "-p:UltrakillDir=$game"
if ($LASTEXITCODE -ne 0) { throw "Plugin build failed." }

New-Item -ItemType Directory -Force ".\dist\PizzaMayhem" | Out-Null
Copy-Item ".\src\PizzaMayhem\bin\Release\netstandard2.1\PizzaMayhem.dll" ".\dist\PizzaMayhem\PizzaMayhem.dll" -Force

$isccCandidates = @(
    "$env:ProgramFiles\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"
)
$iscc = $isccCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1

if (-not $iscc) {
    Write-Warning "Plugin built. Install Inno Setup 6 to generate the .exe installer."
    exit 0
}

& $iscc ".\installer\PizzaMayhem.iss"
if ($LASTEXITCODE -ne 0) { throw "Installer build failed." }
Write-Host "Created dist\PizzaMayhem-Setup.exe"
