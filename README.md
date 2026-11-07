# ULTRAKILL: Pizza Mayhem

A work-in-progress ULTRAKILL mod project with a Windows installer.

## Current status

**Project scaffold only.** The playable Peppino character, Pizza Tower movement, combo system, and a tested installer are not finished yet. This repository does not currently contain a ready-to-run `.exe` or a finished mod.

## Planned prototype

- ULTRAKILL remains the host game: levels, enemies, weapons and combat.
- Peppino-inspired player character and Pizza Tower-style movement.
- Mach acceleration, jumps, dashes and combo scoring.
- A Windows installer that detects the ULTRAKILL folder and installs the mod.

## Build requirements

- Windows 10/11
- .NET SDK
- Inno Setup 6
- A legitimate ULTRAKILL installation
- BepInEx 5 installed in the ULTRAKILL folder

## Build

The mod needs references from the local game installation, because ULTRAKILL's Unity/game assemblies are not redistributed in this repository.

1. Install BepInEx 5 into ULTRAKILL and launch the game once.
2. Open PowerShell in this repository.
3. Set the game path and build:

```powershell
$env:ULTRAKILL_DIR = "C:\Path\To\ULTRAKILL"
.\build.ps1
```

The script compiles the plugin against the local game assemblies and, when Inno Setup 6 is installed, creates `dist\PizzaMayhem-Setup.exe`.

## Safety

The installer places the plugin under `BepInEx\plugins\PizzaMayhem`. It does not overwrite game executables or original game assets. Back up your game folder before installing mods.

## License

MIT. See [LICENSE](LICENSE).
