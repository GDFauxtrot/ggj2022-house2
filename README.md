# House 2

## Setup

Trello link: https://trello.com/b/q1DUXirC/house-2

### Unity
Unity Hub is highly recommended ([link here, scroll down to download](https://unity.com/unity-hub))

Go ahead and download Unity 2021.2.8f1 (project version), which is the latest as of the start of GGJ 2022.

### Git
If you're not familiar with Git, it's a way to sync everyone's work on the same project. For beginners, [pick up GitHub Desktop](https://desktop.github.com/), make an account and you'll be added as a project collaborator shortly.

Pull down the project in Git (or GitHub Desktop) and open the project in Unity Hub, now you have the project!

### VS Code (programming)
VS Code with extensions is also highly recommended, it's a perfectly usable debugger in a quick and light editor. ([link to download here](https://code.visualstudio.com/Download))

VS Code extensions recommended:
  * Debugger for Unity (required for debugging, will automatically pull down C# extension as well)
  * Current File Path (shows file path of currently open files at the bottom, very unobtrusive)
  * TODO Highlight (gives TODO comments a highlight)
  * No need to download Bracket Pair Colorizer 2, VS Code has it built-in now. Go to Edit->Preferences->Editor->Bracket Pair Colorization and enable
  * Nord (if you want a nice VS Code theme)
  * C# XML Documentation Comments (auto-populates C# comments if you insert 3 forward slashes before a function, class or variable declaration)



## Project Notes

Unity version: 2021.2.8f1
Project template: 3D with URP

For info on URP, [check out their documentation here](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@13.1/manual/index.html), but as a summary it's better than built-in because of:

  * Shader graph, VFX graph built-in
  * Visual interfaces for cool graphics effects without coding
  * Multi-camera rendering and render targets without code
  * Meant to be faster and more performant than built-in renderer in all use cases out of the box

Or so Unity people say, we'll see if it helps out or not since I haven't used it since it was called LWRP (2019)
