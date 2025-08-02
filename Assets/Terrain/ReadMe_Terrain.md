# Terrain #

Please make sure that any Terrain.asset files are kept in a "Terrain" or "Terrains" folder.
Failure to do this might result in corrupted terrain data.

The reason we need to put Terrain in a certain folder is so that the .gitattributes file knows which .asset files are terrain and can treat the correctly. Unity uses .asset as an extension for multiple types of files, some are binary files and others are a format called UnityYAML.
Unity YAML is mergable while binary is not. If you try to merge a binary .asset files as a Unity YAML file then it will corrupt said file. If you have any questions about this please don't hesitate to get in contact with a technician for more info.
Feel free to delete this file when this folder is not empty.