using UnityEngine;
using System.Collections;

public class BoxPlusButton : MonoBehaviour {

	/**
	 * This script exists entirely for making sure that there's a plus button functionality for all the different BoxTabs available
	 * 
	 * For knowing which New Class prefab window to open for the user
	 * */

	public void ClickPlusButton( string pathModifier )
	{
		Object prefab;
		string resourcePath = "UI Prefabs/New Class Properties/New";
		resourcePath += pathModifier;
		resourcePath += "Class";

		prefab = Resources.Load( resourcePath );

		Instantiate( prefab );
	}
}
