# Movement integration prototype (0.2.0)

This branch adds the first code-level integration layer for the requested mashup.

## What is implemented in source

- Locates ULTRAKILL's `NewMovement` singleton at runtime using reflection.
- Reads and temporarily scales its `walkSpeed` field/property while Left Ctrl is held, with an acceleration ramp.
- Temporarily scales `jumpPower` slightly while Mach Run is charged, if that member exists.
- Adds a simple score/multiplier prototype for jump and dash inputs while at high Mach charge.
- Restores captured movement values when the plugin component is destroyed.

The ULTRAKILL modding guide identifies `NewMovement` as the player movement controller and documents public `walkSpeed` and `jumpPower` members. See https://ultramodding.github.io/docs/Getting-Started/second-mod .

## Controls

- Left Ctrl: hold to accelerate into Mach Run.
- Space: jump combo action (requires high Mach charge).
- Left Shift: dash combo action (requires high Mach charge).

Keys and tuning values can be changed in the generated BepInEx config.

## Explicit limitations

- This is a first prototype, not a completed Pizza Tower movement port.
- It does not replace the player model with Peppino.
- It does not create Pizza Tower levels, import Pizza Tower assets, or alter ULTRAKILL weapons/enemies.
- Combo scoring is currently input-based; it does not yet detect kills, style ranks, landings, or actual dash completion.
- Reflection member names and behavior must be verified against the user's installed ULTRAKILL version.
- It has not been compiled or tested in-game in this environment. Do not publish it as a tested release until that is done.

Game files and proprietary assemblies are referenced locally only; none are included here.
