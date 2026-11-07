# Melty handoff — ULTRAKILL: Pizza Mayhem

This repository is the source project for a proposed Melty mashup. It is **not yet a verified or publishable Melty release**.

## Target

- Host game: ULTRAKILL (the game that launches and provides the world, enemies and weapons).
- Companion game: Pizza Tower (movement inspiration and Peppino character concept).
- Desired prototype: play as Peppino in ULTRAKILL, with acceleration/Mach Run, jumps, dashes and combo scoring.
- Preserve the host game's original files and provide a reversible install/uninstall path.

## Important implementation requirements

1. Start from the current Melty instructions supplied inside the Melty app. Follow its current project layout, metadata, packaging and publish workflow exactly; do not invent a Melty manifest or claim compatibility based only on this document.
2. Inspect both legally installed games on the creator's PC. Never put proprietary game executables, DLLs, data files, extracted assets, or game content in this repository or the release.
3. Detect the user's installed game paths at build/install time. Do not hard-code this creator's personal Windows paths into a public release.
4. Build the mod against the locally installed ULTRAKILL/Unity assemblies and a supported mod loader. Keep the initial prototype small and offline-safe.
5. Do not claim that Peppino, Pizza Tower movement, combo scoring, multiplayer, or Melty support works until implemented and tested in the actual games.
6. Test installation, launch, gameplay, uninstall, and restoration on the creator's PC before publishing. Melty requires a successful creator-side run and audits uploaded files.
7. Keep the build reproducible. Publish source and build instructions; attach a Windows installer only after it has actually been built and tested.

## Current repository status

The current C# plugin is only a loader-check scaffold. It logs that the plugin loaded; it does not yet implement the mashup gameplay. The existing local build script is a development helper, not a Melty package or a ready-to-run installer.

## Creator workflow

1. Open Melty and use its **Send instructions to coding agent** flow.
2. Give those official, current Melty instructions to Codex together with this repository and this brief.
3. Have Codex adapt the project to Melty's actual required structure and implement the prototype.
4. Run it locally through Melty and fix all failures.
5. Publish through Melty only after a successful local test and the platform's audit.

Official Melty site: https://melty.gg/
