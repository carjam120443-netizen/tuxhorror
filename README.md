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

- Unity **6000.6.7f1**
- Git

The movement and camera scripts currently use Unity's built-in legacy Input API. In **Edit → Project Settings → Player → Other Settings**, set **Active Input Handling** to **Both** (or **Input Manager (Old)**). Unity may ask to restart the Editor.

## Getting started

1. Clone this repository:

   ```bash
   git clone https://github.com/carjam120443-netizen/tuxhorror.git
   ```

2. In Unity Hub, choose **Add → Add project from disk** and select the cloned `tuxhorror` folder.
3. Open it with Unity **6000.6.7f1** and wait for the first import to finish.
4. In the Editor, select **Tools → TuxHorror → Create Starter Scene**.
5. Open `Assets/Scenes/TuxHorror_Prototype.unity` and press **Play**.

The repository now includes the Unity project version and a minimal package manifest, so you do not need to create a temporary project or copy project settings.

## Building a Windows executable with GitHub Actions

The repository includes `.github/workflows/build-windows.yml`. It builds a Windows 64-bit player and uploads a downloadable artifact containing `TuxHorror.exe` and its required data files.

### One-time Unity Personal license setup

GitHub Actions needs a Unity license to run the Editor. For a Unity Personal license:

1. In Unity Hub, sign in and activate your free Personal license.
2. Locate the license file. On Windows, GameCI documents the usual location as `C:\ProgramData\Unity\Unity_lic.ulf`.
3. In this repository, open **Settings → Secrets and variables → Actions**.
4. Add these repository secrets:
   - `UNITY_LICENSE` — the full text of the `.ulf` file
   - `UNITY_EMAIL` — the email address for your Unity account
   - `UNITY_PASSWORD` — the Unity account password

**Never commit the license file or credentials to the repository, and do not paste them into chat.** Store them only as GitHub Actions secrets.

### Run a build

1. Open the repository's **Actions** tab.
2. Select **Build TuxHorror for Windows**.
3. Click **Run workflow**.
4. When the run finishes, open it and download the **TuxHorror-Windows-x64** artifact.

The artifact is a ZIP containing the executable and supporting files. Extract the whole ZIP before launching the game; the `.exe` needs its accompanying data folder and Unity runtime files.

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
- [x] Unity 6000.6.7f1 project metadata
- [x] GitHub Actions Windows x64 build workflow
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
