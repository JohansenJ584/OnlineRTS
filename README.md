# OnlineRTS

If you want to play: <br>
download game <br>
https://johansenj584.itch.io/online-real-time-strategy <br>
To add to steam <br>
1. Launch Steam.
2. Click the. Games. menu, choose. Add a Non-Steam Game to My Library.
3. Browse for games on your computer or put a check next to the game(s) you wish to add to the. Library.
4. Click on. Add Selected Programs.
You need at least to players with both with steam installed.
Load up game and join through Steams friends list.

This game prototype is a Online Real Time Strategy Game that allows for you to play with up to 3 of your friends. This game was made so I could learn how to set up a multiplayer Real Time Strategy game. It was created within Unity using Mirror, a networking API, and The Steam API as a gateway to connect with other clients over Steam's Network.

This game prototype has

* A client-server model for online services.
* Uses synchronization, remote procedure calls and server authoritative logic.
* An in-game lobby so players can wait for all other players to join before starting the game
* Gameplay mechanics include resource economy, base building, unit spawning, multi-selection, unit combat and Nav-Mesh pathfinding.
  * Base Building: Click and Drag buildings to place them. Selected Building changes from red to green when placement is valid and the player has enough resources.
  * Unit Spawning: Selected Unit building sets up a queue for spawning if resource are available.
  * Multi-Selection: Clicking and dragging causes a grey box to form on screen and all units within are selected.
  * Unit's combat system and Navigation Mesh: Units have different speed, health, and damage per second. Units won't fight for position and will find the quickest path around static objects.
* A player wins when all enemy bases are destroyed.

Course: https://www.udemy.com/course/unity-multiplayer/ 

