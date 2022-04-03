# OnlineRTS

Dowload game (need at least 2 players)

This is a game prototype is a Online Real Time Strategy Game allowing you to play with up to 3 of your friends. It was created so I could learn how to set up a multiplayer Real Time game. It was created within Unity using Mirror a networking API and The Steam API as a gateway to connect with friends over steam.


This game prototype has

a client-server model for online services.

Uses synchronization, remote procedure calls and server authoritative logic.

in-game lobby so players can wait for all player to join before starting the game

gameplay features such as resource economy, base building, unit spawning, multi-selection, unit combat and Nav-Mesh pathfinding.

Base Building: Click and Drag buildings to place them. Selected Building changes from red to green when placement is valid and the player has enough resources.

Unit Spawning: Selected Unit building sets up a queue for spawning if resource are available.

Multi-Selection: Clicking and dragging causes a grey box to form on screen and all units within are selected.

Unit's combat system and Navigation Mesh: Units have different speed, health, and damage per second. Units won't fight for position and will find the quickest path around static objects.

A player wins when all enemy bases are destroyed.

Course

