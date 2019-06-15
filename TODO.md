# Road to v1.0

## Uriel v0.5: Draw a Square - DONE
* Draw a square
* Size as per configuration
* Debug Logging as per configuration

## Uriel v0.6: Time Uniform + FPS - DONE
* Implement Time Uniform 
* Measure FPS
* Display FPS + Time on status bar

## Uriel v0.74: Shader Loading from Watcher - DONE
* Decide on Vertex/Fragment treatment/architecture
* Moving between Shaders resets smoothly
* Load Shader by watching directory

## Uriel v0.8: Texture Support - DONE
* Lots of details.

## Uriel v0.87: Shader Loading from File Dialog 
* Load Shader from file via dialog
* Shader Stack with buttons
* Shader Compilation Error Message Popup

## Uriel v0.88: Uriel Shader Directive - PARTIALLY DONE
* flesh out the syntax

## Uriel v0.89: Visual Studio "Plugin" Mode
* Not actually a Plugin
* Test out workflow and tweak.

## Uriel v0.9: Refactor and Cleanup
* Refactor - make it Data Driven
* Isolate Winforms Stuff
* Configuration File - xml

## Uriel v1.0 Minimum Viable Product Fit + Finish
* Splash screen with Uriel Art
* Uriel Icon
* Bugfixes 

# Additional Features

## Optimization
* Learn how milisecond level timing is handled 
* ~~Log4Net, with appropriate settings to be able to handle per-frame log statements~~ Done
* Attempt FPS limiter
* ~~Expectation: 90 FPS for Small Window~~ - Done
* ~~Expectation: 60 FPS for Giant Window~~ - Done
* Profile GC - is it even an issue?
** Not Yet - 5/19/2019

## Build System
* CI framework
* Build Drop
* Actual Versioning System

## Demos and Compilations
* Self explanatory

## Movie Mode
* Runs as per config
* Support Slower Time
* Export as video file

## Cellular Automata Mode
* Requires use of buffers
* Requires pretty sophisticated Uriel Directives

## Audio Input Support
* Audio Input as uniform
* FFT'ed variable? Interesting
* Audio File or Live Audio Feed

## Mouse Uniform
* Seems easy, probably
