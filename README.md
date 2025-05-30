# TS3AudioBot

This is a open-source TeamSpeak3 bot, playing music and much more.

## Features
* Play Youtube and Soundcloud songs as well as stream Twitch (extensible with plugins)
* Song history
* Various voice subscription modes; including to clients, channels and whisper groups
* Playlist management for all users
* Powerful permission configuration
* Plugin support
* Web API
* Multi-instance
* Localization
* Low CPU and memory with our self-written headless ts3 client


## Bot Commands
The bot is fully operable via chat.  
To get started write `!help` to the bot.  
For all commands check out our live [OpenApiV3 generator](http://tab.splamy.de/openapi/index.html).  
For an in-depth command tutorial see [here in the wiki](https://github.com/Splamy/TS3AudioBot/wiki/CommandSystem).

## Install

### Download
Pick and download the build for your platform and liking from the [releases](https://github.com/davidramiro/TS3AudioBot/releases/latest)



#### Linux
Install the required dependencies:
* on **Ubuntu**/**Debian**:  
Run `sudo apt-get install libopus-dev ffmpeg`
* on **Arch Linux**:  
Run `sudo pacman -S opus ffmpeg`
* on **CentOS 7**:  
Run
    ```
    sudo yum -y install epel-release
    sudo rpm -Uvh http://li.nux.ro/download/nux/dextop/el7/x86_64/nux-dextop-release-0-5.el7.nux.noarch.rpm
    sudo yum -y install ffmpeg opus-devel
	```
* **manually**:
    1. Make sure you have a C compiler installed
    1. Make the Opus script runnable with `chmod u+x InstallOpus.sh` and run it with `./InstallOpus.sh`
    1. Get the ffmpeg [32bit](https://johnvansickle.com/ffmpeg/builds/ffmpeg-git-i686-static.tar.xz) or [64bit](https://johnvansickle.com/ffmpeg/builds/ffmpeg-git-amd64-static.tar.xz) binary.
    1. Extract the ffmpeg archive with `tar -vxf ffmpeg-git-*XXbit*-static.tar.xz`
    1. Get the ffmpeg binary from `ffmpeg-git-*DATE*-amd64-static/ffmpeg` and copy it into your TS3AudioBot folder.

#### Windows
1. Get the ffmpeg [32bit](https://ffmpeg.zeranoe.com/builds/win32/static/ffmpeg-latest-win32-static.zip) or [64bit](https://ffmpeg.zeranoe.com/builds/win64/static/ffmpeg-latest-win64-static.zip) binary.
1. Open the archive and copy the ffmpeg binary from `ffmpeg-latest-winXX-static/bin/ffmpeg.exe` into your TS3AudioBot folder.

### Optional Dependencies
If the bot can't play some youtube videos it might be due to some embedding restrictions which are blocking this.  
You can install the [youtube-dl](https://github.com/rg3/youtube-dl/) binary or source folder (and specify the path in the config) to try to bypass this.

### First time setup
1. Run the bot with `./TS3AudioBot` (Linux) or `TS3AudioBot.exe` (Windows) and follow the setup instructions.
1. (Optional) Close the bot and configure your `rights.toml` to your desires.
You can use the template rules as suggested in the automatically generated file,
or dive into the rights syntax [here](https://github.com/Splamy/TS3AudioBot/wiki/Rights).
Then start the bot again.
1. (Optional, but highly recommended for everything to work properly).
   - Create a privilege key for the ServerAdmin group (or a group which has equivalent rights).
   - Send the bot in a private message `!bot setup <privilege key>`.
1. Congratz, you're done! Enjoy listening to your favourite music, experimenting with the crazy command system or do whatever you whish to do ;).  
For further reading check out the [CommandSystem](https://github.com/Splamy/TS3AudioBot/wiki/CommandSystem).

## Building manually

### Download
Download the git repository with `git clone --recurse-submodules https://github.com/davidramiro/TS3AudioBot.git`.

#### Linux
1. Get the latest `.NET 9` version by following [this link](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) and choose your platform
1. Go into the directory of the repository with `cd TS3AudioBot`
1. Execute `dotnet build -c Release TS3AudioBot` to build the AudioBot
1. The binary will be in `./TS3AudioBot/bin/Release/net9.0` and can be run with `dotnet TS3AudioBot.dll`

#### Windows
1. Make sure you have `Visual Studio` with the `.NET 9` development toolchain installed
1. Build the AudioBot with Visual Studio.

### Building the WebInterface
1. Go with the console of your choice into the `./WebInterface` folder
1. Run `yarn install` to restore or update all dependencies for this project
1. Run `yarn run build` to build the project.  
  The built project will be in `./WebInterface/dist`.  
  Make sure to the set the webinterface path in the ts3audiobot.toml to this folder.
1. You can alternatively use `yarn run start` for development.  
  This will use the webpack dev server with live reload instead of the ts3ab server.


## License
This project is licensed under [OSL-3.0](https://opensource.org/licenses/OSL-3.0).

Why OSL-3.0:
- OSL allows you to link to our libraries without needing to disclose your own project, which might be useful if you want to use the TSLib as a library.
- If you create plugins you do not have to make them public like in GPL. (Although we would be happy if you shared them :)
- With OSL we want to allow you providing the TS3AB as a service (even commercially). We do not want the software to be sold but the service. We want this software to be free for everyone.
- TL; DR? https://tldrlegal.com/license/open-software-licence-3.0
