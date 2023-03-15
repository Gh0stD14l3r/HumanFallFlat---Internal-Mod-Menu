Raft Mod Menu - C# DLL Source [Base]

This is a basic mod menu created in C#. It is a base which you can add your own items to it or modifications. Currently it has the following.

- Player ESP
- Self Respawn [F4]
- Players Release (Other players have butterfingers) [F5]
- Knockout Self (4 Seconds)
- Knockout Players (4 Seconds) [F6]
- Decrease Jump [F9]
- Increase Jump [f10]
- Save Teleport Position [F7]
- Teleport To Saved Position [F8]
- Respawn All Players
- Blink (Currently not fully working)

Insert Key - Show/Hide Menu 
End Key - Unload DLL File safely

To compile..

- Download & Open Sln file for Visual Studio
- Compile in Debug or Release (Doesn't matter)

To Inject..

- Use a Mono injector (Possibly MonoSharpInjector)
- Select Process and browse to the assembly to inject (RaftHax.dll)
- Use the following settings.. -- Namespace: HumanFallFlat -- Class name: Loader -- Method name: init
- Press inject


![image](https://user-images.githubusercontent.com/38970826/177023075-133d7588-d608-4fd2-b98f-40a2b662c5b9.png)

