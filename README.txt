<< THIS APP WAS MADE WAS EDUCATIONAL PURPOSES ONLY >>

We wanted to make an app that could track the Danish soccer teams live scores at the FIFA world cup.
ONLY to notify our Phillips Hue lights, anytime Denmark would score a goal!

123 schmeichel er en mur!

The code can easily be changed to your favorite team, so feel free to check it out. Only depends on a few lines.

HOW IT WORKS:

Open the soulution in Visual studio and make sure the Nuget packages are installed:
Newtonsoft.Json(13.0.1)
Q42.HueApi(3.20.0)
Q42.HueApi.ColorConverters(3.18.1)

Open a terminal and navigate to the HUE1.0 project, the use the 'dotnet run' command.
The language in the terminal is in danish, but these are the things you must insert:

1st stop: press enter to start the application
now it says: "finder mulig bridge" which means "finding the bridge" wait for it to finish...

2nd stop: enter your IP adress,  which can be found in your phillips hue smartphone app.

3rd stop: if this is your first time connecting enter "nej", if not enter "ja".

IF IT IS YOUR FIRST TIME: YOU NOW HAVE 10 SECONDS TO PRESS THE BUTTON ON YOU PHYSICAL HUE BRIDGE/HUB
If the connection is made, you should get a key, save that somewhere on your PC please. you will need it next time.

if its not you first time, and you wrote "ja": Now enter your key that was given to you on your first time.

Now all there is left is to press enter, and the apliccation should be running.
You can tell by the update every two seconds in the terminal.

THANKS TO
@michielpost - the github user who made this possible with his wonderfull NuGet pack.








