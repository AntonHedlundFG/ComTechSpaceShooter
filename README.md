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

## Comparing Naive to DOTS with 10k asteroids

### Memory
Ignoring the profiler overhead, the memory usage for the DOTS version was more or less equivalent to the Naive version. I suppose this should not be too surprising, as the asteroids have transforms, meshes and material components, at which point any memory benefits from using Entities over GameObjects would be negligible.
#### Memory profile comparison, Naive(A) vs Dots(B)
![MemoryProfiler, Naive(A) vs DOTS(B)](/Screenshots/MemoryComparisons/Naive10kVSDOTS10k.png)

### CPU
This is where the benefits really show. The first image below shows the CPU profiling of the naive approach, the second image shows the DOTS version. A major benefit was the reduction in physics calculations, which were more or less free in the DOTS version. In fact, the largest culprits in the DOTS version are the "MovingJob" and "EdgeOfScreenJob" which are my implementations that do not spread over multiple threads.
#### Naive, 10k asteroids
![Profiler, Naive 10k asteroids](/Screenshots/Profiler/Naive10k.png)
#### DOTS, 10k asteroids
![Profiler, DOTS 10k asteroids](/Screenshots/Profiler/Dots10k.png)

## Comparing DOTS to Burst with 100k asteroids

### Memory
There do not seem to be any significant differences in memory use between the DOTS and Burst version. The differences in the screenshot below is primarily in profiler overhead.
#### Memory profile comparison, DOTS(A) vs Burst(B)
![MemoryProfiler, DOTS(A) vs Burst(B)](/Screenshots/MemoryComparisons/Dots100kVSBurst100k.png)

### CPU
These results are pretty impressive. I've made virtually no changes to the code, except change a few managed data types to unmanaged ones. The only major change is to mark all my code to be BurstCompiled.
In the first pair of screenshots, we can see the CPU usage while 100k asteroids are on screen, but without the ship shooting. In the third image, we see CPU usage for the DOTS version -while- shooting. This actually causes quite severe stuttering. In the BurstCompiled version, however, there is no noticable change in CPU usage when shooting starts. 
#### Burst, 100k asteroids
![Profiler, Burst 100k asteroids](/Screenshots/Profiler/Burst100k.png)
#### Dots, 100k asteroids, no shooting
![Profiler, DOTS 100k asteroids](/Screenshots/Profiler/Dots100k.png)
#### Dots, 100k asteroids, while shooting
![Profiler, DOTS 100k asteroids with shooting](/Screenshots/Profiler/Dots100kshooting.png)

## TO-DO

- Verify that executables run properly on other devices
