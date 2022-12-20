# procedural-grid-world-generation
 
This is my take on the wave function collapse algorithm in the form of a modular, procedural world-generation system. 
It's certainly less elegant than other versions (i.e. you must manually put in possible neighbouring elements) but it can be easily tailored to suit a variety of games.
It currently only works in two dimensions, but it can spawn in buildings and scenery (so it could work well as an RPG world if you attach colliders to the prefabs).

 To inform you of what integers correspond to what modules:

 This is necessary for the system as the "neighbour matrices" listed in the GridObject class depend on these to understand what modules can spawn next to them. So, if you want to expand on this system, these are necessary to know.

 0 - Full Water/Ocean 
 1 - Outside Corner Top Left
 2 - Top Shoreline
 3 - Outside Corner Top Right
 4 - Left Shoreline
 5 - Full Grass/Ground
 6 - Right Shoreline
 7 - Outside Corner Bottom Left
 8 - Bottom Shoreline
 9 - Outside Corner Bottom Right
 10 - Inside Corner Top Left
 11 - Inside Corner Top Right
 12 - Inside Corner Bottom Left
 13 - Inside Corner Bottom Right

 There is also an image in the Asset Files which I drew. It is a key to the module types and visualises what's written above.