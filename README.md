## Now Playing on FM La Paz (Windows version)

![Notification](assets/now_playing.png)

#Installation:

Release version is not yet available. Lots of testing before deploying :(

If you by any chance have VS installed on your PC (~8GB) then download this folder
as ZIP and expand it on any folder (~/source/repos/) and open the project file 
<NowPlaying.csproj>. 

#Known issues:

1. The application will run as soon as is minimized
it will show at the System Tray the current song playing on https://www.lapaz.fm .
2. It should update after 4minutes, however due to a problem with an async function
it does not update. To update click on the icon (headphones) select <Show> option
to display the app and minimized again.

#Acknowledgments:

Thanks to all the following references, 
it took me about 4hours to develop this application.

1. [Fetch JSON](https://zetcode.com/csharp/json/)

2. [Build System Tray App](https://youtu.be/-6bvqwVYwMY)

3. [Async and JSON](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async)

4. VisualStudio intellisense, without it 52 issues would continue be unsolved.

5. The application icon is from [here](https://icon-icons.com/)

#Running Environment:

- Programming language: C#

- Editor: VS Code

- Environment: Panasonic Let's Note/ Windows10 Pro