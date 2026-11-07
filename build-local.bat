@echo off
setlocal

rem Local paths supplied for ULTRAKILL: Pizza Mayhem.
set "ULTRAKILL_DIR=C:\Users\Pzmon\Desktop\ULTRAKILL"
set "PIZZATOWER_DIR=C:\Program Files (x86)\Steam\steamapps\common\Pizza Tower"

echo ==========================================
echo ULTRAKILL: Pizza Mayhem - local build
echo ==========================================
echo ULTRAKILL: "%ULTRAKILL_DIR%"
echo Pizza Tower: "%PIZZATOWER_DIR%"
echo.

if not exist "%ULTRAKILL_DIR%\ULTRAKILL.exe" (
  echo ERROR: ULTRAKILL.exe was not found at the saved path.
  pause
  exit /b 1
)

if not exist "%PIZZATOWER_DIR%\PizzaTower.exe" (
  echo WARNING: PizzaTower.exe was not found at the saved path.
  echo The current prototype does not yet read Pizza Tower game files.
  echo.
)

if not exist "%ULTRAKILL_DIR%\BepInEx\core\BepInEx.dll" (
  echo ERROR: BepInEx 5 is missing from the ULTRAKILL folder.
  echo Install BepInEx 5 and launch ULTRAKILL once, then retry.
  pause
  exit /b 1
)

where dotnet >nul 2>nul
if errorlevel 1 (
  echo ERROR: Install the .NET SDK before building.
  pause
  exit /b 1
)

set "ULTRAKILL_DIR=%ULTRAKILL_DIR%"
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1"
set "BUILD_RESULT=%ERRORLEVEL%"

echo.
if not "%BUILD_RESULT%"=="0" (
  echo Build failed. Read the error above.
) else (
  echo Plugin build finished.
  if exist "%~dp0dist\PizzaMayhem-Setup.exe" (
    echo Installer created: dist\PizzaMayhem-Setup.exe
  ) else (
    echo The plugin DLL was built, but Inno Setup 6 is needed for the EXE installer.
  )
)
pause
exit /b %BUILD_RESULT%
