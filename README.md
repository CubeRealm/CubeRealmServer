# What is a CubeRealm Server?
[![Paper Build Status](https://img.shields.io/github/actions/workflow/status/Flavelm/CubeRealmServer/build.yml?branch=master)](https://github.com/Flavelm/CubeRealmServer/actions)
[![Discord](https://img.shields.io/discord/1201546286688653372.svg?label=&logo=discord&logoColor=ffffff&color=7389D8&labelColor=6A7EC2)](https://discord.gg/JatDqzG8Db)
===========
It's a new multithreaded minecraft server core. Our core supports versions 1.19.2 and newer. The core is written in C#. Easy API allows allows you to create your own plugins and modules. To build core, plugins or modules you will need .NET 8.0 installed.

## Support us

## Get help
You always can help in our [Discord Server](https://discord.com/) or in [Telegram Chat](https://t.me/)

## Your first server
If you want to try our core and create minecraft server with it, follow the instructions.
### Download files

### Configuration
Let's set up the configuration and change it for you. You need to open file _"appsettings.json"_
#### Logging
System parameter. Change at your own risk.
#### Server
- ***version_protocol***: integer number of protocol. You can find list of protocols [here](https://wiki.vg/Protocol_version_numbers)
- ***max_players***: limit of players who can connect to the server
- ***hide_online_players***: should be players visible for other players
- ***motd***: text which player see in servers list
- ***whitelist***: should be whitelist enabled
#### NetServer
- ***Address***: ip address of machine (default 0.0.0.0 or 127.0.0.1)
- ***Port***: port to which the player will connect (default 25565)
#### Plugins
- ***IsDisabled***: should core enable plugins
- ***Directory***: directory where you put your plugins
#### World
- ***spawn_animals***: should animals be spawned
- ***spawn_monsters***: should monsters be spawned
- ***world_prefix***: prefix of default world (ex. "world_prefix": "game". You will get worlds with names "game", "game_nether", "game_the_end")
- ***allow_nether***: turn this off if you don't want the nether world to be generated
- ***allow_end***: turn this off if you don't want the end world to be generated
- ***view_distance***: distance in chunks that the player will be able to see
- ***simulation_distance***: distance in chunks that the player will be able to simulate
- ***difficulty***: difficulty in the world (Peaceful, Easy, Normal, Hard)
- ***gamemode***: gamemode for all players (Survival, Creative, Adventure, Spectator)
- ***hardcore***: if enabled, the player will be banned upon death
- ***pvp***: should players damage other players
### Default configuration
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Server": {
    "General":{
      "version_protocol": 765,
      "max_players": 50,
      "hide_online_players": false,
      "motd": "CubeRealm powered server",
      "whitelist": true
    },
    "NetServer": {
      "Address": "0.0.0.0",
      "Port": 25565
    },
    "Plugins": {
      "IsDisabled": false,
      "Directory": "plugins"
    },
    "World": {
      "spawn_animals": true,
      "spawn_monsters": true,
      "world_prefix": "world",
      "allow_nether": true,
      "allow_end": true,
      "view_distance": 8,
      "simulation_distance": 10,
      "difficulty": "Normal",
      "gamemode": "Survival",
      "hardcore": false,
      "pvp": true
    }
  }
}
```
### Run the server

## How to build the core from sources

## Getting started with the API
### Create plugin
### Hello world
### Creating command
### Work with events
### API Documentation

### TODO
- [ ] Loading plugins and modules
- [ ] Support different versions
- [ ] Multithreaded operations with world
