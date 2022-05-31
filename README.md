# LoLSSTracker
> This software will not work unless you have the correct credentials. You may use this repository and add your own credientials via AWS, however this is a personal project with intended use for only a few people.

This is a simple non-overlay tool built in C# designed to pull summoner spell data from the API and make it easier to track when a summoner spell was used.

This program uses Riot's Spectator, and Summoner APIs along with Data Dragon to process the summoner spells that are in use on the enemy team and which champion is using them. Included is a checkbox for Ionian Boots of Lucidity in order to recalculate the cooldown changes associated with that item. As well as an option to convert the cooldown time from seconds to MIN:SEC

<p align="center">
  <img src="https://user-images.githubusercontent.com/13126350/170962518-2931d6e5-fdab-4911-99e2-9f9b3672fda2.gif" alt="animated" />

</p>

# Known Issues:
• There does appear to be about a one second delay between the time you click on a spell to the time it is updated, this is due to how the tick system works, I will be looking for a way to resolve this to make it feel more immediate.
