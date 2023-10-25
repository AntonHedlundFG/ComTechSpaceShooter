# ComTechSpaceShooter

This is an assignment for the 4-week Computer Technology course at FutureGames, class GP22.
In it, I made a very simple asteroids clone in Unity, not meant to be a fully playable game but a way to stresstest hardware.
There are three major implementation variants:

## [Naive](https://github.com/AntonHedlundFG/ComTechSpaceShooter/releases/tag/NaiveState)
This version uses Unity's traditional object-oriented C# approach. It has virtually no optimized features; spawning/destroying without pooling and unoptimized built-in collisions.
The naive version is able to render and manage a few thousand asteroids. At 10k asteroids the game stutters heavily.

## [DOTS](https://github.com/AntonHedlundFG/ComTechSpaceShooter/releases/tag/Full-DOTS-Version)
For this version I rewrote the entire game to use the Unity DOTS package, with Jobs and Entities, but without utilizing the Burst compiler. 
With this improvement, the game can easily manage 100k asteroids without stuttering. Once you start shooting 1000 bullets per second, however, the collision calculations in combination with the creation & destruction of both bullets and asteroids get quite heavy.

## [BurstCompile](https://github.com/AntonHedlundFG/ComTechSpaceShooter/releases/tag/BurstCompile-Everything)
Using mostly the same code as in the DOTS version, with a few changes to avoid managed types, this version uses the BurstCompiler for all C# code.

# Analysis

## Comparing Naive(A) to DOTS(B) with 10k asteroids
### Memory
Ignoring the profiler overhead, the memory usage for the DOTS version was more or less equivalent to the Naive version. I suppose this should not be too surprising, as the asteroids have transforms, meshes and material components, at which point any memory benefits from using Entities over GameObjects would be negligible.
![ImageStuff](/Screenshots/MemoryComparisons/Naive10kVSDOTS10k.png)
### CPU




## TO-DO

- Make 10k-asteroid comparison between Naive and DOTS version
- Make 100k-asteroid comparison between DOTS and BurstCompile
- Take deeper look at profiler for 100k BurstCompile to understand bottlenecks
- Verify that executables run properly on other devices
