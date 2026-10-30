# 🐧 TuxHorror

**A third-person 3D horror game starring Tux.**

TuxHorror is a Unity project about exploring an abandoned research facility where something has gone very wrong. The project is being built for **Windows and Linux**.

> 🚧 **Early development:** this repository currently contains starter scripts and a greybox scene generator. Models, animations, story content, audio, and finished levels will be added as development continues.

## Current starter features

- Third-person movement relative to the camera
- Walk, run, and jump controls
- Mouse-controlled orbit camera
- Escape releases the mouse cursor; click the Game view to capture it again
- One-click editor tool that builds a basic research-facility test scene
- Placeholder Tux assembled from Unity primitives

## Requirements

- Unity 6 (a current Unity 6 LTS editor is recommended)
- Windows or Linux for development and testing
- Git

The movement and camera scripts currently use Unity's built-in legacy Input API. In **Edit → Project Settings → Player → Other Settings**, set **Active Input Handling** to **Both** (or **Input Manager (Old)**). Unity may ask to restart the Editor.

## Getting started

### 1. Create the Unity project

1. Open Unity Hub.
2. Create a **3D (URP)** project named `TuxHorror` using your installed Unity 6 editor.
3. Close the project after Unity finishes creating it.

### 2. Add this repository

Clone this repository:

```bash
git clone https://github.com/carjam120443-netizen/tuxhorror.git
```

Copy the repository's `Assets` folder into the Unity project folder. If Unity asks whether to merge folders, choose **Merge**. Keep the Unity-generated `ProjectSettings`, `Packages`, and `ProjectVersion.txt` files from the project you created.

Alternatively, clone the repository first and copy the Unity-generated project files into the cloned folder.

### 3. Generate the prototype scene

1. Reopen `TuxHorror` in Unity Hub.
2. Wait for scripts to compile.
3. In the Unity menu, select **Tools → TuxHorror → Create Starter Scene**.
4. Open `Assets/Scenes/TuxHorror_Prototype.unity`.
5. Press **Play**.

The scene builder creates a corridor, lights, a few props, a placeholder Tux, and a third-person camera. It is a greybox for testing, not the final art direction.

## Controls

| Action | Keyboard / Mouse |
| --- | --- |
| Move | W, A, S, D |
| Look around | Mouse |
| Run | Left Shift |
| Jump | Space |
| Release cursor | Escape |
| Capture cursor | Click inside the Game view |

## Repository layout

```text
Assets/
├── Editor/
│   └── TuxHorrorSceneBuilder.cs
└── Scripts/
    └── Player/
        ├── TuxThirdPersonCamera.cs
        └── TuxThirdPersonController.cs
```

Unity will create `.meta` files when it imports the assets. Keep those files in Git once they exist.

## Development roadmap

- [x] Initial project README and repository scaffold
- [x] Basic third-person Tux movement
- [x] Orbit camera
- [x] Greybox facility scene generator
- [ ] Replace primitive Tux with a proper character model
- [ ] Add idle, walk, run, and jump animations
- [ ] Add interaction system for doors, notes, and computers
- [ ] Build exploration and environmental puzzles
- [ ] Add atmospheric audio, lighting, and story events
- [ ] Add enemy behaviour and encounter design
- [ ] Create main menu, pause menu, and settings
- [ ] Test Windows and Linux builds

## Contributing

This is a personal game-development project. Issues and suggestions are welcome.

## License

No license has been added yet. All rights are reserved by the repository owner unless a license is added.
