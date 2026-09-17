# Unity Prototype Setup

This folder contains the Unity C# prototype scripts for the MOBA project.

## Unity version

Use Unity 2022.3 LTS or newer.

## Folder

Open `UnityProject` as the Unity project root, then place these scripts under `Assets/Scripts` if they are not already imported.

## Scene setup

1. Create a 3D scene with a large Plane named `Ground`.
2. Put the Ground on a `Ground` physics layer.
3. Add a Camera with `MobaCameraController`; set its height and map bounds.
4. Create a hero GameObject with a `CharacterController`, `HeroController`, `AbilityController`, and `PlayerInputController`.
5. Assign the Ground layer to `PlayerInputController.groundMask`.
6. Add a `GameManager` object.
7. Add towers and Ancients with `StructureController`; set their team and mark only the Ancient objects as `ancient`.
8. Add a Canvas and minimap camera, then configure `MinimapController`.
9. Add a directional light with `DayNightCycle` for the ten-minute day/night cycle.
10. Add creep prefabs with `CreepController` and lane target transforms.

## Controls

- Left click on the ground: move hero
- Middle mouse drag: pan camera
- WASD / arrow keys: pan camera
- Q: fireball
- E: heal
- Left click minimap: snap camera
- Right click minimap: move hero

The scripts use Unity's legacy Input API for quick prototype setup. For production, migrate the input layer to Unity's Input System package.
