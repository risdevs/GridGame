using UnityEngine;
using System.Collections;

public class Utils {

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
}
