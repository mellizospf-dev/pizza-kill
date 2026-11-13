# ULTRAKILL: Pizza Mayhem

A work-in-progress ULTRAKILL mod project with a Windows installer.

## Current status

**Early BepInEx movement prototype (not yet tested in-game).** The repository contains a plugin that attempts to bind to ULTRAKILL's `NewMovement` controller through reflection. It adds configurable Mach-style acceleration, a small jump/dash combo score, and an on-screen status display.

This is not yet a finished mashup:
- Peppino's 3D model and player-character replacement are not implemented.
- Pizza Tower animations, taunts, Body Slam, and full movement physics are not implemented.
- The plugin has not been verified against the user's exact ULTRAKILL build.
- No ready-to-run installer or compiled game-ready release is included.

## Planned features

- Keep ULTRAKILL's levels, enemies, weapons, and combat.
- Add an original Peppino-inspired player model and animations.
- Expand Pizza Tower-style movement: Mach Run, charged jump, dash, slide, and Body Slam.
- Add combo scoring and an installer that detects the ULTRAKILL folder.

## Build requirements

- Windows 10/11
- .NET SDK compatible with the project
- A legitimate ULTRAKILL installation
- BepInEx 5 installed in the ULTRAKILL folder
- These local game files:
  - `BepInEx\core\BepInEx.dll`
  - `ULTRAKILL_Data\Managed\UnityEngine.dll`
  - `ULTRAKILL_Data\Managed\UnityEngine.CoreModule.dll`
  - `ULTRAKILL_Data\Managed\UnityEngine.InputLegacyModule.dll`
  - `ULTRAKILL_Data\Managed\Assembly-CSharp.dll`

Game assemblies are referenced from the local installation and are not redistributed in this repository.

## Build

1. Install BepInEx 5 into ULTRAKILL and launch the game once.
2. Open PowerShell in this repository.
3. Set the path to the ULTRAKILL installation and build:

```powershell
$env:ULTRAKILL_DIR = "C:\Path\To\ULTRAKILL"
.\build.ps1
```

The script compiles the plugin against local game assemblies. If Inno Setup 6 is installed, it also creates `dist\PizzaMayhem-Setup.exe`.

## Safety

The installer places the plugin under `BepInEx\plugins\PizzaMayhem`. It is designed not to overwrite game executables or original game assets. Back up the game folder before installing mods.

## License

MIT. See [LICENSE](LICENSE).
