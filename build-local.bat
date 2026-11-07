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
  echo Edit ULTRAKILL_DIR in this file if the folder has moved.
  pause
  exit /b 1
)

if not exist "%PIZZATOWER_DIR%\PizzaTower.exe" (
  echo WARNING: PizzaTower.exe was not found at the saved path.
  echo The current scaffold does not yet import Pizza Tower assets.
  echo.
)

where dotnet >nul 2>nul
if errorlevel 1 (
  echo ERROR: Install the .NET SDK before building.
  pause
  exit /b 1
)

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1"
set "BUILD_RESULT=%ERRORLEVEL%"

echo.
if not "%BUILD_RESULT%"=="0" (
  echo Build failed. Read the error above.
) else (
  echo Build script finished. If Inno Setup 6 is installed,
  echo the installer should be at dist\PizzaMayhem-Setup.exe.
)
pause
exit /b %BUILD_RESULT%
