# Security Shield - Slice 1

This is the first working slice of the Security Shield app: the dark gamer
dashboard with a live Shield Score, an XP bar, and buttons that actually work.

It is a **Windows app**, built with **C# and .NET (WPF)**. You open and run it
in **Visual Studio** (the free Community edition).

---

## How to run it (step by step)

1. **Install Visual Studio Community** (free) from Microsoft's website.
   During the install, tick the workload called **".NET desktop development"**.
   (This installs C#, .NET 8, and WPF - everything this app needs.)

2. **Unzip this folder** somewhere easy to find, like your Desktop.

3. **Open the project.** Double-click **`SecurityShield.csproj`**.
   Visual Studio will open the whole project.

4. **Press the green Run button** at the top (or press **F5**).
   After a moment, the Security Shield dashboard window appears.

That's it - you're running your own app.

---

## What to try once it's open

- Click a **Fix** or **Clean** button. Watch the **Shield Score go up**, the
  green ring fill further, the **XP bar grow**, and the row mark itself "Done".
- Fix all three and the score reaches **100** and says **"Fully protected"**.
- Click **"Game ready - optimise now"** to see a pop-up message.

The numbers here are a demo - nothing is really scanning your PC yet. That comes
in later slices. This slice is about getting the real app, screen, and game
mechanics working.

---

## What each file does

| File | What it is |
| --- | --- |
| `SecurityShield.csproj` | Settings that tell Visual Studio how to build the app |
| `App.xaml` / `App.xaml.cs` | The app's starting point (opens the main window) |
| `MainWindow.xaml` | The **look** of the dashboard - the layout, colours, text |
| `MainWindow.xaml.cs` | The **behaviour** - draws the score ring, handles the buttons |

The `.xaml` files are the *design*; the `.cs` files are the *logic*. That split
is the heart of how WPF apps are built.

---

## Next slice

Add the first **real** feature: a password strength checker. When you're ready,
just say so.
