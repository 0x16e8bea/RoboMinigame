# Game Programmer Interview Test - README

## Project Overview

This project introduces a minigame where players control a robot using a gamepad reminiscent of the classic SNES controller. Created as part of a game programmer interview, the focus was on demonstrating coding practices and design concepts rather than completing every game mechanic.

## Controls

Control the robot using:

### Keyboard Controls

- **Movement**: `WASD` or `Arrow keys`
- **Shooting**: `E` or `Shift`
- **Jumping**: `Space`

### Mouse Controls

- Interact with the on-screen gamepad to perform actions. The way this works is basically by emulating a real input device in Unity's new input system.

## How to Play

The game allows players to navigate a main scene (left / right), controlling the character to engage with enemies, showcasing the primary features of the character controller.

## Development Highlights

- This project demonstrates the use of dependency injection in Unity for game development.
- I decided to create custom 3D assets and animations, utilizing Unity's new GLTF importer for asset optimization (however, .FBX is still more feature rich).
- I spent the most effort on the player character. Some of the additional features were kinda rushed, but serves to highlight the character controller's capabilities in context.
