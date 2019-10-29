# BeatSaberRESTInterface

Let your BeatSaber application be controlled from a distant server.

## Description

The plugin sends a http put request to a server. The address of the server is specified in BeatSaber/UserData/BeatSaberRESTInterface.json. There is also an update interval, after how much seconds a an new message will be send.

The message consists of a json string in the following format:

```
{
  "UserName": "Player4711", // string
  "UserId": 2429129807113296,   // unsigned long
  "Score": 999  // int
}
```

`UserName` and `UserId` are from the users scoresaber profile. `Score` is the current score of the currently played map.

The server can send a response to the message in the following format:

```
{
  "LevelID": null, // string
  "Start": false // bool
}
```

`LevelID` is the Id of a BeatSaber map. For example for the custom map 'Rasputin (Funk Overload)' the level ID is 'custom_level_22DEB3F337EE75AAECF646C257D3FCC91D27728D'. In 'BeatSaberSongs.txt' are some level IDs to look up.

If the game is in the solo game mode, the map will be switched to the given map. If also `Start` is specified to true, the map will be started.

## Installation

Copy 'BeatSaberRESTInterface.dll' into your Plugins folder of BeatSaber.
