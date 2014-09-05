ManiaNet ManiaPlanet
====================

This library is for accessing the data available from the ManiaPlanet WebServices from C#/.NET applications.

It supports the following platforms:

* .NET >4.5
* Windows 8 Store Apps
* Windows Phone 8.1 Apps

--------------------------------------------------------------------------------------------------------------------------------

##Usage##

First, you'll need a WebServices account. Just go to your [Player Page](https://player.maniaplanet.com/webservices) and create one.

Then use the the login credentials to create a Client for the branch you want, or create a CombiClient to get all of them together.

``` CSharp
using ManiaNet.ManiaPlanet;

var combiClient = new CombiClient(login, password);

var playerInfo = await combiClient.Players.GetInfoAsync("banane9");

// etc.
```

--------------------------------------------------------------------------------------------------------------------------------

##License##

#####[LGPL V2.1](https://github.com/Banane9/ManiaNet/blob/master/LICENSE.md)#####
