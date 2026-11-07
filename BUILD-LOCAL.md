# Build on the supplied PC

Run `build-local.bat` from the repository folder. It uses the two paths supplied for this project:

- ULTRAKILL: `C:\Users\Pzmon\Desktop\ULTRAKILL`
- Pizza Tower: `C:\Program Files (x86)\Steam\steamapps\common\Pizza Tower`

The script checks the ULTRAKILL executable and .NET SDK, then runs `build.ps1`. The current plugin is still only a loader-check scaffold; it does not yet implement Peppino, Pizza Tower movement, or combo scoring. The Pizza Tower path is recorded and checked for convenience, but its assets are not yet used by the plugin.

Requirements:
- BepInEx 5 installed in the ULTRAKILL folder, with the game assemblies under `ULTRAKILL_Data\Managed`.
- .NET SDK.
- Inno Setup 6 to produce `dist\PizzaMayhem-Setup.exe`.

This repository does not include game files or a prebuilt executable. The installer is generated locally after a successful build; no fake or empty EXE is supplied.
