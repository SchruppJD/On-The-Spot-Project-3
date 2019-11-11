# Untitled

### On The Spot

| Version Control Master | Scrum Master    |
| ---------------------- | --------------- |
| Name                   | Name            |
| JDS5583                | amf7619         |
| Schames.               | Ferryboi#5260   |
| SchruppJD.             | amf7619         |

#### Keywords

-   Party
-   Cartoony
-   Competitive
-   Snowy
-   Winter
-   Arcade

#### Platforms(s)

-   Windows

#### Target Player Experience
This game will promote goofy fast paced competition and absolute tomfoolery. The action begins right away, throwing players into a room full of traps where they can feel free to mess with each other. The bright colors and the fun, "bouncy" music will give off a playful vibe, while the fast movement and pushing mechanic being the player's only options showing that it can be a lot of fun to push your friend somewhere deadly. Traps should make players afraid to progress forward, but the timer and the first place bonus will make the players take risks, enhancing the feeling of excitement that comes after the slight fears of doing something dangerous. Overall this game will aim to produce smiles and friendly rivalries for all players who choose to enter.

#### Core Diagram

1.  Core Mechanic
- Multiple players, all on the same machine, can move around in a 3D room. Their goal is to get to the last room, which they accomplish by passing through all of the rooms. The camera is top-down (but at a slight angle).

2.  Secondary Mechancics
- The players can push each other around by colliding into each other.
- All surviving players must make it to the end of each room. A few seconds after the players enter the room, the ground starts to fall apart, making it harder to be patient, as a player dies if they fall through the room.
- Alive players may set off traps by running into them. Traps are 3D objects that can kill or push the player.
- Players that die in the current room become 'ghosts' in the next room. They can activate traps at will (with cooldowns), and can push living players around. Ghosts are immune to traps and falling. If a ghost kills a living player, they come back to life in the next room.
- Each room contains traps and other elements that create stress and chaos for the players.
- Players may also be able to jump.
3.  Progression
- A player gets a point whenever they kill another player.
- Players also get points for reaching the end of a room the fastest.
- The players move through a series of rooms until they reach a linear end.
- The player with the highest score wins.

4.  Narrative
- After years of punishing bad children, Krampus has designed a new way to punish children who end up on the naughty list. After being caught, dodge traps, avoid obstacles and try not to get tripped up by your fellow evil cohorts while navigating course from hell, showing Krampus who really deserves to be on the naughty list. Whoever wins gets their freedom and the chance to punish other naughty children as Krampus' little elf!
#### Minimal Viable Interaction
For the MVI, we are going to need the basic gameloop and mechanics. This will include multiplayer, traps, the ablility for players to push eachother, and a way to win. A basic room will be needed for the players to move in and for the trap to reside in. The win condition at this point will mostly likely be whoever gets to the end first, later iterations will have multiple rooms/stages where players can earn points allowing the player with most points to be the winner. There will be only one kind of trap for the MVI with plans to add more variations as we progress.

#### References
Gameplay/Layout Reference - The traps and ghost mechanic are similar to the one found in Crawl. Also, the small-room top-down design aspect is also similar to Crawl.
![alt text](https://edge.alluremedia.com.au/m/k/2014/05/crawl3.jpg "Crawl")
Theming Reference - The game will be Winter/Christmas themed in nature.
![Scary Krampus holding wee bebe](https://www.wweek.com/resizer/kt8_j8nSzFzNIrPWKqwh_CDBw9E=/1200x0/filters:quality(100)/s3.amazonaws.com/arc-wordpress-client-uploads/wweek/wp-content/uploads/2018/11/29160904/26172944_1791082507569786_6080951049921406738_o-e1543537037658.jpg)
![Isometric Runner](https://res.cloudinary.com/dylgjm9z8/image/upload/c_scale,w_949/v1430454005/PH_DP_WaterfallStream_avawhs.jpg)
Gameplay Reference - Isometric View as players avoid obstacles along the way. (With possible timer/points counter)
