using UnityEngine;
using System.Collections;

public class Utils {
    
    public static string NAME_ENEMY_FOLLOWER = "FOLLOWER_ENEMY";
    public static string NAME_END_FLAG = "END_FLAG";

	public enum LAYERS
	{
		Default = 0,
		Player = 8,
		OneWayPlatform = 9,
		Triggers = 10
	}

	public static string GetiPhoneDocumentsPath () 
	{ 
		// Your game has read+write access to /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents 
		// Application.dataPath returns              
		// /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data 
		// Strip "/Data" from path 
		string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5); 
		// Strip application name 
		path = path.Substring(0, path.LastIndexOf('/'));  
		return path + "/Documents"; 
	}
	
	public static string GetSaveDataFile()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return  Utils.GetiPhoneDocumentsPath() + "/savedGames.gd";
		} else{
			return Application.persistentDataPath + "/savedGames.gd";
		}
	}
}
