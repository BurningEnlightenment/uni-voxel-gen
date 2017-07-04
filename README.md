# TNT Map Generator
The project implements a modular Voxel terrain generation pipeline.

## General overview
The project is split into a viewer and a generator component which can execute
independently of each other, sharing the generated level data through a file
format defined with [Google Protocol Buffers](https://developers.google.com/protocol-buffers/).

The viewer is a unity based thin client consisting only of an algorithm to derive
`GameObjects` from the compressed protobuf format and the graphics necessary to
actually render the generated content.

The actual map generator is a commandline program that implements a world
generation pipeline. This pipeline has four different stages:
* biome generation
* density generation
* terrain generation
* details generation
Whereby each stage can have multiple implementations using different generation
strategies. All stages heavily rely on Simplex Noise an algorithm for generating
coherent pseudo random noise (well, except the "debug" implementations).

### Biome generation
In this stage each "point" on the areal map gets a biome assigned.

### Density generation
Decides where non air and water blocks will be placed (usually based on the biome
information from the previous stage).

### Terrain generation
Sets the non air block types and pours water over the world :wink:.

### Details generation
This stage can consist of multiple generators which add vegetation, etc.
