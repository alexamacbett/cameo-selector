## CleverCrumbish's Cameo Selector
A Sunless Sea BepInEx mod that changes the cameo (avatar) selection UI at character creation to one that can handle any number of custom cameos, and makes installing them easier.

### Requirements
- Full version: None.
- - Plugin only version: An existing BepInEx installation in your root Sunless Sea folder (install the full version if you don't know what this is).

### Installation
Download a release. Extract the contents of one of the FOR ROOT FOLDER folders in the zip into your base Sunless Sea folder. On Steam this should be steamapps/common/SunlessSea.
It will be something similar on GOG/Epic etc. If you already have BepInEx installed on Sunless Sea, choose the PLUGIN ONLY folder to extract the contents of. Otherwise, choose the FULL INSTALL folder.

In either case, Extract the contents of FOR APPDATA FOLDER in the zip into your Sunless Sea folder in Appdata. A common path for this is C:\Users\<Your User Name>\AppData\LocalLow\Failbetter Games\Sunless Sea\

### Usage
This mod will not do anything if you don't have some custom cameos installed. Some are available on the nexusmods.com, others elsewhere, and you could always make your own.

As in the vanilla game, you can install these cameos in %Appdata%/LocalLow/Failbetter Games/Sunless Sea/images/sn/icons/ and as long as they are .png files with filenames that begin with "avatar_" the game will load them as cameos.

With this mod installed, however, the game will also load any .png image of any name you put in /images/sn/icons/avatars/ as a cameo. This allows you to much more easily keep your custom cameos separate from the default ones if you ever want to delete them. Do not delete or move the "cc_menu" folder in the avatars folder or put any cameos in there. The images in that folder are necessary for the mod to function.

When you get to select a cameo during character creation, click the rightward and leftward pointing hands to move forward and backward in pages of cameos. If the last page has a number of cameos that leaves some space left over, it will generate some blank slots to fill the space and avoid deforming the UI. These can be clicked on, but won't change your selected cameo or do anything else. Otherwise, selecting a cameo works exactly the same as in the base game.

### Uninstallation
Move any remaining custom cameos you installed from /images/sn/icons/avatars/ to /images/sn/icons/ and rename them to begin with "avatar_" if you want to continue using them. Then delete the "avatars" folder.

Delete the CCCameoSelector.dll file in /BepInEx/plugins/ in your root Sunless Sea folder. If there are then no files in that folder or in the "patchers" folder next to it, you can delete /Bepinex/, winhttp.dll and doorstop_config.ini.

### CREDITS
- Bepis and Horse on the BepInEx modding discord for helping me get this patch running.
- Mayfunction because I stole her BepInEx setup from the Immersive Log Book mod to make absolutely certain it'd be compatible with Sunless Sea.
