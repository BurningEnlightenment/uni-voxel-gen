# TNT Map Generator
The project implements a modular Voxel terrain generation pipeline.
This was a university project developed by Nico Redick (@n-redick), Sabrina Schawohl and Henrik Gaßmann (@BurningEnlightenment).

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
Places air blocks (usually based on the biome information from the previous stage).
The internal representation initializes all blocks of a touched (accessed) chunkwith debug blocks, therefore this stage also touches all chunks in between the floor and the "ceiling" of the map.

### Terrain generation
Sets the non air block types and pours water over the world :wink:.

### Details generation
This stage can consist of multiple generators which add vegetation, etc.


# License
Apache 2.0, see [LICENSE](LICENSE) for details.


# Research links
The following sites have been consulted and contributed to varying degrees to the project.
* http://in2gpu.com/2014/07/27/build-minecraft-unity-part-1/
* http://pcg.wikidot.com/
* http://mc-server.xoft.cz/docs/Generator.html
* http://uniblock.tumblr.com/post/97868843242/noise
* https://www.reddit.com/r/proceduralgeneration/comments/2v2mgy/optimizing_opensimplex_part_2_hyperplanes_bit/
* http://webstaff.itn.liu.se/~stegu/simplexnoise/simplexnoise.pdf
* https://gist.github.com/mfuerstenau/ba870a29e16536fdbaba
* http://www.redblobgames.com/maps/terrain-from-noise/
