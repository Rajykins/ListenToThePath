![Unity](https://img.shields.io/badge/-Unity-000?style=flat&logo=Unity)

# Listen to the Path 
<img src="https://imgur.com/UICMWQo">
<img src="https://i.imgur.com/6nfuUYS.png">
> Listen to the Path is a maze game that is designed for inclusivity to allow everyone, no matter their visual capabilities, to have an immersive experience. <br/> 

Made-by: Rajessen Sanassy ([rajykins](https://github.com/Rajykins)), Jasmine Kang ([jmoonus](https://github.com/jmoonus)), Josh Kim ([LemonTii](https://github.com/LemonTii)), Zijian Meng ([RichardZJM](https://github.com/RichardZJM))

## Minimum Requirements:
 - Windows 7 or higher.
 - macOS 10.13 or later.
 
## Features
- This game was designed with accessible features, particularly with the visually impaired in mind.
	- Auditory features were implemented along with visual features to ensure maximum clarity of the game, no matter the person's limitations. 
	- This includes voiceovers and special audio effects for an enveloping experience, as well as 3D sounds to give audio cues to reach a goal and provide direction and a strong atmosphere to compliment the plot and functionality, guiding the player in their choices.
	- Specific audio features include a mixture of player movement effects and ambient recordings, such as a radial sound emitting from a source or wind audio sound being played in a right/left stereo sound format to indicate spacial availabilities and limitations. 
- Pixelart based sprites are employed with simple animations to represent the player's orientation and movement
	- Move around character with WASD keys or with voice commands "Forward", "Behind", "Left", and "Right".
- The game is accompanied with a plotline that follows a man (Jason) who learns to adapt to, accept, and overcome boundaries that he encounters.
	- The story is told with voiceovers featuring different characters and voice actors.
- Maze generation uses a depth first search algorithm. 
	- A new, randomized maze map is created with each run and level, enhancing replayability and challenge  

## Built With
* Unity         

## Building the Application

You will first need to clone the repository to your local machine:
```
git clone https://github.com/Rajykins/Listen-to-the-Path.git
```

* Install Unity Hub from Unity's [website](https://unity.com/):

* Open Unity Hub:
	* Go to Installs on the left menu
	* Click on Add
	* Select Unity 2020.1.17f1
	* Make sure the module Universal Windows Platform Build Support is selected and install
	* After installation is complete, navigate to Projects
    * Enter the folder named ListenToThePath
	* Click on Add and select the folder named ListenToThePath
	* Click on the project to open with Unity Editor

*  Open the project on Unity Editor:
	* Go to File -> Build Settings
	* Select all scenes inside the box that says scenes in build (NOTE: you must select Scene/TitleCard first)
	* If there are no scenes inside the box. Open the Scene folder within Unity Editor and for each scene open and click Add Open Scene located under the box.
	* After selecting all the scenes click Build located at the bottom right of the Build Settings

## Run the Application

* Open the application called ListenToThePath.exe

## Game Walkthrough

1. Users will face a title screen containing the title of the game, and will be prompted to click any key or use a voice command to proceed.
2. Users will be met with an introductory slide with controls and descriptions of the auditory cues that will appear in the game. 
	- The slide features a narrator who introduces the game, controls, and features, as well as a simple visual slide with colour coding to simplify understanding of the game mechanics.
	- The user will be prompted by the narrator and by text to press any key or use a voice command keyword to continue onto the game, or use "tab" to re-listen to the instructions.
3. An auditory cutscene will introduce the plot, and automatically load the first level.
4. Users will be cued to explore the maze and find an objective in the shape of a star, which loads the succeeding level.
	- A voiceover will accompany the user each time a new level is reached and will further the plot point.
	- The light and visible range of the level will decrease with each ensuing maze map .
	- The voiceover plot will describe the progress of levels and the changes that happen with each subsequent level, progressing the story.
	- This process will repeat until the maze map is completely dark. 
5. Upon reaching the final star objective, the user will face a final auditory cutscene that concludes the game. 
