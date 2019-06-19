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

# Core Features

## Demos and Compilations
* Self explanatory

## Movie Mode
* Runs as per config
* Support Slower Time

# Fix and Finish Features

## Workflow Iteration Design - How should Uriel be used?
* Design the core workflow in a way that's not hacky.
* Create a workflow description Doc that you build a scenario list from.

## Look at other GUI Frameworks
* Are we going forward with winforms or switching to something else.

## Beauty Parlor
* Splash screen with Uriel Art
* Uriel Icon

## GLSL basic file handling improvements 
* Load Shader from file via dialog
* Save File - previous iterations can't be saved, have no cache except in memory.
* Shader Stack with buttons
* Shader Compilation Error Message Popup 
* Line error highlighting! (neat) 

## Build System
* CI framework
* Build Drop
* Actual Versioning System

## Advanced Features

## Advanced Pre-Req - Consistent Uniform handling (Generic Uniforms)
* Current Uniform system is specific not generic
* Requires adding new uniform concept across many classes per new uniform
* Current system Does not distinguish between 
** Required/NonRequired Uniforms (important for testing)
** Uniforms that are consumed in the shader versus 'compiled out'
** TODO: Dynamic verus 'Constant' uniforms? Static Uniforms not updated per frame?

## Movie Mode - Export
* Export as video file

## Cellular Automata Mode
* Requires use of buffers
* Requires pretty sophisticated Uriel Directives

## Audio Input Support
* Audio Input as uniform
* FFT'ed variable? Interesting
* Audio File or Live Audio Feed

## Load Shaders from Shadertoy URI
* Load + Save

## Mouse Uniform Implementation
* Seems easy, probably
